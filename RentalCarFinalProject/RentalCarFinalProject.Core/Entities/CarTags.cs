using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Core.Entities
{
    public class CarTags : BaseEntity
    {
        public int CarId { get; set; }
        public Car Car { get; set; }
        public Nullable<int> TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
