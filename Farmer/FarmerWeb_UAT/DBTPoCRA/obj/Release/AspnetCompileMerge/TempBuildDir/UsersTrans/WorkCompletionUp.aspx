<%@ Page Title="" Language="C#" MasterPageFile="~/UsersTrans/UserMaster.Master" AutoEventWireup="true" CodeBehind="WorkCompletionUp.aspx.cs" Inherits="DBTPoCRA.UsersTrans.WorkCompletionUp" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .table-responsive {
            min-height: 650px !important;
        }
    </style>
    <div class="row">
        <div class="col-md-12">
            <asp:FormView ID="FormView1" runat="server" DataKeyNames="ActivityID" Width="100%">
                <ItemTemplate>
                    <table class="table table-bordered table-hover data-tables">
                        <tr>
                            <td><strong style="font-weight: bold">Activity Name</strong></td>
                            <td>
                                <asp:Label ID="ActivityNameLabel" runat="server" Text='<%# Bind("ActivityName") %>' />
                            </td>
                            <td>&nbsp;</td>
                            <td rowspan="4" style="text-align: center">
                                <asp:Image ID="empimg" AlternateText="" runat="server" CssClass="user_avatar no-b no-p r-5" ImageUrl='<%# CommanClsLibrary.clsSettings.BaseUrl+""+Eval("ActivityImagePath") %>' Height="120px" Width="150px" />
                            </td>
                        </tr>
                        <tr>
                            <td><strong style="font-weight: bold">Activity Code</strong></td>
                            <td>
                                <asp:Label ID="ActivityCodeLabel" runat="server" Text='<%# Bind("ActivityCode") %>' />
                            </td>
                            <td style="font-weight: bold">  
                               
                                <asp:Literal ID="Literal1" Text="Partial Payment Allowed" runat="server"></asp:Literal> &nbsp;::&nbsp;   <asp:Label ID="Label1" runat="server" Text='<%# Bind("PartialPaymentAllowed") %>' /> 


                            </td>
                        </tr>


                    </table>

                </ItemTemplate>
            </asp:FormView>
        </div>
    </div>


    <div class="row">

        <div class="col-md-12">
            <label for="email" class="col-form-label s-12"></label>
            <asp:Literal ID="LiteralMsg" runat="server"></asp:Literal>
            <asp:Literal ID="LiteralM" runat="server"></asp:Literal>
        </div>
    </div>

   
    <div class="row" id="divReq" runat="server" visible="false">

        <div class="col-md-4">
            <label for="mobile" class="col-form-label s-12">Request Number</label>
            <br />
            <asp:Label runat="server" ID="lblReqNo" Text=""></asp:Label>
        </div>
        <div class="col-md-4">
            <label for="mobile" class="col-form-label s-12">Request Date</label>
            <br />
            <asp:Label runat="server" ID="lblReqDate" Text=""></asp:Label>
        </div>
        <div class="col-md-4" style="text-align: right">
            <label for="mobile" class="col-form-label s-12"></label>
            <br />
            <asp:Label ID="lblWorkReportID" Visible="false" runat="server" Text=""></asp:Label>
        </div>
    </div>
    <hr />

    <div class="row">
        <div class="col-md-2">
            <label for="email" class="col-form-label s-12"><i class="icon-mobile-o mr-2"></i>Document Type</label>
            <asp:DropDownList ID="ddlDocType" CssClass="form-control light" runat="server">
                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                <%--<asp:ListItem Text="Progress Photographs" Value="Progress Photographs"></asp:ListItem>--%>
                <asp:ListItem Text="Procurement Documents" Value="Procurement Documents"></asp:ListItem>
                <%--<asp:ListItem Text="Pre-Sanction Letter" Value="Pre-Sanction Letter"></asp:ListItem>--%>
            </asp:DropDownList>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="badge badge-danger badge-mini2" InitialValue="0"
                runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlDocType"></asp:RequiredFieldValidator>
        </div>

        <div class="col-md-2">
            <label for="email" class="col-form-label s-12"><i class="icon-mobile-o mr-2"></i>Levels</label>
            <asp:DropDownList ID="ddlLevels" CssClass="form-control light" runat="server">
                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                <%-- <asp:ListItem Text="Before starting of work" Value="Before starting of work"></asp:ListItem>
                <asp:ListItem Text="During the processing of work" Value="During the processing of work"></asp:ListItem>
                <asp:ListItem Text="After completion of work" Value="After completion of work"></asp:ListItem>--%>
                <asp:ListItem Text="Procurement Bill" Value="Procurement Bill"></asp:ListItem>
               <%-- <asp:ListItem Text="Pre-Sanction Letter" Value="Pre-Sanction Letter"></asp:ListItem>--%>
            </asp:DropDownList>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="badge badge-danger badge-mini2" InitialValue="0"
                runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlLevels"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-2">
            <label for="email" class="col-form-label s-12"><i class="icon-mobile-o mr-2"></i>Details/Remark </label>
            <asp:TextBox ID="txtDocument" CssClass="form-control light" runat="server"></asp:TextBox>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="badge badge-danger badge-mini2"
                runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtDocument"></asp:RequiredFieldValidator>
        </div>

        <div class="col-md-2">
            <label for="phone" class="col-form-label s-12"><i class="icon-mobile mr-2"></i>Completion Date</label>
            <asp:TextBox ID="txtCompletionDate" TextMode="MultiLine" CssClass="form-control light" runat="server"></asp:TextBox>
            <ajax:CalendarExtender CssClass="cal_Theme1" ID="Calendar1" runat="server" TargetControlID="txtCompletionDate" Format="dd/MM/yyyy"></ajax:CalendarExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="badge badge-danger badge-mini2"
                runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtCompletionDate"></asp:RequiredFieldValidator>

        </div>
        <div class="col-md-2">
            <label for="mobile" class="col-form-label s-12"><i class="icon-mobile-phone mr-2"></i>Upload Required Doc.</label>


            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnAdd" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
        <div class="col-md-2" style="text-align: right">
            <label for="mobile" class="col-form-label s-12 white-text"><i class="icon-mobile-phone mr-2"></i>.</label><br />
            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="ADD" OnClick="btnAdd_Click" />
        </div>

    </div>

    <hr />
    <div class="row">
        <div class="col-md-12">
            <asp:GridView ID="grdSubject" runat="server" AutoGenerateColumns="False" Width="100%"
                CssClass="table table-bordered table-hover data-tables" DataKeyNames="WorkCompletionID"
                SelectedRowStyle-BackColor="#F3F3F3" OnRowDeleting="grdSubject_RowDeleting">
                <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Sr No">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="DocTypes" HeaderText="Document Type">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DocLevels" HeaderText="Levels">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>

                    <asp:BoundField DataField="DocumentDetails" HeaderText="DocumentDetails">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="CompletionDate" DataField="CompletionDate">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="DocumentUploaded">

                        <ItemTemplate>

                            <asp:HyperLink ID="hlimk14" runat="server" Target="_blank" NavigateUrl='<%# CommanClsLibrary.clsSettings.BaseUrl+""+Eval("DocumentUploaded") %>'>View </asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="javascript:return confirm ('Are you sure to Delete this record permanently ? ')" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <PagerStyle Font-Bold="True" />

                <SelectedRowStyle BackColor="#F3F3F3"></SelectedRowStyle>
            </asp:GridView>
        </div>
    </div>

   <hr />
    <div class="row">

        <div class="col-md-4">
            <label for="mobile" class="col-form-label s-12">Total Expected subsidy amount :</label>
            <br />
            <asp:TextBox ID="txtExpenditureAmount"  Width="150px" placeholder="Total Expected subsidy amount" MaxLength="9" Text="0" CssClass="form-control light" runat="server"></asp:TextBox>
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtExpenditureAmount"  ErrorMessage="Invalid" 
                CssClass="badge badge-danger badge-mini2" Type="Double" MaximumValue="9999999" MinimumValue="500" SetFocusOnError="True" Display="Dynamic"></asp:RangeValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5"  CssClass="badge badge-danger badge-mini2"
                runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtExpenditureAmount"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-6">
            <label for="mobile" class="col-form-label s-12 white-text">.</label>
            <br />
            Total expenditure amount for this activety (as per uploaded completion document)
        </div>
        <div class="col-md-2" style="text-align: right">
            <label for="mobile" class="col-form-label s-12 white-text">.</label>
            <br />
            <asp:Button ID="btnSave" CssClass="btn btn-primary" ValidationGroup="G" Width="200px" runat="server" Text="Submit and Send For Approval" OnClick="btnSave_Click" />
        </div>
    </div>
     <hr />



</asp:Content>
