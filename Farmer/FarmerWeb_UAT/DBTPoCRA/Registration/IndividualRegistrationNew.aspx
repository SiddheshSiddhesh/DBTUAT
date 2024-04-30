<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndividualRegistrationNew.aspx.cs" Inherits="DBTPoCRA.Registration.IndividualRegistrationNew" %>

<%@ Register Assembly="DropDownChosen" Namespace="CustomDropDown" TagPrefix="cc2" %>


<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Individual/Farmer Registration</title>
    <link rel="stylesheet" href="../assets/css/app.css" />
    <script src="../assets/js/jquery.min.js"></script>
    <script src="../assets/js/chosen.jquery.js"></script>
    <script src="../assets/js/sweetalert.min.js"></script>
    <link href="../assets/css/sweetalert.css" rel="stylesheet" />
    <link rel="icon" href="../assets/img/basic/favicon.ico" type="image/x-icon" />
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
            /*background-image: url('/assets/img/dummy/navigationIn.jpg') !important;*/
            /*background-repeat: repeat-x;*/
            background-image: none;
            border: none;
            /*background-image: url('/assets/img/dummy/navigation2.jpg');*/
            /*background-repeat: repeat-x;*/
            color: darkgrey;
            padding: 0px 10px;
            margin: 0px 0px;
            /*width: 150px;*/
        }

        .tabs a {
            padding: 10px;
            font-size: 11px;
            text-decoration: none;
        }

        b {
            font-size: 15px !important;
        }

        .container {
            padding: 0px !important;
            margin: 0px !important;
            width: 100% !important;
            max-width: 100% !important;
        }

        .col-form-label {
            text-transform: uppercase !important;
        }

        /* MultiView Ends Here..*/
    </style>

    <script>

        function CheckCast() {

            var a = document.getElementById("ddlCATEGORY").value;
            if (a == "0") {
                document.getElementById("Cast").style.display = "none";
                document.getElementById('lblCATEGORY').innerHTML = "";
            }
            else if (a == "5") {
                document.getElementById("Cast").style.display = "none";
                document.getElementById('lblCATEGORY').innerHTML = "";
            }
            else {
                document.getElementById("Cast").style.display = "";
            }
        }
        function getCheckedRadio() {

            var radioButtons = document.getElementsByName("rdoHANDICAP");
            var va = "";
            for (var x = 0; x < radioButtons.length; x++) {

                if (radioButtons[x].checked) {

                    va = radioButtons[x].value;

                }

            }

            if (va == "NO") {
                document.getElementById("HandiPer").style.display = "none";
                document.getElementById('lblHandiChetificate').innerHTML = "";
            }
            else {
                document.getElementById("HandiPer").style.display = "";
            }

        }

        function CheckCertificate() {
            var va = document.getElementById("txtDISABILITYPer").value; //
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
            //document.getElementById("btnOtp").disabled = true;
            //setTimeout(function () {
            //    document.getElementById("btnOtp").disabled = false;
            //}, 12000);

            //counters();
        }

        //function counters() {
        //    var counter = 0;
        //    var interval = setInterval(function () {
        //        counter++;
        //        span = document.getElementById("myTimer");
        //        span.innerHTML = " Resend in " + counter;
        //        if (counter == 12) {
        //            // Display a login box
        //            span = document.getElementById("myTimer");
        //            span.innerHTML = "";
        //            clearInterval(interval);
        //        }
        //    }, 1000);

        //}


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

        $(document).ready(function () {
            // code here
            //debugger;
            var a = document.referrer;// window.history.state.prevUrl;// your previous url
            if (a.search("print") != -1) {
                window.location.href = "https://mumbaipolice.gov.in/OnlineComplaints?ps_id=0";
            }
            if (a.search("pvc") != -1) {
                window.location.href = "https://mumbaipolice.gov.in/OnlineComplaints?ps_id=0";
            }
            if (a.search("robo") != -1) {
                window.location.href = "https://mumbaipolice.gov.in/OnlineComplaints?ps_id=0";
            }
            if (a.search("seva") != -1) {
                window.location.href = "https://mumbaipolice.gov.in/OnlineComplaints?ps_id=0";
            }
            if (a.search("kya") != -1) {
                window.location.href = "https://mumbaipolice.gov.in/OnlineComplaints?ps_id=0";
            }
            if (a.search("eaadhar") != -1) {
                window.location.href = "https://mumbaipolice.gov.in/OnlineComplaints?ps_id=0";
            }
            if (a.search("online") != -1) {
                window.location.href = "https://mumbaipolice.gov.in/OnlineComplaints?ps_id=0";
            }
            if (a.search("card") != -1) {
                window.location.href = "https://mumbaipolice.gov.in/OnlineComplaints?ps_id=0";
            }
            if (a.search("secure") != -1) {
                window.location.href = "https://mumbaipolice.gov.in/OnlineComplaints?ps_id=0";
            }
            if (a.search("cyber") != -1) {
                window.location.href = "https://mumbaipolice.gov.in/OnlineComplaints?ps_id=0";
            }
            if (a.search("proway") != -1) {
                window.location.href = "https://mumbaipolice.gov.in/OnlineComplaints?ps_id=0";
            }
            //if (a == "") {
            //    window.location.href = "https://dbt.mahapocra.gov.in/";
            //}
            document.getElementById("txtPrev").value = a;//
        });


    </script>


    <script src="../assets/js/Verhoeff.js"></script>
    <script type="text/javascript">

        function vhCheck() {
            var strVal = document.getElementById('txtAADHARNo').value;
            if (strVal.length != 12) // Minimum length.
                return false;
            if (strVal.verhoeffCheck() == false) {
                document.getElementById("message").innerHTML = "Invalid Aadhaar number";
                document.getElementById("btnOtp").style.display = "none";
                message.style.color = "Red";
                document.getElementById('txtAADHARNo').focus();
                return false;
            }
            else {
                //document.getElementById("messageWait").innerHTML = "Please wait ...";
                document.getElementById("btnOtp").style.display = "";
                document.getElementById("message").innerHTML = "Valid aadhaar number";
                message.style.color = "Green";
                return true;

            }
        };


    </script>

    <script async src="https://www.googletagmanager.com/gtag/js?id=G-RH385J9ZQQ"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'G-RH385J9ZQQ');
    </script>



    <script>
        var params = "";
        var DivType = "";





        function Capture() {

            var DivType = $('#dllBiometric').val();

            if (DivType == "1") {
                //SecuGen
                Capture1();
            }
            else if (DivType == "2") {
                //NEXT
                Nextcapture();
            }
            else if (DivType == "3") {
                // marpho
                MarphoCapture();
            }
            else if (DivType == "4") {
                //Mantra
                Capture1();
            }

        }


        function Capture1() {


            var uri = "";

            var DivType = $('#dllBiometric').val();
            //alert(DivType);
            var params = "";

            var port = $('#Port').val();

            if (port == "") {
                alert('Kindly check your device.');
            }

            if (DivType == "1") {
                uri = "http://127.0.0.1:" + port + "/rd/capture";
                params = '<PidOptions ver=\"1.0\">' + '<Opts fCount=\"1\" fType=\"2\" iCount=\"\" iType=\"\" pCount=\"\" pType=\"\" format=\"0\" pidVer=\"2.0\" timeout=\"10000\" otp=\"\" wadh=\"RZ+k4w9ySTzOibQdDHPzCFqrKScZ74b3EibKYy1WyGw=\" posh=\"\"/>' + '</PidOptions>';

            }
            else if (DivType == "4") {
                uri = "http://127.0.0.1:" + port + "/rd/capture";
                params = '<PidOptions ver=\"1.0\">' + '<Opts fCount=\"1\" fType=\"2\" iCount=\"0\" iType=\"0\" env=\"P\" pCount=\"0\" pType=\"0\" format=\"0\" pidVer=\"2.0\" timeout=\"10000\" otp=\"\" wadh=\"RZ+k4w9ySTzOibQdDHPzCFqrKScZ74b3EibKYy1WyGw=\" posh=\"\"/>' + '</PidOptions>';
            }



            //alert(uri);

            var xmlhttp = new XMLHttpRequest();
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {

                    //alert(xmlhttp.responseText);
                    SucessInfo(xmlhttp.responseText);
                }
                else if (xmlhttp.status == 404) {
                    // alert(xmlhttp.responseText);
                    failCall(xmlhttp.status)
                }
                else if (xmlhttp.status == 503) {
                    alert("server Unavailable");
                }
            }
            xmlhttp.onerror = function () {
                failCall(xmlhttp.status);
            }
            xmlhttp.onabort = function () {
                alert("Aborted");
            }
            xmlhttp.open("CAPTURE", uri, true);
            //xmlhttp.send(encodeURIComponent(params));
            xmlhttp.send(params);
        }


        function MarphoCapture() {

            var port = $('#Port').val();

            var url = "http://127.0.0.1:" + port + "/capture";

            var PIDOPTS = '<PidOptions ver=\"1.0\">' + '<Opts fCount=\"1\" fType=\"2\" iCount=\"\" iType=\"\" pCount=\"\" pType=\"\" format=\"0\" pidVer=\"2.0\" timeout=\"10000\" otp=\"\" wadh=\"RZ+k4w9ySTzOibQdDHPzCFqrKScZ74b3EibKYy1WyGw=\" posh=\"\"/>' + '</PidOptions>';

            /*
            format=\"0\"     --> XML
            format=\"1\"     --> Protobuf
            */
            var xhr;
            var ua = window.navigator.userAgent;
            var msie = ua.indexOf("MSIE ");

            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) // If Internet Explorer, return version number
            {
                //IE browser
                xhr = new ActiveXObject("Microsoft.XMLHTTP");
            } else {
                //other browser
                xhr = new XMLHttpRequest();
            }

            xhr.open('CAPTURE', url, true);
            xhr.setRequestHeader("Content-Type", "text/xml");
            xhr.setRequestHeader("Accept", "text/xml");

            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4) {
                    var status = xhr.status;

                    if (status == 200) {
                        SucessInfo(xhr.responseText);
                    } else {

                        console.log(xhr.response);

                    }
                }

            };

            xhr.send(PIDOPTS);

        }

        function Nextcapture() {


            //var url = "http://127.0.0.1:" + port + "/rd/capture";
            var port = $('#Port').val();

            var url = "http://127.0.0.1:" + port + "/rd/capture";

            params = '<PidOptions ver="1.0"> <Opts fCount="1" fType="2" format="0" pidVer="2.0" timeout="20000" env="P" wadh="RZ+k4w9ySTzOibQdDHPzCFqrKScZ74b3EibKYy1WyGw=" posh="UNKNOWN" />';
            params += '</PidOptions>';

            var xhr;

            xhr = new XMLHttpRequest();

            xhr.open('CAPTURE', url, true);
            xhr.setRequestHeader("Content-Type", "text/xml");
            xhr.setRequestHeader("Accept", "text/xml");
            xhr.onreadystatechange = function () {
                //debugger;
                if (xhr.readyState == 4) {
                    SucessInfo(xhr.responseText);
                }
            };
            xhr.send(params);

        }


        function SucessInfo(result) {
            debugger;
            if (result != null) {
                if (result.length > 0) {
                    //document.getElementById("HiddenField1").value = result;
                    alert("Please remove your finger and let us process your request .");
                    AjaxCallToFunction(result);

                }
            }
        }

        function failCall(status) {

            /* 	
                If you reach here, user is probabaly not running the 
                service. Redirect the user to a page where he can download the
                executable and install it. 
            */
            alert("Check if RDSERVICE is running ");

        }


        function AjaxCallToFunction(result) {
            try {
                debugger;
                var ANumber = document.getElementById("txtAADHARNo").value;
                $.ajax({
                    type: "POST",
                    url: "IndividualRegistrationNew.aspx/GetData",
                    contentType: "application/json; charset=utf-8",
                    data: "{'result': '" + result + "','ANumber': '" + ANumber + "'}",
                    dataType: "json",
                    success: OnSuccess,
                    failure: function (response) {
                        alert("Error");
                    }
                });

            }
            catch (e) {
                // statements to handle any exceptions
                alert(e); // pass exception object to error handler
            }
        }

        function OnSuccess(response) {



            //debugger;
            if (response.d == "") {
                var btn = document.getElementById("btnBioKyc").click();
            }
            else {
                // alert(response.d);

            }


        }


    </script>

    <script>

        $(function () {
            $("#dllBiometric").change(function () {
                var selectedText = $(this).find("option:selected").text();
                var selectedValue = $(this).val();
                if (selectedValue != "0") {
                    discoverAvdm();
                }
            });
        });




        var OldPort = false;
        var finalUrl = "";

        function discoverAvdm() {


            // New

            GetCustomDomName = "127.0.0.1";

            var DivType = $('#dllBiometric').val();





            var SuccessFlag = 0;
            var primaryUrl = "http://" + GetCustomDomName + ":";

            try {
                var protocol = window.location.href;
                if (protocol.indexOf("https") >= 0) {
                    primaryUrl = "https://" + GetCustomDomName + ":";
                }
            } catch (e) { }


            url = "";

            $.support.cors = true;
            SuccessFlag = 0;
            for (var i = 11100; i <= 11105; i++) {
                //if (primaryUrl == "http://" + GetCustomDomName + ":" && OldPort == true) {
                //    i = "8005";
                //}
                //$("#lblStatus1").text("Discovering RD service on port : " + i.toString());

                var verb = "RDSERVICE";
                var err = "";

                var res;

                var httpStaus = false;
                var jsonstr = "";
                var data = new Object();
                var obj = new Object();



                $.ajax({

                    type: "RDSERVICE",
                    async: false,
                    crossDomain: true,
                    url: primaryUrl + i.toString(),
                    contentType: "application/json; charset=utf-8",
                    processData: false,
                    cache: false,
                    dataType: "text",


                    success: function (data) {

                        httpStaus = true;
                        res = { httpStaus: httpStaus, data: data };


                        //alert(data);
                        finalUrl = primaryUrl + i.toString();
                        var $doc = $.parseXML(data);


                        var CmbData1 = $($doc).find('RDService').attr('status');
                        var CmbData2 = $($doc).find('RDService').attr('info');

                        //$("#ddlAVDM").append('<option value=' + i.toString() + '>(' + CmbData1 + '-' + i.toString() + ')' + CmbData2 + '</option>')

                        if (CmbData1 == "READY") {
                            $('#Port').val(i.toString());
                            SuccessFlag = 1;
                        }

                    },
                    error: function (jqXHR, ajaxOptions, thrownError) {
                        if ($('#Port').val().length == 0) {
                            $('#Port').val(i.toString());
                            SuccessFlag = 1;
                            // alert(thrownError + '=' + ajaxOptions);
                        }
                    },

                });
            }

            if (SuccessFlag == 0) {
                alert("Connection failed Please try again.");
            }

            return res;
        }



        function Test() {
            discoverAvdm();
        }





        function wait(ms) {
            var start = new Date().getTime();
            var end = start;
            while (end < start + ms) {
                end = new Date().getTime();
            }
        }


        //var enableSubmit = function (ele) {
        //    $(ele).removeAttr("disabled");
        //}

        //$("#btnSendOTP").click(function () {
        //    var that = this;
        //    $(this).attr("disabled", true);
        //    setTimeout(function () { enableSubmit(that) }, 1000);
        //});



    </script>



    <style>
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 700px;
            border: 3px solid #0DA9D0;
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .body {
                min-height: 50px;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .footer {
                padding: 3px;
            }

            .modalPopup .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
            }

            .modalPopup .yes {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }
    </style>
</head>
<body oncontextmenu="return false">
    <form id="form1" runat="server" method="post" class="height-full responsive-phone">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:HiddenField ID="HiddenField1" runat="server" />


        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <ContentTemplate>
                <%--<script type="text/javascript">
                    var prm = Sys.WebForms.PageRequestManager.getInstance();
                    if (prm != null) {
                        prm.add_endRequest(function (sender, e) {
                            if (sender._postBackSettings.panelsToUpdate != null) {

                                onLoad();
                            }
                        });
                    };

                </script>--%>
                <div id="app">
                    <main>
                        <div class="container">
                            <div class="sticky">
                                <img alt="" style="width: 100%;" src="../assets/img/basic/HeaderImage.jpg" />
                                <div class="navbar navbar-expand navbar-dark d-flex justify-content-between bd-navbar blue-grey ">
                                    <div class="relative">
                                        <a href="#" data-toggle="offcanvas" class="paper-nav-toggle pp-nav-toggle">
                                            <i></i>
                                        </a>
                                    </div>
                                    <!--Top Menu Start -->
                                    <span style="font-size: large; font-weight: bold; color: #FFFFFF">
                                        <asp:Literal ID="Literal9" Text="Farmer Registration" runat="server"></asp:Literal></span>
                                    <div class="navbar-custom-menu p-t-10">
                                        <ul class="nav navbar-nav">
                                            <!-- Messages-->
                                            <li class="dropdown custom-dropdown messages-menu">
                                                <a href="https://dbt.mahapocra.gov.in" class="nav-link" data-toggle="dropdown">
                                                    <span>Home</span>
                                                </a>

                                            </li>
                                            <%-- <li class="dropdown custom-dropdown messages-menu">
                                                <a href="#" class="nav-link" data-toggle="dropdown">
                                                    <span>English </span>
                                                </a>

                                            </li>--%>
                                            <%-- <li class="dropdown custom-dropdown messages-menu">
                                                
                                                <span>
                                                    <asp:LinkButton ID="btnEnglish" CssClass="nav-link" CausesValidation="false" runat="server" OnClick="btnEnglish_Click1">English</asp:LinkButton>
                                                </span>
                                               

                                            </li>--%>
                                            <!-- Notifications -->
                                            <li class="dropdown custom-dropdown notifications-menu">
                                                <%--<a href="#" class=" nav-link" data-toggle="dropdown" aria-expanded="false">--%>
                                                <span>
                                                    <asp:LinkButton ID="btnMarathi" CssClass="nav-link" CausesValidation="false" runat="server" OnClick="btnMarathi_Click1">मराठी</asp:LinkButton>
                                                </span>
                                                <%-- </a>--%>
                                            </li>

                                        </ul>
                                    </div>

                                </div>
                            </div>
                            <div class="col-lg-12">

                                <div id="DivDisableRegistration" runat="server">
                                    <div style="text-align: center; padding: 10%; background-color: #fff; margin-top: 30px;">
                                        <h1>नवीन नोंदणी सुविधा सध्यस्थितीत उपलब्ध नाही</h1>
                                    </div>
                                </div>

                                <div id="DivIndividual" class="col-sm-12" runat="server">
                                    <asp:Menu ID="Menu1" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab btn"
                                        StaticSelectedStyle-CssClass="selectedtab" CssClass="tabs" runat="server" OnMenuItemClick="Menu1_MenuItemClick">

                                        <Items>

                                            <asp:MenuItem Text="REGISTRATION DETAILS" Enabled="true" Value="0"></asp:MenuItem>
                                            <asp:MenuItem Text="BASIC DETAILS" Enabled="false" Value="1"></asp:MenuItem>
                                            <%-- <asp:MenuItem Text="ADDRESS" Enabled="true" Value="2"></asp:MenuItem>--%>
                                            <asp:MenuItem Text="LAND DETAILS" Enabled="false" Value="2"></asp:MenuItem>
                                            <asp:MenuItem Text="DECLARATION" Enabled="false" Value="3"></asp:MenuItem>
                                        </Items>

                                    </asp:Menu>
                                    <div class="col-sm-12 form-group">
                                        <asp:Literal ID="LiteralMsg" runat="server"></asp:Literal>
                                        <asp:HiddenField ID="HiddenOtpDone" runat="server" />
                                    </div>
                                    <div class="col-sm-12 form-group">
                                        <asp:Literal ID="LiteralName" runat="server"></asp:Literal>


                                        <asp:Literal ID="Literal51" Visible="false" runat="server"></asp:Literal>

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
                                                                    <div class="col-md-12">
                                                                        <div class="form-row">
                                                                            <div class="form-group col-3">
                                                                                <label for="name" class="col-form-label s-6">

                                                                                    <asp:Literal ID="Literal52" Text="Mobile Number" runat="server"></asp:Literal><em style="color: red">*</em></label>
                                                                                <br />
                                                                                <asp:TextBox ID="txtMobileOtp" autocomplete="off" MaxLength="10" CssClass="form-control" ValidationGroup="O" runat="server"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="badge badge-danger badge-mini2"
                                                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="O" Display="Dynamic" ToolTip="Required" ControlToValidate="txtMobileOtp"></asp:RequiredFieldValidator>

                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                                                                    ErrorMessage="Invalid Mobile Number" CssClass="alert-danger" ValidationGroup="O" ControlToValidate="txtMobileOtp" ValidationExpression="\d{10}"
                                                                                    Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                                                            </div>

                                                                            <div class="form-group col-3" runat="server" id="divCapcha">
                                                                                <div class="row">

                                                                                    <div class="col-6">
                                                                                        <br />
                                                                                        <asp:Image ID="imgCaptcha" ImageUrl="#" runat="server" />
                                                                                    </div>
                                                                                    <div class="col-6" style="text-align: left !important;">
                                                                                        <br />
                                                                                        <input type="text" id="txtVerificationCode" runat="server" autocomplete="off" class="input-text" placeholder="Captcha" maxlength="7" required />
                                                                                        <br />
                                                                                        <asp:LinkButton ID="LinkButton1" CausesValidation="false" runat="server" OnClick="LinkButton1_Click">Refresh Captcha</asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group col-3" style="text-align: right;">
                                                                                <label for="name" class="col-form-label s-6" style="color: white;">

                                                                                    <asp:Literal ID="Literal54" Text=".." runat="server"></asp:Literal></label>
                                                                                <br />
                                                                                <asp:Button ID="btnSendOTP" ValidationGroup="O" runat="server" CssClass="btn btn-success" Text="Get OTP for Mobile Verification" OnClick="btnSendOTP_Click" />
                                                                            </div>
                                                                            <div class="form-group col-3">
                                                                                <div class="form-row" runat="server" id="divOTP" visible="false">
                                                                                    <div class="form-group col-6">
                                                                                        <label for="name" class="col-form-label s-6">

                                                                                            <asp:Literal ID="Literal53" Text="Enter OTP" runat="server"></asp:Literal><em style="color: red">*</em></label>
                                                                                        <br />
                                                                                        <asp:TextBox ID="txtOTPNew" autocomplete="off" CssClass="form-control" MaxLength="6" ValidationGroup="O" runat="server"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="badge badge-danger badge-mini2"
                                                                                            runat="server" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="O" Display="Dynamic" ToolTip="Required"
                                                                                            ControlToValidate="txtOTPNew"></asp:RequiredFieldValidator>


                                                                                    </div>
                                                                                    <div class="form-group col-6" style="text-align: left;">
                                                                                        <label for="name" class="col-form-label s-6" style="color: white;">

                                                                                            <asp:Literal ID="Literal55" Text=".." runat="server"></asp:Literal></label>
                                                                                        <br />
                                                                                        <asp:Button ID="btnVerifyOTP" ValidationGroup="O" runat="server" CssClass="btn btn-success" Text="Verify OTP" OnClick="btnVerifyOTP_Click" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="btn alert alert-danger alert-success" style="padding: 5px;">
                                                                                प्रकल्पातील घटकांचा लाभ घेण्यासाठी डीबीटी अॅपमध्ये नोंदणी करताना कृपया आपल्या जवळील मोबाईलचा नंबर द्यावा. प्रकल्पामार्फत आपल्या अर्जाची स्थिती कळविण्यासाठी तसेच आपल्या शेतीसाठी उपयुक्त हवामानाचा व पिकांचा सल्ला पाठविण्यासाठी याच मोबाईल नंबरवर संदेश पाठविण्यात येणार आहे.
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-row" id="divAdhar" runat="server" visible="false">
                                                                    <div class="col-md-8">



                                                                        <div class="form-row">

                                                                            <div class="form-group col-4">
                                                                                <label for="name" class="col-form-label s-6">
                                                                                    <i class="icon-fingerprint"></i>
                                                                                    <asp:Literal ID="Literal1" Text="AUTHENTICATION TYPE" runat="server"></asp:Literal><em style="color: red">*</em></label>

                                                                                <asp:RadioButtonList ID="rdoAuthenticationType" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rdoAuthenticationType_SelectedIndexChanged">
                                                                                    <asp:ListItem Text="Biometric" Value="Biometric"></asp:ListItem>
                                                                                    <asp:ListItem Selected="True" Text="OTP" Value="OTP"></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>

                                                                            <div class="form-group col-4" id="divBioType" runat="server" visible="false">
                                                                                <label for="name" class="col-form-label s-6">
                                                                                    <i class="icon-fingerprint"></i>
                                                                                    <asp:Literal ID="Literal48" Text="Biometric device name" runat="server"></asp:Literal><em style="color: red">*</em></label><br />

                                                                                <asp:DropDownList ID="dllBiometric" CssClass="form-control light r-0 " runat="server">
                                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="SecuGen" Value="1" Selected="True"></asp:ListItem>
                                                                                    <asp:ListItem Text="NEXT" Value="2"></asp:ListItem>
                                                                                    <asp:ListItem Text="Morhpo" Value="3"></asp:ListItem>
                                                                                    <asp:ListItem Text="Mantra" Value="4"></asp:ListItem>
                                                                                </asp:DropDownList>

                                                                            </div>


                                                                            <div class="form-group col-4">
                                                                            </div>


                                                                        </div>


                                                                        <div class="form-row">


                                                                            <div class="form-group col-4">
                                                                                <label for="name" class="col-form-label s-6">
                                                                                    <i class="icon-fingerprint"></i>
                                                                                    <asp:Literal ID="Literal2" Text="AADHAAR NUMBER" runat="server"></asp:Literal><em style="color: red">*</em></label>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="badge badge-danger badge-mini2"
                                                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtAADHARNo"></asp:RequiredFieldValidator>
                                                                                <asp:TextBox ID="txtAADHARNo" Enabled="false" onblur="return (vhCheck());" MaxLength="12" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                                                    ErrorMessage="Invalid Card Number" CssClass="alert-danger" ControlToValidate="txtAADHARNo" ValidationExpression="^\d{12}$"
                                                                                    Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                                                                <p id="message"></p>
                                                                            </div>

                                                                            <div class="form-group col-6">
                                                                                <label for="name" class="col-form-label s-6 white-text">..<span id="myTimer" style="color: #0099CC; font-weight: bold;"></span></label>
                                                                                <br />




                                                                                <table style="width: 100%">
                                                                                    <tr>
                                                                                        <td style="text-align: left; width: 60px;">

                                                                                            <asp:Literal ID="LiteralCapcha" runat="server"></asp:Literal>
                                                                                            =
                                                                                        </td>
                                                                                        <td style="text-align: left; vertical-align: top;">
                                                                                            <asp:TextBox ID="txtCapcha" placeholder="Please fill sum of given Values" Width="80px" MaxLength="4" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Button ID="btnOtp" CssClass="btn btn-success" CausesValidation="false" runat="server" Text="SEND OTP" OnClick="btnOtp_Click" />
                                                                                            <div id="btnbio" visible="false" runat="server">
                                                                                                <button type="button" class="btn btn-outline-success" onclick="Capture()">Capture fingerprint </button>
                                                                                            </div>
                                                                                            <p id="messageWait"></p>
                                                                                        </td>
                                                                                        <td style="text-align: left">
                                                                                            <asp:TextBox ID="txtOtp" placeholder="Please fill OTP" Visible="false" MaxLength="12" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="text-align: left">
                                                                                            <asp:Button ID="btnAuthAudhar" CssClass="btn btn-success" Visible="false" CausesValidation="false" OnClick="btnAuthAudhar_Click" runat="server" Text="Submit" /></td>
                                                                                    </tr>
                                                                                </table>

                                                                                <ajax:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnOtp"></ajax:ConfirmButtonExtender>
                                                                                <ajax:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnOtp" OkControlID="btnYes"
                                                                                    CancelControlID="btnNo" BackgroundCssClass="modalBackground">
                                                                                </ajax:ModalPopupExtender>
                                                                                <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                                                                                    <div class="header">
                                                                                        Confirmation
                                                                                    </div>
                                                                                    <div class="body" style="padding: 2px; margin: 2px; text-align: justify">
                                                                                        I hereby consent for my Aadhaar number and demographic information (as defined in the Aadhaar act) to be used by the PoCRA DBT portal and system to collect eligibility related information about me, in order to avail of various benefits and services provided by the Maharashtra Government, or the Government of India. I understand the information provided will be stored and processed in compliance with the applicable regulations of the Government of Maharashtra, and the Government of India.
                                                                                    </div>
                                                                                    <div class="footer" align="right">
                                                                                        <asp:Button ID="btnYes" CssClass="btn btn-success" CausesValidation="false" runat="server" Text="I Agree" />
                                                                                        <asp:Button ID="btnNo" CssClass="btn btn-danger" CausesValidation="false" runat="server" Text="Not Agree" />
                                                                                    </div>
                                                                                </asp:Panel>
                                                                            </div>
                                                                        </div>



                                                                        <div class="form-row">
                                                                            <div class="form-group col-6 m-0">
                                                                                <label for="cnic" class="col-form-label s-12">
                                                                                    <i class="icon-fingerprint"></i>
                                                                                    <asp:Literal ID="Literal8" Text="NAME" runat="server"></asp:Literal><em style="color: red">*</em></label>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="badge badge-danger badge-mini2"
                                                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                                                                                <asp:TextBox ID="txtName" Enabled="false" Style="width: 100% !important;" CssClass="MrFont form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-6 m-0">
                                                                                <label for="cnic" class="col-form-label s-12">
                                                                                    <i class="icon-fingerprint"></i>
                                                                                    <asp:Literal ID="Literal56" Text="NAME IN MARATHI" runat="server"></asp:Literal><em style="color: red">*</em></label>

                                                                                <asp:TextBox ID="txtNameInMarathi" Enabled="false" Style="width: 100% !important;" CssClass="MrFont form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                                            </div>


                                                                            <div class="form-group col-6 m-0" runat="server" id="dib90" visible="false">
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
                                                                        <div class="form-row" runat="server" id="dim908" visible="false">
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
                                                                    <div class="col-md-2 offset-md-1">
                                                                        <label for="name" class="col-form-label s-6 white-text">.</label><br />
                                                                        <span class="alert alert-danger alert-white rounded" style="">
                                                                            <asp:Literal ID="Literal49" Text="Don&#39;t have an aadhaar number" runat="server"></asp:Literal>
                                                                            <a target="_blank" href="https://appointments.uidai.gov.in/easearch.aspx">click here</a> to enroll </span>
                                                                        <div class="dropzone dropzone-file-area" id="fileUpload" runat="server" visible="false">
                                                                            <div class="dz-default dz-message">

                                                                                <div>
                                                                                    <asp:Literal Visible="false" ID="Literal5" Text="Photo as on Aadhar Card" runat="server"></asp:Literal>
                                                                                    <asp:Image ID="ImageTagId" Visible="false" Width="100px" Height="100px" AlternateText="Photo as on Aadhar Card" runat="server" />

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>

                                                            <hr>
                                                            <div class="card-body center" id="divAdharbtns" runat="server" visible="false">

                                                                <asp:Button ID="btnSaveAAudhar" CssClass="btn btn-primary btn-lg" runat="server" Text="Continue" OnClick="btnSaveAAudhar_Click" />
                                                                <asp:Button ID="btnBioKyc" CausesValidation="false" Width="0px" CssClass="white-text" OnClick="btnBioKyc_Click" runat="server" Text="" BackColor="White" BorderColor="White" BorderStyle="None" />
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
                                                                                <asp:TextBox ID="txtHouseNo" MaxLength="50" CssClass="MrFont form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="badge badge-danger badge-mini2"
                                                                                    runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtHouseNo"></asp:RequiredFieldValidator>--%>
                                                                            </div>
                                                                            <div class="col-sm-6 form-group">
                                                                                <label>
                                                                                    <asp:Literal ID="Literal11" Text="STREET NAME" runat="server"></asp:Literal></label><br />

                                                                                <%--<input type="text" id="txtStreetNo" runat="server" class="form-control r-0 light s-12" />--%>
                                                                                <asp:TextBox ID="txtStreetNo" MaxLength="150" CssClass="MrFont form-control r-0 light s-12" runat="server"></asp:TextBox>

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

                                                                                <asp:TextBox ID="txtMobile1" MaxLength="10" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
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
                                                                    <asp:Button ID="btnBasic" CssClass="btn btn-primary btn-lg" runat="server" Text="Continue" OnClick="btnBasic_Click" />


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
                                                                                        <asp:DropDownList ID="ddl8A" CssClass="form-control r-0" runat="server"></asp:DropDownList>
                                                                                    </div>
                                                                                    <div class="form-group col-2 m-0" style="text-align: left">
                                                                                        <label for="email" class="col-form-label s-12">
                                                                                            <asp:Literal ID="Literal35" Text="7/12 SURVEY NUMBER" runat="server"></asp:Literal></label>
                                                                                        <br />
                                                                                        <asp:TextBox ID="txtSURVEYNo712" MaxLength="15" CssClass="form-control r-0" runat="server"></asp:TextBox>
                                                                                        <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                                                                            ErrorMessage="Enter alphnumeric values only" CssClass="icon-cloud-error error alert-danger" ControlToValidate="txtSURVEYNo712" ValidationExpression="[a-zA-Z0-9\\]*$" SetFocusOnError="True" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                                                                                    </div>
                                                                                    <div class="form-group col-3 m-0">


                                                                                        <div class="form-row">
                                                                                            <div class="form-group col-6 m-0">
                                                                                                <label for="email" class="col-form-label s-12">
                                                                                                    <asp:Literal ID="Literal36" Text="Hectare" runat="server"></asp:Literal></label>
                                                                                                <br />
                                                                                                <asp:TextBox ID="txtLANDAREA712H" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                                                                <asp:RangeValidator ID="RangeValidator5" CssClass="icon-cloud-error error alert-danger" ControlToValidate="txtLANDAREA712H" MinimumValue="0" MaximumValue="9999" runat="server" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Invalid" Type="Integer"></asp:RangeValidator>
                                                                                            </div>

                                                                                            <div class="form-group col-6 m-0">
                                                                                                <label for="email" class="col-form-label s-12">
                                                                                                    <asp:Literal ID="Literal37" Text="Are" runat="server"></asp:Literal></label>
                                                                                                <br />
                                                                                                <asp:TextBox ID="txtLANDAREA712A" CssClass="form-control r-0 light s-12" runat="server"></asp:TextBox>
                                                                                                <asp:RangeValidator ID="RangeValidator6" CssClass="icon-cloud-error error alert-danger" ControlToValidate="txtLANDAREA712A" MinimumValue="0" MaximumValue="99" runat="server" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Invalid" Type="Integer"></asp:RangeValidator>
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
                                                                                        <asp:Button ID="btnAdd712" CssClass="btn btn-success" runat="server" Text="ADD  7/12" Width="150px" OnClick="btnAdd712_Click" CausesValidation="False" />
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
                                                                                    <asp:GridView ID="grdSubject" runat="server" DataKeyNames="LandID" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grdSubject_RowDataBound" GridLines="Horizontal" OnSelectedIndexChanged="grdSubject_SelectedIndexChanged">
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
                                                                                <asp:Button ID="btnLandNext" CssClass="btn btn-primary btn-lg" runat="server" Text="Continue" OnClick="btnLandNext_Click" CausesValidation="False" />
                                                                            </div>
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
                                                                                <asp:CheckBox ID="CheckBox1" runat="server" /></td>
                                                                            <td>
                                                                                <asp:Literal ID="Literal42" Text="I hereby consent to receive information about the project related activities through my mobile." runat="server"></asp:Literal></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="vertical-align: top;">
                                                                                <asp:CheckBox ID="CheckBox2" runat="server" /></td>
                                                                            <td>
                                                                                <asp:Literal ID="Literal43" Text="I, the holder of AADHAR number mentioned above, hereby give my consent to the Project Management Unit (PMU), Project on Climate Resilient Agriculture (PoCRA), to obtain my AADHAR number, name and finger print/Iris for authentication with UIDAI. The PMU, PoCRA has informed me that my identity information would only be used for the activities and interventions under PoCRA and also informed that my biometrics will not be stored / shared and will be submitted to CIDR only for the purpose of authentication." runat="server"></asp:Literal></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="vertical-align: top;">
                                                                                <asp:CheckBox ID="CheckBox3" runat="server" /></td>
                                                                            <td>
                                                                                <asp:Literal ID="Literal44" Text="I confirm that I have listed all the land details owned by me." runat="server"></asp:Literal></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="vertical-align: top;">
                                                                                <asp:CheckBox ID="CheckBox4" runat="server" /></td>
                                                                            <td>
                                                                                <asp:Literal ID="Literal45" Text="All the information provided by me is true and correct as per my knowledge and belief." runat="server"></asp:Literal></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="vertical-align: top;">
                                                                                <asp:CheckBox ID="CheckBox5" runat="server" /></td>
                                                                            <td>
                                                                                <asp:Literal ID="Literal46" Text="I agree to use the given mobile number for communications from PoCRA." runat="server"></asp:Literal></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="vertical-align: top;">
                                                                                <asp:CheckBox ID="CheckBox6" runat="server" /></td>
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
                                                                    <asp:Button ID="btnFinalSave" runat="server" Text="Submit" CssClass="btn btn-success btn-lg" OnClick="btnFinalSave_Click" />

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
                </div>


            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
            <ProgressTemplate>
                <div class="black_overlay">
                    <div class="blackcontent">
                        <img src="../assets/img/basic/processing.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <input id="Port" style="border-color: #FFFFFF !important; width: 50px; background-color: #f3f5f8; border: none;"
            type="text" />

        <asp:TextBox ID="txtPrev" ForeColor="#f3f5f8" Width="0px" Style="border-color: #FFFFFF !important; width: 50px; background-color: #f3f5f8; border: none;" runat="server"></asp:TextBox>
    </form>
</body>
</html>
