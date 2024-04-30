<%@ Page Title="PoCRA" Language="C#" MasterPageFile="~/UsersTrans/UserMaster.Master" AutoEventWireup="true" CodeBehind="ActivetyDetails.aspx.cs" Inherits="DBTPoCRA.UsersTrans.ActivetyDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        td {
            border-color: white !important;
        }
    </style>

    <div>
          <asp:Literal ID="LiteralMsg" runat="server"></asp:Literal>
    </div>

    <asp:FormView ID="FormView1" runat="server" DataKeyNames="ActivityID" Width="100%" OnDataBound="FormView1_DataBound">
        <ItemTemplate>
            <table class="table table-bordered table-hover data-tables">
                <tr>
                    <td><a href="ApplicationForm.aspx" class="btn btn-danger">  <asp:Label ID="Literal1" Text="Back To Activity" runat="server"></asp:Label> </a></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td><a href="ApplicationSubmit.aspx?T=<%# Eval("ActivityID") %>" class=" btn btn-success"><asp:Label ID="Literal2" Text="Continue to Application" runat="server"></asp:Label> 
                        <i class="fa fa-forward"></i>

                        </a></td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center">
                        <hr />
                      
                    </td>
                </tr>
                <tr>
                    <td><strong style="font-weight: bold"><asp:Label ID="Literal3" Text="Activity Name" runat="server"></asp:Label></strong></td>
                    <td>
                        <asp:Label ID="ActivityNameLabel" runat="server" Text='<%# Bind("ActivityName") %>' />
                    </td>
                    <td>&nbsp;</td>
                    <td rowspan="4" style="text-align: center">
                        <asp:Image ID="empimg" AlternateText="" runat="server" CssClass="user_avatar no-b no-p r-5" ImageUrl='<%# CommanClsLibrary.clsSettings.BaseUrl+""+Eval("ActivityImagePath") %>' Height="120px" Width="150px" />
                    </td>
                </tr>
                <tr>
                    <td><strong style="font-weight: bold"><asp:Label ID="Literal4" Text="Activity Code" runat="server"></asp:Label></strong></td>
                    <td>
                        <asp:Label ID="ActivityCodeLabel" runat="server" Text='<%# Bind("ActivityCode") %>' />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><strong style="font-weight: bold"><asp:Label ID="Literal5" Text="Component Name" runat="server"></asp:Label></strong></td>
                    <td>
                        <asp:Label ID="ComponentNameLabel" runat="server" Text='<%# Bind("ComponentName") %>' />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><strong style="font-weight: bold"><asp:Label ID="Literal6" Text="Sub-Component Name" runat="server"></asp:Label></strong></td>
                    <td>
                        <asp:Label ID="SubComponentNameLabel" runat="server" Text='<%# Bind("SubComponentName") %>' />
                    </td>
                    <td>&nbsp;</td>
                </tr>

                <tr>


                    <td><strong style="font-weight: bold"><asp:Label ID="Literal7" Text="Subsidy Details" runat="server"></asp:Label></strong></td>
                    <asp:GridView ID="grdSubject" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-bordered table-hover data-tables" SelectedRowStyle-BackColor="#F3F3F3" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="PaymentTermName" HeaderText="Term">
                                <HeaderStyle Width="300px" />
                            </asp:BoundField>


                            <asp:TemplateField HeaderText="Common Subsidy ">

                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("SubsidyPerComm") %>'></asp:Label>
                                    % or 
                                     <asp:Label ID="Label2" runat="server" Text='<%# Bind("SubsidyAmtComm") %>'></asp:Label>
                                    INR
                                    <br />
                                    Note- <asp:Label ID="Label3" runat="server" Text='<%# Bind("CommanNote") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="SC/ST Subsidy ">

                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("SubsidyPerScST") %>'></asp:Label>
                                    % or 
                                     <asp:Label ID="Label21" runat="server" Text='<%# Bind("SubsidyAmtScST") %>'></asp:Label>
                                    INR
                                      <br />
                                    Note- <asp:Label ID="Label3" runat="server" Text='<%# Bind("SCSTNote") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                           
                        </Columns>
                        <SelectedRowStyle BackColor="#F3F3F3" />
                    </asp:GridView>
                    <tr>
                        <td><%--<strong style="font-weight: bold">Requirements</strong>--%></td>
                        <asp:GridView ID="grdChild" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered" DataKeyNames="" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="TransType" HeaderText="Required"  />
                                <asp:BoundField DataField="Texts" HeaderText="Details" />
                            </Columns>
                        </asp:GridView>
                    </tr>
                    <tr>
                        <td><strong style="font-weight: bold"></strong></td>
                        <asp:GridView ID="grdExeT" runat="server" Visible="false" AutoGenerateColumns="false" CssClass="table table-bordered" DataKeyNames="" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="ApprovalStages" HeaderText="Stages" />
                                <%--<asp:BoundField DataField="TimeLineDay" HeaderText="Execution Time in Day" />--%>
                                <asp:TemplateField HeaderText="Execution Time in Day">

                                    <ItemTemplate>
                                        <asp:Label ID="Label99" runat="server" Text='<%# Bind("TimeLineDay") %>'></asp:Label> Days
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </tr>
            </table>
            <br />
        </ItemTemplate>
    </asp:FormView>



</asp:Content>
