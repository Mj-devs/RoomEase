<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Student_Hostel_and_Room_Booking_System.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <h3>Your contact page.</h3>
        <address>
            Afe Babalola University<br />
            College of Sciences<br />
            <abbr title="Phone">P: 08131976848</abbr>
        </address>

        <address>
            <strong>Support:</strong>   <a href="mailto:developersholotan@gmail.com">developersholotan@gmail.com</a><br />
        </address>
    </main>
</asp:Content>
