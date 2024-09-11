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
                // Retrieving the RoomCoordinatoor Id from the session
                if (Session["RoomCoordinatorId"] != null)
                {
                    int RoomCoordinatorId = (int)Session["RoomCoordinatorId"];

                    using (var context = new StudentHostelDBContext())
                    {
                        var RoomCoordinator = context.RoomCoordinator
                            .Where(r => RoomCoordinatorId == r.RoomCoordinatorId)
                            .FirstOrDefault();
                        if (RoomCoordinator != null)
                        {
                            LoadUnbookedStudents();
                        }
                        else
                        {
                        }
                    }
                }
                else
                {
                    // Case where session is null (redirect to login page)
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        protected void tmRedirect_Tick(object sender, EventArgs e)
        {
            // Disable the Timer to stop it from continuing to tick
            tmRedirect.Enabled = false;

            // Redirect to Manage Students page after the timer ends
            Response.Redirect("/Student_Files/ManageStudents.aspx");
        }
        private void LoadUnbookedStudents()
        {
            using (var context = new StudentHostelDBContext())
            {
                // Get students who have no active bookings (no rooms booked, or all bookings have a checkout date)
                var unbookedStudents = context.Students
                    .Where(s => !context.Bookings.Any(b => b.StudentId == s.StudentId && b.CheckOutDate == null))
                    .Select(s => new
                    {
                        s.StudentId,
                        s.Name,
                    })
                    .ToList();

                // Bind the unbooked students to the dropdown list
                ddlUnbookedStudents.DataSource = unbookedStudents;
                ddlUnbookedStudents.DataTextField = "Name"; // Display the student's name
                ddlUnbookedStudents.DataValueField = "StudentId"; // Use the student's ID as the value
                ddlUnbookedStudents.DataBind();

                // Add a default "Select" option
                ddlUnbookedStudents.Items.Insert(0, new ListItem("--Select a Student--", "0"));
            }
        }

        protected void ddlUnbookedStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            int studentId = int.Parse(ddlUnbookedStudents.SelectedValue);

            if (studentId > 0) // Ensure a student is selected
            {
                // Store the selected student ID in the session
                Session["StudentId"] = studentId;

                // Load hostels based on the student's gender
                LoadHostelsForGender(studentId);

                using (var context = new StudentHostelDBContext())
                {
                    var student = context.Students.Find(studentId);
                    if (student != null)
                    {
                        // Display student's name
                        lblstudents.Text = $"Booking room for: {student.Name}";
                    }
                    else
                    {
                        lblstudents.Text = "Student not found.";
                    }
                }
            }
        }

        protected void ddlHostels_SelectedIndexChanged(object sender, EventArgs e)
        {
            int hostelId = int.Parse(ddlHostels.SelectedValue);

            if (hostelId > 0)
            {
                // Load the available rooms for the selected hostel
                LoadRoomsForHostel(hostelId);
            }
            else
            {
                ddlRooms.Items.Clear(); // Clear rooms dropdown if no valid hostel is selected
                ddlRooms.Items.Insert(0, new ListItem("--Select Room--", "0"));
            }
        }


        private void LoadRoomsForHostel(int hostelId)
        {
            using (var context = new StudentHostelDBContext())
            {
                if (hostelId > 0)
                {
                    var rooms = context.Rooms
                        .Where(r => r.HostelId == hostelId && r.IsAvailable)
                        .ToList();

                    ddlRooms.DataSource = rooms;
                    ddlRooms.DataTextField = "RoomNumber";
                    ddlRooms.DataValueField = "RoomId";
                    ddlRooms.DataBind();

                    ddlRooms.Items.Insert(0, new ListItem("--Select Room--", "0"));
                }
                else
                {
                    lblMessage.Text = "Please select a valid hostel.";
                    ddlRooms.Items.Clear();
                }
            }
        }

        private void LoadHostelsForGender(int studentId)
        {
            using (var context = new StudentHostelDBContext())
            {
                var student = context.Students.Find(studentId);
                if (student != null)
                {
                    string studentGender = student.Gender;

                    var hostels = context.Hostels
                        .Where(h => h.Gender == studentGender)
                        .ToList();

                    ddlHostels.DataSource = hostels;
                    ddlHostels.DataTextField = "HostelName";
                    ddlHostels.DataValueField = "HostelId";
                    ddlHostels.DataBind();

                    ddlHostels.Items.Insert(0, new ListItem("--Select Hostel--", "0"));
                }
                else
                {
                    lblMessage.Text = "Student not found.";
                }
            }
        }



        protected void btnBookRoom_Click(object sender, EventArgs e)
        {
            // Check if a room has been selected from the dropdown list
            if (ddlRooms.SelectedIndex != -1 && ddlUnbookedStudents.SelectedIndex != -1)
            {
                int roomId = Convert.ToInt32(ddlRooms.SelectedValue);
                int studentId = Convert.ToInt32(ddlUnbookedStudents.SelectedValue);

                using (var context = new StudentHostelDBContext())
                {
                    // Retrieve student and room details from the database
                    var student = context.Students.Find(studentId);
                    var room = context.Rooms.Find(roomId);

                    if (student != null && room != null)
                    {
                        // Retrieve the hostel details based on the room's HostelId
                        var hostel = context.Hostels.Find(room.HostelId);

                        if (hostel == null)
                        {
                            lblMessage.Text = "Hostel not found.";
                            return;
                        }

                        // Check if the student's gender matches the hostel's gender
                        if (student.Gender != hostel.Gender)
                        {
                            lblMessage.Text = "You cannot book a room in a hostel for the opposite gender.";
                            return;
                        }

                        // Check if the student is already assigned to another room (has an ACTIVE booking)
                        var existingBooking = context.Bookings
                            .FirstOrDefault(b => b.StudentId == studentId && b.CheckOutDate == null);

                        if (existingBooking != null)
                        {
                            lblMessage.Text = "The student is already assigned to another room.";
                        }
                        else
                        {
                            // Proceed to book the student in the selected room
                            DateTime checkInDate = DateTime.Parse(txtCheckInDate.Text);

                            var booking = new Bookings
                            {
                                StudentId = studentId,
                                RoomId = roomId,
                                BookingDate = DateTime.Now,
                                CheckInDate = checkInDate
                            };

                            // Add the booking to the database
                            context.Bookings.Add(booking);

                            // Update the available bed spaces for the selected room
                            room.AvailableBedSpaces -= 1;

                            // If no bed spaces are left, mark the room as unavailable
                            if (room.AvailableBedSpaces == 0)
                            {
                                room.IsAvailable = false;
                            }

                            // Save changes to the database
                            context.SaveChanges();

                            // Show a success message
                            lblMessage.Text = "Room booked successfully!";

                            // Redirect to the room management page after successful booking
                            Response.Redirect("/Room_files/ManageRooms");
                        }
                    }
                    else
                    {
                        // If student or room is null, display an error message
                        lblMessage.Text = "Student or Room not found.";
                    }
                }
            }
            else
            {
                // If no room or student is selected, display an error message
                lblMessage.Text = "Kindly select a room and a student.";
            }
        }
        protected void btnBackToStudents_Click(object sender, EventArgs e)
        {
            // Redirect back to the Room management page
            Response.Redirect("/Student_files/ManageStudents.aspx");
        }
    }
}