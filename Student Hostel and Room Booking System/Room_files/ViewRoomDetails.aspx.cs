using Student_Hostel_and_Room_Booking_System.Models.Datalayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Hostel_and_Room_Booking_System.Room_files
{
    public partial class ViewRoomDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the room number from the session
                string roomNumber = Session["SelectedRoomNumber"] as string;
                if (!string.IsNullOrEmpty(roomNumber))
                {
                    lblRoomNumber.Text = $"Details for Room: {roomNumber}";
                }

                string roomId = Request.QueryString["RoomId"];
                if (!string.IsNullOrEmpty(roomId))
                {
                    LoadStudents(int.Parse(roomId));
                }
            }
        }

        private void LoadStudents(int roomId)
        {
            using (var context = new StudentHostelDBContext())
            {
                var students = context.Bookings
                    .Where(b => b.RoomId == roomId)
                    .Select(b => new
                    {
                        b.Student.StudentId,
                        StudentName = b.Student.Name + " " + b.Student.MatricNo,
                        b.CheckInDate,
                    })
                    .ToList();

                StudentsGridView.DataSource = students;
                StudentsGridView.DataBind();
            }
        }


    }
}