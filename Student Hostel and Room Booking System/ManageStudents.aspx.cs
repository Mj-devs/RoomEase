using Student_Hostel_and_Room_Booking_System.Models.Datalayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Hostel_and_Room_Booking_System
{
    public partial class _Default : Page
    {
            protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindStudentsGrid();
            }
        }

        private void BindStudentsGrid()
        {
            using (var context = new StudentHostelDbContext())
            {
                var students = context.Students.ToList();
                StudentsGridView.DataSource = students;
                StudentsGridView.DataBind();
            }
        }

        protected void StudentsGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            StudentsGridView.EditIndex = e.NewEditIndex;
            BindStudentsGrid();
        }

        protected void StudentsGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            StudentsGridView.EditIndex = -1;
            BindStudentsGrid();
        }

        protected void StudentsGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int studentId = Convert.ToInt32(StudentsGridView.DataKeys[e.RowIndex].Value);
            GridViewRow row = StudentsGridView.Rows[e.RowIndex];

            string name = (row.FindControl("txtName") as TextBox).Text;
            string email = (row.FindControl("txtEmail") as TextBox).Text;
            string phoneNumber = (row.FindControl("txtPhoneNumber") as TextBox).Text;

            using (var context = new StudentHostelDbContext())
            {
                var student = context.Students.Find(studentId);
                student.Name = name;
                student.Email = email;
                student.PhoneNumber = phoneNumber;

                context.SaveChanges();
            }

            StudentsGridView.EditIndex = -1;
            BindStudentsGrid();
        }

        protected void StudentsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int studentId = Convert.ToInt32(StudentsGridView.DataKeys[e.RowIndex].Value);

            using (var context = new StudentHostelDbContext())
            {
                var student = context.Students.Find(studentId);
                context.Students.Remove(student);
                context.SaveChanges();
            }

            BindStudentsGrid();
        }

        protected void btnAddStudent_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddStudents.aspx");
        }

        protected void btnSearchStudent_Click(object sender, EventArgs e)
        {
            using (var context = new StudentHostelDbContext())
            {
                string searchQuery = txtSearchStudent.Text.ToLower();
                var students = context.Students
                    .Where(s => s.Name.ToLower().Contains(searchQuery) || s.Email.ToLower().Contains(searchQuery))
                    .ToList();

                StudentsGridView.DataSource = students;
                StudentsGridView.DataBind();
            }
            BindStudentsGrid();
        }

    }
}