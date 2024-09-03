using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Student_Hostel_and_Room_Booking_System.Models.Datalayer
{
    public partial class Rooms
    {
        public Rooms()
        {
            Bookings = new HashSet<Bookings>();
        }

        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public int? HostelId { get; set; }
        public bool IsAvailable { get; set; }
        public string RoomType { get; set; }

        public virtual Hostels Hostel { get; set; }
        public virtual ICollection<Bookings> Bookings { get; set; }
    }
}
