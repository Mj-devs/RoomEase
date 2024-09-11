<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RoomDashboard.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System.RoomDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblTitle" runat="server" Text="Room Bookings Dashboard" CssClass="h2"></asp:Label>
    <hr />

    <!-- Date Picker to select a date -->
    <asp:Label ID="lblSelectDate" runat="server" Text="Select Date: " CssClass="h5"></asp:Label>

    <asp:TextBox ID="txtSelectedDate" runat="server" CssClass="form-control mb-2" placeholder="MM/DD/YYYY"></asp:TextBox>
    <asp:Button ID="btnFilter" runat="server" Text="Filter" CssClass="btn btn-primary ms-2" OnClick="btnFilter_Click" />
    
    <br /><br />

    <!-- Display room bookings for the selected date -->
    <asp:GridView ID="BookingsGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
        <Columns>
            <asp:BoundField DataField="RoomNumber" HeaderText="Room Number" />
            <asp:BoundField DataField="HostelName" HeaderText="Hostel Name" />
            <asp:BoundField DataField="StudentName" HeaderText="Booked By" />
            <asp:BoundField DataField="BookingDate" HeaderText="Booking Date" DataFormatString="{0:MM/dd/yyyy}" />
            <asp:BoundField DataField="CheckInDate" HeaderText="Check-In Date" DataFormatString="{0:MM/dd/yyyy}" />
            <asp:BoundField DataField="CheckOutDate" HeaderText="Check-Out Date" DataFormatString="{0:MM/dd/yyyy}" />
        </Columns>
    </asp:GridView>

    <asp:Label ID="lblNoBookings" runat="server" Text="" Visible="false" CssClass="text-danger"></asp:Label>

</asp:Content>
