<%@ Page Title="PoCRA" Language="C#" MasterPageFile="~/UsersTrans/UserMaster.Master" AutoEventWireup="true" CodeBehind="ApplicationGroups.aspx.cs" Inherits="DBTPoCRA.UsersTrans.ApplicationGroups" %>

<%@ Register Assembly="DropDownChosen" Namespace="CustomDropDown" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .table-responsive {
            overflow-x: hidden !important;
        }
    </style>


    <script src="Js/jquery.min.js"></script>
    <script src="Js/jquery-ui.min.js"></script>
    <link href="Js/jquery-ui.css" rel="stylesheet" />


    <script type="text/javascript">

        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            // Place here the first init of the autocomplete
            InitAutoCompl();
        });

        function InitializeRequest(sender, args) {
        }

        function EndRequest(sender, args) {
            // after update occur on UpdatePanel re-init the Autocomplete
            InitAutoCompl();
        }






        function InitAutoCompl() {

        }
    </script>





    <div class="row">
        <div class="col-md-12">
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">

            <div class="card">
                <div class="card-header white">

                    <i class="icon-clipboard-edit blue-text"></i>
                    <asp:Literal ID="Literal1" Text="Search Activity from list" runat="server"></asp:Literal>
                    <asp:HiddenField ID="hfCustomerId" Value="1" runat="server" />
                    <asp:HiddenField ID="HiddenFieldLan" Value="1" runat="server" />
                    <div class="row">
                        <div class="col-md-6">
                            <label for="email" class="col-form-label s-12">
                                <i class="icon-mobile-o mr-2"></i>
                                <asp:Literal ID="Literal4" Text="Activity Groups" runat="server"></asp:Literal></label>
                            <cc2:DropDownListChosen ID="ddlActivityGroups" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match.">
                            </cc2:DropDownListChosen>
                        </div>
                        <div class="col-md-3">
                            <label for="email" class="col-form-label s-12">
                                <br /><br /><br />
                                </label>
                            <asp:Button ID="btnSearch" CssClass="btn btn-info focus" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                        <div class="col-md-3">
                            <label for="email" class="col-form-label s-12">
                                <br /><br /><br />
                                </label>
                            <asp:Button ID="btnShowAll" CssClass="btn btn-info focus" runat="server" Text="Show All" OnClick="btnShowAll_Click" />
                        </div>
                    </div>
                </div>

                <div class="slimScrollDiv">
                    <div class="pt-0 bg-light">
                        <div class="card no-b shadow">
                            <div class="card-body p-0">
                                <div class="table-responsive" style="min-height: 250px;">
                                    <asp:DataList ID="DataList1" RepeatColumns="3" runat="server" CellSpacing="10" CellPadding="10">
                                        <ItemTemplate>
                                            <div class="card" style="height: 300px; width: 300px; border: 1px solid #0487f7 !important;">
                                                <div class="card-header" style="height: 80px; text-align: center;">
                                                    <strong class="bolder">
                                                        <asp:Label ID="ActivityName" runat="server" Text='<%# Eval("ActivityGroupName") %>'></asp:Label>
                                                        <%--<asp:Label ID="ActivityNameMr" runat="server" Text='<%# Eval("ActivityGroupNameMr") %>'></asp:Label>--%>
                                                    </strong>
                                                </div>
                                                <div class="card-body text-center">
                                                    <div class="image m-0">
                                                        <asp:Image ID="empimg" AlternateText="" Style="width: 220px; height: 150px" runat="server" CssClass="user_avatar no-b no-p r-5" ImageUrl='<%# CommanClsLibrary.clsSettings.BaseUrl+""+Eval("ImageOfActivityGroup") %>' />
                                                    </div>
                                                    <a href="ApplicationForm.aspx?T=<%# CommanClsLibrary.EncryptDecryptQueryString.BasicEnryptString(Eval("ActivityGroupID").ToString()) %>" class="btn btn-danger btn-sm mt-3">
                                                        <asp:Label ID="Label1" runat="server" Text="ADD"></asp:Label></a>
                                                </div>
                                            </div>

                                        </ItemTemplate>

                                    </asp:DataList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>



        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
        </div>
    </div>

    <script src="../assets/js/chosen.jquery.js"></script>
    <script src="../assets/js/jquery.sumoselect.js"></script>

</asp:Content>
