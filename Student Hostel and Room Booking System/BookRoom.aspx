<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BookRoom.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System.BookRoom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:DropDownList ID="ddlStudents" runat="server" AutoPostBack="True"></asp:DropDownList><br />
    <asp:DropDownList ID="ddlRooms" runat="server" AutoPostBack="True"></asp:DropDownList><br />
    <asp:TextBox ID="txtCheckInDate" runat="server" placeholder="Check-In Date" CssClass="form-control"></asp:TextBox><br />
    <asp:TextBox ID="txtCheckOutDate" runat="server" placeholder="Check-Out Date" CssClass="form-control"></asp:TextBox><br />
    <asp:Button ID="btnBookRoom" runat="server" Text="Book Room" OnClick="btnBookRoom_Click" /><br />
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

</asp:Content>
