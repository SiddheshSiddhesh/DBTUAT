<%@ Page Language="C#" MasterPageFile="~/UsersTrans/UserMaster.Master" AutoEventWireup="true" CodeBehind="ApplicationSucess.aspx.cs" Inherits="DBTPoCRA.UsersTrans.ApplicationSucess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-group pt-4 pb-4">
        <div class="card">
            <div class="card-header">
                <strong>
                    <asp:Literal ID="Literal4" runat="server" Text="Your application has been successfully submitted."></asp:Literal>
                    <asp:Literal ID="Literal5" runat="server"></asp:Literal>
                </strong>
            </div>
            <div class="card-body text-center">

                <div class="alert-success">
                    <!--<h6 class="p-t-12">Alexander Pierce</h6>-->
                    <img class="user_avatar no-b no-p r-5" style="width: 32px; height: 32px;" src="../assets/img/basic/circle-green.png" alt="User Image">
                    <asp:Literal ID="Literal2" runat="server" Text=" You have successfully applied for "> </asp:Literal><asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    <asp:Literal ID="Literal3" runat="server" Text="scheme under Nanaji Deshmukh Krishi Sanjivani Prakalp. Your Application is pending for Approvals."> </asp:Literal>

                </div>
                <hr />
                <div class=" alert-warning">
                    <h4>
                        <asp:Literal ID="Literal6" Text="Do you want to apply for another scheme,  " runat="server"></asp:Literal> <a href="ApplicationForm.aspx">YES/हो</a>  ||   <a href="UserDashBoard.aspx">NO/नाही</a> </h4>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
