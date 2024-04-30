<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndividualRegistrationEdit.aspx.cs" EnableEventValidation="false" MasterPageFile="~/UsersTrans/UserMaster.Master" Inherits="DBTPoCRA.Registration.IndividualRegistrationEdit" %>

<%@ Register Assembly="DropDownChosen" Namespace="CustomDropDown" TagPrefix="cc2" %>


<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>


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
            width: 180px;
        }

        /*.aspNetDisabled {
            background-image: url('/assets/img/dummy/navigationIn.jpg') !important;
            background-repeat: repeat-x;
        }*/

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

    <script>

        function CheckCast() {

            var a = document.getElementById("ContentPlaceHolder1_ddlCATEGORY").value;
            if (a == "0") {
                document.getElementById("Cast").style.display = "none";
            }
            else if (a == "5") {
                document.getElementById("Cast").style.display = "none";
            }
            else {
                document.getElementById("Cast").style.display = "";
            }
        }
        function getCheckedRadio() {
            debugger;
            var radio = document.getElementById("ContentPlaceHolder1_rdoHANDICAP");
            var radioButtons = radio.getElementsByTagName("input");
            var va = "";
            for (var x = 0; x < radioButtons.length; x++) {

                if (radioButtons[x].checked) {

                    va = radioButtons[x].value;

                }

            }

            if (va == "NO") {
                document.getElementById("HandiPer").style.display = "none";
            }
            else if (va == "YES") {
                document.getElementById("HandiPer").style.display = "";
            }
            else {
                document.getElementById("HandiPer").style.display = "none";
            }

        }

        function CheckCertificate() {
            var va = document.getElementById("ContentPlaceHolder1_txtDISABILITYPer").value; //
            if (va.length == 0) {
                document.getElementById("DivHANDICAPCERITIFICATE").style.display = "none";
            }
            else if (parseFloat(va) >= 40) {
                document.getElementById("DivHANDICAPCERITIFICATE").style.display = "";
            }
            else {
                document.getElementById("DivHANDICAPCERITIFICATE").style.display = "none";
            }
        }

        function CallToAll() {
            CheckCast();
            getCheckedRadio();
            CheckCertificate();
        }


    </script>
    <script>
        function ResetMsg(obj) {
            document.getElementById("MsgDiv").innerHTML = "";
            document.getElementById("MsgDiv").style.display = "none";
        }
    </script>
    <script>
        function check(obj) {
            var isValid = false;
            var regex = /^[a-zA-Z0-9\s]*$/;
            isValid = regex.test(obj.value);
            if (isValid == false) {
                alert('Please input alphanumeric characters only');
                obj.focus();
                return false;
            }
        }

        function submitCheck() {
            document.getElementById("btnOtp").disabled = true;
            setTimeout(function () {
                document.getElementById("btnOtp").disabled = false;
            }, 12000);

            counters();
        }

        function counters() {
            var counter = 0;
            var interval = setInterval(function () {
                counter++;
                span = document.getElementById("myTimer");
                span.innerHTML = " Resend in " + counter;
                if (counter == 12) {
                    // Display a login box
                    span = document.getElementById("myTimer");
                    span.innerHTML = "";
                    clearInterval(interval);
                }
            }, 1000);

        }


    </script>


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





    <link href="../assets/css/thickbox.css" rel="stylesheet" />
    <script src="../assets/js/thickbox.js"></script>
    <script>
        function ShowForms(ID) {
            tb_show('', '' + ID + '');
        }

    </script>



    <main>
        <div class="container">

            <div class="col-lg-12">



                <div id="DivIndividual" class="col-sm-12">
                    <asp:Menu ID="Menu1" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab btn"
                        StaticSelectedStyle-CssClass="selectedtab" CssClass="tabs" runat="server" OnMenuItemClick="Menu1_MenuItemClick">

                        <Items>

                            <asp:MenuItem Text="REGISTRATION DETAILS" Enabled="true" Value="0"></asp:MenuItem>
                            <asp:MenuItem Text="BASIC DETAILS" Value="1"></asp:MenuItem>
                            <asp:MenuItem Text="LAND DETAILS" Value="2"></asp:MenuItem>
                            <asp:MenuItem Text="DECLARATION" Value="3"></asp:MenuItem>
                        </Items>

                    </asp:Menu>
                    <div class="col-sm-12 form-group">
                        <asp:Literal ID="LiteralMsg" runat="server"></asp:Literal>
                        <asp:HiddenField ID="HiddenOtpDone" runat="server" />
                    </div>
                    <div class="col-sm-12 form-group">
                        <asp:Literal ID="LiteralName" runat="server"></asp:Literal>
                    </div>
                    <div class="form-group">
                        <asp:MultiView ID="multiViewEmployee" runat="server">
                            <asp:View ID="viewPersonalDetails" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="card no-b  no-r">
                                            <div class="card-body">
                                                <h5 class="card-title">
                                                    <asp:Literal ID="Literal3" Text="" runat="server"></asp:Literal></h5>

                                                <div class="form-row">
                                                    <div class="col-md-8">

                                                        <div class="form-row" style="display: none;">


                                                           <%-- <div class="form-group col-4">
                                                                <label for="name" class="col-form-label s-6">
                                                                    <i class="icon-fingerprint"></i>
                                                                    <asp:Literal ID="Literal2" Text="AADHAAR NUMBER" runat="server"></asp:Literal><em style="color: red">*</em></label>
                                                               
                                                                <asp:TextBox ID="txtAADHARNo" onblur="return (vhCheck());" Enabled="false" MaxLength="12" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                                    ErrorMessage="Invalid Card Number" CssClass="alert-danger" ControlToValidate="txtAADHARNo" ValidationExpression="^\d{12}$"
                                                                    Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                                                <p id="message"></p>
                                                            </div>--%>

                                                            <div class="form-group col-6">
                                                                <label for="name" class="col-form-label s-6 white-text">..<span id="myTimer" style="color: #0099CC; font-weight: bold;"></span></label>
                                                                <br />
                                                            </div>
                                                        </div>



                                                        <div class="form-row">
                                                            <div class="form-group col-6 m-0">
                                                                <label for="cnic" class="col-form-label s-12">
                                                                    <i class="icon-fingerprint"></i>
                                                                    <asp:Literal ID="Literal8" Text="NAME" runat="server"></asp:Literal><em style="color: red">*</em></label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="badge badge-danger badge-mini2"
                                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                                                                <asp:TextBox ID="txtName" Enabled="false" Style="width: 100% !important;" CssClass=" form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group col-6 m-0">
                                                                <label for="dob" class="col-form-label s-12">
                                                                    <i class="icon-calendar mr-2"></i>
                                                                    <asp:Literal ID="Literal7" Text="DATE OF BIRTH" runat="server"></asp:Literal></label>
                                                                <div class="form-row">
                                                                    <div class="form-group col-4 m-0">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="badge badge-danger badge-mini"
                                                                            runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" InitialValue="0" ControlToValidate="ddlDay"></asp:RequiredFieldValidator>
                                                                        <asp:DropDownList ID="ddlDay" CssClass="form-control light r-0 " runat="server"></asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-4 m-0">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="badge badge-danger badge-mini"
                                                                            runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" InitialValue="0" ControlToValidate="ddlMonth"></asp:RequiredFieldValidator>
                                                                        <asp:DropDownList ID="ddlMonth" CssClass="form-control light r-0 " runat="server"></asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-4 m-0">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="badge badge-danger badge-mini"
                                                                            runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" InitialValue="0" ControlToValidate="ddlYear"></asp:RequiredFieldValidator>
                                                                        <asp:DropDownList ID="ddlYear" CssClass="form-control light r-0 " runat="server"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-row">
                                                            <div class="form-group col-6 m-0">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" CssClass="badge badge-danger badge-mini2"
                                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" InitialValue="0" ControlToValidate="rdoGender"></asp:RequiredFieldValidator>
                                                                <label for="dob" class="col-form-label s-12">
                                                                    <i class="icon-fingerprint"></i>
                                                                    <asp:Literal ID="Literal6" Text="GENDER" runat="server"></asp:Literal></label>
                                                                <br>

                                                                <asp:DropDownList ID="rdoGender" Enabled="false" CssClass="form-control light r-0 " runat="server">
                                                                </asp:DropDownList>

                                                            </div>




                                                        </div>

                                                        <div class="form-row">
                                                            <div class="col-md-6" style="text-align: left;">
                                                                <label for="dob" class="col-form-label s-12">
                                                                    <i class="icon-fingerprint"></i>
                                                                    <asp:Literal ID="Literal50" Text="Any other Certificate related to beneficiary selected criteria" runat="server"></asp:Literal>
                                                                    <br />
                                                                    <asp:DropDownList ID="ddlAnyOtherCertificate" CssClass="form-control light r-0 " runat="server">
                                                                        <asp:ListItem Text="--NA--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Divorcee woman (घटस्फोटीत महिला)" Value="Divorcee woman (घटस्फोटीत महिला)"></asp:ListItem>
                                                                        <asp:ListItem Text="Widow (विधवा)" Value="Widow (विधवा)"></asp:ListItem>
                                                                        <asp:ListItem Text="others (इतर)" Value="others (इतर)"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </label>

                                                            </div>
                                                            <div class="col-md-6 m-0">
                                                                <label for="dob" class="col-form-label s-12">
                                                                    <i class="icon-fingerprint"></i>
                                                                    <asp:Label ID="Label7" runat="server" Font-Size="12px" Text="UPLOAD CERITIFICATE"></asp:Label>
                                                                </label>
                                                                <br />
                                                                <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileAnyOtherCertificate" Style="margin-top: 7px" runat="server" />

                                                                        <asp:Label ID="LabelAnyOtherCertificate" runat="server" Text=""></asp:Label>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnSaveAAudhar" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>

                                                            </div>
                                                        </div>



                                                    </div>
                                                    <div class="col-md-3 offset-md-1">
                                                        <label for="name" class="col-form-label s-6 white-text">.</label><br />

                                                        <div class="dropzone dropzone-file-area" id="fileUpload">
                                                            <div class="dz-default dz-message">

                                                                <div>
                                                                    <asp:Literal Visible="false" ID="Literal5" Text="Photo as on Aadhar Card" runat="server"></asp:Literal>
                                                                    <asp:Image ID="ImageTagId" Width="100px" Height="100px" AlternateText="Photo as on Aadhar Card" runat="server" />

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                            <hr>
                                            <div class="card-body center">
                                                <%--  <button type="submit" class="btn btn-primary btn-lg"><i class="icon-save mr-2"></i>Continue</button>--%>
                                                <asp:Button ID="btnSaveAAudhar" CssClass="btn btn-primary btn-lg" runat="server" Text="Submit" OnClick="btnSaveAAudhar_Click" />

                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:View>
                            <asp:View ID="viewContactDetails" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="card no-b  no-r">
                                            <div class="card-body">
                                                <h5 class="card-title">
                                                    <asp:Literal ID="Literal4" Text="BASIC DETAILS" runat="server"></asp:Literal></h5>
                                                <div class="form-row">
                                                    <div class="col-md-12">

                                                        <div class="form-row">

                                                            <div class="col-sm-6 form-group">
                                                                <label>
                                                                    <asp:Literal ID="Literal10" Text="HOUSE NO." runat="server"></asp:Literal>
                                                                </label>
                                                                <br />

                                                                <%-- <input type="text" id="txtHOUSENo" runat="server" class="form-control r-0 light s-12" />--%>
                                                                <asp:TextBox ID="txtHouseNo" MaxLength="50" CssClass=" form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="badge badge-danger badge-mini2"
                                                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtHouseNo"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                            <div class="col-sm-6 form-group">
                                                                <label>
                                                                    <asp:Literal ID="Literal11" Text="STREET NAME" runat="server"></asp:Literal></label><br />

                                                                <%--<input type="text" id="txtStreetNo" runat="server" class="form-control r-0 light s-12" />--%>
                                                                <asp:TextBox ID="txtStreetNo" MaxLength="150" CssClass=" form-control r-0 light s-12" runat="server"></asp:TextBox>

                                                            </div>
                                                        </div>


                                                        <div class="form-row">

                                                            <div class="col-sm-6 form-group">
                                                                <label>
                                                                    <asp:Literal ID="Literal12" Text="DISTRICT" runat="server"></asp:Literal>
                                                                    <em style="color: red">*</em></label><br />

                                                                <cc2:DropDownListChosen ID="ddlDISTRICT" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match." AutoPostBack="True" OnSelectedIndexChanged="ddlDISTRICT_SelectedIndexChanged">
                                                                </cc2:DropDownListChosen>



                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="badge badge-danger badge-mini2" InitialValue="0"
                                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlDISTRICT"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="col-sm-6 form-group">
                                                                <label>
                                                                    <asp:Literal ID="Literal13" Text="TALUKA" runat="server"></asp:Literal>
                                                                    <em style="color: red">*</em>
                                                                </label>
                                                                <br />

                                                                <cc2:DropDownListChosen ID="ddlTALUKA" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match." AutoPostBack="True" OnSelectedIndexChanged="ddlTALUKA_SelectedIndexChanged">
                                                                </cc2:DropDownListChosen>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="badge badge-danger badge-mini2" InitialValue="0"
                                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlTALUKA"></asp:RequiredFieldValidator>

                                                            </div>



                                                        </div>
                                                        <div class="form-row">

                                                            <div class="col-sm-3 form-group">
                                                                <label>
                                                                    <asp:Literal ID="Literal14" Text="POST" runat="server"></asp:Literal>
                                                                    <%--  <em style="color: red">*</em>--%>
                                                                </label>
                                                                <br />

                                                                <cc2:DropDownListChosen ID="ddlPOST" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match." AutoPostBack="True" OnSelectedIndexChanged="ddlPOST_SelectedIndexChanged">
                                                                </cc2:DropDownListChosen>

                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="badge badge-danger badge-mini2" InitialValue="0"
                                                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlPOST"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                            <div class="col-sm-3 form-group">
                                                                <label>
                                                                    <asp:Literal ID="Literal15" Text="PIN CODE" runat="server"></asp:Literal>
                                                                    <em style="color: red">*</em>
                                                                </label>
                                                                <br />
                                                                <asp:TextBox ID="txtPostPin" CssClass="form-control r-0 light s-12" MaxLength="12" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-3 form-group">
                                                                <label>
                                                                    <asp:Literal ID="Literal16" Text="VILLAGE" runat="server"></asp:Literal>
                                                                    <em style="color: red">*</em></label><br />


                                                                <cc2:DropDownListChosen ID="ddlVILLAGE" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match." AutoPostBack="True" OnSelectedIndexChanged="ddlVILLAGE_SelectedIndexChanged">
                                                                </cc2:DropDownListChosen>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" CssClass="badge badge-danger badge-mini2" InitialValue="0"
                                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlVILLAGE"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-sm-3 form-group" style="display: none;">
                                                                <label>
                                                                    <asp:Literal ID="Literal17" Text="CLUSTER CODE" runat="server"></asp:Literal>
                                                                    <em style="color: red">*</em>
                                                                </label>
                                                                <br />
                                                                <asp:TextBox ID="txtCLUSTARCODE" ReadOnly="true" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>


                                                        <div class="form-row">
                                                            <div class="col-sm-4 form-group">
                                                                <label>
                                                                    <asp:Literal ID="Literal18" Text="MOBILE 1" runat="server"></asp:Literal><em style="color: red">*</em></label>
                                                                <em style="display: none;">Your number is not linked with AADHAAR. Kindly link your AADHAAR with your mobile number and then try logging in or select biometric login and take assistance from AADHAAR KYC center.</em>

                                                                <asp:TextBox ID="txtMobile1" MaxLength="10" Enabled="false" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                                                    ErrorMessage="Enter numeric value only" CssClass="icon-cloud-error error alert-danger" ControlToValidate="txtMobile1" ValidationExpression="\d{10}" SetFocusOnError="True" Display="Dynamic"></asp:RegularExpressionValidator>

                                                            </div>
                                                            <div class="col-sm-4 form-group">
                                                                <label>
                                                                    <asp:Literal ID="Literal19" Text="MOBILE 2" runat="server"></asp:Literal></label>
                                                                <asp:TextBox ID="txtMobile2" MaxLength="10" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"
                                                                    ErrorMessage="Enter numeric value only" CssClass="icon-cloud-error error alert-danger" ControlToValidate="txtMobile2" ValidationExpression="\d{10}" SetFocusOnError="True" Display="Dynamic"></asp:RegularExpressionValidator>
                                                            </div>
                                                            <div class="col-sm-4 form-group">
                                                                <label>
                                                                    <asp:Literal ID="Literal20" Text="LANDLINE NO." runat="server"></asp:Literal></label>

                                                                <asp:TextBox ID="txtLandLine" MaxLength="12" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-row">
                                                            <div class="col-sm-6 form-group">
                                                                <label>
                                                                    <asp:Literal ID="Literal21" Text="EMAIL ID" runat="server"></asp:Literal></label><asp:Literal ID="LiteralEmail" Text="" runat="server"></asp:Literal>
                                                                <asp:TextBox ID="txtEmailID" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                                    ErrorMessage="Invalid EMAIL ID" CssClass="alert-danger" ControlToValidate="txtEmailID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                    Display="Dynamic" SetFocusOnError="True" Font-Italic="False"></asp:RegularExpressionValidator>
                                                            </div>
                                                            <div class="col-sm-6 form-group">
                                                                <label>
                                                                    <asp:Literal ID="Literal22" Text="PAN NO" runat="server"></asp:Literal></label>

                                                                <asp:TextBox ID="txtPAN" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                                                    ErrorMessage="Invalid Pan No." CssClass="alert-danger" ControlToValidate="txtPAN" ValidationExpression="[A-Z]{5}\d{4}[A-Z]{1}"
                                                                    Display="Dynamic" SetFocusOnError="True" Font-Italic="False"></asp:RegularExpressionValidator>
                                                            </div>


                                                        </div>
                                                        <div class="form-row">
                                                            <div class="col-sm-3 form-group">
                                                                <label>
                                                                    <asp:Literal ID="Literal23" Text="CATEGORY" runat="server"></asp:Literal>
                                                                    <em style="color: red">*</em>
                                                                </label>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" CssClass="badge badge-danger badge-mini2" InitialValue="0"
                                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlCATEGORY"></asp:RequiredFieldValidator>
                                                                <asp:DropDownList ID="ddlCATEGORY" onchange="CheckCast();" CssClass="form-control r-0 light s-12" runat="server"></asp:DropDownList>

                                                            </div>

                                                            <div id="Cast" style="display: none;" class="col-sm-3 form-group">

                                                                <asp:Label ID="Label1" runat="server" Font-Size="12px" Text="UPLOAD CERITIFICATE"></asp:Label>
                                                                <br />
                                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileCATEGORYCERITIFICATE" Style="margin-top: 7px" runat="server" />

                                                                        <asp:Label ID="LabelCatFile" runat="server" Text=""></asp:Label>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnBasic" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>

                                                            </div>
                                                            <div class="col-sm-3 form-group">
                                                                <asp:Label ID="Label4" Font-Size="12px" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="lblCATEGORY" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </div>


                                                        <div class="form-row">
                                                            <div class="col-sm-3 form-group">
                                                                <label>
                                                                    <asp:Literal ID="Literal24" Text="PHYSICALLY HANDICAPPED" runat="server"></asp:Literal>
                                                                    <em style="color: red">*</em>
                                                                </label>
                                                                <br />
                                                                <asp:RadioButtonList ID="rdoHANDICAP" onClick="getCheckedRadio()" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                    <asp:ListItem Text=" NO" Selected="True" Value="NO"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                            <div id="HandiPer" style="display: none;" class="col-sm-3 form-group">
                                                                <asp:Label ID="Label5" Font-Size="12px" runat="server" Text="DISABILITY PERCENTAGE"></asp:Label>
                                                                <br />
                                                                <asp:TextBox ID="txtDISABILITYPer" onkeyup="CheckCertificate();" onblur="CheckCertificate();" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtDISABILITYPer" MinimumValue="40" MaximumValue="100" runat="server" ErrorMessage="Invalid" Type="Integer"></asp:RangeValidator>
                                                            </div>
                                                            <div style="display: none;" id="DivHANDICAPCERITIFICATE" class="col-sm-3 form-group">
                                                                <asp:Label ID="Label3" Font-Size="12px" runat="server" Text="UPLOAD CERITIFICATE"></asp:Label>
                                                                <br />
                                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:FileUpload ID="FileHANDICAPCERITIFICATE" Style="margin-top: 7px" runat="server" />
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnBasic" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-sm-3 form-group">
                                                                <asp:Label ID="Label2" Font-Size="12px" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="lblHandiChetificate" runat="server" Text=""></asp:Label>
                                                                <span>
                                                                    <asp:Literal ID="Literal41" Text="Disability with 40% or more will be considered for scheme benefit. Kindly attached certificate from Government Medical Practitioner displaying the same." runat="server"></asp:Literal></span>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <hr>
                                                <div class="card-body center" style="text-align: center">
                                                    <%--<button type="submit" class="btn btn-primary btn-lg"><i class="icon-save mr-2"></i>Continue</button>--%>
                                                    <asp:Button ID="btnBasicBack" CssClass="btn btn-primary btn-lg" CausesValidation="false" ValidationGroup="0" runat="server" Text="Previous" OnClick="btnBasicBack_Click" />
                                                    &nbsp; &nbsp;
                                                                    <asp:Button ID="btnBasic" CssClass="btn btn-primary btn-lg" runat="server" Text="Submit" OnClick="btnBasic_Click" />


                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:View>
                            <asp:View ID="viewLand" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="card no-b  no-r">
                                            <div class="card-body">
                                                <h5 class="card-title">
                                                    <asp:Literal ID="Literal25" Text="LAND DETAILS" runat="server"></asp:Literal></h5>

                                                <div class="form-row">
                                                    <div class="col-md-3" style="text-align: left;">
                                                        <label for="dob" class="col-form-label s-12">
                                                            <i class="icon-fingerprint"></i>
                                                            <asp:Literal ID="Literal26" Text="Land Status" runat="server"></asp:Literal>
                                                        </label>
                                                        <br>
                                                        <asp:RadioButtonList ID="rdoLandStatus" runat="server" OnClientClick="javascript:return confirm ('Are you sure register with select land Status ? ')" OnSelectedIndexChanged="rdoLandStatus_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="YES"> &nbsp; WITH LAND</asp:ListItem>
                                                            <asp:ListItem Value="NO" Selected="True"> &nbsp; WITHOUT LAND</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>


                                                    <div id="divLandLess" runat="server" class="col-md-3 m-0">
                                                        <label for="dob" class="col-form-label s-12">
                                                            <i class="icon-fingerprint"></i>
                                                            <asp:Label ID="Label6" runat="server" Font-Size="12px" Text="UPLOAD CERITIFICATE"></asp:Label>
                                                        </label>
                                                        <br />
                                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:FileUpload ID="FileLandLessCertificate" Style="margin-top: 7px" runat="server" />

                                                                <asp:Label ID="LabelLandlessCert" runat="server" Text=""></asp:Label>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="btnLandNext" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </div>
                                                </div>

                                                <div class="form-row">
                                                    <div class="col-md-12">
                                                        <div id="divLand" runat="server" visible="false">

                                                            <div class="form-row">
                                                                <div class="form-group col-4 m-0">
                                                                    <label for="email" class="col-form-label s-12">
                                                                        <asp:Literal ID="Literal27" Text="DISTRICT" runat="server"></asp:Literal>
                                                                        <em style="color: red">*</em></label>

                                                                    <cc2:DropDownListChosen ID="ddlLANDDISTRICT" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match." AutoPostBack="True" OnSelectedIndexChanged="ddlLANDDISTRICT_SelectedIndexChanged">
                                                                    </cc2:DropDownListChosen>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" CssClass="badge badge-danger badge-mini2" InitialValue="0"
                                                                        runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlLANDDISTRICT"></asp:RequiredFieldValidator>

                                                                </div>
                                                                <div class="form-group col-4 m-0">
                                                                    <label for="phone" class="col-form-label s-12">
                                                                        <asp:Literal ID="Literal28" Text="TALUKA" runat="server"></asp:Literal>
                                                                        <em style="color: red">*</em></label>

                                                                    <cc2:DropDownListChosen ID="ddlLANDTALUKA" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match." AutoPostBack="True" OnSelectedIndexChanged="ddlLANDTALUKA_SelectedIndexChanged">
                                                                    </cc2:DropDownListChosen>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" CssClass="badge badge-danger badge-mini2" InitialValue="0"
                                                                        runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlLANDTALUKA"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="form-group col-4 m-0">
                                                                    <label for="mobile" class="col-form-label s-12">
                                                                        <asp:Literal ID="Literal29" Text="VILLAGE" runat="server"></asp:Literal>
                                                                        <em style="color: red">*</em></label>


                                                                    <cc2:DropDownListChosen ID="ddlLANDVILLAGE" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match.">
                                                                    </cc2:DropDownListChosen>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" CssClass="badge badge-danger badge-mini2" InitialValue="0"
                                                                        runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlLANDVILLAGE"></asp:RequiredFieldValidator>
                                                                </div>

                                                            </div>


                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <div class="form-row">
                                                                        <div class="form-group col-3 m-0" style="text-align: left">
                                                                            <label for="email" class="col-form-label s-12">
                                                                                <asp:Literal ID="Literal30" Text="8-A KHATA KRAMANK" runat="server"></asp:Literal></label>
                                                                            <br />
                                                                            <asp:TextBox ID="txtSURVEYNo8A" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                                            <asp:RangeValidator ID="RangeValidator2" Display="Dynamic" SetFocusOnError="True" CssClass="icon-cloud-error error alert-danger" ControlToValidate="txtSURVEYNo8A" MinimumValue="0" MaximumValue="99999999" runat="server" ErrorMessage="Invalid" Type="Integer"></asp:RangeValidator>
                                                                            <%--                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                                                                                ErrorMessage="Enter alphnumeric values only" CssClass="icon-cloud-error error alert-danger" ControlToValidate="txtSURVEYNo8A" ValidationExpression="[a-zA-Z0-9\\]*$" SetFocusOnError="True" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                                                                        </div>
                                                                        <div class="form-group col-4 m-0">


                                                                            <div class="form-row">
                                                                                <div class="form-group col-6 m-0">
                                                                                    <label for="email" class="col-form-label s-12">
                                                                                        <asp:Literal ID="Literal31" Text="Hectare" runat="server"></asp:Literal></label>
                                                                                    <br />
                                                                                    <asp:TextBox ID="txtLANDAREA8AH" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                                                    <asp:RangeValidator ID="RangeValidator3" Display="Dynamic" SetFocusOnError="True" CssClass="icon-cloud-error error alert-danger" ControlToValidate="txtLANDAREA8AH" MinimumValue="0" MaximumValue="999999" runat="server" ErrorMessage="Invalid" Type="Integer"></asp:RangeValidator>
                                                                                </div>

                                                                                <div class="form-group col-6 m-0">
                                                                                    <label class="col-form-label s-12">
                                                                                        <asp:Literal ID="Literal32" Text="Are" runat="server"></asp:Literal></label>
                                                                                    <br />
                                                                                    <asp:TextBox ID="txtLANDAREA8AA" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                                                    <asp:RangeValidator ID="RangeValidator4" Display="Dynamic" SetFocusOnError="True" CssClass="icon-cloud-error error alert-danger" ControlToValidate="txtLANDAREA8AA" MinimumValue="0" MaximumValue="99" runat="server" ErrorMessage="Invalid" Type="Integer"></asp:RangeValidator>
                                                                                </div>

                                                                            </div>


                                                                        </div>
                                                                        <div class="form-group col-3 m-0">
                                                                            <label for="phone" class="col-form-label s-12">
                                                                                <asp:Literal ID="Literal33" Text="FORM 8 A" runat="server"></asp:Literal></label><br />

                                                                            <asp:FileUpload ID="FileFORM8A" Style="width: 100%" runat="server" />


                                                                        </div>
                                                                        <div class="form-group col-2 m-0" style="text-align: left">
                                                                            <label for="email" class="col-form-label s-12">..</label>
                                                                            <br />
                                                                            <asp:Button ID="btnADD" runat="server" CssClass="btn btn-success" Text="ADD 8A" Width="150px" OnClick="btnADD_Click" />
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnADD" />

                                                                </Triggers>
                                                            </asp:UpdatePanel>




                                                            <asp:Panel ID="div7A" runat="server">
                                                                <div class="form-row">
                                                                    <div class="form-group col-2 m-0" style="text-align: left">
                                                                        <label for="email" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal34" Text="8-A KHATA KRAMANK" runat="server"></asp:Literal>
                                                                        </label>
                                                                        <br />
                                                                        <asp:DropDownList ID="ddl8A" CssClass="form-control r-0" ValidationGroup="P" runat="server"></asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-2 m-0" style="text-align: left">
                                                                        <label for="email" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal35" Text="7/12 SURVEY NUMBER" runat="server"></asp:Literal></label>
                                                                        <br />
                                                                        <asp:TextBox ID="txtSURVEYNo712" MaxLength="15" ValidationGroup="P" CssClass="form-control r-0" runat="server"></asp:TextBox>
                                                                        <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                                                                            ErrorMessage="Enter alphnumeric values only" CssClass="icon-cloud-error error alert-danger" ControlToValidate="txtSURVEYNo712" ValidationExpression="[a-zA-Z0-9\\]*$" SetFocusOnError="True" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                                                                    </div>
                                                                    <div class="form-group col-3 m-0">


                                                                        <div class="form-row">
                                                                            <div class="form-group col-6 m-0">
                                                                                <label for="email" class="col-form-label s-12">
                                                                                    <asp:Literal ID="Literal36" Text="Hectare" runat="server"></asp:Literal></label>
                                                                                <br />
                                                                                <asp:TextBox ID="txtLANDAREA712H" CssClass="form-control r-0 light s-12" ValidationGroup="P" runat="server"></asp:TextBox>
                                                                                <asp:RangeValidator ID="RangeValidator5" ValidationGroup="P" CssClass="icon-cloud-error error alert-danger" ControlToValidate="txtLANDAREA712H" MinimumValue="0" MaximumValue="9999" runat="server" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Invalid" Type="Integer"></asp:RangeValidator>

                                                                            </div>

                                                                            <div class="form-group col-6 m-0">
                                                                                <label for="email" class="col-form-label s-12">
                                                                                    <asp:Literal ID="Literal37" Text="Are" runat="server"></asp:Literal></label>
                                                                                <br />
                                                                                <asp:TextBox ID="txtLANDAREA712A" CssClass="form-control r-0 light s-12" ValidationGroup="P" runat="server"></asp:TextBox>
                                                                                <asp:RangeValidator ID="RangeValidator6" CssClass="icon-cloud-error error alert-danger" ValidationGroup="P" ControlToValidate="txtLANDAREA712A" MinimumValue="0" MaximumValue="99" runat="server" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Invalid" Type="Integer"></asp:RangeValidator>

                                                                            </div>

                                                                        </div>


                                                                    </div>
                                                                    <div class="form-group col-3 m-0">
                                                                        <label for="email" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal38" Text="7 / 12 Extracts" runat="server"></asp:Literal>
                                                                        </label>
                                                                        <br />
                                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <asp:FileUpload ID="File712" Style="width: 100%" runat="server" />
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:PostBackTrigger ControlID="btnAdd712" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                        <br />
                                                                        <label for="email" class="col-form-label s-12"></label>
                                                                    </div>
                                                                    <div class="form-group col-2 m-0" style="text-align: left">
                                                                        <label for="email" class="col-form-label s-12">..</label>
                                                                        <br />
                                                                        <asp:Button ID="btnAdd712" CssClass="btn btn-success" runat="server" Text="ADD  7/12" Width="150px" OnClick="btnAdd712_Click" ValidationGroup="P" />
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>


                                                            <div class="form-row">
                                                                <div class="form-group col-12 m-0">
                                                                </div>
                                                            </div>

                                                            <div class="form-row" style="text-align: right">
                                                                <br />
                                                                <div class="form-group col-12 m-0">
                                                                    <asp:GridView ID="grdSubject" runat="server" DataKeyNames="LandID" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grdSubject_RowDataBound" GridLines="Horizontal" OnSelectedIndexChanged="grdSubject_SelectedIndexChanged" OnRowDeleting="grdSubject_RowDeleting">
                                                                        <Columns>
                                                                            <asp:TemplateField ItemStyle-Width="20px">
                                                                                <ItemTemplate>
                                                                                    <a href="JavaScript:StateCity('div<%# Eval("LandID") %>');">
                                                                                        <img alt="City" id="imgdiv<%# Eval("LandID") %>" src="../assets/img/icon/Plus.png" />
                                                                                    </a>
                                                                                    <div id="div<%# Eval("LandID") %>" style="display: none;">
                                                                                        <asp:GridView ID="grdChild" GridLines="none" runat="server" Font-Size="9pt" Width="100%" AutoGenerateColumns="false" DataKeyNames="LandID"
                                                                                            OnSelectedIndexChanged="grdChild_SelectedIndexChanged"
                                                                                            CssClass="">
                                                                                            <Columns>
                                                                                                <asp:BoundField HeaderText="7/12 SURVEY NUMBER" DataField="SurveyNo712"></asp:BoundField>
                                                                                                <asp:BoundField HeaderText="7/12 HECTARE" DataField="Hectare712"></asp:BoundField>
                                                                                                <asp:BoundField HeaderText="7/12 ARE" DataField="Are712"></asp:BoundField>
                                                                                                <asp:TemplateField HeaderText="7 / 12 EXTRACTS">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Extracts712Doc") %>'></asp:Label>
                                                                                                        |
                                                                                                                         <asp:Label ID="Label8" ForeColor="Red" runat="server" Text='<%# Bind("AddNEW") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>

                                                                                                <asp:TemplateField ShowHeader="False">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="LinkButton11" runat="server" CausesValidation="False" OnClientClick="javascript:return confirm ('Are you sure to Delete this record permanently ? ')" CommandName="Select" Text="<i class='s-30 icon-delete_forever'></i>"></asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>

                                                                                            </Columns>
                                                                                            <HeaderStyle BackColor="WhiteSmoke" />
                                                                                        </asp:GridView>
                                                                                    </div>
                                                                                </ItemTemplate>

                                                                                <ItemStyle Width="20px"></ItemStyle>
                                                                            </asp:TemplateField>

                                                                            <asp:BoundField HeaderText="DISTRICT" DataField="Cityname"></asp:BoundField>
                                                                            <asp:BoundField DataField="TALUKA" HeaderText="TALUKA"></asp:BoundField>
                                                                            <asp:BoundField HeaderText="VILLAGE" DataField="VillageName"></asp:BoundField>
                                                                            <asp:BoundField HeaderText="8-A KHATA KRAMANK" DataField="AccountNumber8A"></asp:BoundField>
                                                                            <asp:BoundField HeaderText="HECTARE" DataField="Hectare8A"></asp:BoundField>
                                                                            <asp:BoundField HeaderText="ARE" DataField="Are8A"></asp:BoundField>
                                                                            <asp:TemplateField HeaderText="FORM 8 A">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Form8ADoc") %>'></asp:Label>
                                                                                    | 
                                                                                                    <asp:Label ID="Label8" ForeColor="Red" runat="server" Text='<%# Bind("AddNEW") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>





                                                                            <asp:TemplateField ShowHeader="False">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="Red" CausesValidation="False" CommandName="Delete" Text="Update Area"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField ShowHeader="False">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" OnClientClick="javascript:return confirm ('Are you sure to Delete this record permanently ? ')" CommandName="Select" Text="<i class='s-30 icon-delete_forever'></i>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>

                                                                        <HeaderStyle BackColor="#F3F3F3" />

                                                                    </asp:GridView>


                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-row">
                                                            <div class="form-group col-12 m-0" style="text-align: center">
                                                                <br />
                                                                <asp:Button ID="btnBasicBack0" runat="server" CausesValidation="false" CssClass="btn btn-primary btn-lg" OnClick="btnBasicBack_Click" Text="Previous" ValidationGroup="1" />
                                                                &nbsp;
                                                                                <asp:Button ID="btnLandNext" CssClass="btn btn-primary btn-lg" runat="server" Text="Submit" OnClick="btnLandNext_Click" CausesValidation="False" />
                                                                <br />
                                                                Note- Record will not save untill you click on submit button .
                                                            </div>
                                                        </div>

                                                        <div id="div8aupdate" runat="server" visible="false" style="padding: 5px; margin: 5px; border: 1px solid #0099FF; position: absolute; height: 220px; width: 374px; top: 8%; left: 239px; background-color: #669999;">
                                                            <table class="table">
                                                                <%--<tr>
                                                                    <td>8-A KHATA KRAMANK
                                                                    </td>
                                                                    <td>
                                                                        21245121
                                                                    </td>

                                                                </tr>--%>
                                                                <tr>
                                                                    <td>NEW HECTARE</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtUpHe" CssClass="form-control r-0 light s-12" ValidationGroup="PP" runat="server"></asp:TextBox>
                                                                        <asp:RangeValidator ID="RangeValidator7" ValidationGroup="PP"  ControlToValidate="txtUpHe" MinimumValue="0" MaximumValue="9999" runat="server" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Invalid" Type="Integer"></asp:RangeValidator>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="height: 2px;"></td>

                                                                </tr>
                                                                <tr>
                                                                    <td>NEW ARE</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtUpAre" CssClass="form-control r-0 light s-12" ValidationGroup="PP" runat="server"></asp:TextBox>
                                                                        <asp:RangeValidator ID="RangeValidator8"  ValidationGroup="PP" ControlToValidate="txtLANDAREA712A" MinimumValue="0" MaximumValue="99" runat="server" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Invalid" Type="Integer"></asp:RangeValidator>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="text-align:right;">
                                                                        <asp:Button ID="btnUpdate8a" CssClass="btn btn-success" runat="server" Text="Update" ValidationGroup="PP" OnClick="btnUpdate8a_Click" />
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:View>

                            <asp:View ID="viewDECLARATION" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="card no-b  no-r">
                                            <div class="card-body">
                                                <h5 class="card-title">
                                                    <asp:Literal ID="Literal39" Text="DECLARATION" runat="server"></asp:Literal></h5>

                                            </div>
                                            <div class="form-row">
                                                <div class="form-group col-1 m-0"></div>
                                                <div class="form-group col-10 m-0">
                                                    <label for="email" class="col-form-label s-12">
                                                        <asp:Literal ID="Literal40" Text="Declarations" runat="server"></asp:Literal>
                                                        <em style="color: red">*</em></label>


                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="vertical-align: top;">
                                                                <asp:CheckBox ID="CheckBox1" Checked="true" Enabled="false" runat="server" /></td>
                                                            <td>
                                                                <asp:Literal ID="Literal42" Text="I hereby consent to receive information about the project related activities through my mobile." runat="server"></asp:Literal></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: top;">
                                                                <asp:CheckBox ID="CheckBox2" Checked="true" Enabled="false" runat="server" /></td>
                                                            <td>
                                                                <asp:Literal ID="Literal43" Text="I, the holder of AADHAR number mentioned above, hereby give my consent to the Project Management Unit (PMU), Project on Climate Resilient Agriculture (PoCRA), to obtain my AADHAR number, name and finger print/Iris for authentication with UIDAI. The PMU, PoCRA has informed me that my identity information would only be used for the activities and interventions under PoCRA and also informed that my biometrics will not be stored / shared and will be submitted to CIDR only for the purpose of authentication." runat="server"></asp:Literal></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: top;">
                                                                <asp:CheckBox ID="CheckBox3" Checked="true" Enabled="false" runat="server" /></td>
                                                            <td>
                                                                <asp:Literal ID="Literal44" Text="I confirm that I have listed all the land details owned by me." runat="server"></asp:Literal></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: top;">
                                                                <asp:CheckBox ID="CheckBox4" Checked="true" Enabled="false" runat="server" /></td>
                                                            <td>
                                                                <asp:Literal ID="Literal45" Text="All the information provided by me is true and correct as per my knowledge and belief." runat="server"></asp:Literal></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: top;">
                                                                <asp:CheckBox ID="CheckBox5" Checked="true" Enabled="false" runat="server" /></td>
                                                            <td>
                                                                <asp:Literal ID="Literal46" Text="I agree to use the given mobile number for communications from PoCRA." runat="server"></asp:Literal></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: top;">
                                                                <asp:CheckBox ID="CheckBox6" Checked="true" Enabled="false" runat="server" /></td>
                                                            <td>
                                                                <asp:Literal ID="Literal47" Text="I understand that AADHAR based disbursal will be credited to my AADHAR linked bank account." runat="server"></asp:Literal></td>
                                                        </tr>
                                                        <%--<tr>
                                                                            <td>
                                                                                <asp:CheckBox ID="CheckBox7" runat="server" /></td>
                                                                            <td></td>
                                                                        </tr>--%>
                                                    </table>


                                                </div>
                                                <div class="form-group col-1 m-0"></div>
                                            </div>
                                            <div class="form-row">
                                                <div class="form-group col-12 m-0" style="text-align: center">
                                                    <asp:Button ID="btnBasicBack1" runat="server" CausesValidation="false" CssClass="btn btn-primary btn-lg" OnClick="btnBasicBack_Click" Text="Previous" ValidationGroup="2" />
                                                    &nbsp;
                                                                   

                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>

            </div>
        </div>
    </main>
</asp:Content>
