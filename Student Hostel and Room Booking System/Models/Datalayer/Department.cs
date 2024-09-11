using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Student_Hostel_and_Room_Booking_System.Models.Datalayer
{
    public partial class Department
    {
        public Department()
        {
            Students = new HashSet<Students>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public virtual ICollection<Students> Students { get; set; }

        public override string ToString()
        {
            return this.DepartmentName;
        }
    }
}
