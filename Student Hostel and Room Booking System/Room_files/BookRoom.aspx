<%@ Page Title="Book Room" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BookRoom.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System.BookRoom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="text-primary">Book Room</h3>
    <h6 class="text-primary">Select student to book room for.</h6>
    <asp:Label ID="lblstudents" Text="" runat="server" /><br />

    <asp:Label  Text="Select Student: " runat="server" />
    <asp:DropDownList ID="ddlUnbookedStudents" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlUnbookedStudents_SelectedIndexChanged"></asp:DropDownList><br />
    <asp:Label ID="lblhostel" Text="Select Hostel: " runat="server" />
    <asp:DropDownList ID="ddlHostels" runat="server"  CssClass="form-control" 
        OnSelectedIndexChanged="ddlHostels_SelectedIndexChanged"
        AutoPostBack="true"></asp:DropDownList><br />
    <asp:Label ID="lblrooms" Text="Select Room: " runat="server" />
    <asp:DropDownList ID="ddlRooms" runat="server"  CssClass="form-control" AutoPostBack="True"></asp:DropDownList><br />
    <asp:Label ID="lblCheckindate" Text="Select Date: " runat="server" />
    <asp:TextBox ID="txtCheckInDate" runat="server" placeholder="Check-In Date" CssClass="form-control" TextMode="Date"></asp:TextBox><br />
    <asp:Label ID="lblCheckoutdate" Text="Select Date: " runat="server" />
    <asp:TextBox ID="txtCheckOutDate" runat="server" placeholder="Check-Out Date" CssClass="form-control" TextMode="Date"></asp:TextBox><br />
    
    <asp:Button ID="btnBookRoom" runat="server" Text="Book Room" OnClick="btnBookRoom_Click" CssClass="btn btn-outline-secondary" /><br />
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

    <asp:UpdatePanel ID="upltimer" runat="server">
    <ContentTemplate>
        <asp:Label ID="lblstudent" runat="server" Text=""></asp:Label>
        <asp:Timer ID="tmRedirect" runat="server" Interval="3000" OnTick="tmRedirect_Tick" Enabled="false"></asp:Timer>
    </ContentTemplate>
</asp:UpdatePanel>
    <br />
    <asp:Button ID="btnBackToStudents" runat="server" Text="Back to Student's list" OnClick="btnBackToStudents_Click" CssClass="btn btn-secondary" />
</asp:Content>
