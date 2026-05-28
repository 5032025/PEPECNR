using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WEBPEPE.Domain.Entities;
using WEBPEPE.Infrastructure.Identity;

namespace WEBPEPE.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<AppIdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);


            // Relación Usuario (1) -> Reportes (N)
            builder.Entity<Report>()
                .HasOne<AppIdentityUser>()        // Propiedad en Report.cs
                .WithMany()    // Propiedad en User.cs 
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relación Reporte (1) -> Records (N)
            builder.Entity<Record>()
                .HasOne(rec => rec.Report)   // Propiedad en Record.cs
                .WithMany(rep => rep.Records)// Propiedad en Report.cs
                .HasForeignKey(rec => rec.ReportId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Report> Reports { get; set; }
        public DbSet<Record> Records { get; set; }
    }
}
