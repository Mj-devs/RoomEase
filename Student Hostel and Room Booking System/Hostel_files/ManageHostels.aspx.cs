using Student_Hostel_and_Room_Booking_System.Models.Datalayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Hostel_and_Room_Booking_System.Hostel_files
{
    public partial class ManageHostels : System.Web.UI.Page
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
                // Query hostels and include room details, count total and available rooms.
                var hostels = context.Hostels
                    .Select(h => new
                    {
                        h.HostelId,
                        h.HostelName,
                        h.Gender,
                        TotalRooms = h.Rooms.Count(), // Total rooms in the hostel
                        AvailableRooms = h.Rooms.Count(r => r.IsAvailable) // Rooms that are not booked (IsAvailable = true)
                    })
                    .ToList();

                // Bind the data to the GridView
                HostelGridView.DataSource = hostels;
                HostelGridView.DataBind();
            }
        }

        protected void HostelGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewRooms")
            {
                // Get the selected Hostel ID
                int hostelId = Convert.ToInt32(e.CommandArgument);

                // Store the HostelId in session
                Session["SelectedHostelId"] = hostelId;

                // Redirect to the ViewHostelDetails page
                Response.Redirect("ViewHostelDetails.aspx");
            }
        }
        // Row Editing
        protected void HostelGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            HostelGridView.EditIndex = e.NewEditIndex;
            LoadHostels();
        }

        // Row Updating
        protected void HostelGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int hostelId = Convert.ToInt32(HostelGridView.DataKeys[e.RowIndex].Value);

            GridViewRow row = HostelGridView.Rows[e.RowIndex];
            TextBox hostelName = (TextBox)(row.Cells[1].Controls[0]);
            TextBox gender = (TextBox)(row.Cells[2].Controls[0]);
            TextBox totalRooms = (TextBox)(row.Cells[3].Controls[0]);

            using (var context = new StudentHostelDBContext())
            {
                var hostel = context.Hostels.Find(hostelId);
                if (hostel != null)
                {
                    hostel.HostelName = hostelName.Text;
                    hostel.Gender = gender.Text;
                    hostel.TotalRooms = Convert.ToInt32(totalRooms.Text);
                    context.SaveChanges();
                }
            }

            HostelGridView.EditIndex = -1;
            LoadHostels();
        }

        // Cancel Edit
        protected void HostelGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            HostelGridView.EditIndex = -1;
            LoadHostels();
        }

        // Delete Hostel
        protected void HostelGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int hostelId = Convert.ToInt32(HostelGridView.DataKeys[e.RowIndex].Value);

            using (var context = new StudentHostelDBContext())
            {
                var hostel = context.Hostels.Find(hostelId);
                if (hostel != null)
                {
                    // Check if there are any rooms assigned to this hostel
                    var rooms = context.Rooms.Where(r => r.HostelId == hostelId).ToList();
                    if (rooms.Count > 0)
                    {
                        lblMessage.Text = "Cannot delete hostel because it has assigned rooms.";
                        return;
                    }

                    context.Hostels.Remove(hostel);
                    context.SaveChanges();
                    lblMessage.Text = "Hostel deleted successfully!";
                }
            }

            LoadHostels();
        }

        // This method is triggered when the user clicks the "Search" button to search for a hostel.
        protected void btnSearchStudent_Click(object sender, EventArgs e)
        {
            using (var context = new StudentHostelDBContext())
            {
                // Get the search query from the TextBox and convert it to lowercase.
                string searchQuery = txtSearchHostel.Text.ToLower();

                // Query the database to find hostel whose name or gender contains the search query.
                var students = context.Hostels
                    .Where(s => s.HostelName.ToLower().Contains(searchQuery) || s.Gender.ToLower().Contains(searchQuery))
                    .ToList();

                // Bind the search result to the GridView.
                HostelGridView.DataSource = students;
                HostelGridView.DataBind();
            }
        }
        protected void btnAddHostel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Hostel_files/AddHostel.aspx");
        }

    }
}