using Microsoft.EntityFrameworkCore;
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
            {    // Retrieving the RoomCoordinatoor Id from the session
                if (Session["RoomCoordinatorId"] != null)
                {
                    int RoomCoordinatorId = (int)Session["RoomCoordinatorId"];

                    using (var context = new StudentHostelDBContext())
                    {
                        var RoomCoordinator = context.RoomCoordinator.Where(r => RoomCoordinatorId == r.RoomCoordinatorId).FirstOrDefault();

                        if (RoomCoordinator != null)
                        {
                            if (Request.QueryString["StudentId"] != null)
                            {
                                int studentId = int.Parse(Request.QueryString["StudentId"]);
                                LoadStudentDetails(studentId);
                            }
                                LoadDepartmemnts();
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
        private void LoadDepartmemnts()
        {
            using (var context = new StudentHostelDBContext())
            {
                var departments = context.Department.ToList();

                ddlDepartment.DataSource = departments;
                ddlDepartment.DataTextField = "DepartmentName";
                ddlDepartment.DataValueField = "DepartmentId";
                ddlDepartment.DataBind();
            }

            ddlDepartment.Items.Insert(0, new ListItem("--Select Department--", "0"));
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            // Initialize a variable to track if validation passes.
            bool isValid = true;

            // Clear any previous error messages.
            lblMessage.Text = string.Empty;

            // Check if all required fields are filled.
            if (string.IsNullOrWhiteSpace(txtname.Text))
            {
                lblMessage.Text += "Name is required. ";
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtmatricno.Text))
            {
                lblMessage.Text += "Matriculation number is required. ";
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtphonenumber.Text))
            {
                lblMessage.Text += "Phone number is required. ";
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtlevel.Text))
            {
                lblMessage.Text += "Level is required. ";
                isValid = false;
            }

            if (ddlDepartment.SelectedIndex == 0)
            {
                lblMessage.Text += "Please select a department. ";
                isValid = false;
            }

            // If validation fails, do not proceed with saving.
            if (!isValid)
            {
                return; // Exit the method if any validation fails.
            }

            using (var context = new StudentHostelDBContext())
            {
                Students student;

                // Check if "StudentId" exists in the query string.
                if (Request.QueryString["StudentId"] != null)
                {
                    int studentId = int.Parse(Request.QueryString["StudentId"]);
                    student = context.Students.Find(studentId);

                    // If the student is not found, handle the case (optional).
                    if (student == null)
                    {
                        lblMessage.Text = "Student not found.";
                        return; // Exit the method if no student is found.
                    }
                }
                else
                {
                    // Create a new student if no StudentId is provided.
                    student = new Students();
                    context.Students.Add(student);
                }

                // Update the student's properties with values from the form fields.
                student.Name = txtname.Text.Trim();
                student.PhoneNumber = txtphonenumber.Text.Trim();
                student.Gender = ddlGender.SelectedValue;
                student.Level = txtlevel.Text.Trim();

                if (string.IsNullOrEmpty(student.MatricNo))
                {
                    student.MatricNo = "N/A";  // Placeholder value until MatricNo is generated
                }

                // Retrieve the Department object based on the selected value from dropdown.
                int departmentId = int.Parse(ddlDepartment.SelectedValue);
                var department = context.Department.Find(departmentId);

                // Assign the Department object to the student.
                if (department != null)
                {
                    student.Department = department;
                }
                else
                {
                    lblMessage.Text = "Selected department not found.";
                    lblMessage.Visible = true;
                    return; // Exit the method if the department is not found.
                }

                try
                {
                    // Save changes to the database.
                    context.SaveChanges();

                    // Redirect to ManageStudents.aspx after successfully saving.
                    Response.Redirect("ManageStudents.aspx");
                }
                catch (DbUpdateException ex)
                {
                    // Check for unique constraint violation.
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("UQ__Students__8E55BF888B1A263A"))
                    {
                        lblMessage.Text = "A student with this matriculation number already exists." + Environment.NewLine + "Please use a different matriculation number.";
                        lblMessage.Visible = true;

                        // Reset and start the timer
                        ErrorTimer.Enabled = true;
                    }
                    else
                    {
                        // General error message for other exceptions.
                        lblMessage.Text = "An error occurred while saving the student. Please try again later.";
                    }
                }
            }
        }

        protected void ErrorTimer_Tick(object sender, EventArgs e)
        {
            lblMessage.Visible = false; 
            
            // Hide the error message
            ErrorTimer.Enabled = false;
        }

        protected void lbToggleFields_Click(object sender, EventArgs e)
        {
            // Toggle visibility of the Panel
            pnlHiddenFields.Visible = !pnlHiddenFields.Visible;
            
            // Toggle visibility of the Original Save button
            btnsave.Visible = !btnsave.Visible;

            pnlTextBoxContainer.Visible = !pnlTextBoxContainer.Visible;

            if (pnlHiddenFields.Visible)
            {
                lbToggleFields.Text = "Add Existing Student +";
            }
            else
            {
                lbToggleFields.Text = "Add Fresher +";
            }
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

                // Add Fresher to database
                student.Name = txtname.Text;
                student.PhoneNumber = txtphonenumber.Text;
                student.JambRegNo = txtjambno.Text;
                student.Gender = ddlGender.Text;

                context.SaveChanges();
                ClearForm();
            }
        }
        protected void ClearForm()
        {
            txtname.Text = string.Empty;
            txtlevel.Text = string.Empty;
            txtmatricno.Text = string.Empty;
            txtphonenumber.Text = string.Empty;
            txtjambno.Text = string.Empty;
            txtdob.Text=string.Empty;
            ddlGender.Text = string.Empty;
        }
        protected void btnBackToStudents_Click(object sender, EventArgs e)
        {
            // Redirect back to the Room management page
            Response.Redirect("/Student_Files/ManageStudents.aspx");
        }
    }
}