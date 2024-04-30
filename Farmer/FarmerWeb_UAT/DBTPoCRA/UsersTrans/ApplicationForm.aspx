<%@ Page Title="PoCRA" Language="C#" MasterPageFile="~/UsersTrans/UserMaster.Master" AutoEventWireup="true" CodeBehind="ApplicationForm.aspx.cs" Inherits="DBTPoCRA.UsersTrans.ApplicationForm" %>

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
            debugger;
          
            $("#<%=txtSearch.ClientID %>").autocomplete({

                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/APPData/CommanAPI.asmx/GetSearchTerm") %>',
                        data: "{ 'prefix': '" + request.term + "','ID': '" + $("#<%=hfCustomerId.ClientID %>").val() + "','Lan': '" + $("#<%=HiddenFieldLan.ClientID %>").val() + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
           <%-- select: function (e, i) {
                $("#<%=hfCustomerId.ClientID %>").val(i.item.val);
            },--%>
                minLength: 1
            });
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
                            <asp:TextBox ID="txtSearch" CssClass="form-control light" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnSearch" CssClass="btn btn-info focus" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                         <div class="col-md-3">
                            <asp:Button ID="btnShowAll" CssClass="btn btn-info focus" runat="server" Text="Show All"  OnClick="btnShowAll_Click" />
                        </div>
                    </div>
                </div>
                <div class="card-header white">
                    <div class="row">

                       

                        <div class="col-md-4">
                            <label for="phone" class="col-form-label s-12"><i class="icon-mobile mr-2"></i>
                                <asp:Literal ID="Literal2" Text="Component" runat="server"></asp:Literal></label>
                            <cc2:DropDownListChosen ID="ddlComponent" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match." AutoPostBack="True" OnSelectedIndexChanged="ddlComponent_SelectedIndexChanged">
                            </cc2:DropDownListChosen>
                        </div>
                        <div class="col-md-4">
                            <label for="mobile" class="col-form-label s-12"><i class="icon-mobile-phone mr-2"></i>
                                <asp:Literal ID="Literal3" Text="Sub-Component" runat="server"></asp:Literal></label>
                            <cc2:DropDownListChosen ID="ddlSubComponent" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match." AutoPostBack="True" OnSelectedIndexChanged="ddlSubComponent_SelectedIndexChanged">
                            </cc2:DropDownListChosen>
                        </div>
                         <div class="col-md-4">
                            <label for="email" class="col-form-label s-12"><i class="icon-mobile-o mr-2"></i>
                                <asp:Literal ID="Literal4" Text="Activity" runat="server"></asp:Literal></label>
                            <cc2:DropDownListChosen ID="ddlActivity" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match." AutoPostBack="True" OnSelectedIndexChanged="ddlActivity_SelectedIndexChanged">
                            </cc2:DropDownListChosen>
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
                                            <div class="card" style="height: 300px; width:300px; border: 1px solid #0487f7 !important;">
                                                <div class="card-header" style="height: 80px; text-align: center;">
                                                    <strong class="bolder">
                                                        <asp:Label ID="ActivityName" runat="server" Text='<%# Eval("ActivityName") %>'></asp:Label>
                                                         <asp:Label ID="ActivityNameMr" runat="server" Text='<%# Eval("ActivityNameMr") %>'></asp:Label>
                                                    </strong>
                                                </div>
                                                <div class="card-body text-center">
                                                    <div class="image m-0">
                                                        <asp:Image ID="empimg" AlternateText="" Style="width: 220px; height: 150px" runat="server" CssClass="user_avatar no-b no-p r-5" ImageUrl='<%# CommanClsLibrary.clsSettings.BaseUrl+""+Eval("ActivityImagePath") %>' />
                                                    </div>
                                                    <a href="ActivetyDetails.aspx?T=<%# Eval("ActivityID") %>" class="btn btn-danger btn-sm mt-3">
                                                        <asp:Label ID="Label1" runat="server" Text="ADD"></asp:Label></a>
                                                </div>
                                            </div>

                                        </ItemTemplate>
                                       <%-- <SeparatorTemplate>
                                            <hr />
                                            <br />
                                        </SeparatorTemplate>--%>
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
