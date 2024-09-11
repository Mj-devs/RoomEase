<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddStudents.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System.AddStudents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Register Student for the session</h2>
    <h6 class="text-primary">Register information of either a returning student or a new student(fresher) by clicking the add fresher button</h6>

    <!--Form Add Student-->
    <div>
        <asp:Label Text="Name:" runat="server" />
    <asp:TextBox ID="txtname" runat="server" placeholder="Name" CssClass="form-control"></asp:TextBox> <br />
    <asp:Panel ID="pnlTextBoxContainer" runat="server">
        <asp:Label Text="Matric Number:" runat="server" />
        <asp:TextBox ID="txtmatricno" runat="server" placeholder="Matric Number" CssClass="form-control" /> <br />
    </asp:Panel>
        <asp:Label Text="Department:" runat="server" />
        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"></asp:DropDownList><br />
        <asp:Label Text="Date of Birth:" runat="server" />
    <asp:TextBox ID="txtdob" runat="server" placeholder="Date of Birth" CssClass="form-control"></asp:TextBox> <br />
        <asp:Label Text="Select Gender:" runat="server" />
        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
            <asp:ListItem Value=""></asp:ListItem>
            <asp:ListItem Value="Male">Male</asp:ListItem>
            <asp:ListItem Value="Female">Female</asp:ListItem>
        </asp:DropDownList><br />
        <asp:Label Text="Phone Number:" runat="server" />
    <asp:TextBox ID="txtphonenumber" runat="server" placeholder="Phone Number" CssClass="form-control"></asp:TextBox> <br />
        <asp:Label Text="Level:" runat="server" />
    <asp:TextBox ID="txtlevel" runat="server" placeholder="Level" CssClass="form-control"></asp:TextBox> <br />
    <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" CssClass="btn btn-primary" />
    </div>
    <br />
     <div>
            <!-- Panel for hidden fields -->
            <asp:Panel ID="pnlHiddenFields" runat="server" CssClass="form-group" Visible="false">
         <div>
        <asp:Label Text="Jamb Reg No:" runat="server" />
            <asp:TextBox ID="txtjambno" runat="server" placeholder="Jamb Registration No." CssClass="form-control"></asp:TextBox><br />
            
             <!-- Customvalidator to check matric no same-->
            <asp:CustomValidator 
                ID="cvMatricNo" 
                runat="server" 
                ControlToValidate="txtmatricno" 
                OnServerValidate="cvMatricNo_ServerValidate" 
                ErrorMessage="Matriculation number already exists." 
                Display="Dynamic"
                ValidationGroup="SaveGroup"
                ForeColor="Red">
            </asp:CustomValidator>
            
            <!-- LinkButton to toggle visibility -->

            <!--Save button-->
            <asp:Button ID="btnsavefresher" runat="server"  Text="Save" 
                        CssClass=" btn btn-primary h-25 w-25" 
                        OnClick="btnsavefresher_Click" ValidationGroup="SaveGroup" />
                </div>
            </asp:Panel>

         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" CssClass="error-message" Visible="False"></asp:Label>
                <asp:Timer ID="ErrorTimer" runat="server" Interval="5000" OnTick="ErrorTimer_Tick" />
            </ContentTemplate>
        </asp:UpdatePanel>
             <br />
            <asp:LinkButton ID="lbToggleFields" runat="server" Text="Add Fresher +" CausesValidation="false" OnClick="lbToggleFields_Click" CssClass="btn btn-primary" />
         <br />
        </div>
        <br />
    <asp:Button ID="btnBackToStudents" runat="server" Text="Back to Student's List" OnClick="btnBackToStudents_Click" CssClass="btn btn-outline-secondary" />
</asp:Content>
