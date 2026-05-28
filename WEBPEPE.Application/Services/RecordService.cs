using WEBPEPE.Domain.Entities;
using WEBPEPE.Domain.Interfaces;

namespace WEBPEPE.Application.Services
{
    public class RecordService
    {
        private readonly IRecord _recordRepository;
        private readonly IReport _reportRepository;

        public RecordService(IRecord recordRepository, IReport reportRepository)
        {
            _recordRepository = recordRepository;
            _reportRepository = reportRepository;
        }

        public async Task<Record> Add(Record record, string userId)
        {
            
            var report = await _reportRepository.FindFirstOrDefaultAsync(
                r => r.Id == record.ReportId && r.UserId == userId && !r.IsDeleted
            );

            if (report == null)
                throw new Exception("El reporte asociado no existe o no tienes permisos.");

           
            if (report.Month != record.Date.Month || report.Year != record.Date.Year)
            {
                throw new Exception($"La fecha de la medición ({record.Date:MM/yyyy}) no coincide con el periodo del reporte ({report.Month:00}/{report.Year}).");
            }

          
            record.CreateBy = userId;
            var insertedRecord = await _recordRepository.AddAsync(record);

            if (insertedRecord == null)
                throw new Exception("Error al guardar la medición.");

            return insertedRecord;
        }

        public async Task<List<Record>> GetRecordsByReport(int reportId, int page, int take, string userId)
        {
            
            var report = await _reportRepository.FindFirstOrDefaultAsync(r => r.Id == reportId && r.UserId == userId);
            if (report == null) return new List<Record>();

            int skip = (page - 1) * take;
            var query = await _recordRepository.GetAll(r => r.ReportId == reportId && !r.IsDeleted, take, skip, string.Empty);

            return query.ToList();
        }

        public async Task<bool> Delete(int recordId, string userId)
        {
           
            var record = await _recordRepository.FindFirstOrDefaultAsync(
                r => r.Id == recordId && r.Report.UserId == userId,
                r => r.Report
            );

            if (record == null) return false;

            return await _recordRepository.Delete(recordId);
        }
    }
}