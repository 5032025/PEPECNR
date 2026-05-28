using System;
using System.Collections.Generic;
using System.Text;

namespace WEBPEPE.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreaAt { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateBy { get; set; }
    }
}
