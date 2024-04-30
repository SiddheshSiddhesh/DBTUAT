<%@ Page Title="PoCRA" Language="C#" MasterPageFile="~/UsersTrans/UserMaster.Master" AutoEventWireup="true" CodeBehind="ApplicationSubmit.aspx.cs" Inherits="DBTPoCRA.UsersTrans.ApplicationSubmit" %>

<%@ Register Assembly="DropDownChosen" Namespace="CustomDropDown" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <script src="../assets/js/sweetalert.min.js"></script>
    <link href="../assets/css/sweetalert.css" rel="stylesheet" />
    <style>
        .table-responsive {
            overflow-x: hidden !important;
        }
    </style>


    <div class="row">
        <div class="col-md-12">
        </div>
    </div>



    <div class="row">
        <div class="col-md-12">
            <asp:FormView ID="FormView1" runat="server" DataKeyNames="ActivityID" Width="100%" OnDataBound="FormView1_DataBound">
                <ItemTemplate>
                    <table class="table table-bordered table-hover data-tables">
                        <tr>
                            <td></td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td><a href="ActivetyDetails.aspx?T=<%# Eval("ActivityID") %>" class=" btn btn-danger"><asp:Literal ID="Literal25" Text="Back To Activety Details" runat="server"></asp:Literal> </a></td>
                        </tr>

                        <tr>
                            <td><strong style="font-weight: bold"><asp:Literal ID="Literal1" Text="Activity Name" runat="server"></asp:Literal></strong></td>
                            <td>
                                <asp:Label ID="ActivityNameLabel" runat="server" Text='<%# Bind("ActivityName") %>' />
                            </td>
                            <td>&nbsp;</td>
                            <td rowspan="4" style="text-align: center">
                                <asp:Image ID="empimg" AlternateText="" runat="server" CssClass="user_avatar no-b no-p r-5" ImageUrl='<%# CommanClsLibrary.clsSettings.BaseUrl+""+Eval("ActivityImagePath") %>' Height="120px" Width="150px" />
                            </td>
                        </tr>
                        <tr>
                            <td><strong style="font-weight: bold"><asp:Literal ID="Literal2" Text="Activity Code" runat="server"></asp:Literal></strong></td>
                            <td>
                                <asp:Label ID="ActivityCodeLabel" runat="server" Text='<%# Bind("ActivityCode") %>' />
                            </td>
                            <td>&nbsp;</td>
                        </tr>


                    </table>
                    <br />
                </ItemTemplate>
            </asp:FormView>
        </div>
    </div>


    <div id="DivApplication">

        <div id="DivLand" runat="server" visible="true">
            <div class="form-row">
                <div class="col-sm-12 form-group">

                    <span style="color: darkgoldenrod; font-weight: bold; font-size: medium; text-transform: uppercase;"><asp:Literal ID="Literal24" Text="Land Detail" runat="server"></asp:Literal><fieldset style="background-color: cadetblue; height: 1PX;">
                        <legend></legend>
                    </fieldset>
                    </span>

                </div>

            </div>
            <div class="row">
                <div class="col-md-4">
                    <label for="email" class="col-form-label s-12"><i class="icon-mobile-o mr-2"></i><asp:Literal ID="Literal4" Text="8A Khata Kramank" runat="server"></asp:Literal><em style="color: red">*</em></label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="badge badge-danger badge-mini2" InitialValue="0"
                        runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddl8A"></asp:RequiredFieldValidator>
                    <cc2:DropDownListChosen ID="ddl8A" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match." AutoPostBack="True" OnSelectedIndexChanged="ddl8A_SelectedIndexChanged">
                    </cc2:DropDownListChosen>
                </div>

                <div class="col-md-4">
                    <label for="phone" class="col-form-label s-12"><i class="icon-mobile mr-2"></i><asp:Literal ID="Literal5" Text="7/12 Survey Number" runat="server"></asp:Literal><em style="color: red">*</em></label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="badge badge-danger badge-mini2" InitialValue="0"
                        runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddl712"></asp:RequiredFieldValidator>
                    <cc2:DropDownListChosen ID="ddl712" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match.">
                    </cc2:DropDownListChosen>
                </div>
                <div class="col-md-4">
                    <label for="phone" class="col-form-label s-12"><i class="icon-mobile mr-2"></i><asp:Literal ID="Literal6" Text="Area used for this activety from Selected 7/12" runat="server"></asp:Literal> </label>
                     <input type="text" id="txtUsedAre" runat="server" class="form-control light" />
                </div>

            </div>

        </div>
        <div class="form-row" style="display: none;">
            <div class="col-sm-12 form-group">

                <span style="color: darkgoldenrod; font-weight: bold; font-size: medium; text-transform: uppercase;"><asp:Literal ID="Literal7" Text="Bank Detail" runat="server"></asp:Literal><fieldset style="background-color: cadetblue; height: 1PX;">
                    <legend></legend>
                </fieldset>
                </span>

            </div>

        </div>
        <div class="form-row" style="display: none;">
            <div class="col-sm-6 form-group">
                <label><asp:Literal ID="Literal8" Text="BANK A/c NO" runat="server"></asp:Literal> <em style="color: red">*</em> </label>

                <input type="text" id="txtBankACNo" runat="server" class="form-control light" />
            </div>
            <div class="col-sm-6 form-group">
            </div>
        </div>
        <div class="form-row" style="display: none;">
            <div class="col-sm-6 form-group">
                <label><asp:Literal ID="Literal9" Text="BANK NAME" runat="server"></asp:Literal> <em style="color: red">*</em> </label>
                <%--<input type="text" id="txtIFSC" runat="server" class="form-control" />--%>
            </div>
            <div class="col-sm-6 form-group">
                <label><asp:Literal ID="Literal10" Text="IFSC CODE" runat="server"></asp:Literal> <em style="color: red">*</em> </label>
                <%--<div class="row">
            <div class="col-md-12 m-3">
            </div>
        </div>--%>
            </div>
        </div>
        <div class="form-row">
            <div class="col-sm-12 form-group">

                <span style="color: darkgoldenrod; font-weight: bold; font-size: medium; text-transform: uppercase;"><asp:Literal ID="Literal11" Text="Other Details" runat="server"></asp:Literal><fieldset style="background-color: cadetblue; height: 1PX;">
                    <legend></legend>
                </fieldset>
                </span>

            </div>

        </div>
        <%-- <div class="row">

            <div class="col-md-6">
                <label for="email" class="col-form-label s-12"><i class="icon-mobile-o mr-2"></i>Name of the crops cultivated *</label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="badge badge-danger badge-mini2" InitialValue="0"
                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlCrops"></asp:RequiredFieldValidator>
                <cc2:DropDownListChosen ID="ddlCrops" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match.">
                </cc2:DropDownListChosen>
            </div>

            <div class="col-md-3">
            </div>
            <div class="col-md-3">
            </div>


        </div>--%>
        <div class="row" style="display: none;">

            <div class="col-md-6">
                <label for="email" class="col-form-label s-12"><i class="icon-mobile-o mr-2"></i><asp:Literal ID="Literal12" Text="Estimated cost of the activity applied for" runat="server"></asp:Literal><em style="color: red">*</em></label>
                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="badge badge-danger badge-mini2" Type="Integer"
                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtEstimatedCost"></asp:RequiredFieldValidator>--%>
                <asp:TextBox ID="txtEstimatedCost" CssClass="form-control light" Text="0" runat="server"></asp:TextBox>
                <asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtEstimatedCost" CssClass="form-control light" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" Type="Double" MaximumValue="99999999" MinimumValue="0" runat="server" ErrorMessage="Invalid"></asp:RangeValidator>
            </div>

            <div class="col-md-5">
                <label for="email" class="col-form-label s-12"><i class="icon-mobile-o mr-2"></i><asp:Literal ID="Literal13" Text="Expected financial assistance as per the cost norms" runat="server"></asp:Literal><em style="color: red">*</em></label>
                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="badge badge-danger badge-mini2" 
                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtExpectedCost"></asp:RequiredFieldValidator>--%>
                <asp:TextBox ID="txtExpectedCost" CssClass="form-control light" Text="0" runat="server"></asp:TextBox>
                <asp:RangeValidator ID="RangeValidator2" ControlToValidate="txtExpectedCost" CssClass="form-control light" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" Type="Double" MaximumValue="99999999" MinimumValue="0" runat="server" ErrorMessage="Invalid"></asp:RangeValidator>

            </div>



        </div>
        <div class="row">

            <div class="col-md-6">
                <label for="email" class="col-form-label s-12"><i class="icon-mobile-o mr-2"></i><asp:Literal ID="Literal14" Text="How would the applicant arrange for the balance funds?" runat="server"></asp:Literal></label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="badge badge-danger badge-mini2" InitialValue="0"
                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlArenge"></asp:RequiredFieldValidator>
                <cc2:DropDownListChosen ID="ddlArenge" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match.">
                   <%-- <asp:ListItem Value="0">--Select--</asp:ListItem>
                    <asp:ListItem>Self</asp:ListItem>
                    <asp:ListItem>Bank Loan</asp:ListItem>--%>
                </cc2:DropDownListChosen>
            </div>

            <div class="col-md-3">
            </div>
            <div class="col-md-3">
            </div>


        </div>
        <div class="row">

            <div class="col-md-12">
                <label for="email" class="col-form-label s-12"><i class="icon-mobile-o mr-2"></i>
                    <asp:Literal ID="Literal15" Text="Whether the applicant or his family  member(s) have, in past, taken the benefit of similar activity under any scheme / program of the government?" runat="server"></asp:Literal>
                    </label>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                    <asp:ListItem>YES</asp:ListItem>
                    <asp:ListItem Selected="True">NO</asp:ListItem>
                </asp:RadioButtonList>

            </div>


        </div>
        <div class="row" id="divd" runat="server" visible="false">
            <div class="col-md-12 m-3">
                <label for="email" class="col-form-label s-12"><i class="icon-mobile-o mr-2"></i><asp:Literal ID="Literal16" Text="Provide details thereof including year, amount scheme etc." runat="server"></asp:Literal></label>
                <asp:TextBox ID="txtLastDetails" TextMode="MultiLine" CssClass="form-control light" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="form-row">
            <div class="col-sm-12 form-group">

                <span style="color: darkgoldenrod; font-weight: bold; font-size: medium; text-transform: uppercase;"><asp:Literal ID="Literal17" Text="Undertaking" runat="server"></asp:Literal><fieldset style="background-color: cadetblue; height: 1PX;">
                    <legend></legend>
                </fieldset>
                </span>

            </div>

        </div>
        <div class="row">
            <div class="col-md-12 text-justify" style="font-size: 9pt;">


                <table style="width: 100%; font-size: 11pt;">
                    <tr>
                        <td style="width: 10px; text-align: center; vertical-align: top;">
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </td>
                        <td><b><asp:Literal ID="Literal18" Text="If I am selected for assistance under the activity mentioned above, I shall carry it out as per all guidelines applicable to the project and under the technical supervision of the project officials in the given time limit." runat="server"></asp:Literal><br />
                        </b>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px; text-align: center; vertical-align: top;">
                            <asp:CheckBox ID="CheckBox2" runat="server" />
                        </td>
                        <td>
                            <b><asp:Literal ID="Literal19" Text="I am aware that I shall be eligible to get the financial assistance as a reimbursement only after completing the activity as mentioned in 1 above. I am also aware that if I do not complete the activity as mentioned in 1 above, I shall not be eligible for the assistance." runat="server"></asp:Literal></b>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px; text-align: center; vertical-align: top;">
                            <asp:CheckBox ID="CheckBox3" runat="server" />
                        </td>
                        <td>
                            <b><asp:Literal ID="Literal20" Text="I am aware that the PMU has full rights to suspend and / or recover the assistance in case I am found to have indulged in fraud and / or misrepresentation to get the benefits under the project." runat="server"></asp:Literal></b>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px; text-align: center; vertical-align: top;">
                            <asp:CheckBox ID="CheckBox4" runat="server" /></td>
                        <td>
                            <b><asp:Literal ID="Literal21" Text="I hereby declare that I shall allow the representatives of the project to inspect my farms, equipment and records in relation to this application and will give my full cooperation for this purpose. And I will also take care of maintenance of
the given assets under the activity for minimum given period" runat="server"></asp:Literal></b>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px; text-align: center; vertical-align: top;">
                            <asp:CheckBox ID="CheckBox6" runat="server" /></td>
                        <td><b><asp:Literal ID="Literal22" Text="I confirm that, I have not taken benefit from any other Government Scheme for the applied activity / applied area." runat="server"></asp:Literal>  </b></td>
                    </tr>
                    <tr>
                        <td style="width: 10px; text-align: center; vertical-align: top;">
                            <asp:CheckBox ID="CheckBox5" runat="server" /></td>
                        <td><b><asp:Literal ID="Literal23" Text="All the information above is true as per my knowledge and belief. " runat="server"></asp:Literal> </b></td>
                    </tr>


                </table>




            </div>
        </div>
        <div class="row">
            <div class="col-md-12" style="text-align: center">
                <asp:Literal ID="LiteralMsg" runat="server"></asp:Literal>

            </div>
        </div>
        <div class="row">
            <div class="col-md-6" style="text-align: right">
                <asp:Button ID="btnSave" CssClass="btn btn-info focus" runat="server" Text="Submit" OnClick="btnSave_Click" />

            </div>
            <div class="col-md-6" style="text-align: left">
                <asp:Button ID="Button1" CssClass="btn btn-info focus" PostBackUrl="~/UsersTrans/ApplicationForm.aspx" CausesValidation="false" runat="server" Text="Cancel" />

            </div>
        </div>
    </div>


</asp:Content>
