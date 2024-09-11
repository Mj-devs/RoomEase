<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <h3>Room Ease</h3>
        <div class="container">
            <asp:Label ID="lblTitle" runat="server" CssClass="page-title" Text="About RoomEase: Student Hostel & Room Management System" />

            <asp:Panel ID="pnlContent" runat="server" CssClass="content-panel">
                <h2>Overview</h2>
                <p> <b class="text-primary">RoomEase</b> is a comprehensive <b class="text-primary">Student Hostel and Room Management System</b> designed to streamline the management of student accommodations in academic institutions. The system simplifies various administrative tasks, from room bookings and student registrations to hostel management, offering a seamless, user-friendly interface for students and administrators alike.</p>

                <h3>Key Features:</h3>
                <ul>
                    <li><strong>Hostel Management:</strong>
                        <ul>
                            <li>Administrators can manage hostels, including adding new hostels, viewing all existing hostels, and tracking the number of rooms available.</li>
                            <li>Rooms within hostels are classified based on availability, with real-time updates on occupied, booked, or vacant rooms.</li>
                            <li>The system allows gender-specific hostel allocation, ensuring that students are only assigned rooms in appropriate hostels.</li>
                        </ul>
                    </li>
                    <li><strong>Student Registration and Management:</strong>
                        <ul>
                            <li>The system supports the registration of both <b class="text-primary">returning students</b> (200 level and above) and <b class="text-primary">new students</b> (freshers, including JAMB or direct-entry applicants).</li>
                            <li>Students are categorized based on their level, matriculation number, and admission type (JAMB or direct-entry).</li>
                            <li>Administrators can view and manage student details, including student profiles and room booking history.</li>
                        </ul>
                    </li>
                    <li><strong>Room Booking:</strong>
                        <ul>
                            <li>Only <b class="text-primary">unbooked</b> students can select rooms from available hostels, preventing duplicate bookings.</li>
                            <li>Administrators can book rooms for students, with the option to filter rooms by <b class="text-primary">hostel availability</b> and <b class="text-primary">gender restrictions</b>.</li>
                            <li>RoomEase ensures that once a student is assigned to a room, the room is marked as booked, providing up-to-date room availability.</li>
                        </ul>
                    </li>
                    <li><strong>Session-Based Management:</strong>
                        <ul>
                            <li>RoomEase uses session management to facilitate secure and accurate handling of data. For instance, student selections and bookings are stored in the session, ensuring that data persists across different actions, such as viewing available rooms or returning to previous selections.</li>
                        </ul>
                    </li>
                    <li><strong>Error Handling & Validation:</strong>
                        <ul>
                            <li>Built-in error handling ensures smooth data management, preventing issues like duplicate student entries or invalid room bookings.</li>
                            <li>Detailed error messages and validations are provided, guiding users when actions cannot be completed, such as trying to delete a student assigned to a room.</li>
                        </ul>
                    </li>
                </ul>

                <p><b class="text-primary">RoomEase</b> empowers administrators and students alike by offering a centralized, intuitive solution for managing student hostels, ensuring a smooth, transparent, and efficient room assignment process.</p>
            </asp:Panel>
        </div>
</asp:Content>
