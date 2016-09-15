<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OCSalary.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <p>Reference for data: <a href="http://www.bizjournals.com/orlando/morning_call/2014/09/public-paychecks-search-through-orange-county.html" target="_blank">Business Journal</a></p>
<p>
    Search Name <br />
        First name: <asp:TextBox ID="txtFName" runat="server"></asp:TextBox> 
          Last name: <asp:TextBox ID="txtLName" runat="server"></asp:TextBox>
    Department: <asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox>  <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
    </p>
    </div>
        <div><asp:Label ID="lblcountrows" runat="server"></asp:Label></div>
        <asp:GridView ID="GVOCSalary" runat="server" CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" AllowSorting="true" OnSorting="GVOCSalary_Sorting" AllowPaging="true" OnPageIndexChanging="GVOCSalary_PageIndexChanging" PageSize="30">
            <AlternatingRowStyle BackColor="#DCDCDC" />

            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
        </asp:GridView>
    </form>
</body>
</html>
