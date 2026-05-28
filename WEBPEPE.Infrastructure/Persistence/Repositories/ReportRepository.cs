using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using WEBPEPE.Domain.Entities;
using WEBPEPE.Domain.Interfaces;

namespace WEBPEPE.Infrastructure.Persistence.Repositories
{
    public class ReportRepository : BaseRepository<Report>, IReport
    {
        public ReportRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

      
    }
}
