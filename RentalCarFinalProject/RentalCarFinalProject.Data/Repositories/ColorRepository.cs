using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Data.Repositories
{
    public class ColorRepository :Repository<Color>,IColorRepository
    {
        public ColorRepository(AppDbContext context) : base(context)
        {

        }
    }
}
