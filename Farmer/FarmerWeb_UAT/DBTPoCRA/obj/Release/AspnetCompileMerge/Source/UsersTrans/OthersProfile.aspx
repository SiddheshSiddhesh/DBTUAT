<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OthersProfile.aspx.cs" Inherits="DBTPoCRA.UsersTrans.OthersProfile" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Individual/Farmer Registration</title>
    <link rel="stylesheet" href="../assets/css/app.css" />
    <script src="../assets/js/jquery.min.js"></script>

    <link rel="icon" href="../assets/img/basic/favicon.ico" type="image/x-icon" />
    <link href="../assets/js/sumoselect.css" rel="stylesheet" />
    <script src="../assets/js/app.js"></script>
    <script src="../assets/js/chosen.jquery.js"></script>
    <script src="../assets/js/jquery.sumoselect.js"></script>
    <style>
        label {
            font-weight: bold !important;
        }
        li label
        {
            font-weight: 500 !important;
        }
        td {
            padding: 0px !important;
        }

        .card-body {
            margin: 0 !important;
            padding: 0 !important;
        }

        .form-group {
            margin: 0 !important;
            padding: 0 !important;
        }

        .cpHeader {
            width: 100%;
            height: 30px;
            background-image: url(../assets/img/basic/bg-menu-main.png);
            background-repeat: repeat-x;
            color: #FFF;
            font-weight: bold;
            text-align: center;
            vertical-align: middle;
            margin: 2px;
        }

        .container {
            min-width: 100% !important;
            margin: 0 !important;
            padding: 0 !important;
        }

        .col-lg-12 {
            margin: 0 !important;
            padding: 0 !important;
        }

        body {
            background-color: white !important;
        }

        #nprogress {
            display: none !important;
        }
    </style>
    <script type="text/javascript">
        function StateCity(input) {
            var displayIcon = "img" + input;
            if ($("#" + displayIcon).attr("src") == "../assets/img/icon/Plus.png") {
                $("#" + displayIcon).closest("tr")
                .after("<tr><td></td><td colspan = '100%'>" + $("#" + input)
                .html() + "</td></tr>");
                $("#" + displayIcon).attr("src", "../assets/img/icon/minus.png");
            } else {
                $("#" + displayIcon).closest("tr").next().remove();
                $("#" + displayIcon).attr("src", "../assets/img/icon/Plus.png");
            }
        }
    </script>
    <script type="text/javascript">
        function CheckUpdate() {
            $('.listbox').SumoSelect({ okCancelInMulti: true, selectAll: true });
        }

        function callParentClick() {
            //window.parent.location.href = window.parent.location.href;
            window.parent.updateGrid();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <main>
                        <div class="container">

                            <div class="col-lg-12">

                                <div id="DivIndividual" class="col-sm-12">

                                    <div class="form-group">
                                        <div class="card no-b  no-r">
                                            <div class="card-body">
                                                <h5 class="card-title btn bg-grey text-white font-weight-bolder" style="width: 100% !important">
                                                    <asp:Literal ID="Literal5" runat="server"></asp:Literal>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="card no-b  no-r">
                                            <div class="card-body">
                                                <asp:Panel ID="pHeader" runat="server" CssClass="cpHeader">
                                                    <asp:Label ID="lblText" runat="server" />

                                                </asp:Panel>

                                                <ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="Panel1" CollapseControlID="pHeader" ExpandControlID="pHeader"
                                                    Collapsed="true" TextLabelID="lblText" CollapsedText="Click to Show Registration details." ExpandedText="Click to Hide Registration Details."
                                                    CollapsedSize="0"></ajax:CollapsiblePanelExtender>

                                                <asp:Panel ID="Panel1" Width="99%" Height="100%" ScrollBars="None" runat="server">
                                                    <iframe id="ifram" runat="server" src="#" height="900px" width="100%" style="overflow: hidden"></iframe>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="card no-b  no-r">
                                            <div class="card-body">

                                                <fieldset style="border: 1px solid #CCCCCC">
                                                    <legend>
                                                        <asp:Literal ID="Literal2" Text="VERIFICATION" runat="server"></asp:Literal>
                                                    </legend>

                                                    <div class="form-row">
                                                        <div class="col-md-4">
                                                            <label>
                                                                <asp:Literal ID="Literal3" Text="Registration Status" runat="server"></asp:Literal>
                                                            </label>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="badge badge-danger badge-mini2" InitialValue="0"
                                                                runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlApplicationStatus"></asp:RequiredFieldValidator>

                                                            <asp:DropDownList ID="ddlApplicationStatus" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlApplicationStatus_SelectedIndexChanged">
                                                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                <asp:ListItem Value="7" Text="Verified"></asp:ListItem>
                                                                <asp:ListItem Value="6" Text="Hold"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="Rejection"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3" style="text-align: left;">
                                                            <label>
                                                                <asp:Literal ID="Literal6" Text="Date" runat="server"></asp:Literal>
                                                            </label>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="badge badge-danger badge-mini2"
                                                                runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtDate"></asp:RequiredFieldValidator>
                                                            <asp:TextBox ID="txtDate" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <ajax:CalendarExtender CssClass="cal_Theme1" ID="Calendar1" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy"></ajax:CalendarExtender>
                                                        </div>
                                                        <div class="col-md-3 align-content-center">
                                                            <label>
                                                                <asp:Literal ID="Literal7" Text="Current Status" runat="server"></asp:Literal>
                                                            </label>
                                                            <br />
                                                            <span class="btn alert-danger">
                                                                <asp:Literal ID="Literal1" runat="server"></asp:Literal></span>
                                                        </div>
                                                    </div>
                                                    <div id="divReg" runat="server" class="form-row">
                                                        <div class="col-md-6">
                                                            <label>
                                                                <asp:Literal ID="Literal8" Text="Reason" runat="server"></asp:Literal>
                                                            </label>
                                                            <br />
                                                            <asp:ListBox ID="chkReasons" runat="server" SelectionMode="Multiple" class="form-control light listbox sumo"></asp:ListBox>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label>
                                                                <asp:Literal ID="Literal9" Text="Remark if any" runat="server"></asp:Literal>
                                                            </label>
                                                            <br />
                                                            <asp:TextBox ID="txtResion" TextMode="MultiLine" Height="40px" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>


                                                    <div class="form-row">
                                                        
                                                        <div class="col-md-12" style="overflow: auto; min-height: 150px; border: 0px solid #0099CC">
                                                            <label>
                                                                <asp:Literal ID="Literal14" Text="Approval Checklist*" runat="server"></asp:Literal>
                                                            </label>
                                                            <br />

                                                            <asp:CheckBoxList ID="chkFeasibility" CssClass="form-control" Font-Bold="false" runat="server" Height="100%" RepeatLayout="OrderedList"></asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                    <div class="form-row">
                                                        <div class="col-md-6">
                                                            <asp:Literal ID="LiteralMsg" runat="server"></asp:Literal>
                                                        </div>
                                                        <div class="col-md-3" style="text-align: left;">
                                                        </div>
                                                        <div class="col-md-3">

                                                            <asp:Button ID="btnUpdate" runat="server" Width="100px" CssClass="btn btn-success" Text="Update" OnClick="btnUpdate_Click" />
                                                        </div>
                                                    </div>


                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>





                                    <fieldset style="border: 1px solid #CCCCCC">
                                        <legend>
                                            <asp:Literal ID="Literal73" Text="VERIFICATION" runat="server"></asp:Literal>
                                        </legend>
                                        <div class="form-row">
                                            <div class="col-md-12 table-responsive">
                                                <label>
                                                    <asp:Literal ID="Literal74" Text="VERIFICATION LOG" runat="server"></asp:Literal></label>
                                                <br />
                                                <asp:GridView ID="grdLog" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    CssClass="" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>

                                                        <asp:BoundField DataField="Date" HeaderText="Updated On Date" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FullName" HeaderText="Updated By" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" >
                                                       
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle Width="20%" />
                                                        </asp:BoundField>
                                                       
                                                        <asp:BoundField DataField="Status" HeaderText="Update To Status" ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                                                            <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Reason/Remarks" ItemStyle-Width="60%" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="60%" />
                                                        </asp:TemplateField>
                                                        
                                                    </Columns>
                                                    <EditRowStyle BackColor="#7C6F57" />
                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#E3EAEB" />
                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                                                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                                                </asp:GridView>
                                            </div>

                                        </div>
                                    </fieldset>
                                </div>

                            </div>
                        </div>
                    </main>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

    </form>
</body>
</html>
