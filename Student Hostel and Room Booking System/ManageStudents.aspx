<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageStudents.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
    <asp:GridView ID="StudentsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="StudentId"
    OnRowEditing="StudentsGridView_RowEditing" OnRowCancelingEdit="StudentsGridView_RowCancelingEdit"
    OnRowUpdating="StudentsGridView_RowUpdating" OnRowDeleting="StudentsGridView_RowDeleting">
    <Columns>
        <asp:BoundField DataField="StudentId" HeaderText="ID" ReadOnly="True" />
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" />
        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
    </Columns>
    </asp:GridView>
    <asp:Button ID="btnAddStudent" runat="server" Text="Add New Student" OnClick="btnAddStudent_Click" />

        <asp:TextBox ID="txtSearchStudent" runat="server" placeholder="Search by Name or Email"></asp:TextBox>
        <asp:Button ID="btnSearchStudent" runat="server" Text="Search" OnClick="btnSearchStudent_Click" /><br />
        <asp:GridView ID="GridView1" runat="server" />

    </main>

</asp:Content>
