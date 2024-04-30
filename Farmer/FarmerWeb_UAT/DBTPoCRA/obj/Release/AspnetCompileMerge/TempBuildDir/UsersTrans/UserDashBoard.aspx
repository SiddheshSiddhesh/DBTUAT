<%@ Page Title="" Language="C#" MasterPageFile="~/UsersTrans/UserMaster.Master" AutoEventWireup="true" CodeBehind="UserDashBoard.aspx.cs" Inherits="DBTPoCRA.UsersTrans.UserDashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../assets/css/thickbox.css" rel="stylesheet" />
    <script src="../assets/js/thickbox.js"></script>

    <script>
        function ShowLetterPre(ID) {
            tb_show('', '' + ID + '');
        }

    </script>
    <script type="text/javascript">
        function StateCity(input) {
            debugger;
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

    <div class="row">
        <div class="col-md-12">
            <div class="card mb-3">
                <div class="card-header  btn  badge-primary">
                    <asp:Literal ID="Literal10" Text="REGISTRATION  DETAILS" runat="server"></asp:Literal>
                </div>
                <div class="slimScrollDiv">
                    <div class="card-body slimScroll">
                        <div class="table-responsive" style="overflow-x:auto;">
                            <table class="table table-hover">
                                <tbody>
                                    <tr class="no-b">

                                        <td>
                                            <h6>
                                                <asp:Literal ID="Literal6" Text="NAME" runat="server"></asp:Literal>
                                            </h6>
                                            <small class="text-muted">
                                                <asp:Literal ID="Literal1" runat="server"></asp:Literal></small>
                                        </td>
                                        <td>
                                            <h6>
                                                <asp:Literal ID="Literal7" Text="MOBILE NO." runat="server"></asp:Literal>
                                            </h6>
                                            <small class="text-muted">
                                                <asp:Literal ID="Literal2" runat="server"></asp:Literal></small>
                                            <br />
                                            <a href="MobileVerification.aspx">Click here to Update Mobile No.</a>
                                        </td>
                                        <td>
                                            <h6>
                                                <asp:Literal ID="Literal8" Text="REGISTRATION DATE" runat="server"></asp:Literal>
                                            </h6>
                                            <small class="text-muted">
                                                <asp:Literal ID="Literal3" runat="server"></asp:Literal></small>

                                        </td>
                                        <td>
                                            <h6>
                                                <asp:Literal ID="Literal9" Text="REGISTRATION STATUS" runat="server"></asp:Literal>
                                            </h6>
                                            <span class="badge badge-warning orange darken-1">
                                                <asp:Literal ID="Literal4" runat="server"></asp:Literal></span>
                                        </td>
                                        <td>
                                            <span style="display: none">
                                                <asp:Literal ID="Literal5" runat="server"></asp:Literal></span>
                                            <asp:Literal ID="LiteralImage" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr class="no-b">
                                        <td colspan="5">

                                            <span>
                                                <asp:Literal ID="LiteralResion" runat="server"></asp:Literal></span>
                                            <asp:CheckBoxList ID="CheckBoxList1" runat="server"></asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr class="no-b" id="TrResion" runat="server" visible="false">
                                        <td colspan="4">
                                            <asp:Literal ID="LiteralMsg" runat="server"></asp:Literal>
                                            <span>टीप: वरील सर्व कारणास्तव निराकरण केल्यानंतरच पुन्हा सत्यापन करण्याची परवानगी दिली जाऊ शकते.</span>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-info" OnClientClick="javascript:return confirm ('
आपणास खात्री आहे की आपण वर उल्लेख केलेल्या सर्व कारणांचे निराकरण केले आहे ? ')"
                                                CausesValidation="false" Text="Send for ReVerification" OnClick="btnUpdate_Click" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card mb-3">
                <div class="card-header  btn  badge-success">
                    <asp:Literal ID="Literal11" Text="OPEN APPLICATIONS" runat="server"></asp:Literal>
                </div>
                <div class="slimScrollDiv">
                    <div class="card-body">
                        <div class="table-responsive"  style="overflow-x:auto;">

                            <asp:GridView ID="grdSubject" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="RegistrationID,ApplicationID"
                                CssClass="table table-bordered table-hover data-tables" OnRowDataBound="grdSubject_RowDataBound"
                                SelectedRowStyle-BackColor="#F3F3F3">
                                <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                                <Columns>

                                    <asp:TemplateField HeaderText="Sr. No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <a href="JavaScript:StateCity('div<%# Eval("ApplicationID") %>');">
                                                <img style="min-height: 16px; min-width: 16px;" alt="City" id="imgdiv<%# Eval("ApplicationID") %>" src="../assets/img/icon/Plus.png" />
                                            </a>
                                            <div id="div<%# Eval("ApplicationID") %>" style="display: none;">
                                                <br />
                                                <div class="alert alert-success">Application Status </div>

                                                <asp:GridView ID="grdChild" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="ApplicationID"
                                                    CssClass="table table-bordered">
                                                    <Columns>
                                                        <asp:BoundField DataField="Date" HeaderText="Date" />
                                                        <asp:BoundField DataField="FullName" HeaderText="Updated By" />
                                                        <asp:BoundField DataField="Desig_Name" HeaderText="Level" />
                                                        <asp:BoundField DataField="ApprovalStages" HeaderText="Stage" />
                                                        <asp:BoundField DataField="ApplicationStatus" HeaderText="Status" />
                                                        <asp:TemplateField HeaderText="Reason">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Remark" HeaderText="Remark" />

                                                    </Columns>
                                                </asp:GridView>
                                                <br />
                                                <div class="alert alert-success">Payment Request Status </div>
                                                <asp:GridView ID="grdChildPay" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames=""
                                                    CssClass="table table-bordered">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr. No">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Request No." DataField="RequestNo">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>

                                                        <asp:TemplateField HeaderText="Request Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("TotalAmtByBen") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Approved Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label15" Text='<%# Bind("FinalAmtApproved") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" Text='<%# Bind("ApprovalStage") %>' runat="server"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label145" Font-Size="8pt" Text='<%# Bind("ApplicationStatus") %>' runat="server"></asp:Label>

                                                                <br />
                                                                <asp:Label ID="Label344" Font-Size="8pt" Text='<%# Bind("btnFarmer") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="ApplicationCode" HeaderText="Application Code">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="ApplicationDate" HeaderText="Date">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>


                                    <asp:BoundField HeaderText="Activity" DataField="ActivityName">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Activity Code" DataField="ActivityCode">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Component" DataField="ComponentName">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("ApplicationStatus") %>'></asp:Label>
                                            <br />
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("PreSenLetterFarmer") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField ShowHeader="False" HeaderText="">
                                        <ItemTemplate>

                                            <asp:Label ID="Label35" runat="server" Text='<%# Bind("Pay") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                                <PagerStyle Font-Bold="True" />

                                <SelectedRowStyle BackColor="#F3F3F3"></SelectedRowStyle>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-12">
            <div class="card mb-3">
                <div class="card-header  btn  badge-success">CLOSED APPLICATIONS  </div>
                <div class="slimScrollDiv">
                    <div class="card-body">
                        <div class="table-responsive">

                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="RegistrationID,ApplicationID"
                                CssClass="table table-bordered table-hover data-tables" OnRowDataBound="GridView1_RowDataBound"
                                SelectedRowStyle-BackColor="#F3F3F3">
                                <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                                <Columns>

                                    <asp:TemplateField HeaderText="Sr. No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <a href="JavaScript:StateCity('div<%# Eval("ApplicationID") %>');">
                                                <img style="min-height: 16px; min-width: 16px;" alt="City" id="imgdiv<%# Eval("ApplicationID") %>" src="../assets/img/icon/Plus.png" />
                                            </a>
                                            <div id="div<%# Eval("ApplicationID") %>" style="display: none;">
                                                <br />
                                                <div class="alert alert-success">Application Status </div>

                                                <asp:GridView ID="grdChild" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="ApplicationID"
                                                    CssClass="table table-bordered">
                                                    <Columns>
                                                        <asp:BoundField DataField="Date" HeaderText="Date" />
                                                        <asp:BoundField DataField="FullName" HeaderText="Updated By" />
                                                        <asp:BoundField DataField="Desig_Name" HeaderText="Level" />
                                                        <asp:BoundField DataField="ApprovalStages" HeaderText="Stage" />
                                                        <asp:BoundField DataField="ApplicationStatus" HeaderText="Status" />
                                                        <asp:TemplateField HeaderText="Reason">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Remark" HeaderText="Remark" />

                                                    </Columns>
                                                </asp:GridView>
                                                <br />
                                                <div class="alert alert-success">Payment Request Status </div>
                                                <asp:GridView ID="grdChildPay" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames=""
                                                    CssClass="table table-bordered">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr. No">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Request No." DataField="RequestNo">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>

                                                        <asp:TemplateField HeaderText="Request Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("TotalAmtByBen") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Approved Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label15" Text='<%# Bind("FinalAmtApproved") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" Text='<%# Bind("ApprovalStage") %>' runat="server"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label145" Font-Size="8pt" Text='<%# Bind("ApprovalStagesFarmer") %>' runat="server"></asp:Label>


                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:BoundField DataField="ApplicationDate" HeaderText="Date">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>


                                    <asp:BoundField HeaderText="Activity" DataField="ActivityName">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Activity Code" DataField="ActivityCode">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Component" DataField="ComponentName">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>



                                    <%-- <asp:BoundField HeaderText="Status" DataField="ApplicationStatus">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>--%>
                                </Columns>
                                <PagerStyle Font-Bold="True" />

                                <SelectedRowStyle BackColor="#F3F3F3"></SelectedRowStyle>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
