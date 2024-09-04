using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Student_Hostel_and_Room_Booking_System.Models.Datalayer
{
    public partial class RoomCoordinator
    {
        public int RoomCoordinatorId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
