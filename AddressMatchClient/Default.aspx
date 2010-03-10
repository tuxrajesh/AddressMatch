<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <p class="heading">Validate Postal Address</p>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <p class="intro">Use this page to make sure that the postal address you have is reachable and also in a standard USPS recommended format.</p>
    <hr />
    <asp:Table ID="tableForm" runat="server" cellspacing="0" cellpadding="4" CssClass="table">
        <asp:TableRow ID="TableRow0" runat="server">
            <asp:TableCell id="cell1" runat="server" CssClass="frmtext"><asp:Label ID="lblAddress1" runat="server" Text="Address Line 1 :" /></asp:TableCell><asp:TableCell id="cell2" runat="server" CssClass="frmInput"><asp:TextBox ID="txtAddress1" runat="server" /></asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow1" runat="server">
            <asp:TableCell id="TableCell1" runat="server" CssClass="frmtext"><asp:Label ID="lblAddress2" runat="server" Text="Address Line 2 :" /></asp:TableCell><asp:TableCell id="TableCell2" runat="server" CssClass="frmInput"><asp:TextBox ID="txtAddress2" runat="server" /></asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow2" runat="server">
            <asp:TableCell id="TableCell3" runat="server" CssClass="frmtext"><asp:Label ID="lblCity" runat="server" Text="City :" /></asp:TableCell><asp:TableCell id="TableCell4" runat="server" CssClass="frmInput"><asp:TextBox ID="txtCity" runat="server" /></asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow3" runat="server">
            <asp:TableCell id="TableCell5" runat="server" CssClass="frmtext"><asp:Label ID="lblState"  runat="server" Text="State :" /></asp:TableCell><asp:TableCell id="TableCell6" runat="server" CssClass="frmInput"><asp:TextBox ID="txtState" runat="server" /></asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow4" runat="server">
            <asp:TableCell id="TableCell7" runat="server" CssClass="frmtext"><asp:Label ID="lblZip5" runat="server" Text="Zip Code :" /></asp:TableCell><asp:TableCell id="TableCell8" runat="server" CssClass="frmInput"><asp:TextBox ID="txtZip5" runat="server" /></asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow5" runat="server">
            <asp:TableCell id="TableCell9" runat="server" CssClass="frmtext"><asp:Label ID="lblZip4" runat="server" Text="Zip + 4 :" /></asp:TableCell><asp:TableCell id="TableCell10" runat="server" CssClass="frmInput"><asp:TextBox ID="txtZip4" runat="server" /></asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow6" runat="server">
            <asp:TableCell id="TableCell11" runat="server" CssClass="frmtext" ColumnSpan="2"><asp:Button ID="btnValidate" runat="server" CssClass="button" text="Validate Address" OnClick="btnValidate_Click" /></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <hr />
    <h3>Matching Addresses:</h3>
    <asp:DataGrid ID="dgResult" runat="server" CssClass="table"
        CellPadding="4" GridLines="None" AutoGenerateColumns="False" 
        Font-Bold="False" Font-Italic="False" Font-Overline="False" 
        Font-Strikeout="False" Font-Underline="False"><AlternatingItemStyle 
            BackColor="White" /><EditItemStyle 
            BackColor="#7C6F57" /><FooterStyle BackColor="#1C5E55" Font-Bold="True" 
            ForeColor="White" /><HeaderStyle BackColor="#000D26" Font-Bold="True" 
            Font-Names="Verdana" Font-Size="Medium" ForeColor="White"
            HorizontalAlign="Center" VerticalAlign="Middle" Font-Italic="False" 
            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" /><ItemStyle 
            BackColor="#E3EAEB" /><PagerStyle BackColor="#666666" 
            ForeColor="White" HorizontalAlign="Center" /><SelectedItemStyle 
            BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" /><Columns>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelection" runat="server" OnCheckedChanged="OnCheckChanged" AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="BuildingOrFirmName" HeaderText="Building/Firm Name" />
                <asp:BoundColumn DataField="AddressLine1" HeaderText="Address Line 1" />
                <asp:BoundColumn DataField="AddressLine2" HeaderText="Address Line 2" />
                <asp:BoundColumn DataField="City" HeaderText="City" />
                <asp:BoundColumn DataField="State" HeaderText="State Code" />
                <asp:BoundColumn DataField="Zip5" HeaderText="Zip Code" />
                <asp:BoundColumn DataField="Zip4" HeaderText="Zip + 4 " />
            </Columns>
    </asp:DataGrid>
</asp:Content>