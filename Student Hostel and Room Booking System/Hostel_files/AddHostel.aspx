<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddHostel.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System.Hostel_files.AddHostel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label Text="Hostel Name:" runat="server" />
        <asp:TextBox ID="txtHostelName" runat="server" CssClass="form-control" placeholder="Hostel Name"></asp:TextBox>
        <br />
    <asp:Label Text="Select Gender for the Hostel:" runat="server" />
        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
            <asp:ListItem Value="Male">Male</asp:ListItem>
            <asp:ListItem Value="Female">Female</asp:ListItem>
        </asp:DropDownList>
        <br />
    <asp:Label Text="Total Number of Room:" runat="server" />
        <asp:TextBox ID="txtTotalRoom" runat="server" CssClass="form-control" placeholder="Total No. of  Room"></asp:TextBox>
        <br />
        <asp:Button ID="btnSaveHostel" runat="server" Text="Save Hostel" OnClick="btnSaveHostel_Click" CssClass="btn btn-primary" />
        <br /><br />
        <asp:Label ID="lblMessage" Text="" runat="server" />
        <asp:Button ID="btnBackToHostels" runat="server" Text="Back to Hostels" OnClick="btnBackToHostels_Click" CssClass="btn btn-secondary" />

</asp:Content>
