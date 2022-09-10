using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Age { get; set; }
        public string FinCode { get; set; }
        public string SeriaNumber { get; set; }
        public string DriverLicanse { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool LoginStatus { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Company Company { get; set; }
        public string Role { get; set; }
    }
}
