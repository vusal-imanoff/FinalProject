using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.Extentions
{
    public static class CustomDateTime
    {
        public static DateTime currentDate = DateTime.UtcNow.AddHours(4);
    }
}
