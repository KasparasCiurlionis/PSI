<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Insertion.aspx.cs" Inherits="WebProject.Insertion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section id="main-content">
        <section id="wrapper">
            <div class="row">
                <div class="col-lg-12">
                    <section class="panel">
                        <header class="panel-heading">
                            <div class="col-md-4 col-md-offset-4">
                                <h1>Update the data with Insertion</h1>
                            </div>
                            <div class="col-md-4 col-md-offset-6">
                                <h1>Enter Data Manually</h1>
                                <div>
                                    Enter Data to...
                                    <asp:Label Text="Gatve kazkokia" runat="server" ID="ManualGasStation" />
                                    in
                                    <asp:Label Text="Circle k" runat="server" ID="ManualLocation" />
                                </div>

                            </div>
                        </header>


                        <div class="panel-body">
                            <div class="row">
                            </div>
                            <div class="col-md-4 col-md-offset-1">
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

                                <div>
                                    <asp:Label Text="Choose how many gas types this gas station has:" runat="server" />
                                    <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control input-sm" Enabled="true" ID="GasTypeCount" OnSelectedIndexChanged="GasTypeCountEntered">
                                    </asp:DropDownList>
                                </div>

                                <!-- Gas types and their prices-->
                                <div>
                                    <asp:Label ID="Label2" runat="server" Visible="false" style="color:red;" Text="Sorry, the data entered should be of x,xxx format"></asp:Label>
                                        </div>

                                <div>
                                    <asp:DropDownList ID="GasType1" runat="server" Visible=false></asp:DropDownList>

                                    <asp:TextBox ID="GasPrice1" runat="server" Visible=false></asp:TextBox>
                                </div>

                                <div>
                                    <asp:DropDownList ID="GasType2" runat="server" Visible=false></asp:DropDownList>

                                    <asp:TextBox ID="GasPrice2" runat="server" Visible=false></asp:TextBox>
                                </div>

                                <div>
                                    <asp:DropDownList ID="GasType3" runat="server" Visible=false></asp:DropDownList>

                                    <asp:TextBox ID="GasPrice3" runat="server" Visible=false></asp:TextBox>
                                </div>

                                <div>
                                    <asp:DropDownList ID="GasType4" runat="server" Visible=false></asp:DropDownList>

                                    <asp:TextBox ID="GasPrice4" runat="server" Visible=false></asp:TextBox>
                                </div>

                                <div>
                                    <asp:DropDownList ID="GasType5" runat="server" Visible=false></asp:DropDownList>

                                    <asp:TextBox ID="GasPrice5" runat="server" Visible=false></asp:TextBox>
                                </div>

                                <div class="col-md-4 col-md-offset-2">
                                    <div class="form-group">
                                        <asp:Label Text="Insert a photo" runat="server" /><asp:FileUpload runat="server" ID="FileHolder"></asp:FileUpload>
                                    </div>
                                </div>


                            </div>
                        </div>


                        <div class="row">
                            <div class="col-md8 col-md-off">
                                <asp:Button Text="Refresh" ID="btnupdate" CssClass="btn btn-primary" Width="200" runat="server" OnClick="btnupdate_Click" />
                                <asp:Button Text="Save" ID="btnsave" CssClass="btn btn-primary" Width="200" runat="server" OnClick="Btnsave_Click" />

                            </div>
                        </div>





                    </section>
                </div>
            </div>


        </section>



    </section>


</asp:Content>
