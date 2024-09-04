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
                            PopulateHostels();
                            if (Request.QueryString["RoomId"] != null)
                            {
                                int roomId = int.Parse(Request.QueryString["RoomId"]);
                                LoadRoomDetails(roomId);
                            }
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

        private void PopulateHostels()
        {
            using (var context = new StudentHostelDBContext())
            {
                var hostels = context.Hostels.ToList();
                ddlHostel.DataSource = hostels;
                ddlHostel.DataTextField = "HostelName";
                ddlHostel.DataValueField = "HostelId";
                ddlHostel.DataBind();
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
                    ddlHostel.SelectedValue = room.HostelId.ToString();
                    chkIsAvailable.Checked = room.IsAvailable;
                    txtRoomType.Text = room.RoomType;
                }
            }
        }
        protected void cvRoomNo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string roomNo = txtRoomNumber.Text.Trim();
            int hostelId = int.Parse(ddlHostel.SelectedValue);

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
                using (var context = new StudentHostelDBContext())
                {
                    Rooms room;

                    if (Request.QueryString["RoomId"] != null)
                    {
                        int roomId = int.Parse(Request.QueryString["RoomId"]);
                        room = context.Rooms.Find(roomId);

                        if (room == null)
                        {
                            return;
                        }
                    }
                    else
                    {
                        room = new Rooms();
                        context.Rooms.Add(room);
                    }

                    room.RoomNumber = txtRoomNumber.Text.Trim();
                    room.HostelId = int.Parse(ddlHostel.SelectedValue);
                    room.RoomType = txtRoomType.Text.Trim();
                    room.BedSpaces = int.Parse(txtBedSpaces.Text);
                    room.AvailableBedSpaces = int.Parse(txtBedSpaces.Text);
                    room.IsAvailable = true;

                    context.SaveChanges();
                    Response.Redirect("/Room_files/ManageRooms.aspx");
                }
            }
        }

    }
}