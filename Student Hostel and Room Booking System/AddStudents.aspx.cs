using Student_Hostel_and_Room_Booking_System.Models.Datalayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Hostel_and_Room_Booking_System
{
    public partial class AddStudents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["StudentId"] != null)
                {
                    int studentId = int.Parse(Request.QueryString["StudentId"]);
                    LoadStudentDetails(studentId);
                }
            }
        }

        private void LoadStudentDetails(int studentId)
        {
            using (var context = new StudentHostelDbContext())
            {
                var student = context.Students.Find(studentId);
                if (student != null)
                {
                    txtname.Text = student.Name;
                    txtemail.Text = student.Email;
                    txtphonenumber.Text = student.PhoneNumber;
                }
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            using (var context = new StudentHostelDbContext())
            {
                Students student;
                if (Request.QueryString["StudentId"] != null)
                {
                    int studentId = int.Parse(Request.QueryString["StudentId"]);
                    student = context.Students.Find(studentId);
                }
                else
                {
                    student = new Students();
                    context.Students.Add(student);
                }

                student.Name = txtname.Text;
                student.Email = txtemail.Text;
                student.PhoneNumber = txtphonenumber.Text;

                context.SaveChanges();
                Response.Redirect("ManageStudents.aspx");
            }
        }
    }
}