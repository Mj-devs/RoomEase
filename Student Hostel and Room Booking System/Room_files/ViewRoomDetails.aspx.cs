using Microsoft.EntityFrameworkCore;
using Student_Hostel_and_Room_Booking_System.Models.Datalayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Hostel_and_Room_Booking_System.Room_files
{
    public partial class ViewRoomDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the session variable for RoomId exists
                if (Session["SelectedRoomId"] != null)
                {
                    int roomId = (int)Session["SelectedRoomId"];

                    // Bind the room details and students list using the roomId
                    BindRoomDetails(roomId);
                    BindStudentsGrid(roomId);
                }
                else
                {
                    // Case where no room is selected (redirect)
                    Response.Redirect("ManageRooms.aspx");
                }
            }
        }

        private void BindRoomDetails(int roomId)
        {
            using (var context = new StudentHostelDBContext())
            {
                var room = context.Rooms.Find(roomId);

                if (room != null)
                {
                    lblRoomNumber.Text = $"Room Number: {room.RoomNumber}";
                    lblBedSpaces.Text = $"Total Bed Spaces: {room.BedSpaces.ToString()}";
                    lblAvailableBedSpaces.Text =$"Available Bed Spaces: {room.AvailableBedSpaces.ToString()}";
                }
            }
        }

        private void BindStudentsGrid(int roomId)
        {
            using (var context = new StudentHostelDBContext())
            {
                // Fetch the students currently assigned to this room via the Bookings table
                var studentsInRoom = context.Bookings
                    .Where(b => b.RoomId == roomId)
                    .Select(b => new
                    {
                        b.Student.StudentId,
                        b.Student.Name,
                        b.Student.MatricNo,
                        b.Student.Department,
                        b.CheckInDate
                    })
                    .ToList();

                StudentsGridView.DataSource = studentsInRoom;
                StudentsGridView.DataBind();
            }
        }
        protected void StudentsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UnbookStudent")
            {
                // Get the selected student ID
                int index = Convert.ToInt32(e.CommandArgument);
                int studentId = Convert.ToInt32(StudentsGridView.DataKeys[index].Value);

                // Retrieve RoomId from session
                if (Session["SelectedRoomId"] != null)
                {
                    int roomId = (int)Session["SelectedRoomId"];

                    using (var context = new StudentHostelDBContext())
                    {
                        // Find the booking for the student and room
                        var booking = context.Bookings
                            .FirstOrDefault(b => b.StudentId == studentId && b.RoomId == roomId);

                        if (booking != null)
                        {
                            // Remove the booking
                            context.Bookings.Remove(booking);

                            // Update the room's available bed spaces
                            var room = context.Rooms.Find(roomId);
                            if (room != null)
                            {
                                room.AvailableBedSpaces += 1;
                                room.IsAvailable = room.AvailableBedSpaces > 0;
                            }

                            // Save changes
                            context.SaveChanges();

                            // Rebind the grid to refresh the view
                            BindStudentsGrid(roomId);
                        }
                    }
                }
            }
        }
        protected void btnBackToRooms_Click(object sender, EventArgs e)
        {
            // Redirect back to the Room management page
            Response.Redirect("/Hostel_files/ViewHostelDetails.aspx");
        }
    }
}