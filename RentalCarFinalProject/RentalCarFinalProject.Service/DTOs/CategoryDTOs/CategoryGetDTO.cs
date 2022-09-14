﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.DTOs.CategoryDTOs
{
    public class CategoryGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }

    }
}
