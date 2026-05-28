using System;
using System.Collections.Generic;
using System.Text;

namespace WEBPEPE.Domain.Entities
{
    public class Report : BaseEntity
    {
        public string UserId { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }

        public ReportStatus Status { get; set; }


        public ICollection<Record> Records { get; set; }


        // Datos del paciente
        public int Age { get; set; }

        public decimal Weight { get; set; }

        public decimal Height { get; set; }
    }

    public enum ReportStatus
    {
        Validated,
        Created,
        Invalidated
    }
}
