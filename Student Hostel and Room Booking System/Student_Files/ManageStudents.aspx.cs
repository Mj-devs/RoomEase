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
                             LoadStudents();
                            BindStudentsGrid();
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
                    //

                }
            }
        }

        private void BindStudentsGrid()
        {
            using (var context = new StudentHostelDBContext())
            {
                // Retrieve student data along with active room and hostel information
                var students = context.Students
                    .Select(s => new
                    {
                        s.StudentId,
                        s.Name,
                        s.MatricNo,
                        s.PhoneNumber,
                        s.Gender,
                        s.Level,
                        s.Department,
                        // Get the room number and hostel name for the active booking (if available)
                        RoomNumber = s.Bookings
                        .Where(b => b.CheckOutDate == null)
                        .Select(b => b.Room.RoomNumber)
                        .FirstOrDefault() ?? "Not Assigned",
                        HostelName = s.Bookings
                        .Where(b => b.CheckOutDate == null)
                        .Select(b => b.Room.Hostel.HostelName)
                        .FirstOrDefault() ?? "Not Assigned"
                    })
                    .ToList();

                // Bind the result to the GridView
                StudentsGridView.DataSource = students;
                StudentsGridView.DataBind();
            }
        }

        private void LoadStudents()
        {
            using (var context = new StudentHostelDBContext())
            {
                // Include Bookings and Room with its related Hostel data
                var students = context.Students
                    .Include(s => s.Bookings)  // Include the Bookings related to Students
                    .ThenInclude(b => b.Room)  // Include the Room related to each Booking
                    .ThenInclude(r => r.Hostel)  // Include the Hostel related to each Room
                    .Select(s => new
                    {
                        s.StudentId,
                        s.Name,
                        s.MatricNo,
                        s.PhoneNumber,
                        s.Gender,
                        s.Level,
                        s.Department,
                        // Get the room number and hostel name for the active booking (if available)
                         RoomNumber = s.Bookings
                        .Where(b => b.CheckOutDate == null)
                        .Select(b => b.Room.RoomNumber)
                        .FirstOrDefault() ?? "Not Assigned",
                         HostelName = s.Bookings
                        .Where(b => b.CheckOutDate == null)
                        .Select(b => b.Room.Hostel.HostelName)
                        .FirstOrDefault() ?? "Not Assigned"

                    })
                    .ToList();

                // Bind the result to the GridView
                StudentsGridView.DataSource = students;
                StudentsGridView.DataBind();
            }
        }

        protected void StudentsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "BookRoom")
            {
                // Retrieve the StudentId from the CommandArgument
                int studentId = Convert.ToInt32(e.CommandArgument);

                // Store the StudentId in the session
                Session["StudentId"] = studentId;

                // Redirect to the Book Room page
                Response.Redirect("~/Room_files/BookRoom.aspx");
            }
        }

        protected void StudentsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                DropDownList ddlDepartment = (DropDownList)e.Row.FindControl("ddlDepartment");

                using (var context = new StudentHostelDBContext())
                {
                    // Bind the DropDownList to the Departments
                    ddlDepartment.DataSource = context.Department.ToList();
                    ddlDepartment.DataTextField = "DepartmentName";
                    ddlDepartment.DataValueField = "DepartmentId";
                    ddlDepartment.DataBind();

                    // Set the selected value based on the student's department
                    var student = (Students)e.Row.DataItem;
                    if (student != null)
                    {
                       ddlDepartment.SelectedValue = student.DepartmentId.ToString();
                    }
                }
            }
        }
        // This method is triggered when the user clicks the "Edit" button in a row in the StudentsGridView.
        protected void StudentsGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Set the EditIndex of the GridView to the index of the row that is being edited.
            StudentsGridView.EditIndex = e.NewEditIndex;

            // Rebind the GridView to display it in edit mode for the selected row.
            BindStudentsGrid();
        }

        // This method is triggered when the user clicks the "Cancel" button while editing a row.
        protected void StudentsGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Set the EditIndex to -1 to exit the edit mode.
            StudentsGridView.EditIndex = -1;

            // Rebind the GridView to cancel editing and restore the grid to its normal view.
            BindStudentsGrid();
        }

        // This method is triggered when the user updates a row after editing.
        protected void StudentsGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Get the Student ID of the current row being updated.
            int studentId = Convert.ToInt32(StudentsGridView.DataKeys[e.RowIndex].Value);

            // Get the row that is being updated.
            GridViewRow row = StudentsGridView.Rows[e.RowIndex];

            // Retrieve updated values from the TextBoxes and DropDownList in the row.
            TextBox name = (TextBox)(row.Cells[1].Controls[0]);
            TextBox matricno = (TextBox)(row.Cells[2].Controls[0]);
            DropDownList ddlDepartment = (DropDownList)(row.FindControl("ddlDepartment"));
            TextBox Gender = (TextBox)(row.Cells[4].Controls[0]);
            TextBox phoneNumber = (TextBox)(row.Cells[5].Controls[0]);

            // Using a database context to update the student details in the database.
            using (var context = new StudentHostelDBContext())
            {
                // Find the student entity by the studentId.
                var student = context.Students.Find(studentId);

                // Update the student entity with the new values entered in the TextBoxes.
                student.Name = name.Text;
                student.MatricNo = matricno.Text;
                student.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
                student.Gender = Gender.Text;
                student.PhoneNumber = phoneNumber.Text;

                // Save the changes to the database.
                context.SaveChanges();
            }

            // Exit edit mode by setting EditIndex to -1.
            StudentsGridView.EditIndex = -1;

            // Rebind the GridView to reflect the updated data.
            BindStudentsGrid();
        }

        // This method is triggered when the user deletes a row.
        protected void StudentsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the Student ID of the current row being deleted.
            int studentId = Convert.ToInt32(StudentsGridView.DataKeys[e.RowIndex].Value);

            using (var context = new StudentHostelDBContext())
            {
                // Check if there are any bookings for this student
                var hasBookings = context.Bookings.Any(b => b.StudentId == studentId);

                if (hasBookings)
                {
                    // Display error message if the student has bookings
                    lblErrorMessage.Text = "Cannot delete student because they have active booking.<br/>To delete student, kindly go to Manage Rooms and unbook the student from the room.";
                    lblErrorMessage.Visible = true;

                    // Reset and start the timer
                    ErrorTimer.Enabled = true;
                }
                else
                {
                    // Find the student entity by the studentId.
                    var student = context.Students.Find(studentId);

                    if (student != null)
                    {
                        // Remove the student entity from the database.
                        context.Students.Remove(student);
                        // Save the changes to the database.
                        context.SaveChanges();
                    }
                }
            }
            // Rebind the GridView to reflect the deletion.
            BindStudentsGrid();
        }

        protected void ErrorTimer_Tick(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false; // Hide the error message
            ErrorTimer.Enabled = false;
        }
        // This method is triggered when the user clicks the "Add Student" button.
        protected void btnAddStudent_Click(object sender, EventArgs e)
        {
            // Redirect the user to the AddStudents.aspx page to add a new student.
            Response.Redirect("/Student_Files/AddStudents.aspx");
        }
        protected void btnBookStudent_Click(object sender, EventArgs e)
        {
            // Redirect the user to the AddStudents.aspx page to add a new student.
            Response.Redirect("/Room_files/BookRoom.aspx");
        }

        // This method is triggered when the user clicks the "Search" button to search for a student.
        protected void btnSearchStudent_Click(object sender, EventArgs e)
        {
            // Using a database context to search for students based on the search query.
            using (var context = new StudentHostelDBContext())
            {
                // Get the search query from the TextBox and convert it to lowercase.
                string searchQuery = txtSearchStudent.Text.ToLower();

                // Query the database to find students whose name or matric number contains the search query.
                var students = context.Students
                    .Where(s => s.Name.ToLower().Contains(searchQuery) || s.MatricNo.ToLower().Contains(searchQuery))
                    .ToList();

                // Bind the search result to the GridView.
                StudentsGridView.DataSource = students;
                StudentsGridView.DataBind();
            }
        }
    }
}