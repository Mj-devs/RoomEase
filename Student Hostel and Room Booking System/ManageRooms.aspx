<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageRooms.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System.ManageRooms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="RoomsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="RoomId"
    OnRowEditing="RoomsGridView_RowEditing" OnRowCancelingEdit="RoomsGridView_RowCancelingEdit"
    OnRowUpdating="RoomsGridView_RowUpdating" OnRowDeleting="RoomsGridView_RowDeleting">
    <Columns>
        <asp:BoundField DataField="RoomId" HeaderText="ID" ReadOnly="True" />
        <asp:BoundField DataField="RoomNumber" HeaderText="Room Number" />
        <asp:TemplateField HeaderText="Hostel">
            <ItemTemplate>
                <asp:Label ID="lblHostelName" runat="server" Text='<%# Eval("Hostel.HostelName") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="ddlHostel" runat="server">
                    
                </asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="RoomType" HeaderText="Room Type" />
        <asp:CheckBoxField DataField="IsAvailable" HeaderText="Available" />
        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
    </Columns>
    </asp:GridView>
    <asp:Button ID="btnAddRoom" runat="server" Text="Add New Room" OnClick="btnAddRoom_Click" />
    <asp:TextBox ID="txtSearchRoom" runat="server" placeholder="Search by Room Number"></asp:TextBox>
    <asp:Button ID="btnSearchRoom" runat="server" Text="Search" OnClick="btnSearchRoom_Click" /><br />
    

</asp:Content>
