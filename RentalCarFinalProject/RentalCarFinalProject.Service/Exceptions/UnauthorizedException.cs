﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Service.Exceptions
{
    public class UnauthorizedException:Exception
    {
        public UnauthorizedException(string message) : base(message)
        {

        }
    }
}
