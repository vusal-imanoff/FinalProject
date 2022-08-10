using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Core.Entities
{
    public class CarImages:BaseEntity
    {
        public string Image { get; set; }
        public int CarID { get; set; }
        public Car Car { get; set; }
    }
}
