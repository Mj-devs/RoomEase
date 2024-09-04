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
            using (var context = new StudentHostelDBContext())
            {
                ddlStudents.DataSource = context.Students.ToList();
                ddlStudents.DataTextField = "Name";
                ddlStudents.DataValueField = "StudentId";
                ddlStudents.DataBind();
            }
        }

        private void LoadAvailableRooms()
        {
            using (var context = new StudentHostelDBContext())
            {
                ddlRooms.DataSource = context.Rooms
                    .Where(r => r.IsAvailable && r.AvailableBedSpaces > 0)
                    .ToList();

                ddlRooms.DataTextField = "RoomNumber";
                ddlRooms.DataValueField = "RoomId";
                ddlRooms.DataBind();
            }
        }


        protected void btnBookRoom_Click(object sender, EventArgs e)
        {
            if (ddlRooms.SelectedIndex != -1)
            {
                using (var context = new StudentHostelDBContext())
                {
                    int studentId = int.Parse(ddlStudents.SelectedValue);
                    int roomId = int.Parse(ddlRooms.SelectedValue);
                    DateTime checkInDate = DateTime.Parse(txtCheckInDate.Text);
                    DateTime? checkOutDate = null;

                    if (!string.IsNullOrEmpty(txtCheckOutDate.Text))
                        checkOutDate = DateTime.Parse(txtCheckOutDate.Text);

                    // Create a new booking
                    var booking = new Bookings
                    {
                        StudentId = studentId,
                        RoomId = roomId,
                        BookingDate = DateTime.Now,
                        CheckInDate = checkInDate,
                        CheckOutDate = checkOutDate
                    };

                    // Add the booking to the database context
                    context.Bookings.Add(booking);

                    // Find the room being booked
                    var room = context.Rooms.Find(roomId);

                    // Check if there are available bed spaces
                    if (room.AvailableBedSpaces > 0)
                    {
                        // Decrease the available bed spaces by 1
                        room.AvailableBedSpaces -= 1;

                        // If no more bed spaces are available, mark the room as unavailable
                        if (room.AvailableBedSpaces == 0)
                        {
                            room.IsAvailable = false;
                        }

                        // Save all changes to the database
                        context.SaveChanges();

                        // Show success message
                        lblMessage.Text = "Room booked successfully!";
                    }
                    else
                    {
                        // No available bed spaces
                        lblMessage.Text = "No available bed spaces in this room.";
                    }

                    // Redirect to the room management page
                    Response.Redirect("/Room_files/ManageRooms");
                }
            }
            else
            {
                lblMessage.Text = "Kindly select a room.";
            }

        }
    }
}