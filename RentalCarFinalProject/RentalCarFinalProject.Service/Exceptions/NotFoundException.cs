using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string msg) : base(msg)
        {

        }
    }
}
