<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationForm.aspx.cs" Inherits="DBTPoCRA.Registration.RegistrationForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../assets/css/app.css" />
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

        .aspNetDisabled {
            border: 1px solid black;
            background-image: url('/assets/img/dummy/navigationIn.jpg') !important;
            background-repeat: repeat-x;
            color: White;
            padding: 5px 5px;
            margin: 5px 5px;
            width: 180px;
        }

        .tabs a {
            padding: 10px;
            font-size: 11px;
            text-decoration: none;
        }


        /* MultiView Ends Here..*/
    </style>
</head>
<body>
    <form id="form1" runat="server" class="blue4 height-full responsive-phone">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="app">
                    <main>
                        <div class="container">
                            <h3 class="well" style="padding: 20px !important" align="center">REGISTRATION FORM
                                <span class="btn alert-danger right" style="float: right">मराठी </span>
                                <span class="btn alert-danger right" style="float: left">Home </span>
                            </h3>
                            <div class="col-lg-12 well" style="padding: 20px !important">
                                <div class="col-sm-12 form-group">
                                    <div class="col-sm-12 form-group">
                                    </div>

                                    <div class="form-group col-6 m-0">
                                        <label for="cnic" class="col-form-label s-12">REGISTER AS </label>
                                        <asp:DropDownList ID="ddlBENEFICIARY" CssClass="form-control r-0 " runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBENEFICIARY_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-12 form-group">
                                    </div>
                                    <span style="color: darkgoldenrod; font-weight: bold; font-size: large;">
                                        <fieldset style="background-color: cadetblue; height: 1PX;">
                                            <legend></legend>
                                        </fieldset>
                                    </span>
                                    <div class="col-sm-12 form-group">
                                    </div>

                                </div>

                                <div id="Div1" class="col-sm-12">
                                    <iframe id="frmIfrem" runat="server" frameborder="0" scrolling="no"  src=""  width="100%" height="1550px" style="border-style: none"></iframe>
                                </div>


                               
                            </div>
                        </div>
                    </main>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
