using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.SliderDTOs
{
    public class SliderListDTO
    {
        public int Id { get; set; }
        public string MainTitle { get; set; }
        public string SubTitle { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }

    }
}
