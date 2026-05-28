using System;
using System.Collections.Generic;
using System.Text;
using WEBPEPE.Domain.Entities;
using WEBPEPE.Domain.Interfaces;

namespace WEBPEPE.Infrastructure.Persistence.Repositories
{
    public class RecordRepository : BaseRepository<Record>, IRecord
    {
        public RecordRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        
    }
}
