using Student_Hostel_and_Room_Booking_System.Models.Datalayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Hostel_and_Room_Booking_System.Hostel_files
{
    public partial class ViewHostelDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["SelectedHostelId"] != null)
                {
                    int hostelId = Convert.ToInt32(Session["SelectedHostelId"]);
                    LoadRoomsForHostel(hostelId);
                    using (var context = new StudentHostelDBContext())
                    {
                        var hostel = context.Hostels.Where(h=> hostelId == h.HostelId).FirstOrDefault();
                        if (hostel != null)
                        {
                            lblHostelName.Text = $" {hostel.HostelName}";
                        }
                        else
                        {
                            lblHostelName.Text = "Hostel can't be found.";
                        }
                    }
                }
                else
                {
                    lblerrormessage.Text = "No hostel selected. Please go back and select a hostel.";
                }
            }
        }

        private void LoadRoomsForHostel(int hostelId)
        {
            using (var context = new StudentHostelDBContext())
            {
                var rooms = context.Rooms.Where(r => r.HostelId == hostelId).ToList();

                RoomsGridView.DataSource = rooms;
                RoomsGridView.DataBind();
            }
        }

        protected void btnBackToHostels_Click(object sender, EventArgs e)
        {
            // Redirect back to the Hostel management page
            Response.Redirect("/Hostel_files/ManageHostels.aspx");
        }

        protected void RoomsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "ViewDetails")
            {
                // Get the selected room ID
                int roomId = Convert.ToInt32(e.CommandArgument);

                // Store the RoomId in a session variable
                Session["SelectedRoomId"] = roomId;

                // Redirect to the RoomDetails page
                Response.Redirect("/Room_files/ViewRoomDetails.aspx");
            }
        }
        protected void RoomsGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            RoomsGridView.EditIndex = e.NewEditIndex;
            int hostelId = Convert.ToInt32(Session["SelectedHostelId"]);
            LoadRoomsForHostel(hostelId);
        }

        protected void RoomsGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            RoomsGridView.EditIndex = -1;
            int hostelId = Convert.ToInt32(Session["SelectedHostelId"]);
            LoadRoomsForHostel(hostelId);
        }

        protected void RoomsGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Get the Room ID of the room being updated from the DataKeys collection of the GridView.
            int roomId = Convert.ToInt32(RoomsGridView.DataKeys[e.RowIndex].Value);

            // Get the GridViewRow corresponding to the row that is being updated.
            GridViewRow row = RoomsGridView.Rows[e.RowIndex];

            // Retrieve the controls for Room Number, Hostel DropDown, Room Type, Bed Spaces, Available Bed Spaces, and IsAvailable checkbox.
            TextBox roomNumber = (TextBox)(row.Cells[1].Controls[0]);
            int hostelId = int.Parse((row.FindControl("ddlHostel") as DropDownList).SelectedValue);
            TextBox roomType = (TextBox)(row.Cells[3].Controls[0]);
            TextBox bedSpacesTextBox = (TextBox)(row.Cells[4].Controls[0]);
            TextBox availableBedSpacesTextBox = (TextBox)(row.Cells[5].Controls[0]);
            //CheckBox isAvailable = (CheckBox)(row.Cells[6].Controls[0]);

            using (var context = new StudentHostelDBContext())
            {
                // Find the room by its ID in the database.
                var room = context.Rooms.Find(roomId);

                // Get the new number of bed spaces entered by the user.
                int newBedSpaces = int.Parse(bedSpacesTextBox.Text);

                // Store the current number of available bed spaces for the room.
                int currentAvailableBedSpaces = room.AvailableBedSpaces;

                // Update the available bed spaces based on the change in the number of bed spaces.
                // If the new number of bed spaces is less than the current bed spaces, decrease available bed spaces.
                if (newBedSpaces < room.BedSpaces)
                {
                    room.AvailableBedSpaces -= (room.BedSpaces - newBedSpaces);
                }
                else
                {
                    // Otherwise, increase the available bed spaces.
                    room.AvailableBedSpaces += (newBedSpaces - room.BedSpaces);
                }

                // Update the room's properties with the new values from the form.
                room.RoomNumber = roomNumber.Text;
                room.HostelId = hostelId;
                room.BedSpaces = newBedSpaces;
                room.AvailableBedSpaces = currentAvailableBedSpaces;
                //room.IsAvailable = isAvailable.Checked;

                // Save the changes to the database.
                context.SaveChanges();

                // Reset the EditIndex property of the GridView to exit edit mode.
                RoomsGridView.EditIndex = -1;

                // Refresh the displayed data with updated information.
                LoadRoomsForHostel(hostelId);
            }
        }
        protected void RoomsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int roomId = Convert.ToInt32(RoomsGridView.DataKeys[e.RowIndex].Value);

            using (var context = new StudentHostelDBContext())
            {
                var room = context.Rooms.Find(roomId);

                var bookingsExist = context.Bookings.Any(b => b.RoomId == roomId);
                if (bookingsExist)
                {
                    lblerrormessage.Text = "Cannot delete room. There are student booked to the room. Kindly first unbook them before deleting the room.";
                    lblerrormessage.Visible = true;

                    // Reset and start the timer
                    ErrorTimer.Enabled = true;

                }
                else
                {
                    if (room != null)
                    {
                        context.Rooms.Remove(room);
                        context.SaveChanges();
                        lblerrormessage.Text = "Room deleted successfully!";
                    }
                }
            }
            int hostelId = Convert.ToInt32(Session["SelectedHostelId"]);
            LoadRoomsForHostel(hostelId);
        }
        protected void ErrorTimer_Tick(object sender, EventArgs e)
        {
            lblerrormessage.Visible = false; // Hide the error message
            ErrorTimer.Enabled = false;
        }
    }
}