using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.BrandDTOs
{
    public class BrandListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string  Image { get; set; }
        public bool IsDeleted { get; set; }
    }
}
