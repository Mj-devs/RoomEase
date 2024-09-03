using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Student_Hostel_and_Room_Booking_System.Models.Datalayer
{
    public partial class Hostels
    {
        public Hostels()
        {
            Rooms = new HashSet<Rooms>();
        }

        public int HostelId { get; set; }
        public string HostelName { get; set; }
        public int? TotalRooms { get; set; }

        public virtual ICollection<Rooms> Rooms { get; set; }
    }
}
