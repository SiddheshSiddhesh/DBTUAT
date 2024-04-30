<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestFarmerLogin.aspx.cs" Inherits="DBTPoCRA.TestFarmerLogin" %>

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
            //var uri ="http://127.0.0.1:11101/rd/capture";// "http://localhost:11100/rd/capture";
            // params  += "&PidXML=" + encodeURIComponent(enc_pid_b64);
            //params = '<PidOptions ver="1.0"> <Opts fCount="1"  format="0" pidVer="2.0" timeout="20000" env="P" wadh="RZ+k4w9ySTzOibQdDHPzCFqrKScZ74b3EibKYy1WyGw=" posh="UNKNOWN" />';
            //params += '</PidOptions>';

            //var params = '<PidOptions ver=\"1.0\">' + '<Opts fCount=\"1\" fType=\"0\" iCount=\"0\" iType=\"0\" env=\"P\" pCount=\"0\" pType=\"0\" format=\"0\" pidVer=\"2.0\" timeout=\"10000\" otp=\"\" wadh=\"RZ+k4w9ySTzOibQdDHPzCFqrKScZ74b3EibKYy1WyGw=\" posh=\"\"/>' + '</PidOptions>';
            var uri = "";
            var DivType = $('#dllBiometric').val();
            //alert(DivType);
            var params = "";
            if (DivType == "1") {
                uri = "http://127.0.0.1:11101/rd/capture";

                params = '<PidOptions ver="1.0"> <Opts fCount="1"  format="0" pidVer="2.0" timeout="20000" env="P" wadh="RZ+k4w9ySTzOibQdDHPzCFqrKScZ74b3EibKYy1WyGw=" posh="UNKNOWN" />';
                params += '</PidOptions>';
            }
            else if (DivType == "2") {
                uri = "http://127.0.0.1:11100/rd/capture";
                params = '<PidOptions ver=\"1.0\">' + '<Opts fCount=\"1\" fType=\"0\" iCount=\"0\" iType=\"0\" env=\"P\" pCount=\"0\" pType=\"0\" format=\"0\" pidVer=\"2.0\" timeout=\"10000\" otp=\"\" wadh=\"RZ+k4w9ySTzOibQdDHPzCFqrKScZ74b3EibKYy1WyGw=\" posh=\"\"/>' + '</PidOptions>';
            }
            else if (DivType == "3") {

            }
            else if (DivType == "4") {

            }
            var xmlhttp = new XMLHttpRequest();
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    debugger;
                    SucessInfo(xmlhttp.responseText);
                }
                else if (xmlhttp.status == 404) {
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
        function DriverInfo() {
            var uri = "http://localhost:11100/rd/info";
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
            xmlhttp.open("DEVICEINFO", uri, true);
            xmlhttp.send();
        }
        function SucessInfo(result) {
            alert("Please remove your finger and let us process your request .");
            AjaxCallToFunction(result);
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
                var ANumber = document.getElementById("txtname").value;
                $.ajax({
                    type: "POST",
                    url: "FarmerLogin.aspx/CallSessionVariables",
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



            debugger;
            if (response.d == "") {
                window.location.href = 'UsersTrans/UserDashBoard.aspx';
            }
            else {
                alert(response.d);
                window.location.href = 'FarmerLogin.aspx';
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

                                                        <div class="row">
                                                            <div class="col-lg-12">

                                                                <asp:Label ID="Label2" runat="server" CssClass="forget-pass bolder" Text="Aadhar Number"></asp:Label><br />

                                                                <input type="text" id="txtname" autocorrect="off" runat="server" class="form-control" maxlength="12"
                                                                    placeholder="" required />

                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-lg-12">

                                                                <asp:Label ID="Label3" runat="server" CssClass="forget-pass bolder" Text="Password"></asp:Label><br />
                                                                <input type="password" id="txtPassword" autocorrect="off" runat="server" class="form-control"
                                                                    placeholder="" required />

                                                            </div>

                                                        </div>


                                                        <div class="row">

                                                            <div class="col-lg-4">
                                                                <br />
                                                            </div>
                                                            <div class="col-lg-4">
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
                                                                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                                                </p>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
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

    <script src="assets/js/google_analytics.js"></script>

</body>
</html>
