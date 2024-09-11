using Student_Hostel_and_Room_Booking_System.Models.Datalayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Hostel_and_Room_Booking_System.Hostel_files
{
    public partial class AddHostel : System.Web.UI.Page
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
        protected void btnSaveHostel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtHostelName.Text))
            {
                using (var context = new StudentHostelDBContext())
                {
                    var hostel = new Hostels
                    {
                        HostelName = txtHostelName.Text.Trim(),
                        Gender = ddlGender.SelectedValue
                    };

                    context.Hostels.Add(hostel);
                    context.SaveChanges();

                    // Redirect back to Hostels page after saving
                    Response.Redirect("ManageHostels.aspx");
                }
            }
            else
            {
                lblMessage.Text = "Please enter a valid hostel name.";
            }
        }

        protected void btnBackToHostels_Click(object sender, EventArgs e)
        {
            // Redirect back to the Hostel management page
            Response.Redirect("/Hostel_files/ManageHostels.aspx");
        }
    }
}