<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UsersTrans/UserMaster.Master" CodeBehind="CommunityRegistrationProfile.aspx.cs" Inherits="DBTPoCRA.Registration.CommunityRegistrationProfile" %>

<%@ Register Assembly="DropDownChosen" Namespace="CustomDropDown" TagPrefix="cc2" %>


<%--<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        /* MultiView Tab Using Menu Control */
        .tabs {
            position: relative;
            top: 1px;
            z-index: 2;
        }

        .tab {
            border: 1px solid black;
            background-image: url('/assets/img/dummy/navigation.jpg');
            background-repeat: repeat-x;
            color: White;
            padding: 5px 5px;
            margin: 5px 5px;
            width: 150px;
        }

        .aspNetDisabled {
            background-image: url('/assets/img/dummy/navigationIn.jpg') !important;
            background-repeat: repeat-x;
        }

        .tabs a {
            padding: 10px;
            font-size: 11px;
            text-decoration: none;
        }

        .container {
            padding: 0px !important;
            margin: 0px !important;
            width: 100% !important;
            max-width: 100% !important;
        }


        /* MultiView Ends Here..*/
    </style>
    <script>
        function CallToAll() {
        }
    </script>
    <%-- <div id="app">
        <main>
            <div class="container">--%>

    <div class="col-lg-12">

        <div id="DivIndividual" class="col-sm-12">
            <asp:Menu ID="Menu1" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab btn"
                StaticSelectedStyle-CssClass="selectedtab" CssClass="tabs" runat="server" OnMenuItemClick="Menu1_MenuItemClick">

                <Items>

                    <asp:MenuItem Text=" <i class='icon-fingerprint'></i> REGISTRATION DETAILS" Enabled="true" Value="0"></asp:MenuItem>
                    <asp:MenuItem Text="<i class='icon-fingerprint'></i> REGISTERED ADDRESS" Enabled="true" Value="1"></asp:MenuItem>
                    <%-- <asp:MenuItem Text="ADDRESS" Enabled="true" Value="2"></asp:MenuItem>--%>
                    <asp:MenuItem Text="<i class='icon-fingerprint'></i> VCRMC BANK DETAILS" Enabled="true" Value="2"></asp:MenuItem>
                    <asp:MenuItem Text="<i class='icon-fingerprint'></i> DECLARATION" Enabled="true" Value="3"></asp:MenuItem>
                </Items>

            </asp:Menu>
            <div class="col-sm-12 form-group">
                <asp:Literal ID="LiteralMsg" Text="" runat="server"></asp:Literal>
            </div>
            <div class="form-group">
                <asp:MultiView ID="multiViewEmployee" ActiveViewIndex="0" runat="server">
                    <asp:View ID="viewPersonalDetails" runat="server">

                        <div class="card no-b  no-r">
                            <div class="card-body">
                                <h5 class="card-title"><asp:Literal ID="Literal30" Text="REGISTRATION DETAILS" runat="server"></asp:Literal></h5>

                                <div class="form-row">
                                    <div class="col-md-12">
                                        <div class="form-row">
                                            <%-- <div class="form-group col-6">--%>

                                            <div class="form-group col-4 m-0">
                                                <label for="name" class="col-form-label s-6"><i class="icon-fingerprint"></i><asp:Literal ID="Literal1" Text="GRAM PANCHAYAT CODE" runat="server"></asp:Literal><em style="color: red">*</em></label>
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="badge badge-danger badge-mini2"
                                                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" InitialValue="0" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlGramPanchayatCode"></asp:RequiredFieldValidator>
                                                                                <cc2:DropDownListChosen ID="ddlGramPanchayatCode" runat="server" CssClass="form-control" Visible="false" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match.">
                                                                                </cc2:DropDownListChosen>--%>
                                                <asp:TextBox ID="txtGramPanchayatCode" CssClass="form-control r-0 light s-12" runat="server" Enabled="false"></asp:TextBox>
                                            </div>

                                        </div>

                                        <div class="form-row">
                                            <div class="form-group col-4 m-0">
                                                <label for="cnic" class="col-form-label s-12"><i class="icon-fingerprint"></i><asp:Literal ID="Literal2" Text="AUTHORISED PERSON NAME" runat="server"></asp:Literal> <em style="color: red">*</em></label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="badge badge-danger badge-mini2"
                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtName" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-4 m-0">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" CssClass="badge badge-danger badge-mini2"
                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" InitialValue="0" ControlToValidate="rdoGender"></asp:RequiredFieldValidator>
                                                <label for="dob" class="col-form-label s-12"><asp:Literal ID="Literal3" Text="GENDER" runat="server"></asp:Literal></label>
                                                <br>

                                                <asp:DropDownList ID="rdoGender" CssClass="form-control light r-0 " runat="server">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                                    <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                                    <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>

                                        </div>
                                        <div class="form-row">
                                            <div class="form-group col-4 m-0">
                                                <label for="cnic" class="col-form-label s-12"><i class="icon-fingerprint"></i><asp:Literal ID="Literal4" Text="AUTHORISED PERSON MOBILE" runat="server"></asp:Literal><em style="color: red">*</em></label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="badge badge-danger badge-mini2"
                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtAuthMobileNo"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtAuthMobileNo" MaxLength="10" CssClass="form-control r-0 light s-12" runat="server" onkeyup="if (/\D/g.test(this.value)) this.value = this.value.replace(/\D/g,'')"></asp:TextBox>

                                            </div>
                                            <div class="form-group col-4 m-0">
                                                <label for="cnic" class="col-form-label s-12"><i class="icon-fingerprint"></i><asp:Literal ID="Literal5" Text="AUTHORISED PERSON EMAILID" runat="server"></asp:Literal><em style="color: red">*</em></label>

                                                <asp:TextBox ID="txtAuthMobileEmail" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                    ErrorMessage="Invalid EMAIL ID" CssClass="alert-danger" ControlToValidate="txtAuthMobileEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    Display="Dynamic" SetFocusOnError="True" Font-Italic="False"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <hr>
                            <div class="card-body center">
                                <%--  <button type="submit" class="btn btn-primary btn-lg"><i class="icon-save mr-2"></i>Continue</button>--%>
                                <asp:Button ID="btnSaveAAudhar" CssClass="btn btn-primary btn-lg" runat="server" Text="Continue" OnClick="btnSaveAAudhar_Click" />
                            </div>
                        </div>

                    </asp:View>
                    <asp:View ID="viewContactDetails" runat="server">

                        <div class="card no-b  no-r">
                            <div class="card-body">
                                <h5 class="card-title"><asp:Literal ID="Literal6" Text="REGISTERED ADDRESS" runat="server"></asp:Literal></h5>
                                <div class="form-row">
                                    <div class="col-md-12">

                                        <div class="form-row">

                                            <div class="col-sm-6 form-group">
                                                <label><asp:Literal ID="Literal7" Text="GRAM PANCHAYAT ADDRESS." runat="server"></asp:Literal> <em style="color: red">*</em></label><br />

                                                <%-- <input type="text" id="txtHOUSENo" runat="server" class="form-control r-0 light s-12" />--%>
                                                <asp:TextBox ID="txtHouseNo" MaxLength="50" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="badge badge-danger badge-mini2"
                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtHouseNo"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="col-sm-6 form-group">
                                                <label><asp:Literal ID="Literal8" Text="STREET NO." runat="server"></asp:Literal></label><br />

                                                <%--<input type="text" id="txtStreetNo" runat="server" class="form-control r-0 light s-12" />--%>
                                                <asp:TextBox ID="txtStreetNo" MaxLength="150" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>

                                            </div>
                                        </div>


                                        <div class="form-row">

                                            <div class="col-sm-6 form-group">
                                                <label><asp:Literal ID="Literal9" Text="DISTRICT" runat="server"></asp:Literal> <em style="color: red">*</em></label><br />

                                                <cc2:DropDownListChosen ID="ddlDISTRICT" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match." AutoPostBack="True" OnSelectedIndexChanged="ddlDISTRICT_SelectedIndexChanged">
                                                </cc2:DropDownListChosen>



                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="badge badge-danger badge-mini2" InitialValue="0"
                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlDISTRICT"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="col-sm-6 form-group">
                                                <label><asp:Literal ID="Literal10" Text="TALUKA" runat="server"></asp:Literal> <em style="color: red">*</em> </label>
                                                <br />

                                                <cc2:DropDownListChosen ID="ddlTALUKA" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match." AutoPostBack="True" OnSelectedIndexChanged="ddlTALUKA_SelectedIndexChanged">
                                                </cc2:DropDownListChosen>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="badge badge-danger badge-mini2" InitialValue="0"
                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlTALUKA"></asp:RequiredFieldValidator>

                                            </div>



                                        </div>
                                        <div class="form-row">

                                            <div class="col-sm-3 form-group">
                                                <label><asp:Literal ID="Literal11" Text="POST" runat="server"></asp:Literal> <em style="color: red">*</em> </label>
                                                <br />

                                                <cc2:DropDownListChosen ID="ddlPOST" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match." AutoPostBack="True" OnSelectedIndexChanged="ddlPOST_SelectedIndexChanged">
                                                </cc2:DropDownListChosen>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="badge badge-danger badge-mini2" InitialValue="0"
                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlPOST"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-sm-3 form-group">
                                                <label><asp:Literal ID="Literal12" Text="PIN CODE" runat="server"></asp:Literal> <em style="color: red">*</em> </label>
                                                <br />
                                                <asp:TextBox ID="txtPostPin" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-3 form-group">
                                                <label><asp:Literal ID="Literal13" Text="VILLAGE" runat="server"></asp:Literal> <em style="color: red">*</em></label><br />


                                                <cc2:DropDownListChosen ID="ddlVILLAGE" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match." AutoPostBack="True" OnSelectedIndexChanged="ddlVILLAGE_SelectedIndexChanged">
                                                </cc2:DropDownListChosen>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" CssClass="badge badge-danger badge-mini2" InitialValue="0"
                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlVILLAGE"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-sm-3 form-group">
                                                <label><asp:Literal ID="Literal14" Text="CLUSTER CODE" runat="server"></asp:Literal> <em style="color: red">*</em> </label>
                                                <br />
                                                <asp:TextBox ID="txtCLUSTARCODE" ReadOnly="true" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                            </div>
                                        </div>


                                        <div class="form-row">
                                            <div class="col-sm-4 form-group">
                                                <label><asp:Literal ID="Literal15" Text="MOBILE 1" runat="server"></asp:Literal><em style="color: red">*</em></label>

                                                <asp:TextBox ID="txtMobile1" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 form-group">
                                                <label><asp:Literal ID="Literal16" Text="MOBILE 2" runat="server"></asp:Literal></label>
                                                <asp:TextBox ID="txtMobile2" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 form-group">
                                                <label><asp:Literal ID="Literal17" Text="LANDLINE NO." runat="server"></asp:Literal></label>

                                                <asp:TextBox ID="txtLandLine" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-row">
                                            <div class="col-sm-6 form-group">
                                                <label><asp:Literal ID="Literal18" Text="EMAIL ID" runat="server"></asp:Literal></label><asp:TextBox ID="txtEmailID" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                    ErrorMessage="Invalid EMAIL ID" CssClass="alert-danger" ControlToValidate="txtEmailID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    Display="Dynamic" SetFocusOnError="True" Font-Italic="False"></asp:RegularExpressionValidator>
                                            </div>
                                            <div class="col-sm-6 form-group">
                                                <label></label>

                                                <%-- <asp:TextBox ID="txtPAN" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                                                                    ErrorMessage="Invalid Pan No." CssClass="alert-danger" ControlToValidate="txtPAN" ValidationExpression="[A-Z]{5}\d{4}[A-Z]{1}"
                                                                                    Display="Dynamic" SetFocusOnError="True" Font-Italic="False"></asp:RegularExpressionValidator>--%>
                                            </div>


                                        </div>

                                    </div>
                                </div>
                                <hr>
                                <div class="card-body center" style="text-align: center">
                                    <%--<button type="submit" class="btn btn-primary btn-lg"><i class="icon-save mr-2"></i>Continue</button>--%>
                                    <asp:Button ID="btnBasic" CssClass="btn btn-primary btn-lg" runat="server" Text="Continue" OnClick="btnBasic_Click" />
                                </div>
                            </div>
                        </div>

                    </asp:View>
                    <asp:View ID="viewLand" runat="server">

                        <div class="card no-b  no-r">
                            <div class="card-body">
                                <h5 class="card-title"><asp:Literal ID="Literal19" Text="VCRMC BANK DETAILS" runat="server"></asp:Literal></h5>
                                <div class="form-row">
                                    <div class="col-sm-6 form-group">
                                        <label><asp:Literal ID="Literal20" Text="BANK A/c NO" runat="server"></asp:Literal> <em style="color: red">*</em> </label>
                                        <asp:TextBox ID="txtBankACNo" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>
                                    <div class="col-sm-6 form-group">
                                        <label><asp:Literal ID="Literal21" Text="A/c HOLDER NAME" runat="server"></asp:Literal> <em style="color: red">*</em> </label>
                                        <br />

                                        <asp:TextBox ID="txtBankHolder" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-row">
                                    <div class="col-sm-6 form-group">
                                        <label><asp:Literal ID="Literal22" Text="BANK NAME" runat="server"></asp:Literal> <em style="color: red">*</em> </label>
                                        <cc2:DropDownListChosen ID="ddlBankName" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match." AutoPostBack="True" OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged">
                                        </cc2:DropDownListChosen>

                                    </div>
                                    <div class="col-sm-6 form-group">
                                        <label><asp:Literal ID="Literal23" Text="BRANCH NAME" runat="server"></asp:Literal> <em style="color: red">*</em> </label>
                                        <br />

                                        <cc2:DropDownListChosen ID="ddlBranchName" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match." AutoPostBack="True" OnSelectedIndexChanged="ddlBranchName_SelectedIndexChanged">
                                        </cc2:DropDownListChosen>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="col-sm-6 form-group">
                                        <label><asp:Literal ID="Literal24" Text="IFSC CODE" runat="server"></asp:Literal> <em style="color: red">*</em> </label>
                                        <cc2:DropDownListChosen ID="ddlIFSC" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match.">
                                        </cc2:DropDownListChosen>

                                    </div>

                                </div>
                            </div>
                        </div>
                        <hr>
                        <div class="form-row">
                            <div class="form-group col-12 m-0" style="text-align: center">
                                <asp:Button ID="btnLandNext" CssClass="btn btn-primary btn-lg" runat="server" Text="Continue" OnClick="btnLandNext_Click" />
                            </div>
                        </div>

                    </asp:View>

                    <asp:View ID="viewDECLARATION" runat="server">

                        <div class="card no-b  no-r">
                            <div class="card-body">
                                <h5 class="card-title"><asp:Literal ID="Literal25" Text="DECLARATION" runat="server"></asp:Literal></h5>

                            </div>
                            <div class="form-row">
                                <div class="form-group col-1 m-0"></div>
                                <div class="form-group col-10 m-0">
                                    <label for="email" class="col-form-label s-12"><asp:Literal ID="Literal26" Text="Declarations" runat="server"></asp:Literal> <em style="color: red">*</em></label>
                                    <ul>

                                        <li>
                                            <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" Enabled="false" />
                                            <asp:Literal ID="Literal27" Text="I hereby consent to receive information about the project related activities through my mobile." runat="server"></asp:Literal>
                                            </li>
                                        <li>
                                            <asp:CheckBox ID="CheckBox2" runat="server" Checked="true" Enabled="false" />
                                            <asp:Literal ID="Literal28" Text="All the information provided by me on behalf of Community is true and correct as per my knowledge and belief." runat="server"></asp:Literal>
                                            </li>
                                        <li>
                                            <asp:CheckBox ID="CheckBox3" runat="server" Checked="true" Enabled="false" />
                                            <asp:Literal ID="Literal29" Text="I understand that disbursal will be credited to registered bank account of the community." runat="server"></asp:Literal>
                                            </li>


                                    </ul>
                                </div>
                                <div class="form-group col-1 m-0"></div>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-12 m-0" style="text-align: center">
                                    <asp:Button ID="btnFinalSave" Visible="false" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnFinalSave_Click" />

                                </div>
                            </div>
                        </div>

                    </asp:View>
                </asp:MultiView>
            </div>
        </div>

    </div>
    <%--</div>
        </main>
    </div>--%>
</asp:Content>
