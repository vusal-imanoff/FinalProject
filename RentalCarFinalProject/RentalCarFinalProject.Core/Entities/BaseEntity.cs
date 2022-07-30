using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
    }
}
