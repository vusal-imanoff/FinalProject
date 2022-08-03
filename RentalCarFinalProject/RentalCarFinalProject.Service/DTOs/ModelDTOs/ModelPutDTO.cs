using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.ModelDTOs
{
    public class ModelPutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
    }
}
