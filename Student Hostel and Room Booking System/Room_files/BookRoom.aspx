<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BookRoom.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System.BookRoom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="text-primary">Booking Room</h3>
    <asp:DropDownList ID="ddlStudents" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList><br />
    <asp:DropDownList ID="ddlRooms" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList><br />
    <asp:TextBox ID="txtCheckInDate" runat="server" placeholder="Check-In Date" CssClass="form-control" TextMode="Date"></asp:TextBox><br />
    <asp:TextBox ID="txtCheckOutDate" runat="server" placeholder="Check-Out Date" CssClass="form-control" TextMode="Date"></asp:TextBox><br />
    <asp:Button ID="btnBookRoom" runat="server" Text="Book Room" OnClick="btnBookRoom_Click" CssClass="btn btn-outline-secondary" /><br />
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
