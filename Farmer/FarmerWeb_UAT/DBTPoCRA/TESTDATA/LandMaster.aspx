<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandMaster.aspx.cs" Inherits="DBTPoCRA.TESTDATA.LandMaster" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../assets/css/app.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            table-layout:fixed;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width:90%">

            <table class="auto-style1">
                <tr>
                    <td>Div</td>
                    <td>
                        <asp:DropDownList ID="ddlD" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlD_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>District</td>
                    <td>
                        <asp:DropDownList ID="ddlC" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlC_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>Taluka</td>
                    <td>
                        <asp:DropDownList ID="ddlT" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlT_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnTaluka" runat="server" OnClick="btnTaluka_Click" Text="Update" />
                    </td>
                </tr>
                <tr>
                    <td>Village</td>
                    <td>
                        <asp:DropDownList ID="ddlV" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlV_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnVillage" runat="server" OnClick="btnVillage_Click" Text="Update" />
                    </td>
                </tr>
                <tr>
                    <td>Khata Numbar</td>
                    <td>
                        <asp:TextBox ID="txtKhataNo" CssClass="form-control" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Khata Numbar From DBT</td>
                    <td>
                        <asp:DropDownList ID="ddlDbt" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnGet8A" runat="server" OnClick="btnGet8A_Click" Text="Get 8A Details" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnGetVillage" runat="server" OnClick="btnGetVillage_Click" Text="Get All Village" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Label ID="Label1" Visible="false" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="2">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" Width="100%" >
                           <%-- <Columns>
                                <asp:BoundField DataField="owner" HeaderText="owner" />
                                <asp:BoundField DataField="khata_description" HeaderText="khata_description" />
                                <asp:BoundField DataField="culti_area_in_khata" HeaderText="culti_area_in_khata" />
                                <asp:BoundField DataField="non_culti_area_in_khata" HeaderText="non_culti_area_in_khata" />
                                <asp:BoundField DataField="assessment" HeaderText="assessment" />
                            </Columns>--%>
                        </asp:GridView>
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
