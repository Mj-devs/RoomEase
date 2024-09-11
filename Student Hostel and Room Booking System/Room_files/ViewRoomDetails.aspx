<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewRoomDetails.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System.Room_files.ViewRoomDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblRoomNumber" Text="" runat="server" />
    <asp:Label ID="lblBedSpaces" Text="" runat="server" />
    <asp:Label ID="lblAvailableBedSpaces" Text="" runat="server" />

    <asp:GridView ID="StudentsGridView" runat="server" AutoGenerateColumns="False" 
        OnRowCommand="StudentsGridView_RowCommand" DataKeyNames="StudentID" 
        CssClass="table table-bordered table-striped table-hover">
    <Columns>
        <asp:TemplateField HeaderText="S/N">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Student Name" />
        <asp:BoundField DataField="MatricNo" HeaderText="Matric Number" />
        <asp:BoundField DataField="Department" HeaderText="Department" />
        <asp:BoundField DataField="CheckInDate" HeaderText="Check-In Date" />
        <asp:ButtonField ButtonType="Button" Text="Unbook" CommandName="UnbookStudent" />
    </Columns>
</asp:GridView>
    <asp:Button ID="btnBackToRooms" runat="server" Text="Back to Rooms" OnClick="btnBackToRooms_Click" CssClass="btn btn-secondary" CommandName="backToRooms"/>
</asp:Content>
