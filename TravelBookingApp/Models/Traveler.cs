﻿using System;
using System.Collections.Generic;

namespace TravelBookingApp.Models
{
    public partial class Traveler
    {
        public Traveler()
        {
            FlightRoute = new HashSet<FlightRoute>();
            Passenger = new HashSet<Passenger>();
        }

        public int TravelerId { get; set; }
        public string TravelerName { get; set; }
        public DateTime TravelDate { get; set; }

        public virtual ICollection<FlightRoute> FlightRoute { get; set; }
        public virtual ICollection<Passenger> Passenger { get; set; }
    }
}
