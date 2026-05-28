using WEBPEPE.Domain.Entities;
using WEBPEPE.Domain.Interfaces;

namespace WEBPEPE.Application.Services
{
    public class ReportService
    {
        private readonly IReport _reportRepository;

        public ReportService(IReport reportRepository)
        {
            _reportRepository = reportRepository;
        }

        
        public async Task<Report> Add(Report report)
        {
            var insertedReport = await _reportRepository.AddAsync(report);

            if (insertedReport == null)
            {
                throw new Exception("No se pudo insertar el reporte médico en la base de datos.");
            }

            return insertedReport;
        }

       
        public async Task<bool> Delete(int reportId)
        {
            return await _reportRepository.Delete(reportId);
        }

       
        public async Task<Report?> FindByIdAsync(int reportId, string userId)
        {
           
            return await _reportRepository.FindFirstOrDefaultAsync(
                t => t.Id == reportId && t.UserId == userId && !t.IsDeleted,
                t => t.Records
            );
        }


        public async Task<List<Report>> GetAll(int page, int take, string? search, string userId)
        {
            int skip = (page - 1) * take;

            
            var result = await _reportRepository.GetAll(
                r => r.UserId == userId && !r.IsDeleted,
                take,
                skip,
                search ?? string.Empty
            );

            return result.ToList();
        }
    }
}