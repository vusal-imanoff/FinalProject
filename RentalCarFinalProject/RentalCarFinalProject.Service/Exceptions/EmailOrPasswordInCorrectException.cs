using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.Exceptions
{
    public class EmailOrPasswordInCorrectException : Exception
    {
        public EmailOrPasswordInCorrectException(string message) : base(message)
        {

        }
    }
}
