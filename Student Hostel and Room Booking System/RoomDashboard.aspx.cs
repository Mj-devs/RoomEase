using Student_Hostel_and_Room_Booking_System.Models.Datalayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Hostel_and_Room_Booking_System
{
    public partial class RoomDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load today's bookings on initial page load
                LoadBookingsForDate(DateTime.Today);
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime selectedDate;

            // Try to parse the entered date
            if (DateTime.TryParse(txtSelectedDate.Text, out selectedDate))
            {
                LoadBookingsForDate(selectedDate);
            }
            else
            {
                lblNoBookings.Text = "Invalid date format. Please use MM/DD/YYYY.";
                lblNoBookings.Visible = true;
            }
        }

        private void LoadBookingsForDate(DateTime selectedDate)
        {
            using (var context = new StudentHostelDBContext())
            {
                var bookings = context.Bookings
                    .Where(b => b.CheckInDate == selectedDate) // Filter bookings by the selected date
                    .Select(b => new
                    {
                        b.Room.RoomNumber,
                        b.Room.Hostel.HostelName,
                        StudentName = b.Student.Name,
                        BookingDate = b.BookingDate,
                        b.CheckInDate,
                        b.CheckOutDate
                    })
                    .ToList();

                if (bookings.Count > 0)
                {
                    BookingsGridView.DataSource = bookings;
                    BookingsGridView.DataBind();
                    lblNoBookings.Visible = false;
                }
                else
                {
                    // If no bookings for the selected date, display a message
                    lblNoBookings.Text = "No bookings found for the selected date.";
                    lblNoBookings.Visible = true;
                }
            }
        }

    }
}