<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddRoom.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System.AddRoom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="txtRoomNumber" runat="server" placeholder="Room Number" CssClass="form-control"></asp:TextBox><br />
    <asp:DropDownList ID="ddlHostel" runat="server"></asp:DropDownList><br />
    <br />
    <asp:CheckBox ID="chkIsAvailable" runat="server" Text="Available"></asp:CheckBox><br />
    <asp:TextBox ID="txtRoomType" runat="server" CssClass="form-control" placeholder="Room Type"></asp:TextBox><br />
    <asp:Button ID="btnsave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnsave_Click" />

</asp:Content>
