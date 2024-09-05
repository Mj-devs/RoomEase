<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageRooms.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System.ManageRooms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <header class="d-flex justify-content-between align-items-center">
    <div>
        <h2 id="title">Rooms List</h2>
    </div>
        <div class="d-flex">
    <asp:TextBox ID="txtSearchRoom" runat="server" placeholder="Search by Room Number" CssClass="form-control"></asp:TextBox>
    <asp:Button ID="btnSearchRoom" runat="server" Text="Search" OnClick="btnSearchRoom_Click" CssClass="btn btn-primary ms-2" /><br />
        </div>
    </header>
    <br />

    <asp:Label ID="lbluser" Text="" runat="server" CssClass="text-primary" /><br />

    <!--Show rooms in grid view-->
    <asp:GridView ID="RoomsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="RoomId"
    OnRowEditing="RoomsGridView_RowEditing" OnRowCancelingEdit="RoomsGridView_RowCancelingEdit"
    OnRowUpdating="RoomsGridView_RowUpdating" OnRowDeleting="RoomsGridView_RowDeleting"
    OnRowDataBound="RoomsGridView_RowDataBound"
    CssClass="table table-bordered table-striped table-hover">
    <Columns>
            <asp:TemplateField HeaderText="S/N" ItemStyle-Width="50px">
    <ItemTemplate>
        <%# Container.DataItemIndex + 1 %>
    </ItemTemplate>
    </asp:TemplateField>
        <asp:TemplateField HeaderText="Room Number" SortExpression="RoomNumber">
            <ItemTemplate>
                <asp:LinkButton ID="lnkRoomNumber" runat="server" Text='<%# Eval("RoomNumber") %>' 
                    CommandArgument='<%# Eval("RoomId") %>' OnClick="lnkRoomNumber_Click" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Hostel">
            <ItemTemplate>
                <asp:Label ID="lblHostelName" runat="server" Text='<%# Eval("Hostel.HostelName") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="ddlHostel" runat="server">
                </asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="roomType" HeaderText="Room Type" SortExpression="roomType" />
        <asp:BoundField DataField="BedSpaces" HeaderText="Total Bed Spaces" SortExpression="BedSpaces" />

        <asp:BoundField DataField="AvailableBedSpaces" HeaderText="Available Bed Spaces" 
            SortExpression="AvailableBedSpaces" />
        <asp:CheckBoxField DataField="IsAvailable" HeaderText="Available" />
        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
    </Columns>
    </asp:GridView>
    
    <!--Buttons to either Add New Room or to Book Room-->
    <asp:Button ID="btnAddRoom" runat="server" Text="Add New Room" OnClick="btnAddRoom_Click" CssClass="btn btn-outline-primary" />
    <asp:Button ID="btnBookRoom" runat="server" Text="Book Room" OnClick="btnBookRoom_Click" CssClass="btn btn-primary ms-2" />

</asp:Content>
