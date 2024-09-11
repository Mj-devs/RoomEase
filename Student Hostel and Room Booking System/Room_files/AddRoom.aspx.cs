using Student_Hostel_and_Room_Booking_System.Models.Datalayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Hostel_and_Room_Booking_System
{
    public partial class AddRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {    // Retrieving the RoomCoordinatoor Id from the session
                if (Session["RoomCoordinatorId"] != null)
                {
                    int RoomCoordinatorId = (int)Session["RoomCoordinatorId"];

                    using (var context = new StudentHostelDBContext())
                    {
                        var RoomCoordinator = context.RoomCoordinator.Where(r => RoomCoordinatorId == r.RoomCoordinatorId).FirstOrDefault();

                        if (RoomCoordinator != null)
                        {
                            if (Request.QueryString["RoomId"] != null)
                            {
                                int roomId = int.Parse(Request.QueryString["RoomId"]);
                                LoadRoomDetails(roomId);
                                LoadHostels();
                            }
                                LoadHostels();
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

        private void LoadHostels()
        {
            using (var context = new StudentHostelDBContext())
            {
                var hostels = context.Hostels.ToList();
                ddlHostels.DataSource = hostels;
                ddlHostels.DataTextField = "HostelName";
                ddlHostels.DataValueField = "HostelId";
                ddlHostels.DataBind();
                ddlHostels.Items.Insert(0, new ListItem("--Select Hostel--", "0"));
            }
        }

        private void LoadRoomDetails(int roomId)
        {
            using (var context = new StudentHostelDBContext())
            {
                var room = context.Rooms.Find(roomId);
                if (room != null)
                {
                    txtRoomNumber.Text = room.RoomNumber;
                    ddlHostels.SelectedValue = room.HostelId.ToString();
                    chkIsAvailable.Checked = room.IsAvailable;
                    txtRoomType.Text = room.RoomType;
                }
            }
        }
        protected void cvRoomNo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string roomNo = txtRoomNumber.Text.Trim();
            int hostelId = int.Parse(ddlHostels.SelectedValue);

            using (var context = new StudentHostelDBContext())
            {
                // Check if the room number already exists
                bool roomNoExists = context.Rooms.Any(i => i.RoomNumber == roomNo && i.HostelId == hostelId);

                // If the room number exists, validation fails
                args.IsValid = !roomNoExists;
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) // Ensure that all validators have passed
            {
                int selectedHostelId = int.Parse(ddlHostels.SelectedValue); // Get selected hostel

                if (selectedHostelId > 0)
                {
                    using (var context = new StudentHostelDBContext())
                    {
                        Rooms room;

                        if (Request.QueryString["RoomId"] != null)
                        {
                            int roomId = int.Parse(Request.QueryString["RoomId"]);
                            room = context.Rooms.Find(roomId);

                            if (room == null)
                            {
                                lblMessage.Text = "Room not found.";
                                return;
                            }
                        }
                        else
                        {
                            // Adding a new room if RoomId is not in query string
                            room = new Rooms();
                            context.Rooms.Add(room);
                        }

                        // Populate room details
                        room.RoomNumber = txtRoomNumber.Text.Trim();
                        room.HostelId = selectedHostelId; // Set the selected hostel
                        room.RoomType = txtRoomType.Text.Trim();
                        room.BedSpaces = int.Parse(txtBedSpaces.Text);
                        room.AvailableBedSpaces = int.Parse(txtBedSpaces.Text); // Initially, available bed spaces are the total bed spaces
                        room.IsAvailable = true;

                        // Save the room to the database
                        context.SaveChanges();

                        // Display success message
                        lblMessage.Text = "Room saved successfully!";
                        ClearForm(); // Clear the form after save
                        tmRedirect.Enabled = true;
                        // Redirect to room management page
                        Response.Redirect("/Room_files/ManageRooms.aspx");
                    }
                }
                else
                {
                    lblMessage.Text = "Please select a hostel.";
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
        // Method to clear the form after successful room addition or update
        private void ClearForm()
        {
            txtRoomNumber.Text = string.Empty;
            txtRoomType.Text = string.Empty;
            txtBedSpaces.Text = string.Empty;
            ddlHostels.SelectedIndex = 0;
        }

        protected void btnBackToRooms_Click(object sender, EventArgs e)
        {
            // Redirect back to the Room management page
            Response.Redirect("/Room_files/ManageRooms.aspx");
        }
    }
}