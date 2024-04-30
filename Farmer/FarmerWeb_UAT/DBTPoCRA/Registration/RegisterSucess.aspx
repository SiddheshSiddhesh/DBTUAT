<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterSucess.aspx.cs" Inherits="DBTPoCRA.Registration.RegisterSucess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="../assets/img/basic/favicon.ico" type="image/x-icon">
    <title>PoCRA</title>
    <!-- CSS -->
    <link rel="stylesheet" href="../assets/css/app.css">
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

        .card-header {
            text-align: center !important;
            font-weight: bold;
            color: gray;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Pre loader -->
        <div id="loader" class="loader">
            <div class="plane-container">
                <div class="preloader-wrapper small active">
                    <div class="spinner-layer spinner-blue">
                        <div class="circle-clipper left">
                            <div class="circle"></div>
                        </div>
                        <div class="gap-patch">
                            <div class="circle"></div>
                        </div>
                        <div class="circle-clipper right">
                            <div class="circle"></div>
                        </div>
                    </div>

                    <div class="spinner-layer spinner-red">
                        <div class="circle-clipper left">
                            <div class="circle"></div>
                        </div>
                        <div class="gap-patch">
                            <div class="circle"></div>
                        </div>
                        <div class="circle-clipper right">
                            <div class="circle"></div>
                        </div>
                    </div>

                    <div class="spinner-layer spinner-yellow">
                        <div class="circle-clipper left">
                            <div class="circle"></div>
                        </div>
                        <div class="gap-patch">
                            <div class="circle"></div>
                        </div>
                        <div class="circle-clipper right">
                            <div class="circle"></div>
                        </div>
                    </div>

                    <div class="spinner-layer spinner-green">
                        <div class="circle-clipper left">
                            <div class="circle"></div>
                        </div>
                        <div class="gap-patch">
                            <div class="circle"></div>
                        </div>
                        <div class="circle-clipper right">
                            <div class="circle"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="app">

            <!--Sidebar End-->


            <div class="sticky" id="strl" runat="server">
                <img alt="" src="../assets/img/basic/HeaderImage.jpg" style="width: 100%; min-height: 80px;" />
                <div class="navbar navbar-expand navbar-dark d-flex justify-content-between bd-navbar blue-grey ">
                    <div class="relative">
                        <a href="#" data-toggle="offcanvas" class="paper-nav-toggle pp-nav-toggle">
                            <i></i>
                        </a>
                    </div>
                    <!--Top Menu Start -->
                    <div class="navbar-custom-menu p-t-10">
                        <ul class="nav navbar-nav">
                            <!-- Messages-->

                            <li class="dropdown custom-dropdown messages-menu">
                                <a href="../Default.aspx" class="nav-link">
                                    <span>Home </span>
                                </a>

                            </li>

                            <li class="dropdown custom-dropdown messages-menu">
                                <a href="#" class="nav-link">
                                    <span>English </span>
                                </a>

                            </li>
                            <!-- Notifications -->
                            <li class="dropdown custom-dropdown notifications-menu">
                                <a href="#" class=" nav-link">

                                    <span>मराठी  </span>
                                </a>

                            </li>

                        </ul>
                    </div>
                </div>
            </div>



            <div class="card-group pt-4 pb-4">
                <div class="card">
                    <div class="card-header">
                        <strong>
                            <div class="alert-success">
                                <img class="user_avatar no-b no-p r-5" style="width: 32px; height: 32px;" src="../assets/img/basic/circle-green.png" alt="User Image">
                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                            </div>
                        </strong>
                    </div>
                    <div class="card-body">

                        <div class="form-row" id="Log" runat="server">
                            <div class="col-sm-6">
                                <div class="form-row">
                                    <div class="col-sm-6">
                                        <label>
                                            USER NAME (LOGINID)
                                        </label>
                                        <br />
                                        <b>
                                            <asp:Literal ID="Literal2" runat="server"></asp:Literal></b>
                                    </div>
                                    <div class="col-sm-6">
                                        <label>
                                            NAME
                                   
                                        </label>
                                        <br />
                                        <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="col-sm-6">
                                        <hr />
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="col-sm-6">
                                        <label>
                                            PASSWORD
                                    <em style="color: red">*</em>
                                        </label>
                                        <asp:TextBox ID="txtPassword" class="form-control light" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" CssClass="badge badge-danger badge-mini2"
                                            runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="badge badge-danger badge-mini2"
                                            ControlToValidate="txtPassword" ErrorMessage="Invalid Format" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}"></asp:RegularExpressionValidator>


                                    </div>

                                </div>
                                <div class="form-row">
                                    <div class="col-sm-6">
                                        <label>
                                            CONFIRM PASSWORD
                                    <em style="color: red">*</em>
                                        </label>
                                        <asp:TextBox ID="txtRepassword" class="form-control light" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="badge badge-danger badge-mini2"
                                            runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtRepassword"></asp:RequiredFieldValidator>

                                        <asp:CompareValidator ID="CompareValidator1" ControlToCompare="txtPassword" Type="String" ControlToValidate="txtRepassword" SetFocusOnError="True" Display="Dynamic" CssClass="badge badge-danger badge-mini2"
                                            runat="server" ErrorMessage="Password and confirm password must be same."></asp:CompareValidator>
                                    </div>


                                </div>
                                <div class="form-row">
                                    <div class="col-sm-6">
                                        <asp:Literal ID="LiteralMsgBox" runat="server"></asp:Literal>
                                    </div>
                                </div>

                                <asp:Button ID="btnCreatePass" CssClass="btn btn-success btn-sm mt-3" runat="server" Text="Create Password" OnClick="btnCreatePass_Click" />

                            </div>

                            <div class="col-sm-6">
                                <b>Password policy </b>
                                <br />
                                * The string must contain at least 1 lowercase alphabetical character<br />
                                * The string must contain at least 1 uppercase alphabetical character<br />
                                * The string must contain at least 1 numeric character<br />
                                * The string must contain at least 1 special character<br />
                                * The string must be eight(8) characters or longer.
                            </div>
                        </div>


                        <div class="form-row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-3" style="text-align:right">
                                <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-success btn-sm mt-3" CausesValidation="false" Text="Login" OnClick="btnLogin_Click" />
                            </div>
                            <div class="col-sm-3" style="text-align:left">
                                <asp:Button ID="btnHome" runat="server" CssClass="btn btn-success btn-sm mt-3" PostBackUrl="~/Default.aspx" CausesValidation="false" Text="Go To Home" />
                            </div>
                            <div class="col-sm-3"></div>
                        </div>
                        <br />

                         <br /> <br />

                    </div>
                </div>

            </div>

        </div>
        <!--/#app -->
        <script src="../assets/js/app.js"></script>
    </form>
</body>
</html>
