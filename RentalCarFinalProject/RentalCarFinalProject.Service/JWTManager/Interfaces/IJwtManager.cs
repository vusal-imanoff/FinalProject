using RentalCarFinalProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.JWTManager.Interfaces
{
    public interface IJwtManager
    {
        Task<string> GenerateTokenAsync(AppUser appUser);
        string GetUserNameByToken(string token);
    }
}
