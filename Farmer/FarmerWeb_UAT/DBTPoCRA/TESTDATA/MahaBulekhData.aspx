<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MahaBulekhData.aspx.cs" Inherits="DBTPoCRA.TESTDATA.MahaBulekhData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../assets/css/app.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="display:none;">

            <asp:FileUpload ID="FileUpload1" runat="server" />

            <asp:Button ID="btnUpload" runat="server" Text="Upload"
                OnClick="btnUpload_Click" />

            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />

            <br />

            <asp:Label ID="Label1" runat="server" Text="Has Header ?" />

            <asp:RadioButtonList ID="rbHDR" runat="server">

                <asp:ListItem Text="Yes" Value="Yes" Selected="True">

                </asp:ListItem>

                <asp:ListItem Text="No" Value="No"></asp:ListItem>

            </asp:RadioButtonList>

            <asp:GridView ID="GridView1" runat="server"
                AllowPaging="false">
            </asp:GridView>

            <br />
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" />
            <br />

            <br />

        </div>


        <div style="font-family: Calibri; font-size: medium; width: 100%; background-color:white!important;"> 
        <asp:Repeater ID="rptCustomers" runat="server" OnItemDataBound="OnItemDataBound">
            <HeaderTemplate>
                <br />
                <h3 style="width:100%;" class="btn btn-info">
                    Mahabhulekh Data of Village :: Anvi (530114)<br />
                    No of Registrations : 158 <br />
                    No of Farmers with Land : 86 <br />
                    No of Farmers data avilable on Mahabhulekh : 25 <br />
                    No of Farmers Data not Found on Mahabhulekh : 61 <br />
                </h3>
            </HeaderTemplate>
            <ItemTemplate>
                <table class="table table-danger" cellspacing="0" rules="all" border="1">
                    <tr>

                        <th scope="col" style="width: 150px">Name
                        </th>
                        <th scope="col" style="width: 150px">Sub-Division
                        </th>
                        <th scope="col" style="width: 150px">Taluka
                        </th>
                        <th scope="col" style="width: 150px">Village
                        </th>
                        <th scope="col" style="width: 150px">Village Code
                        </th>
                    </tr>
                    <tr>
                        <td>
                           <b>   <asp:Label ID="lblContactName" runat="server" Text='<%# Eval("RegisterName") %>' /></b>
                        </td>
                        <td>
                            <asp:Label ID="lblCity" runat="server" Text='<%# Eval("Subdivision") %>' />
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Taluka") %>' />
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("VillageName") %>' />
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("VillageCode") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">

                            <asp:Panel ID="pnlOrders" runat="server">
                                <asp:Repeater ID="rptOrders" runat="server">
                                    <HeaderTemplate>
                                        <table class="table table-primary" style="width:100%;" cellspacing="0" rules="all" border="1">
                                            <tr>
                                                <th scope="col" style="width: 150px">Owner

                                                </th>
                                                <th scope="col" style="width: 150px">khata Type
                                                </th>
                                                <th scope="col" style="width: 150px">Survey Number
                                                </th>
                                                <th scope="col" style="width: 150px">khata Number
                                                </th>
                                                <th scope="col" style="width: 150px">Culti Area
                                                </th>
                                                <th scope="col" style="width: 150px">Non Culti Area
                                                </th>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <tr>
                                            <td>
                                              <asp:Label ID="lblOrderId" runat="server" Text='<%# Eval("owner") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblOrderDate" runat="server" Text='<%# Eval("khata_description") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("survey_number") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("khata_no") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("culti_area_in_khata") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("non_culti_area_in_khata") %>' />
                                            </td>

                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("ErrorsLog") %>' />
                                            </td>
                                        </tr>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <hr />


                                <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>
                                        <table class="table table-success" cellspacing="0" rules="all" border="1">
                                            <tr>

                                                <th scope="col" style="width: 150px">Survey Number
                                                </th>
                                                <th scope="col" style="width: 150px">khata Number
                                                </th>
                                                <th scope="col" style="width: 150px">Area Used For Crop
                                                </th>
                                                <th scope="col" style="width: 150px">Year
                                                </th>
                                                <th scope="col" style="width: 150px">Season
                                                </th>
                                                <th scope="col" style="width: 150px">Crop
                                                </th>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <tr>

                                            <td>
                                                <asp:Label ID="Label15" runat="server" Text='<%# Eval("survey_number") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label61" runat="server" Text='<%# Eval("khata_no") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label17" runat="server" Text='<%# Eval("AreaUsedForCrop") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label18" runat="server" Text='<%# Eval("year_culti") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblOrderId1" runat="server" Text='<%# Eval("season_name") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblOrderDate1" runat="server" Text='<%# Eval("crop_name") %>' />
                                            </td>
                                        </tr>

                                         <tr>
                                            <td colspan="6">
                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("ErrorsLog") %>' />
                                            </td>
                                        </tr>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                            <asp:HiddenField ID="hfCustomerId" runat="server" Value='<%# Eval("RegistrationID") %>' />
                        </td>

                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
               
            </FooterTemplate>
            <SeparatorTemplate>
                <div style="height:80px">
                    <hr />
                </div>
            </SeparatorTemplate>
        </asp:Repeater>
        </div>
    </form>
</body>
</html>
