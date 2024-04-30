<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdharTestAPI.aspx.cs" Inherits="DBTPoCRA.AdharTestAPI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="assets/img/basic/favicon.ico" type="image/x-icon">
    <title>PoCRA</title>
    <!-- CSS -->
    <link rel="stylesheet" href="assets/css/app.css">
    <script src="assets/js/jquery.min.js"></script>
    <script src="assets/js/sweetalert.min.js"></script>
    <link href="assets/css/sweetalert.css" rel="stylesheet" />


    <style>
        .btn-line {
            margin: 0 !important;
        }

        form label {
            font-size: 15px;
        }

        .form-row {
            margin: 0 !important;
        }
    </style>
    <script>
        function ResetMsg(obj) {
            document.getElementById("MsgDiv").innerHTML = "";
            document.getElementById("MsgDiv").style.display = "none";
        }
    </script>


    <script>
        var params = "";
        var DivType = "";

        function Info() {
            var uri = "";

            var DivType = $('#dllBiometric').val();
            // alert(DivType);

            if (DivType == "1") {
                uri = "http://127.0.0.1:11100";
            }
            else if (DivType == "2") {
                uri = "http://127.0.0.1:11101";
            }
            else if (DivType == "3") {

            }
            else if (DivType == "4") {

            }
            var xmlhttp = new XMLHttpRequest();
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    SucessInfo(xmlhttp.responseText);
                }
                else if (xmlhttp.status == 404) {
                    failCall(xmlhttp.status)
                }
                else if (xmlhttp.status == 503) {
                    failCall(xmlhttp.status)
                }
            }
            xmlhttp.onerror = function () {
                failCall(xmlhttp.status);
            }
            xmlhttp.onabort = function () {
                alert("Aborted");
            }
            xmlhttp.open("RDSERVICE", uri, true);
            xmlhttp.send();

        }



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
                params = '<PidOptions ver=\"1.0\">' + '<Opts fCount=\"1\" fType=\"0\" iCount=\"\" iType=\"\" pCount=\"\" pType=\"\" format=\"0\" pidVer=\"2.0\" timeout=\"10000\" otp=\"\" wadh=\"RZ+k4w9ySTzOibQdDHPzCFqrKScZ74b3EibKYy1WyGw=\" posh=\"\"/>' + '</PidOptions>';
              
            }
            else if (DivType == "4") {
                uri = "http://127.0.0.1:" + port + "/rd/capture";
                params = '<PidOptions ver=\"1.0\">' + '<Opts fCount=\"1\" fType=\"0\" iCount=\"0\" iType=\"0\" env=\"P\" pCount=\"0\" pType=\"0\" format=\"0\" pidVer=\"2.0\" timeout=\"10000\" otp=\"\" wadh=\"RZ+k4w9ySTzOibQdDHPzCFqrKScZ74b3EibKYy1WyGw=\" posh=\"\"/>' + '</PidOptions>';
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

            var PIDOPTS = '<PidOptions ver=\"1.0\">' + '<Opts fCount=\"1\" fType=\"0\" iCount=\"\" iType=\"\" pCount=\"\" pType=\"\" format=\"0\" pidVer=\"2.0\" timeout=\"10000\" otp=\"\" wadh=\"RZ+k4w9ySTzOibQdDHPzCFqrKScZ74b3EibKYy1WyGw=\" posh=\"\"/>' + '</PidOptions>';

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

            params = '<PidOptions ver="1.0"> <Opts fCount="1"  format="0" pidVer="2.0" timeout="20000" env="P" wadh="RZ+k4w9ySTzOibQdDHPzCFqrKScZ74b3EibKYy1WyGw=" posh="UNKNOWN" />';
            params += '</PidOptions>';

            var xhr;

            xhr = new XMLHttpRequest();

            xhr.open('CAPTURE', url, true);
            xhr.setRequestHeader("Content-Type", "text/xml");
            xhr.setRequestHeader("Accept", "text/xml");
            xhr.onreadystatechange = function () {
                debugger;
                if (xhr.readyState == 4) {
                    SucessInfo(xhr.responseText);
                }
            };
            xhr.send(params);

        }



        function SucessInfo(result) {

            if (result != null) {
                if (result.length > 0) {
                    alert("Please remove your finger and let us process your request .");
                    AjaxCallToFunction(result);
                }
            }
        }

        function failCall(status) {
            alert("Check if RDSERVICE is running");
        }

        function AjaxCallToFunction(result) {
            try {

                var ANumber = document.getElementById("txtname").value;
                $.ajax({
                    type: "POST",
                    url: "AdharTestAPI.aspx/CallSessionVariables",
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

            if (response.d == "") {
                window.location.href = 'UsersTrans/UserDashBoard.aspx';
            }
            else {
               // alert(response.d);
                window.location.href = 'FarmerLogin.aspx';
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

            debugger;
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
            } catch (e)
            { }


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

                        $("#ddlAVDM").append('<option value=' + i.toString() + '>(' + CmbData1 + '-' + i.toString() + ')' + CmbData2 + '</option>')

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



    </script>




</head>
<body class="light">
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>


                <div id="app">

                    <!--Sidebar End-->


                    <div class="sticky">
                        <img alt="" src="assets/img/basic/HeaderImage.jpg" style="width: 100%;" />
                        <div class="navbar navbar-expand navbar-dark d-flex justify-content-between bd-navbar blue-grey ">
                            <div class="relative">
                                <a href="#" data-toggle="offcanvas" class="paper-nav-toggle pp-nav-toggle">
                                    <i></i>
                                </a>
                            </div>
                            <!--Top Menu Start -->
                            <div class="navbar-custom-menu p-t-10">
                                <ul class="nav navbar-nav">

                                    <li class="dropdown custom-dropdown">

                                        <span>
                                            <a class="badge badge-success text-white" href="Default.aspx">Home</a>
                                        </span>

                                    </li>

                                    <li class="dropdown custom-dropdown">

                                        <span style="padding-left: 10px; padding-right: 10px">
                                            <asp:LinkButton ID="btnMarathi" CssClass="badge badge-success text-white" CausesValidation="false" ToolTip="" Text="मराठी" runat="server" OnClick="btnMarathi_Click"></asp:LinkButton>
                                        </span>

                                    </li>

                                </ul>
                            </div>
                        </div>
                    </div>

                    <div class="page">



                        <div class="form-row">
                            <div class="form-group col-12">
                                <div class="container-fluid  my-1">

                                    <div class="row row-eq-height my-1">
                                        <!-- Social Widget Start -->
                                        <div class="col-md-3"></div>
                                        <div class="col-md-6">
                                            <br />
                                            <div class="card">
                                                <div class="card-header bolder text-white" style="background-color: #607D8B">
                                                    <strong>
                                                        <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                                                    </strong>
                                                </div>
                                                <div class="card-body text-left bg-white">
                                                    <div class="col-lg-12">



                                                        <%-- <div class="row" style="display: none;">
                                                            <div class="col-lg-6">
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label1" runat="server" CssClass="forget-pass bolder" Text="Login As"></asp:Label><br />

                                                                    <asp:DropDownList ID="ddlUserType" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged">

                                                                        <asp:ListItem Value="I" Text="Individual"></asp:ListItem>

                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6">
                                                            </div>

                                                        </div>--%>


                                                        <div class="row">


                                                            <div class="col-lg-6">
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label5" runat="server" CssClass="forget-pass bolder" Text="Login With "></asp:Label><br />
                                                                    <asp:RadioButtonList ID="RadioButtonList1" Font-Size="10px" CssClass="forget-pass" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                                                        <asp:ListItem Selected="True" Value="OTP">  &nbsp; OTP  &nbsp; </asp:ListItem>
                                                                        <asp:ListItem Value="Boimetric">  &nbsp; Biometric (Thumb)  &nbsp;</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                    <asp:RadioButtonList ID="RadioButtonList2" Visible="false" Font-Size="10px" CssClass="forget-pass" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                                                        <asp:ListItem Selected="True" Value="OTP">  &nbsp; ओटीपी  &nbsp; </asp:ListItem>
                                                                        <asp:ListItem Value="Boimetric">  &nbsp; बॉयोमेट्रिक (थंब)  &nbsp;</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group col-6" id="divBioType" runat="server" visible="false">
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
                                                        </div>


                                                        <div class="row">
                                                            <div class="col-lg-5">

                                                                <asp:Label ID="Label2" runat="server" CssClass="forget-pass bolder" Text="Aadhar Number"></asp:Label><br />

                                                                <input type="text" id="txtname" autocomplete="off" runat="server" class="form-control" maxlength="12"
                                                                    placeholder="" required />

                                                            </div>
                                                            <div class="col-lg-3" style="text-align: left">
                                                                ..<br />
                                                                <asp:Button ID="btnOtp" class="btn btn-outline-success" runat="server" Text="Send opt" OnClick="btnOtp_Click" />
                                                                <div id="btnbio" runat="server">
                                                                    <button type="button" class="btn btn-outline-success" onclick="Capture();">Capture fingerprint </button>
                                                                </div>

                                                            </div>
                                                            <div class="col-lg-4">

                                                                <asp:Label ID="Label3" runat="server" CssClass="forget-pass bolder" Text="Password"></asp:Label><br />
                                                                <input type="password" id="txtPassword" autocomplete="off" runat="server" class="form-control"
                                                                    placeholder="" required />

                                                            </div>

                                                        </div>


                                                        <div class="row">

                                                            <div class="col-lg-6">
                                                                <br />
                                                            </div>
                                                            <div class="col-lg-6" style="text-align: right">
                                                                <select style="border-color: #FFFFFF; width: 1px" id="AVDMCode">
                                                                </select>
                                                                <input id="Port" style="border-width: 0px; border-style: none; border-color: #FFFFFF !important; width: 0px" type="text" />
                                                            </div>
                                                        </div>

                                                        <div class="row text-center">

                                                            <div class="col-lg-4">
                                                                <asp:Button ID="btnLogin" CssClass="btn btn-danger btn-lg btn-block" runat="server" Style="max-width: 200px;" Width="80px" Text="Login" OnClick="btnLogin_Click" />
                                                            </div>
                                                            


                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-12">

                                                                <p class="forget-pass text-white">
                                                                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>

                                                                </p>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                           
                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>



                    </div>

                </div>












            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div class="black_overlay">
                    <div class="blackcontent">
                        <img src="assets/img/basic/processing.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

    </form>
</body>
</html>
