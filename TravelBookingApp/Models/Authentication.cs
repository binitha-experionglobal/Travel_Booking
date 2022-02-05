using System;
using System.Collections.Generic;

namespace TravelBookingApp.Models
{
    public partial class Authentication
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
