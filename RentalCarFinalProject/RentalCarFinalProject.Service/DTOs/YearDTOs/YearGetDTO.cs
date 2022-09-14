using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.YearDTOs
{
    public class YearGetDTO
    {
        public int Id { get; set; }
        public int ProductionYear { get; set; }
        public bool IsDeleted { get; set; }

    }
}
