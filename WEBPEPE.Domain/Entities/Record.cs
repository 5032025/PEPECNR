using System;
using System.Collections.Generic;
using System.Text;

namespace WEBPEPE.Domain.Entities
{
    public class Record : BaseEntity
    {
        public DateTime Date { get; set; }


        // Mediciones del Oxímetro
        public decimal BloodOxygenLevel { get; set; }

        public decimal HeartRate { get; set; }

        public decimal StressIntensity { get; set; }

        public decimal Temperature { get; set; }

        //Llave foranea
        public int ReportId { get; set; }

        public Report Report { get; set; }
    }
}
