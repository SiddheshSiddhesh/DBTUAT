<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateLandCerti.aspx.cs" Inherits="DBTPoCRA.UsersTrans.UpdateLandCerti" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../assets/css/app.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <center> 
            <table style="width: 100%; table-layout: fixed ; max-width:550px;">
                <tr>
                    <td style="width:50%">Browse New Document
                   
                    </td>
                    <td style="width:50%">&nbsp;</td>
                </tr>
                <tr>
                    <td>

                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:FileUpload ID="File712" CssClass="custom-file" Style="margin-top: 7px" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnUpload" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:Button ID="btnUpload" CssClass="btn btn-primary" runat="server" OnClick="btnUpload_Click" Text="Upload" />
                    </td>
                </tr>
            </table>
                </center>

        </div>
    </form>
</body>
</html>
