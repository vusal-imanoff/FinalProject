    using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Core.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string WorkTime { get; set; }
        public bool CompanyStatus { get; set; }
        public List<Car> Cars { get; set; }
    }
}
