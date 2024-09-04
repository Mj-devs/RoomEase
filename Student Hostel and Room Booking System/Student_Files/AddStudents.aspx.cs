using Student_Hostel_and_Room_Booking_System.Models.Datalayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
            using (var context = new StudentHostelDBContext())
            {
                var student = context.Students.Find(studentId);
                if (student != null)
                {
                    txtname.Text = student.Name;
                    txtmatricno.Text = student.MatricNo;
                    txtphonenumber.Text = student.PhoneNumber;
                }
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            using (var context = new StudentHostelDBContext())
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
                student.MatricNo = txtmatricno.Text;
                student.PhoneNumber = txtphonenumber.Text;

                context.SaveChanges();
                Response.Redirect("ManageStudents.aspx");
            }
        }

        protected void lbToggleFields_Click(object sender, EventArgs e)
        {
            // Toggle visibility of the Panel
            pnlHiddenFields.Visible = !pnlHiddenFields.Visible;
            
            // Toggle visibility of the Original Save button
            btnsave.Visible = !btnsave.Visible;

            pnlTextBoxContainer.Visible = !pnlTextBoxContainer.Visible;
        }

        protected void cvMatricNo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string matricNo = txtmatricno.Text;

            string matricno= args.Value.Trim();
            
            using (var context = new StudentHostelDBContext())
            {
                // Check if the matriculation number already exists
                bool matricNoExists = context.Students.Any(i => i.MatricNo == matricNo);

                // If the matriculation number exists, validation fails
                if (matricNoExists)
                {
                    args.IsValid = false; // Set validation to fail
                }
                else
                {
                    args.IsValid = true; // Set validation to pass
                }

                // Check if the username is in email format
                args.IsValid = matricno.Contains("/") && matricno.Contains("/");
            }
        }
        protected void btnsavefresher_Click(object sender, EventArgs e)
        {
            using (var context = new StudentHostelDBContext())
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
                student.MatricNo = txtmatricno.Text;
                student.PhoneNumber = txtphonenumber.Text;
                student.JambRegNo = txtjambno.Text;
                student.Gender = txtgender.Text;

                context.SaveChanges();
                Response.Redirect("/Student_Files/ManageStudents.aspx");
            }
        }
    }
}