<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FarmerProfile.aspx.cs" Inherits="DBTPoCRA.UsersTrans.FarmerProfile" %>

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

    <link href="../assets/css/thickbox.css" rel="stylesheet" />
    <script src="../assets/js/thickbox.js"></script>
    <script>
        function ShowDetails(ID) {
            tb_show('', '' + ID + '');
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

                                    <%-- <div class="col-sm-12 form-group">
                                <asp:Label ID="LiteralMsg" runat="server"></asp:Label>
                            </div>--%>
                                    <div class="form-group">
                                        <div class="card no-b  no-r">
                                            <div class="">
                                                <h5 class="card-title btn bg-grey text-white font-weight-bolder" style="width: 100% !important">
                                                    <asp:Literal ID="Literal5" runat="server"></asp:Literal>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="card no-b  no-r">
                                            <div class="card-body">

                                                <fieldset style="border: 1px solid #CCCCCC">
                                                    <legend>
                                                        <asp:Literal ID="Literal6" Text="VERIFICATION" runat="server"></asp:Literal>
                                                    </legend>

                                                    <div class="form-row">
                                                        <div class="col-md-3">
                                                            <label>
                                                                <asp:Literal ID="Literal7" Text="Application Status" runat="server"></asp:Literal>
                                                            </label>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="badge badge-danger badge-mini2" InitialValue="0"
                                                                runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlApplicationStatus"></asp:RequiredFieldValidator>

                                                            <asp:DropDownList ID="ddlApplicationStatus" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlApplicationStatus_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3" style="text-align: left;">
                                                            <label>
                                                                <asp:Literal ID="Literal8" Text="Date" runat="server"></asp:Literal>
                                                            </label>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="badge badge-danger badge-mini2"
                                                                runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtDate"></asp:RequiredFieldValidator>
                                                            <asp:TextBox ID="txtDate" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <ajax:CalendarExtender CssClass="cal_Theme1" ID="Calendar1" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy"></ajax:CalendarExtender>
                                                        </div>
                                                        <div class="col-md-3 align-content-center">
                                                            <label>
                                                                <asp:Literal ID="Literal9" Text="Current Status" runat="server"></asp:Literal>
                                                            </label>
                                                            <br />
                                                            <span class="btn alert-danger">
                                                                <asp:Literal ID="Literal1" runat="server"></asp:Literal></span>
                                                        </div>

                                                         <div class="col-md-3 align-content-center">
                                                            
                                                            <br />
                                                             <asp:Literal ID="LiteralCom" runat="server"></asp:Literal>
                                                        </div>

                                                    </div>
                                                    <div id="divReg" runat="server" class="form-row">
                                                        <div class="col-md-12">
                                                            <label>
                                                                <asp:Literal ID="Literal10" Text="Reason" runat="server"></asp:Literal>
                                                            </label>
                                                            <br />
                                                            <asp:ListBox ID="chkReasons" runat="server" SelectionMode="Multiple" class="form-control light listbox sumo"></asp:ListBox>
                                                        </div>

                                                    </div>


                                                    <div class="form-row">
                                                        <div class="col-md-8">
                                                            <label>
                                                                <asp:Literal ID="Literal11" Text="Remark if any" runat="server"></asp:Literal>
                                                            </label>
                                                            <br />
                                                            <asp:TextBox ID="txtResion" TextMode="MultiLine" Height="40px" CssClass="form-control" runat="server"></asp:TextBox>
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


                                   
                                    <div id="divFarmer" runat="server">
                                        <asp:Panel ID="pHeader" runat="server" CssClass="cpHeader">
                                            <asp:Label ID="lblText" runat="server" />

                                        </asp:Panel>

                                        <ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="Panel1" CollapseControlID="pHeader" ExpandControlID="pHeader"
                                            Collapsed="true" TextLabelID="lblText" CollapsedText="Click to Show Registration details." ExpandedText="Click to Hide Registration Details."
                                            CollapsedSize="0">
                                        </ajax:CollapsiblePanelExtender>

                                        <asp:Panel ID="Panel1" Width="99%" Height="100%" ScrollBars="None" runat="server">
                                            <div class="form-group">
                                                <div class="card no-b  no-r">
                                                    <div class="card-body">

                                                        <fieldset style="border: 1px solid #CCCCCC">
                                                            <legend>
                                                                <asp:Literal ID="Literal12" Text="BASIC DETAILS" runat="server"></asp:Literal>
                                                            </legend>

                                                            <table class="table" style="width: 100%; table-layout: fixed">
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="Literal2" Text="" runat="server"></asp:Literal>

                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblAadharNu" Visible="false" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="Literal4" Text="FARMER NAME " runat="server"></asp:Literal>
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblName" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>

                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="Literal13" Text="GENDER :" runat="server"></asp:Literal></label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblGENDER" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="Literal3" Text="DATE OF BIRTH :" runat="server"></asp:Literal></label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblDOB" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr id="TrComOnly" visible="false" runat="server">

                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="Literal14" Text="AUTHORISED PERSON MOBILE :" runat="server"></asp:Literal></label></td>
                                                                    <td>
                                                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="Literal15" Text="AUTHORISED PERSON EMAILID" runat="server"></asp:Literal>
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label3" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>


                                                                <tr id="TrCat" runat="server">
                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="Literal16" Text="CATEGORY:" runat="server"></asp:Literal>
                                                                        </label>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblCategy" runat="server"></asp:Label>
                                                                        &nbsp;&nbsp;
                                                        <asp:Label ID="lblCertificate" runat="server"></asp:Label>
                                                                    </td>

                                                                    <td></td>
                                                                </tr>

                                                            </table>



                                                        </fieldset>
                                                    </div>


                                                </div>
                                                <div class="card no-b  no-r">
                                                    <div class="card-body">

                                                        <fieldset style="border: 1px solid #CCCCCC">
                                                            <legend>
                                                                <asp:Literal ID="Literal17" Text="ADDRESS AND OTHER DETAILS" runat="server"></asp:Literal>
                                                            </legend>

                                                            <table class="table" style="width: 100%; table-layout: fixed">
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="Literal18" Text="HOUSE NO." runat="server"></asp:Literal>
                                                                        </label>
                                                                    </td>
                                                                    <td>

                                                                        <asp:Label ID="lblHouseNo" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="Literal19" Text="STREET NO." runat="server"></asp:Literal></label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblStreetNo" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="Literal20" Text="DISTRICT" runat="server"></asp:Literal>
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblDistrict" runat="server"></asp:Label>

                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            &nbsp;
                                                                            <asp:Literal ID="Literal21" Text="TALUKA" runat="server"></asp:Literal></label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblTaluka" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="Literal22" Text="POST" runat="server"></asp:Literal>
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblPost" runat="server"></asp:Label>

                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="Literal23" Text="PIN CODE" runat="server"></asp:Literal>
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblPinCode" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="Literal24" Text="VILLAGE" runat="server"></asp:Literal></label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblVillage" runat="server"></asp:Label>

                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            &nbsp;
                                                                            <asp:Literal ID="Literal25" Text="CLUSTER CODE" runat="server"></asp:Literal></label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCluster" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="Literal26" Text="MOBILE 1" runat="server"></asp:Literal></label></td>
                                                                    <td>

                                                                        <asp:Label ID="lblMobile" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="Literal27" Text="MOBILE 2" runat="server"></asp:Literal></label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblMobile2" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="Literal28" Text="LANDLINE NO." runat="server"></asp:Literal></label></td>
                                                                    <td>

                                                                        <asp:Label ID="lblLandLine" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="Literal29" Text="EMAIL ID" runat="server"></asp:Literal></label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="Literal30" Text="PAN NO" runat="server"></asp:Literal></label></td>
                                                                    <td>

                                                                        <asp:Label ID="lblPanNo" runat="server"></asp:Label>

                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr id="rdDis" runat="server">
                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="Literal31" Text="PHYSICALLY HANDICAP" runat="server"></asp:Literal>
                                                                        </label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblHANDICAP" runat="server"></asp:Label>

                                                                    </td>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="Literal32" Text="DISABILITY PERCENTAGE" runat="server"></asp:Literal>
                                                                        </label>
                                                                    </td>
                                                                    <td>

                                                                        <asp:Label ID="lblDisabilityPer" runat="server"></asp:Label>
                                                                        &nbsp;&nbsp; 
                                                        <asp:Label ID="lblHandiChetificate" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>



                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            <asp:Literal ID="LiteralLandless" Text="Landless Farmer Certificate" runat="server"></asp:Literal></label></td>
                                                                    <td>

                                                                        <asp:Label ID="Label4" runat="server"></asp:Label>

                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>

                                                            </table>





                                                        </fieldset>

                                                    </div>
                                                </div>
                                                <div id="divLand" runat="server" class="card no-b  no-r">
                                                    <div class="card-body">
                                                        <h5 class="card-title"></h5>
                                                        <fieldset style="border: 1px solid #CCCCCC">
                                                            <legend>
                                                                <asp:Literal ID="Literal33" Text="LAND DETAILS" runat="server"></asp:Literal>
                                                            </legend>
                                                            <div class="form-row">
                                                                <div class="col-md-2">
                                                                    <asp:Literal ID="Literal34" Text="Land Status" runat="server"></asp:Literal>
                                                                </div>
                                                                <div class="col-md-4" style="text-align: left;">
                                                                    <asp:Label ID="lblLandStatus" runat="server"></asp:Label>
                                                                </div>
                                                                <div class="col-md-6">
                                                                </div>
                                                            </div>
                                                            <div class="form-row">
                                                                <div class="col-md-12">
                                                                    <div>
                                                                        <div class="form-row" style="text-align: right">
                                                                            <br />
                                                                            <div class="form-group col-12 m-0 table-responsive" style="overflow: auto">
                                                                                <asp:GridView ID="grdSubject" runat="server" DataKeyNames="LandID,RegistrationID" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grdSubject_RowDataBound"
                                                                                    CssClass="" GridLines="Horizontal">
                                                                                    <Columns>
                                                                                        <asp:TemplateField ItemStyle-Width="20px">
                                                                                            <ItemTemplate>
                                                                                                <a href="JavaScript:StateCity('div<%# Eval("LandID") %>');">
                                                                                                    <img alt="City" id="imgdiv<%# Eval("LandID") %>" src="../assets/img/icon/Plus.png" />
                                                                                                </a>
                                                                                                <div id="div<%# Eval("LandID") %>" style="display: none;">
                                                                                                    <asp:GridView ID="grdChild" GridLines="none" runat="server" Font-Size="9pt" Width="100%" AutoGenerateColumns="false" DataKeyNames=""
                                                                                                        CssClass="">
                                                                                                        <Columns>
                                                                                                            <asp:BoundField HeaderText="SURVEY NUMBER" DataField="SurveyNo712"></asp:BoundField>
                                                                                                            <asp:BoundField HeaderText="7/12 HECTARE" DataField="Hectare712"></asp:BoundField>
                                                                                                            <asp:BoundField HeaderText="7/12 ARE" DataField="Are712"></asp:BoundField>
                                                                                                            <asp:TemplateField HeaderText="7 / 12 EXTRACTS">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Extracts712Doc") %>'></asp:Label>
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
                                                                                        <asp:BoundField HeaderText="ACCOUNT NO." DataField="AccountNumber8A"></asp:BoundField>
                                                                                        <asp:BoundField HeaderText="HECTARE" DataField="Hectare8A"></asp:BoundField>
                                                                                        <asp:BoundField HeaderText="ARE" DataField="Are8A"></asp:BoundField>
                                                                                        <asp:TemplateField HeaderText="FORM 8 A">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Form8ADoc") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>


                                                                                    </Columns>

                                                                                    <HeaderStyle BackColor="#F3F3F3" />

                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </div>




                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                    </div>
                                                </div>
                                                <div class="card no-b  no-r" style="display: none;">
                                                    <div class="card-body">
                                                        <h5 class="card-title"></h5>

                                                    </div>
                                                    <fieldset style="border: 1px solid #CCCCCC">
                                                        <legend>DECLARATION
                                                        </legend>
                                                        <div class="form-row">

                                                            <div class="form-group col-12 m-0">
                                                                <label for="email" class="col-form-label s-12">Declarations </label>
                                                                <ul id="ulFarmer" runat="server">

                                                                    <li>
                                                                        <asp:CheckBox ID="CheckBox1" Checked="true" runat="server" Enabled="false" />I hereby consent to receive information about the project related activities through my mobile</li>
                                                                    <li>
                                                                        <asp:CheckBox ID="CheckBox2" Checked="true" runat="server" Enabled="false" />I, the holder of AADHAR number mentioned above, hereby give my consent to the Project Management Unit (PMU), Project on Climate Resilient Agriculture (PoCRA), to obtain my AADHAR number, name and finger print/Iris for authentication with UIDAI. The PMU, PoCRA has informed me that my identity information would only be used for the activities and interventions under PoCRA and also informed that my biometrics will not be stored / shared and will be submitted to CIDR only for the purpose of authentication.</li>
                                                                    <li>
                                                                        <asp:CheckBox ID="CheckBox3" Checked="true" runat="server" Enabled="false" />I confirm that I have listed all the land details owned by me</li>
                                                                    <li>
                                                                        <asp:CheckBox ID="CheckBox4" Checked="true" runat="server" Enabled="false" />All the information provided by me is true and correct as per my knowledge and belief</li>
                                                                    <li>
                                                                        <asp:CheckBox ID="CheckBox5" Checked="true" runat="server" Enabled="false" />While registering mobile number of other person, user to accept terms & conditions which states “I agree to use the given mobile number for communications from PoCRA”  </li>
                                                                    <li>
                                                                        <asp:CheckBox ID="CheckBox6" Checked="true" runat="server" Enabled="false" />At the time of registering Bank Account, user to accept T&C stating “I understand that AADHAR based disbursal will be credited to my AADHAR linked bank account” </li>
                                                                    <li>
                                                                        <asp:CheckBox ID="CheckBox7" Checked="true" runat="server" Enabled="false" />Note to be added for Person with Disability selection saying “Disability with 40% or more will be considered for scheme benefit. Kindly attached certificate from Government Medical Practitioner displaying the same” </li>
                                                                    <li>
                                                                        <asp:CheckBox ID="CheckBox8" Checked="true" runat="server" Enabled="false" />In case mobile number is not linked, system to give pop up saying “your number is not linked with AADHAAR. Kindly link your AADHAAR with your mobile number and then try logging in or select biometric login and take assistance from AADHAAR KYC center” </li>

                                                                </ul>
                                                                <ul id="ulCom" visible="false" runat="server">
                                                                    <li>
                                                                        <asp:CheckBox ID="CheckBox9" Checked="true" runat="server" Enabled="false" />I hereby consent to receive information about the project related activities through my mobile</li>
                                                                    <li>
                                                                        <asp:CheckBox ID="CheckBox10" Checked="true" runat="server" Enabled="false" />All the information provided by me on behalf of Community is true and correct as per my knowledge and belief.</li>
                                                                    <li>
                                                                        <asp:CheckBox ID="CheckBox11" Checked="true" runat="server" Enabled="false" />At the time of registering Bank Account, user to accept T&C stating “I understand that disbursal will be credited to registered bank account of the community”</li>
                                                                </ul>

                                                            </div>

                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <fieldset style="border: 1px solid #CCCCCC">
                                            <legend>
                                                <asp:Literal ID="Literal35" Text="VERIFICATION" runat="server"></asp:Literal>
                                            </legend>
                                            <div class="form-row">
                                                <div class="col-md-12 table-responsive">
                                                    <label>
                                                        <asp:Literal ID="Literal36" Text="VERIFICATION LOG" runat="server"></asp:Literal>
                                                    </label>
                                                    <br />
                                                    <asp:GridView ID="grdLog" runat="server" AutoGenerateColumns="False" Width="100%"
                                                        CssClass="table table-bordered table-hover data-tables table-responsiv">
                                                        <Columns>

                                                            <asp:BoundField DataField="Date" HeaderText="Date" />


                                                            <asp:BoundField DataField="FullName" HeaderText="Updated By" />
                                                            <asp:BoundField DataField="Desig_Name" HeaderText="Hierarchy" />
                                                            <asp:BoundField DataField="Status" HeaderText="Status" />
                                                            <asp:TemplateField HeaderText="Reason">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Reason") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Remark" HeaderText="Remark" />
                                                        </Columns>
                                                        <PagerStyle Font-Bold="True" />
                                                    </asp:GridView>
                                                </div>

                                            </div>
                                        </fieldset>

                                    </div>



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
