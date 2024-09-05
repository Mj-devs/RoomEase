<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageStudents.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <header class="d-flex justify-content-between align-items-center">
        <div>
            <h2 id="title">Student's List</h2>
            <asp:Label ID="lblMessage" Text="" runat="server" />
        </div>

    <!--Search TextBox and Button-->
    <div class="d-flex">
        <asp:TextBox ID="txtSearchStudent" runat="server" placeholder="Search by Name or Matric No." CssClass="form-control"></asp:TextBox>
        <asp:Button ID="btnSearchStudent" runat="server" Text="Search" OnClick="btnSearchStudent_Click" CssClass="btn btn-outline-primary rounded-2 mx-1" /><br />
    </div>
    </header>
    <br />

    <div>
        <!--GridView-->
    <asp:GridView ID="StudentsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="StudentId"
    OnRowEditing="StudentsGridView_RowEditing" OnRowCancelingEdit="StudentsGridView_RowCancelingEdit"
    OnRowUpdating="StudentsGridView_RowUpdating" OnRowDeleting="StudentsGridView_RowDeleting" CssClass="table table-bordered table-striped table-hover">
        <Columns>
            <asp:TemplateField HeaderText="S/N" ItemStyle-Width="50px">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="MatricNo" HeaderText="Matric Number" />
            <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" />
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
        <!--Button Add Student-->
    <asp:Button ID="btnAddStudent" runat="server" Text="Add New Student" OnClick="btnAddStudent_Click" CssClass="btn btn-primary" />
    </div>

</asp:Content>
