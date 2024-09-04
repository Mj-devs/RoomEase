<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewRoomDetails.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System.Room_files.ViewRoomDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2></h2>
    <asp:GridView ID="StudentsGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover">
    <Columns>
        <asp:BoundField DataField="StudentId" HeaderText="Student ID" />
        <asp:BoundField DataField="StudentName" HeaderText="Student Name" />
        <asp:BoundField DataField="CheckInDate" HeaderText="Check-In Date" />
        <asp:BoundField DataField="CheckOutDate" HeaderText="Check-Out Date" />
    </Columns>
</asp:GridView>

</asp:Content>
