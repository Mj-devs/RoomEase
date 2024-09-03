using Microsoft.EntityFrameworkCore;
using Student_Hostel_and_Room_Booking_System.Models.Datalayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Hostel_and_Room_Booking_System
{
    public partial class ManageRooms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRoomsGrid();
            }
        }

        private void BindRoomsGrid()
        {
            using (var context = new StudentHostelDbContext())
            {
                var rooms = context.Rooms.Include("Hostel").ToList();
                RoomsGridView.DataSource = rooms;
                RoomsGridView.DataBind();
            }
        }

        protected void RoomsGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            RoomsGridView.EditIndex = e.NewEditIndex;
            BindRoomsGrid();
        }

        protected void RoomsGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            RoomsGridView.EditIndex = -1;
            BindRoomsGrid();
        }

        protected void RoomsGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int roomId = Convert.ToInt32(RoomsGridView.DataKeys[e.RowIndex].Value);
            GridViewRow row = RoomsGridView.Rows[e.RowIndex];

            string roomNumber = (row.FindControl("txtRoomNumber") as TextBox).Text;
            int hostelId = int.Parse((row.FindControl("ddlHostel") as DropDownList).SelectedValue);
            bool isAvailable = (row.FindControl("chkIsAvailable") as CheckBox).Checked;
            string roomType = (row.FindControl("txtRoomType") as TextBox).Text;

            using (var context = new StudentHostelDbContext())
            {
                var room = context.Rooms.Find(roomId);
                room.RoomNumber = roomNumber;
                room.HostelId = hostelId;
                room.IsAvailable = isAvailable;
                room.RoomType = roomType;

                context.SaveChanges();
            }

            RoomsGridView.EditIndex = -1;
            BindRoomsGrid();
        }

        protected void RoomsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int roomId = Convert.ToInt32(RoomsGridView.DataKeys[e.RowIndex].Value);

            using (var context = new StudentHostelDbContext())
            {
                var room = context.Rooms.Find(roomId);
                context.Rooms.Remove(room);
                context.SaveChanges();
            }

            BindRoomsGrid();
        }

        protected void btnAddRoom_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddRoom.aspx");
        }
        protected void btnSearchRoom_Click(object sender, EventArgs e)
        {
            using (var context = new StudentHostelDbContext())
            {
                string searchQuery = txtSearchRoom.Text.ToLower();
                var rooms = context.Rooms
                    .Where(r => r.RoomNumber.ToLower().Contains(searchQuery))
                    .ToList();

                RoomsGridView.DataSource = rooms;
                RoomsGridView.DataBind();
            }
            BindRoomsGrid();
        }

    }
}