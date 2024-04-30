<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DBTPoCRA.Default" %>

<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="PoCRA" />
    <meta name="author" content="PoCRA" />
    <link rel="icon" href="assets/img/basic/favicon.ico" type="image/x-icon" />
    <script src="assets/js/jquery.min.js"></script>
    <title>PoCRA,dbt maharashtra,Direct Benefit Transfer,Homepage</title>
    <!-- CSS -->
    <link rel="stylesheet" href="assets/css/app.css" />
    <style>
        .loader {
            position: fixed;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            background-color: #F5F8FA;
            z-index: 9998;
            text-align: center;
        }

        .plane-container {
            position: absolute;
            top: 50%;
            left: 50%;
        }

        .form-row {
            margin: 0 !important;
            padding: 0 !important;
        }

        .card {
            background-color: white !important;
            margin: 30px !important;
        }

        .card-header {
            text-align: center !important;
            font-weight: bold;
            color: gray;
        }

        html {
            height: 100%
        }

        .card {
            border-radius: 10px !important;
        }

        .navbar {
            margin-bottom: -30px;
            margin-top: -40px !important;
        }

        .card {
            border-radius: 0 !important;
        }


        .user_avatar {
            height: 80px;
        }
    </style>
    <script type="text/javascript">
        jQuery(window).load(function () {
            // Draw();
        });
    </script>
    <script> 
        function Draw() {
            $.ajax({
                type: "POST",
                url: "Default.aspx/UpdateLoginCount",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    //var data = (r.d);
                    document.getElementById("divCount").innerHTML = r.d;
                    //alert(r.d);
                },
                failure: function (r) {
                    // alert(r.d);
                },
                error: function (r) {
                    //  alert(r.d);
                }
            });
        }

        $(document).ready(function () {
            // code here
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
        });
    </script>
</head>
<body style="background-image: url('../../../assets/img/basic/bg.jpg'); background-repeat: no-repeat; background-color: #05210D !important; background-size: 100% 100%;">
    <form id="form1" runat="server">
        <div style="width: 100%; height: 160px;">
        </div>
        <div>
            <div id="app">

                <!--Sidebar End-->


                <div class="sticky">
                    <%-- <img alt="" src="assets/img/basic/HeaderImage.jpg" style="width: 100%; min-height: 80px;" />--%>

                    <div class="navbar navbar-expand navbar-dark d-flex justify-content-between bd-navbar blue-grey ">
                        <div class="relative">
                        </div>
                        <!--Top Menu Start -->
                        <span style="font-size: large; font-weight: bold; color: #FFFFFF; text-align: center;">
                            <asp:Literal ID="Literal9" Text="Welcome To DBT-PoCRA" runat="server"></asp:Literal>
                            <br />
                            <asp:Label ID="Label1" Font-Size="10pt" Font-Bold="True" ForeColor="Red" runat="server" BackColor="#FFFF66"></asp:Label>
                        </span>
                        <div class="navbar-custom-menu p-t-10">
                            <ul class="nav navbar-nav">
                                <!-- Messages-->
                                <li class="dropdown custom-dropdown">
                                    <%--  <a href="#" class="nav-link" data-toggle="dropdown">--%>
                                    <span>
                                        <asp:LinkButton ID="btnEnglish" CssClass=" btn-link white-text" CausesValidation="false" runat="server" OnClick="btnEnglish_Click">English</asp:LinkButton>
                                    </span>
                                    <%-- </a>--%>

                                </li>
                                <!-- Notifications -->
                                <li class="dropdown custom-dropdown">
                                    <%--<a href="#" class=" nav-link" data-toggle="dropdown" aria-expanded="false">--%>

                                    <span style="padding-left: 10px; padding-right: 10px">
                                        <asp:LinkButton ID="btnMarathi" CssClass="btn-link white-text" CausesValidation="false" runat="server" OnClick="btnMarathi_Click">  मराठी</asp:LinkButton>
                                    </span>
                                    <%-- </a>--%>

                                </li>


                            </ul>
                        </div>
                    </div>
                </div>



                <div class="card-group pt-4 pb-4">
                    <div class="card">
                        <div class="card-header">
                            <strong>
                                <asp:Literal ID="Literal1" Text="Farmer" runat="server"></asp:Literal>
                            </strong>
                        </div>
                        <div class="card-body text-center">
                            <div class="image m-0">
                                <img class="user_avatar no-b no-p r-5" src="assets/img/basic/kisan.jpg" alt="Image not found" />
                            </div>
                            <div style="height: 55px">
                                <!--<h6 class="p-t-12">Alexander Pierce</h6>-->
                                <asp:Literal ID="Literal2" Text="Farmers having land in 15 identified districts of the State of Maharashtra for the scheme" runat="server"></asp:Literal>

                            </div>

                            <a href="Registration/IndividualRegistrationNew.aspx" class="btn btn-success btn-sm mt-3">
                                <asp:Literal ID="Literal10" Text="Register" runat="server"></asp:Literal></a>
                            <a href="FarmerLogin.aspx?T=I&D=1" class="btn btn-danger btn-sm mt-3">
                                <asp:Literal ID="Literal13" Text="Login" runat="server"></asp:Literal></a>

                             <a href="TestFarmerLogin.aspx" class="btn btn-danger btn-sm mt-3">
                                <asp:Literal ID="Literal18" Text="Test Login" runat="server"></asp:Literal></a>

                        </div>
                    </div>
                    <div class="card" style="display: none;">
                        <div class="card-header">
                            <strong>
                                <asp:Literal ID="Literal3" Text="Community" runat="server"></asp:Literal>
                            </strong>
                        </div>
                        <div class="card-body text-center">
                            <div class="image m-0">
                                <img class="user_avatar no-b no-p r-5" src="assets/img/basic/Community.jpg" alt="Image not found for Community" />
                            </div>
                            <div style="height: 55px">
                                <!--<h6 class="p-t-10">Alexander Pierce</h6>-->
                                <asp:Literal ID="Literal4" Text="Community  set up at Gram Panchayat level in the 15 identified districts of the State of Maharashtra" runat="server"></asp:Literal>
                            </div>
                            <a href="Registration/CommunityRegistration.aspx" class="btn btn-success btn-sm mt-3">
                                <asp:Literal ID="Literal11" Text="Register" runat="server"></asp:Literal></a>
                            <a href="UserLogin.aspx?T=C&D=2" class="btn btn-danger btn-sm mt-3">
                                <asp:Literal ID="Literal14" Text="Login" runat="server"></asp:Literal></a> 
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header">
                            <strong>
                                <asp:Literal ID="Literal5" Text="FPO/FPC/FIG/SHGs" runat="server"></asp:Literal>
                            </strong>
                        </div>
                        <div class="card-body text-center">
                            <div class="image m-0">
                                <img class="user_avatar no-b no-p r-5" src="assets/img/basic/groups.jpg" alt=" Image not found for groups" />
                            </div>
                            <div style="height: 55px">
                                <!--<h6 class="p-t-10">Alexander Pierce</h6>-->
                                <asp:Literal ID="Literal6" Text=" Groups working in 15 identified districts of the State of Maharashtra for the upliftment of marginalized farmers" runat="server"></asp:Literal>
                            </div>
                            <a href="Registration/OthersRegistration.aspx" class="btn btn-success btn-sm mt-3">
                                <asp:Literal ID="Literal12" Text="Register" runat="server"></asp:Literal></a>
                            <a href="FPOLogin.aspx?T=F&D=1" class="btn btn-danger btn-sm mt-3">
                                <asp:Literal ID="Literal15" Text="Login" runat="server"></asp:Literal></a>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header">
                            <strong>PoCRA
                                <asp:Literal ID="Literal7" Text="officials" runat="server"></asp:Literal>
                            </strong>
                        </div>
                        <div class="card-body text-center">
                            <div class="image m-0">
                                <img class="user_avatar no-b no-p r-5" src="assets/img/basic/login.jpg" alt="Image not found for login" />
                            </div>
                            <div style="height: 55px">
                                <!--<h6 class="p-t-10">Alexander Pierce</h6>-->
                                <asp:Literal ID="Literal8" Text=" Officials working with PoCRA office" runat="server"></asp:Literal><br />

                            </div>
                            <!--<a href="#" class="btn btn-success btn-sm mt-3">Register</a>-->
                            <a href="../UserLogin.aspx?T=O&D=99" class="btn btn-danger btn-sm mt-3">
                                <asp:Literal ID="Literal16" Text="Login" runat="server"></asp:Literal></a>
                        </div>
                    </div>
                </div>

                <div class="form-row" style="color: white !important;">
                    <div class="col-sm-12 form-group" style="text-align: center;">
                        <div class="card" style="background-color: green!important; margin-top: -38px!important;">
                            <div class="card-header">
                                <strong style="color: white !important; font-weight: 900;">Please Register Only If You Have Land In Below Districts / Your Residence Is In Same District.
                                </strong>

                                <p style="color: snow!important;">
                                    <asp:Literal ID="Literal17" Text="Akola अकोला , Amravati अमरावती , Chhatrapati Sambhajinagar छत्रपती संभाजीनगर , Beed बीड , Buldhana बुलढाणा , Hingoli हिंगोली , Jalgaon जळगाव , Jalna जालना , Latur लातूर , Nanded नांदेड , Dharashiv धाराशिव , Parbhani परभणी , Wardha वर्धा , Washim वाशिम , Yavatmal यवतमाळ " runat="server"></asp:Literal>
                                </p>
                            </div>

                            <br />
                            <span style="font-weight: bold">डीबीटी ऍप्लिकेशनशी संबंधित सहाय्यासाठी, कृपया या नंबरशी संपर्क साधावा - 022-22153351 </span>
                        </div>
                    </div>
                </div>



            </div>
        </div>
    </form>

    <script src="assets/js/google_analytics.js"></script>
</body>
</html>
