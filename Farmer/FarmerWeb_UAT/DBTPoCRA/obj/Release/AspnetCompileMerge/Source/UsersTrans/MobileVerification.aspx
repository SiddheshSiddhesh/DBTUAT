<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MobileVerification.aspx.cs" Inherits="DBTPoCRA.UsersTrans.MobileVerification" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="../assets/css/app.css" rel="stylesheet" />
    <script src="../assets/js/jquery.min.js"></script>
    <script src="../assets/js/sweetalert.min.js"></script>
    <link href="../assets/css/sweetalert.css" rel="stylesheet" />
    <script>
        function reenable(i) {
            document.getElementById(i).removeAttribute('disabled');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="app" class="table-responsive">
            <div class="container-fluid  my-1">
                <div class="row">
                    <div class="col-md-12" style="text-align: center">
                        <h1>मोबाइल नंबर सत्यापन फॉर्म</h1>
                    </div>

                </div>



                <table class="table" style="">
                    
                    <tr>
                        <td style="width: 40%">Registered Mobile No<br />
                            ( नोंदणीकृत मोबाइल क्रमांक ) :</td>
                        <td style="width: 40%">
                            <asp:Literal ID="Literal1" Text="XXXXXXXXXX" runat="server"></asp:Literal>
                        </td>
                        <td style="width: 20%"></td>

                    </tr>
                    <tr>
                        <td>If want to Change your Register Mobile No.<br />
                            (आपला नोंदणीकृत मोबाईल क्रमांक बदलू इच्छित असल्यास )</td>
                        <td>
                            <asp:CheckBox ID="CheckBox1" CssClass="form-control" Width="70%" runat="server" Text="Click here ( इथे क्लिक करा)" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        </td>
                        <td></td>

                    </tr>
                    <tr id="DivNew" runat="server" visible="false">
                        <td></td>
                        <td>
                            <asp:TextBox ID="txtMobile1" MaxLength="10" CssClass="form-control" placeholder="Please fill your new mobile Number (कृपया आपला नवीन मोबाइल नंबर भरा)" runat="server" Width="90%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="badge badge-danger badge-mini2"
                                runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtMobile1"></asp:RequiredFieldValidator>


                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                ErrorMessage="Invalid Mobile Number" CssClass="icon-cloud-error error alert-danger" ControlToValidate="txtMobile1" ValidationExpression="\d{10}" SetFocusOnError="True" Display="Dynamic"></asp:RegularExpressionValidator>

                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers"
                                TargetControlID="txtMobile1" />
                        </td>
                        <td>
                            <asp:Literal ID="Literal2" runat="server" Text="कृपया आपला नवीन मोबाइल नंबर भरा"></asp:Literal>
                        </td>

                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSendOTP" runat="server" CssClass="btn btn-success" Text="Get OTP for Mobile Number Verification" OnClick="btnSendOTP_Click" />
                        </td>
                        <td></td>

                    </tr>
                    <tr id="div1" runat="server" visible="false">
                        <td>
                            <asp:Literal ID="Literal3" runat="server" Text="Please fill OTP that You have Received on Your Registered Mobile No. <br> कृपया आपण आपल्या नोंदणीकृत मोबाइल नंबरवर प्राप्त केलेला ओटीपी भरा."></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOTP" MaxLength="6" CssClass="form-control" runat="server" Width="90%"></asp:TextBox>
                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                TargetControlID="txtOTP" />
                        </td>

                        <td></td>
                    </tr>
                    <tr id="div2" runat="server" visible="false">
                        <td></td>
                        <td>
                            <asp:Button ID="btnVerify" runat="server" CssClass="btn btn-success" Text="Verify OTP for Mobile Number" OnClick="btnVerify_Click" />
                        </td>
                        <td></td>

                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>

                    </tr>
                </table>

            </div>
        </div>
    </form>
</body>
</html>
