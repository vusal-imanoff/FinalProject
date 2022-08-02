using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.BrandDTOs
{
    public class BrandPostDTO
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public IFormFile File { get; set; }
    }
}
