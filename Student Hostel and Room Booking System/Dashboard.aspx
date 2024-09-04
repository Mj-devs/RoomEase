<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
      <div class="card text-center mb-3" style="width: 18rem;">
      <div class="card-body">
        <h5 class="card-title">Manage Rooms</h5>
        <p class="card-text">Here you can see available bedspaces rooms, add new rooms and book rooms...</p>
        <a href="/Room_files/ManageRooms.aspx" class="btn btn-primary">Go to Manage Rooms</a>
      </div>
    </div>
  <div class="card text-center mb-3 ms-3" style="width: 18rem;">
  <div class="card-body">
    <h5 class="card-title">Manage Students</h5>
    <p class="card-text">Here you can see already added students, add new students... </p>
    <a href="/Student_files/ManageStudents.aspx" class="btn btn-primary">Go to Manage Students</a>
  </div>
</div>
</div>
</asp:Content>
