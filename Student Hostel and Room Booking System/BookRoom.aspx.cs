using Student_Hostel_and_Room_Booking_System.Models.Datalayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Hostel_and_Room_Booking_System
{
    public partial class BookRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStudents();
                LoadAvailableRooms();
            }
        }

        private void LoadStudents()
        {
            using (var context = new StudentHostelDbContext())
            {
                ddlStudents.DataSource = context.Students.ToList();
                ddlStudents.DataTextField = "Name";
                ddlStudents.DataValueField = "StudentId";
                ddlStudents.DataBind();
            }
        }

        private void LoadAvailableRooms()
        {
            using (var context = new StudentHostelDbContext())
            {
                ddlRooms.DataSource = context.Rooms.Where(r => r.IsAvailable).ToList();
                ddlRooms.DataTextField = "RoomNumber";
                ddlRooms.DataValueField = "RoomId";
                ddlRooms.DataBind();
            }
        }

        protected void btnBookRoom_Click(object sender, EventArgs e)
        {
            using (var context = new StudentHostelDbContext())
            {
                int studentId = int.Parse(ddlStudents.SelectedValue);
                int roomId = int.Parse(ddlRooms.SelectedValue);
                DateTime checkInDate = DateTime.Parse(txtCheckInDate.Text);
                DateTime? checkOutDate = null;

                if (!string.IsNullOrEmpty(txtCheckOutDate.Text))
                    checkOutDate = DateTime.Parse(txtCheckOutDate.Text);

                var booking = new Bookings
                {
                    StudentId = studentId,
                    RoomId = roomId,
                    BookingDate = DateTime.Now,
                    CheckInDate = checkInDate,
                    CheckOutDate = checkOutDate
                };

                context.Bookings.Add(booking);
                var room = context.Rooms.Find(roomId);
                room.IsAvailable = false;

                context.SaveChanges();

                lblMessage.Text = "Room booked successfully!";
            }
        }
    }
}