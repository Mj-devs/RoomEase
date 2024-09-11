<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewHostelDetails.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System.Hostel_files.ViewHostelDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Rooms in Selected Hostel</h2>
    <h4 class="text-secondary">Hostel: <asp:Label ID="lblHostelName" runat="server"></asp:Label></h4>
            

    <asp:GridView ID="RoomsGridView" runat="server" AutoGenerateColumns="False" 
         OnRowEditing="RoomsGridView_RowEditing" OnRowCancelingEdit="RoomsGridView_RowCancelingEdit"
         OnRowUpdating="RoomsGridView_RowUpdating" OnRowDeleting="RoomsGridView_RowDeleting"
         OnRowCommand="RoomsGridView_RowCommand"
        CssClass="table table-bordered table-striped table-hover">
    <Columns>
        <asp:BoundField DataField="RoomNumber" HeaderText="Room Number" />
        <asp:BoundField DataField="BedSpaces" HeaderText="Total Bed Spaces" />
        <asp:BoundField DataField="AvailableBedSpaces" HeaderText="Available Bed Spaces" />
        <asp:BoundField DataField="IsAvailable" HeaderText="Availability" DataFormatString="{0:Yes;No}" />
        <asp:TemplateField>
        <ItemTemplate>
            <asp:LinkButton ID="btnViewDetails" runat="server" CommandName="ViewDetails" CssClass="link-primary"
                CommandArgument='<%# Eval("RoomId") %>' Text="View Details" />
        </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Label ID="lblerrormessage" runat="server" CssClass="text-danger"></asp:Label>
        <asp:Timer ID="ErrorTimer" runat="server" Interval="5000" OnTick="ErrorTimer_Tick" />
    </ContentTemplate>
    </asp:UpdatePanel>
<asp:Button ID="btnBackToHostels" runat="server" Text="Back to Hostels" OnClick="btnBackToHostels_Click" CssClass="btn btn-secondary" />

</asp:Content>
