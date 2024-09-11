<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageStudents.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <header class="d-flex justify-content-between align-items-center">
        <div>
            <h2 id="title">Assigned Student's List</h2>
            <h5 class="text-primary">Here is the list of students which have been assigned or yet to be assigned rooms</h5>
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
    <h5 class="text-secondary">Here is the list of returning students</h5>
        <!--GridView for returning students-->
    <asp:GridView ID="StudentsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="StudentId"
    OnRowEditing="StudentsGridView_RowEditing" OnRowCancelingEdit="StudentsGridView_RowCancelingEdit"
    OnRowUpdating="StudentsGridView_RowUpdating" OnRowDeleting="StudentsGridView_RowDeleting" 
    OnRowDataBound="StudentsGridView_RowDataBound" OnRowCommand="StudentsGridView_RowCommand"
    CssClass="table table-bordered table-striped table-hover">
        <Columns>
            <asp:TemplateField HeaderText="S/N" ItemStyle-Width="50px">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="MatricNo" HeaderText="Matric Number" />
            <asp:TemplateField HeaderText="Department">
            <EditItemTemplate>
                <asp:DropDownList ID="ddlDepartment" runat="server" DataTextField="DepartmentName" DataValueField="DepartmentId"></asp:DropDownList>
            </EditItemTemplate>
            <ItemTemplate>
                <%# Eval("Department.DepartmentName") %>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Gender" HeaderText="Gender" />
            <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" />
            <asp:BoundField DataField="RoomNumber" HeaderText="Assigned Room" ReadOnly="True" ItemStyle-CssClass="gridview-cell" />
            <asp:BoundField DataField="HostelName" HeaderText="Assigned Hostel" ReadOnly="True" ItemStyle-CssClass="gridview-cell" />

            <%--<asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />--%>
        </Columns>
    </asp:GridView>

    <h5 class="text-secondary">Here is the list of new students (freshers)</h5>
    <asp:GridView ID="NewStudentsGridView" runat="server" AutoGenerateColumns="False" 
                  CssClass="table table-bordered table-striped">
    <Columns>
            <asp:TemplateField HeaderText="S/N" ItemStyle-Width="50px">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Department" HeaderText="Department" />
            <asp:BoundField DataField="JambRegNo" HeaderText="JAMB Reg No" /> 
            <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" />
            <asp:BoundField DataField="Gender" HeaderText="Gender" />
            <asp:BoundField DataField="RoomNumber" HeaderText="Assigned Room" ReadOnly="True" ItemStyle-CssClass="gridview-cell" />
            <asp:BoundField DataField="HostelName" HeaderText="Assigned Hostel" ReadOnly="True" ItemStyle-CssClass="gridview-cell" />
        </Columns>
    </asp:GridView>

        <asp:updatepanel id="updatepanel1" runat="server">
            <contenttemplate>
                <asp:label id="lblErrorMessage" runat="server" forecolor="red" cssclass="error-message" visible="false"></asp:label>
                <asp:timer id="ErrorTimer" runat="server" interval="5000" ontick="ErrorTimer_Tick" />
            </contenttemplate>
        </asp:updatepanel>

        <!--button add student-->
    <asp:Button ID="btnAddStudent" runat="server" Text="Add New Student" OnClick="btnAddStudent_Click" CssClass="btn btn-outline-primary" />
    <asp:Button ID="btnBookRoom" runat="server" Text="Assingn Room" OnClick="btnBookStudent_Click" CssClass="btn btn-primary" />
    </div>

</asp:Content>
