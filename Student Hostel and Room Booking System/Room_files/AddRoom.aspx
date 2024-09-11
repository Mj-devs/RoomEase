<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddRoom.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System.AddRoom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-primary">Add Room to Hostels</h2>

    <asp:Label Text="Room Number:" runat="server" />
    <asp:TextBox ID="txtRoomNumber" runat="server" placeholder="Room Number" CssClass="form-control"></asp:TextBox><br />
    <asp:Label Text="Select Hostel:" runat="server" />
    <asp:DropDownList ID="ddlHostels" runat="server" CssClass="form-control"></asp:DropDownList><br />
    <p><asp:Label Text="Check Available" runat="server" /></p>
    <asp:CheckBox ID="chkIsAvailable" runat="server" Text="Available"></asp:CheckBox><br />
    <br />
    <asp:TextBox ID="txtRoomType" runat="server" CssClass="form-control" placeholder="Room Type"></asp:TextBox><br />
    <asp:TextBox ID="txtBedSpaces" runat="server" CssClass="form-control" placeholder="Bed Spaces"></asp:TextBox><br />
   
    <asp:CustomValidator 
            ID="cvRoomNo" 
            runat="server" 
            ControlToValidate="txtRoomNumber" 
            OnServerValidate="cvRoomNo_ServerValidate" 
            ErrorMessage="Room has already been inputed to this Hostel" 
            Display="Dynamic"
            ValidationGroup="SaveGroup"
            ForeColor="Red">
        </asp:CustomValidator> <br />
    <asp:Button ID="btnsave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnsave_Click" ValidationGroup="SaveGroup" />

    <asp:Button ID="btnBackToRooms" runat="server" Text="Back to Rooms" OnClick="btnBackToRooms_Click" CssClass="btn btn-outline-secondary" />
    <asp:UpdatePanel ID="upltimer" runat="server">
    <ContentTemplate>
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        <asp:Timer ID="tmRedirect" runat="server" Interval="3000" OnTick="tmRedirect_Tick" Enabled="false"></asp:Timer>
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
