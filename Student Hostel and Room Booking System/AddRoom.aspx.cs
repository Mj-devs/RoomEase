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
            {
                PopulateHostels();
                if (Request.QueryString["RoomId"] != null)
                {
                    int roomId = int.Parse(Request.QueryString["RoomId"]);
                    LoadRoomDetails(roomId);
                }
            }
        }

        private void PopulateHostels()
        {
            using (var context = new StudentHostelDbContext())
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
            using (var context = new StudentHostelDbContext())
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

        protected void btnsave_Click(object sender, EventArgs e)
        {
            using (var context = new StudentHostelDbContext())
            {
                Rooms room;
                if (Request.QueryString["RoomId"] != null)
                {
                    int roomId = int.Parse(Request.QueryString["RoomId"]);
                    room = context.Rooms.Find(roomId);
                }
                else
                {
                    room = new Rooms();
                    context.Rooms.Add(room);
                }

                room.RoomNumber = txtRoomNumber.Text;
                room.HostelId = int.Parse(ddlHostel.SelectedValue);
                room.IsAvailable = chkIsAvailable.Checked;
                room.RoomType = txtRoomType.Text;

                context.SaveChanges();
                Response.Redirect("ManageRooms.aspx");
            }
        }
    }
}