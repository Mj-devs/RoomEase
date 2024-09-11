<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageHostels.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System.Hostel_files.ManageHostels" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <header class="d-flex justify-content-between align-items-center">
        <div>
            <h2 class="text-primary">Setup Hostels</h2>
            <h5 class="text-secondary">See alread inputed hostels and add hostels</h5>
        </div>
        <div class="d-flex">
            <asp:TextBox ID="txtSearchHostel" runat="server" placeholder="Search by Name or Gender" CssClass="form-control"></asp:TextBox>
            <asp:Button ID="btnSearchHostel" runat="server" Text="Search" OnClick="btnSearchStudent_Click" CssClass="btn btn-outline-primary rounded-2 mx-1" /><br />
        </div>
    </header>
    <asp:GridView ID="HostelGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="HostelId"
    OnRowEditing="HostelGridView_RowEditing" OnRowUpdating="HostelGridView_RowUpdating" 
    OnRowDeleting="HostelGridView_RowDeleting" OnRowCancelingEdit="HostelGridView_RowCancelingEdit"
    OnRowCommand="HostelGridView_RowCommand" CssClass="table table-bordered table-striped table-hover">
    <Columns>
        <asp:TemplateField HeaderText="S/N">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="HostelName" HeaderText="Hostel Name" />
        <asp:BoundField DataField="Gender" HeaderText="Gender" />
        <asp:BoundField DataField="TotalRooms" HeaderText="Total No. of Rooms" />
        <asp:BoundField DataField="AvailableRooms" HeaderText="Available Rooms" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnViewRooms" runat="server" Text="View Rooms" CssClass="btn btn-outline-secondary" 
                    CommandArgument='<%# Eval("HostelId") %>' CommandName="ViewRooms" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />

    </Columns>
</asp:GridView>

    <asp:Label ID="lblMessage" Text="" runat="server" />
<asp:Button ID="btnAddHostel" runat="server" Text="Add New Hostel" OnClick="btnAddHostel_Click" CssClass="btn btn-primary" />

</asp:Content>
