<%@ Page Title="" Language="C#" MasterPageFile="~/UsersTrans/UserMaster.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="DBTPoCRA.UsersTrans.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" class="table" style="table-layout: fixed">
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Literal ID="Label1" runat="server"></asp:Literal>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
               Old Password/जुना पासवर्ड:
            </td>
            <td style="text-align: left">
                <asp:TextBox ID="TextBox1" runat="server" CssClass="mytext" MaxLength="45" TextMode="Password"></asp:TextBox>
            </td>
            <td style="text-align: left">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                    Display="Dynamic" ErrorMessage="Fill Old  Password" CssClass="error" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
               New Password/नवीन पासवर्ड :
            </td>
            <td style="text-align: left">
                <asp:TextBox ID="TextBox2" runat="server" CssClass="mytext" MaxLength="20" TextMode="Password"></asp:TextBox>
            </td>
            <td style="text-align: left">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="error" runat="server" ControlToValidate="TextBox2"
                    Display="Dynamic" ErrorMessage="Fill Password" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
               Re-Type Password/पासवर्ड पुन्हा टाईप करा:
            </td>
            <td style="text-align: left">
                <asp:TextBox ID="TextBox3" runat="server" CssClass="mytext" MaxLength="20" TextMode="Password"></asp:TextBox>
            </td>
            <td style="text-align: left">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="error" runat="server" ControlToValidate="TextBox3"
                    Display="Dynamic" ErrorMessage="Re- Fill Password" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox2"
                    ControlToValidate="TextBox3"  Display="Dynamic" CssClass="error" ErrorMessage="Password Mismatch"
                    SetFocusOnError="True"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="text-align: left">
                <asp:Button ID="Button1" runat="server" CssClass="btn-danger" OnClick="Button1_Click"
                    Text="Change" />&nbsp;
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
