using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.ModelDTOs
{
    public class ModelListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public int BrandId { get; set; }

        public bool IsDeleted { get; set; }

    }
}
