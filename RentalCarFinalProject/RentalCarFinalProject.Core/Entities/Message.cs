using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCarFinalProject.Core.Entities
{
    public class Message : BaseEntity
    {
        public string Text { get; set; }
        public int MyProperty { get; set; }
        public string AppUserId { get; set; }
        public string ReciveUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
