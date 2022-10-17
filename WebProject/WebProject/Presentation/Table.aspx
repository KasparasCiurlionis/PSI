<%@ Page Title="Table" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Table.aspx.cs" Inherits="WebProject.Table" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  

      <h3>HtmlTable Example</h3>
       <style>
           table, tr, td{
               border: 1px solid black;
              
           }
       </style> 
      <table id="Table1" 
        style="border: 1px solid black"
       runat="server">
        
       </table>
        
     <section>
        <div class="form-group">
            <asp:Label Text="Choose Gas Station" runat="server" />
            <!-- <asp:TextBox runat ="server" Enabled="true" CssClass="form-control input-sm" placeholder="gas station Name" />-->
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control input-sm" Enabled="True" ID="GasStation" OnSelectedIndexChanged="GasStationSelected">
            </asp:DropDownList>
        </div>
    </section>

  
     

</asp:Content>
