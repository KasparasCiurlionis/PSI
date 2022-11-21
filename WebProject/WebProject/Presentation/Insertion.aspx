<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Insertion.aspx.cs" Inherits="WebProject.Insertion" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <hr />

    <section>
        <header>
            <h1 style="text-align: left">Update the data with Insertion</h1>
        </header>
    </section>
    <section class="panel">
        <h1 style="text-align: left">Enter Data Manually</h1>
        <div style="text-align: left">
            Enter Data to...
            <b>
            <asp:Label Text="Gatve kazkokia" runat="server" ID="ManualGasStation" /></b>
            in <b><asp:Label Text="Circle k" runat="server" ID="ManualLocation" /></b>
        </div>
    </section>


    <section>
        <div class="form-group">
            <asp:Label Text="Choose Gas Station" runat="server" />
            <!-- <asp:TextBox runat ="server" Enabled="true" CssClass="form-control input-sm" placeholder="gas station Name" />-->
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control input-sm" Enabled="True" ID="GasStation" OnSelectedIndexChanged="GasStationSelected">
            </asp:DropDownList>
        </div>

        <div class="form-group">

            <asp:Label Text="Select Location" runat="server" ID="Label1" />
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control input-sm" Enabled="True" ID="Location" OnSelectedIndexChanged="GasStationLocationSelected">
            </asp:DropDownList>

        </div>


        <!-- Gas prices-->
        <div>
            <asp:Label ID="Label2" runat="server" Visible="false" Style="color: red;" Text="Sorry, the data entered should be of x,xxx format"></asp:Label>
        </div>

        <div>
            <asp:Label ID="GasLabel1" runat="server" Text=""></asp:Label>
            <asp:TextBox ID="GasPrice1" runat="server" Visible="true"></asp:TextBox>
        </div>

        <div>
            <asp:Label ID="GasLabel2" runat="server" Text=""></asp:Label>
            <asp:TextBox ID="GasPrice2" runat="server" Visible="true"></asp:TextBox>
        </div>

        <div>
            <asp:Label ID="GasLabel3" runat="server" Text=""></asp:Label>
            <asp:TextBox ID="GasPrice3" runat="server" Visible="true"></asp:TextBox>
        </div>

        <div>
            <asp:Label ID="GasLabel4" runat="server" Text=""></asp:Label>
            <asp:TextBox ID="GasPrice4" runat="server" Visible="true"></asp:TextBox>
        </div>

        <div class="col">
            <div class="form-group">
                <asp:Label Text="Insert a photo" runat="server" /><asp:FileUpload runat="server" ID="FileHolder"></asp:FileUpload>
                <asp:Button Text="Upload" ID="btnUpload" CssClass="btn btn-primary" Width="200" runat="server" OnClick="UploadFile" />
            </div>
        </div>
    </section>

    <div></div>
    <div class="form-group">
            <asp:TextBox ID ="AutoTextBox1" Visible="false" CssClass="form-control input-sm" runat="server" BackColor="#CCCCCC" />
            <asp:Label ID ="AutoTextLabel1" Visible="false" Text="" runat="server" />
        </div>
    <div class="form-group">
            <asp:TextBox ID ="AutoTextBox2" Visible="false" CssClass="form-control input-sm" runat="server" BackColor="#CCCCCC" />
            <asp:Label ID ="AutoTextLabel2" Visible="false" Text="" runat="server" />
        </div>
    <div class="form-group">
            <asp:TextBox ID ="AutoTextBox3" Visible="false" CssClass="form-control input-sm" runat="server" BackColor="#CCCCCC" />
            <asp:Label ID ="AutoTextLabel3" Visible="false" Text="" runat="server" />
        </div>
    <div class="form-group">
            <asp:TextBox ID ="AutoTextBox4" Visible="false" CssClass="form-control input-sm" runat="server" BackColor="#CCCCCC" />
            <asp:Label ID ="AutoTextLabel4" Visible="false" Text="" runat="server" />
        </div>


    <hr />


    <section>
        <asp:Button Text="Refresh" ID="btnupdate" CssClass="btn btn-primary" Width="200" runat="server" OnClick="btnupdate_Click" />
        <asp:Button Text="Save" ID="btnsave" CssClass="btn btn-primary" Width="200" runat="server" OnClick="Btnsave_Click" />
        <asp:Button Text="Discard" ID="btndiscard" CssClass="btn btn-primary" Width="200" runat="server" Visible="false" OnClick="Btndiscard_Click" BackColor ="#FF5050" />
    </section>

    <div>
            <asp:Label ID ="testigo" Visible="false" Text="saved sucesfully" runat="server" />
     </div>

</asp:Content>
