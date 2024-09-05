<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewRoomDetails.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System.Room_files.ViewRoomDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblRoomNumber" Text="" runat="server" />
    <asp:GridView ID="StudentsGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover">
    <Columns>
        <asp:BoundField DataField="StudentId" HeaderText="Student ID" />
        <asp:BoundField DataField="StudentName" HeaderText="Student Name" />
        <asp:BoundField DataField="CheckInDate" HeaderText="Matric Number" />
        <asp:BoundField DataField="CheckInDate" HeaderText="Check-In Date" />
    </Columns>
</asp:GridView>

</asp:Content>
