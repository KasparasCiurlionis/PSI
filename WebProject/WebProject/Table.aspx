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
        
     

  
     

</asp:Content>
