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
            {    // Retrieving the RoomCoordinatoor Id from the session
                if (Session["RoomCoordinatorId"] != null)
                {
                    int RoomCoordinatorId = (int)Session["RoomCoordinatorId"];

                    using (var context = new StudentHostelDBContext())
                    {
                        var RoomCoordinator = context.RoomCoordinator.Where(r => RoomCoordinatorId == r.RoomCoordinatorId).FirstOrDefault();
                        if (RoomCoordinator != null)
                        {
                            lbluser.Text = $"User that is signed in: {RoomCoordinator.Username}";
                        }
                        else
                        {
                            // Handle the case where the RoomCoordinator is not found
                            lbluser.Text = "Coordinator not found";
                        }
                    }
                }
                else
                {
                    // Case where session is null (redirect to login page)
                    Response.Redirect("~/Login.aspx");
                }
                BindRoomsGrid();
            }
        }

        private void BindRoomsGrid()
        {
            using (var context = new StudentHostelDBContext())
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

            // Retrieve controls from the GridView row
            //TextBox roomNumber = (TextBox)(row.Cells[1].Controls[0]);
            int hostelId = int.Parse((row.FindControl("ddlHostel") as DropDownList).SelectedValue);
            TextBox roomType = (TextBox)(row.Cells[3].Controls[0]);
            TextBox bedSpacesTextBox = (TextBox)(row.Cells[4].Controls[0]);
            TextBox availableBedSpacesTextBox = (TextBox)(row.Cells[5].Controls[0]);
            CheckBox isAvailable = (CheckBox)(row.Cells[6].Controls[0]);

            using (var context = new StudentHostelDBContext())
            {
                var room = context.Rooms.Find(roomId);

                int newBedSpaces = int.Parse(bedSpacesTextBox.Text);
                int currentAvailableBedSpaces = room.AvailableBedSpaces;

                if (newBedSpaces < room.BedSpaces)
                {
                    room.AvailableBedSpaces -= (room.BedSpaces - newBedSpaces);
                }
                else
                {
                    room.AvailableBedSpaces += (newBedSpaces - room.BedSpaces);
                }

               //room.RoomNumber = roomNumber.Text;
                room.HostelId = hostelId;
                room.BedSpaces = newBedSpaces;
                room.AvailableBedSpaces = currentAvailableBedSpaces;
                room.IsAvailable = isAvailable.Checked;

                
                context.SaveChanges();

                RoomsGridView.EditIndex = -1;
                BindRoomsGrid();
            }

        }

        protected void RoomsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int roomId = Convert.ToInt32(RoomsGridView.DataKeys[e.RowIndex].Value);

            using (var context = new StudentHostelDBContext())
            {
                var room = context.Rooms.Find(roomId);
                context.Rooms.Remove(room);
                context.SaveChanges();
            }

            BindRoomsGrid();
        }

        protected void RoomsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var _context = new StudentHostelDBContext();
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState.HasFlag(DataControlRowState.Edit))
            {
                // Find the DropDownList in the EditItemTemplate
                var ddlHostel = e.Row.FindControl("ddlHostel") as DropDownList;

                if (ddlHostel != null)
                {
                    // Get the list of hostels from the database
                    var hostels = _context.Hostels.ToList();

                    // Bind the DropDownList to the hostel data
                    ddlHostel.DataSource = hostels;
                    ddlHostel.DataTextField = "HostelName";  // Replace with your actual column name
                    ddlHostel.DataValueField = "HostelId";   // Replace with your actual column name
                    ddlHostel.DataBind();

                    // Optionally, set the selected value to match the current student record
                    var currentHostelId = DataBinder.Eval(e.Row.DataItem, "HostelId").ToString();
                    ddlHostel.SelectedValue = currentHostelId;
                }
            }

        }

        protected void btnAddRoom_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Room_files/AddRoom.aspx");
        }
        protected void btnBookRoom_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Room_files/BookRoom.aspx");
        }
        protected void btnSearchRoom_Click(object sender, EventArgs e)
        {
            using (var context = new StudentHostelDBContext())
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