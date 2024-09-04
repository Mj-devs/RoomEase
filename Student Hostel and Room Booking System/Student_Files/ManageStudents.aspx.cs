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
            {    // Retrieving the RoomCoordinatoor Id from the session
                if (Session["RoomCoordinatorId"] != null)
                {
                    int RoomCoordinatorId = (int)Session["RoomCoordinatorId"];

                    using (var context = new StudentHostelDBContext())
                    {
                        var RoomCoordinator = context.RoomCoordinator.Where(r => RoomCoordinatorId == r.RoomCoordinatorId).FirstOrDefault();


                        if (RoomCoordinator != null)
                        {
                        }
                        else
                        {
                            lblMessage.Text = "Lecturer not found.";
                        }
                    }
                }
                else
                {
                    // Case where session is null (redirect to login page)
                    Response.Redirect("~/Login.aspx");
                }
                BindStudentsGrid();
            }
        }

        private void BindStudentsGrid()
        {
            using (var context = new StudentHostelDBContext())
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

            TextBox name = (TextBox)(row.Cells[1].Controls[0]);
            TextBox email = (TextBox)(row.Cells[2].Controls[0]);
            TextBox phoneNumber = (TextBox)(row.Cells[3].Controls[0]);

            using (var context = new StudentHostelDBContext())
            {
                var student = context.Students.Find(studentId);
                student.Name = name.Text;
                student.MatricNo = email.Text;
                student.PhoneNumber = phoneNumber.Text;

                context.SaveChanges();
            }

            StudentsGridView.EditIndex = -1;
            BindStudentsGrid();
        }

        protected void StudentsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int studentId = Convert.ToInt32(StudentsGridView.DataKeys[e.RowIndex].Value);

            using (var context = new StudentHostelDBContext())
            {
                var student = context.Students.Find(studentId);
                context.Students.Remove(student);
                context.SaveChanges();
            }

            BindStudentsGrid();
        }

        protected void btnAddStudent_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Student_Files/AddStudents.aspx");
        }

        protected void btnSearchStudent_Click(object sender, EventArgs e)
        {
            using (var context = new StudentHostelDBContext())
            {
                string searchQuery = txtSearchStudent.Text.ToLower();
                var students = context.Students
                    .Where(s => s.Name.ToLower().Contains(searchQuery) || s.MatricNo.ToLower().Contains(searchQuery))
                    .ToList();

                StudentsGridView.DataSource = students;
                StudentsGridView.DataBind();
            }
            BindStudentsGrid();
        }

    }
}