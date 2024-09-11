<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="text-primary">Welcome to RoomEase</h3>
    <h5 class="text-secondary">Here's your dashboard, user <asp:Label ID="lbluser" CssClass="text-success" Text="" runat="server" /></h5>
    <div class="row">
      <div class="card text-center mb-3" style="width: 18rem;">
      <div class="card-body">
        <h5 class="card-title">Setup Hostels</h5>
        <p class="card-text">Here you can see hostels, number of rooms in which hostels have and see students in rooms...</p>
        <a href="/Hostel_files/ManageHostels.aspx" class="btn btn-primary">Go to Setup Hostels</a>
      </div>
    </div>
      <div class="card text-center mb-3 ms-3" style="width: 18rem;">
      <div class="card-body">
        <h5 class="card-title">Setup Rooms</h5>
        <p class="card-text">Here you can see available bedspaces rooms, add new rooms and book rooms...</p>
        <a href="/Room_files/ManageRooms.aspx" class="btn btn-primary">Go to Setup Rooms</a>
      </div>
    </div>
      <div class="card text-center mb-3 ms-3" style="width: 18rem;">
      <div class="card-body">
        <h5 class="card-title">Setup Students</h5>
        <p class="card-text">Here you can see already added students, add new students... </p>
        <a href="/Student_files/ManageStudents.aspx" class="btn btn-primary">Go to Setup Students</a>
      </div>
    </div>
    </div>
</asp:Content>
