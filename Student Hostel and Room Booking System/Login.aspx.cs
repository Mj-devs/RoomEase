using Student_Hostel_and_Room_Booking_System.Models.Datalayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Hostel_and_Room_Booking_System
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            using (var context = new StudentHostelDBContext())
            {
                var coordinator = context.RoomCoordinator
                    .SingleOrDefault(c => c.Username == username);

                    // Validator to enter RoomEase
                if (coordinator != null && VerifyPassword(password, coordinator.PasswordHash))
                {
                    // Store user information in session
                    Session["RoomCoordinatorId"] = coordinator.RoomCoordinatorId;
                    Session["Username"] = coordinator.Username;

                    // Redirect to the room booking page
                    Response.Redirect("~/Dashboard");
                }
                else
                {
                    lblMessage.Text = "Invalid username or password.";
                }
            }
        }

        // Method to verify password
        private bool VerifyPassword(string password, string hashedPassword)
        {
            // Implement password verification logic
            return password == hashedPassword; 
        }

    }
}