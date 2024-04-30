<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommunityProfile.aspx.cs" Inherits="DBTPoCRA.UsersTrans.CommunityProfile" %>
<%@ Register Assembly="DropDownChosen" Namespace="CustomDropDown" TagPrefix="cc2" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
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

        .container {
            padding: 0px !important;
            margin: 0px !important;
            width: 100% !important;
            max-width: 100% !important;
            background-color: white !important;
        }

        /* MultiView Ends Here..*/
    </style>

    <script>


        function CallToAll() {

        }


    </script>
    <script>
        function ResetMsg(obj) {
            document.getElementById("MsgDiv").innerHTML = "";
            document.getElementById("MsgDiv").style.display = "none";
        }


    </script>

    <script type="text/javascript">
        function checkDate(sender, args) {
           
            if (sender._selectedDate > new Date()) {
                alert("You cannot select a Future dates!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }

        }


        function GetSum1()
        {
            debugger;
            var a = document.getElementById("txtForestrybasedfarmingPractices").value;
            if (a == "") a = "0";
            var b = document.getElementById("txtTotalorchardPlanting").value;
            if (b == "") b = "0";
            var c = document.getElementById("txtTotalalkalinelandmanagement").value;
            if (c == "") c = "0";
            var d = document.getElementById("txtTotalprotectedfarming").value;
            if (d == "") d = "0";
            var e = document.getElementById("txtTotalIntegratedfarming").value;
            if (e == "") e = "0";
            var f = document.getElementById("ImprovingoverallSoilhealth").value;
            if (f == "") f = "0";

            var total = parseFloat(a) + parseFloat(b) + parseFloat(c) + parseFloat(d) + parseFloat(e) + parseFloat(f);

            document.getElementById("txtTotal1").value = total;

        }

        function GetSum2() {
            debugger;
            var a = document.getElementById("txtEfficientAndsustainableuseoftotalwater").value;
            if (a == "") a = "0";
            var b = document.getElementById("txtWaterStorageAtthebase").value;
            if (b == "") b = "0";
            var c = document.getElementById("txtFineIrrigation").value;
            if (c == "") c = "0";
            var d = document.getElementById("txtAvailabilitywaterForProtectedIrrigation").value;
            if (d == "") d = "0";
          

            var total = parseFloat(a) + parseFloat(b) + parseFloat(c) + parseFloat(d);

            document.getElementById("txtTotal2").value = total;

        }

        function GetSum3() {
            debugger;
            var a = document.getElementById("txtCreatingInfrastructure").value;
            if (a == "") a = "0";
            var b = document.getElementById("txtAgriculturalEquipmentCenter").value;
            if (b == "") b = "0";
            var c = document.getElementById("txtClimatefriendlyvarieties").value;
            if (c == "") c = "0";
            var d = document.getElementById("txtTotalSeedHubInfrastructure").value;
            if (d == "") d = "0";


            var total = parseFloat(a) + parseFloat(b) + parseFloat(c) + parseFloat(d);

            document.getElementById("txtTotal3").value = total;

        }


    </script>

</head>
<body>
    <form id="form1" runat="server" class="height-full responsive-phone">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="app">
                    <main>
                        <div class="container">

                            <div class="col-lg-12">

                                <div id="DivIndividual" class="col-sm-12">

                                    <div class="col-sm-12 form-group">
                                        <asp:Literal ID="LiteralMsg" runat="server"></asp:Literal>
                                    </div>
                                    <div class="col-sm-12 form-group">
                                        <asp:Literal ID="LiteralName" runat="server"></asp:Literal>
                                    </div>
                                    <div class="form-group">

                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="card no-b  no-r">
                                                    <div class="card-body">
                                                        <h5 class="card-title">
                                                            <asp:Literal ID="Literal9" Text="नोंदणी तपशील " runat="server"></asp:Literal></h5>

                                                        <div class="form-row">
                                                            <div class="col-md-12">
                                                                <div class="form-row">
                                                                    <div class="form-group col-4 m-0">


                                                                        <label for="name" class="col-form-label s-12">
                                                                            <i class="icon-fingerprint"></i>
                                                                            <asp:Literal ID="Literal1" Text="गाव" runat="server"></asp:Literal><em style="color: red">*</em></label>
                                                                        <br />
                                                                        <asp:Label ID="lblVillage"  runat="server"></asp:Label>
                                                                      

                                                                    </div>
                                                                    <div class="form-group col-4 m-0">
                                                                        <label for="cnic" class="col-form-label s-12">
                                                                            <i class="icon-fingerprint"></i>
                                                                            <asp:Literal ID="Literal2" Text="ग्राम कृषि संजीवनी समितीचे नाव " runat="server"></asp:Literal><em style="color: red">*</em></label>
                                                                          <br />
                                                                        <asp:Label ID="txtName"  runat="server"></asp:Label>
                                                                    </div>
                                                                </div>

                                                                <div class="form-row">

                                                                    <div class="form-group col-4 m-0">
                                                                        
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal31" Text="सूक्ष्म नियोजनाचा कालावधी <br>  पासून" runat="server"></asp:Literal></label>
                                                                        <br>
                                                                        <asp:Label ID="txtCom_PeriodofMicroplanningFrom" runat="server" placeholder="Date in DD/MM/YYYY" ></asp:Label>
                                                                       
                                                                    </div>
                                                                    <div class="form-group col-4 m-0">
                                                                        
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal10" Text="सूक्ष्म नियोजनाचा कालावधी <br> पर्यंत" runat="server"></asp:Literal></label>
                                                                        <br>
                                                                        <asp:Label ID="txtCom_PeriodofMicroplanningFromTo" runat="server" placeholder="Date in DD/MM/YYYY" ></asp:Label>
                                                                      
                                                                    </div>


                                                                    <div class="form-group col-4 m-0">
                                                                       
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal32" Text="सविस्तर प्रकल्प अहवालास  ग्राम कृषि संजीवनी समितीचा <br> मान्यता दिनांक" runat="server"></asp:Literal></label>
                                                                        <br>
                                                                        <asp:Label ID="txtCom_ProjectReportDate" runat="server" placeholder="Date in DD/MM/YYYY" ></asp:Label>
                                                                       
                                                                    </div>



                                                                </div>

                                                                <div class="form-row">
                                                                    <div class="form-group col-4 m-0">
                                                                       
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal33" Text="सविस्तर प्रकल्प अहवालास  जिल्हा समन्वय समितीचा <br> मान्यता दिनांक" runat="server"></asp:Literal></label>
                                                                        <br>
                                                                        <asp:Label ID="txtCom_DCCApprovalDate" runat="server" placeholder="Date in DD/MM/YYYY" ></asp:Label>
                                                                       
                                                                    </div>

                                                                    <%--<div class="form-group col-4 m-0">
                                                                        <asp:FieldValidator ID="FieldValidator17" CssClass="badge badge-danger badge-mini2"
                                                                            runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="" ControlToValidate="txtCom_For21ProjectReport"></asp:FieldValidator>
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal34" Text="सविस्तर प्रकल्प अहवालातील प्रपत्र क्र.२१ नुसार हवामान अनुकूल कृषि पद्धतीस <br> प्रोत्साहन (१+२+३ +४+५)" runat="server"></asp:Literal></label>
                                                                        <br>
                                                                        <asp:Label ID="txtCom_For21ProjectReport" runat="server" ></asp:Label>

                                                                    </div>

                                                                    <div class="form-group col-4 m-0">
                                                                        <asp:FieldValidator ID="FieldValidator18" CssClass="badge badge-danger badge-mini2"
                                                                            runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="" ControlToValidate="txtCom_UseOfWaterProjectReport"></asp:FieldValidator>
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal35" Text="सविस्तर प्रकल्प अहवालातील प्रपत्र क्र.२१ नुसार पाण्याचा कार्यक्षम व शाश्वत पद्धतीने <br> वापर (१+२+३+४ +५+६+७ +८)" runat="server"></asp:Literal></label>
                                                                        <br>
                                                                        <asp:Label ID="txtCom_UseOfWaterProjectReport" runat="server" placeholder="" ></asp:Label>

                                                                    </div>--%>
                                                                </div>



                                                                <br />
                                                                <div class="form-row">
                                                                    <div class="form-group col-4 m-0" style="border: 1px solid #CCCCCC; padding-top: 5px; padding-bottom: 5px;">

                                                                        <label for="dob" class="col-form-label s-12" style="font-weight: bold">
                                                                            <asp:Literal ID="Literal4" Text="हवामान अनुकूल कृषि पद्धतीस प्रोत्साहन" runat="server"></asp:Literal></label>
                                                                        <br>

                                                                        <div class="form-row">
                                                                            <div class="form-group col-6 m-0">
                                                                                वानिकी आधारीत शेती पद्धती
                                                                            </div>
                                                                            <div class="form-group col-6 m-0">
                                                                                <asp:Label ID="txtForestrybasedfarmingPractices" runat="server" onkeyup="GetSum1();" onblur="GetSum1();" placeholder="मूल्य लक्षात" ></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                      <%--  <div class="form-row">
                                                                            
                                                                        </div>--%>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-6 m-0">
                                                                                एकूण  फळबाग लागवड
                                                                            </div>
                                                                        
                                                                         <div class="form-group col-6 m-0">
                                                                                <asp:Label ID="txtTotalorchardPlanting" runat="server" placeholder="मूल्य लक्षात" onkeyup="GetSum1();" onblur="GetSum1();" ></asp:Label>
                                                                            </div>
                                                                        </div>

                                                                        <%-- <div class="form-row">
                                                                            </div>--%>

                                                                        <div class="form-row">
                                                                            <div class="form-group col-6 m-0">
                                                                                एकूण क्षारपड जमिन व्यवस्थापन
                                                                            </div>
                                                                             <div class="form-group col-6 m-0">
                                                                                <asp:Label ID="txtTotalalkalinelandmanagement" runat="server" placeholder="मूल्य लक्षात" onkeyup="GetSum1();" onblur="GetSum1();" ></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                       <%-- <div class="form-row">
                                                                           
                                                                        </div>--%>

                                                                        <div class="form-row">
                                                                            <div class="form-group col-6 m-0">
                                                                                एकूण संरक्षित शेती
                                                                            </div>
                                                                            <div class="form-group col-6 m-0">
                                                                                <asp:Label ID="txtTotalprotectedfarming" runat="server" placeholder="मूल्य लक्षात" onkeyup="GetSum1();" onblur="GetSum1();" ></asp:Label>
                                                                            </div>

                                                                        </div>
                                                                       <%-- <div class="form-row">
                                                                            
                                                                        </div>--%>

                                                                        <div class="form-row">
                                                                            <div class="form-group col-6 m-0">
                                                                                एकूण एकात्मिक शेती
                                                                            </div>
                                                                            <div class="form-group col-6 m-0">
                                                                                <asp:Label ID="txtTotalIntegratedfarming" runat="server" placeholder="मूल्य लक्षात" onkeyup="GetSum1();" onblur="GetSum1();" ></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                        <%--<div class="form-row">
                                                                            
                                                                        </div>--%>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-6 m-0">
                                                                                एकूण जमिन आरोग्य सुधारणे
                                                                            </div>
                                                                            <div class="form-group col-6 m-0">
                                                                                <asp:Label ID="ImprovingoverallSoilhealth" runat="server" placeholder="मूल्य लक्षात" onkeyup="GetSum1();" onblur="GetSum1();" ></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                         
                                                                       <%-- <div class="form-row">
                                                                           
                                                                        </div>--%>

                                                                    </div>



                                                                    <div class="form-group col-4 m-0" style="border: 1px solid #CCCCCC; padding-top: 5px; padding-bottom: 5px;">

                                                                        <label for="dob" class="col-form-label s-12" style="font-weight: bold">
                                                                            <asp:Literal ID="Literal5" Text="पाण्याचा कार्यक्षम व शाश्वत पद्धतीने वापर " runat="server"></asp:Literal></label>
                                                                        <br>

                                                                        <div class="form-row">
                                                                            <div class="form-group col-6 m-0">
                                                                                एकूण पाण्याचा कार्यक्षम व शाश्वत पद्धतीने वापर
                                                                            </div>
                                                                             <div class="form-group col-6 m-0">
                                                                                <asp:Label ID="txtEfficientAndsustainableuseoftotalwater" onkeyup="GetSum2();" onblur="GetSum2();" runat="server" placeholder="मूल्य लक्षात"  ></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                       <%-- <div class="form-row">
                                                                           
                                                                        </div>--%>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-6 m-0">
                                                                                मुलस्थानी जलसंधारण 
                                                                            </div>
                                                                            <div class="form-group col-6 m-0">
                                                                                <asp:Label ID="txtWaterStorageAtthebase" runat="server" onkeyup="GetSum2();" onblur="GetSum2();" placeholder="मूल्य लक्षात"  ></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                       <%-- <div class="form-row">
                                                                            
                                                                        </div>--%>

                                                                        <div class="form-row">
                                                                            <div class="form-group col-6 m-0">
                                                                                सूक्ष्म सिंचन 
                                                                            </div>
                                                                             <div class="form-group col-6 m-0">
                                                                                <asp:Label ID="txtFineIrrigation" runat="server" placeholder="मूल्य लक्षात" onkeyup="GetSum2();" onblur="GetSum2();"  ></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                       <%-- <div class="form-row">
                                                                           
                                                                        </div>--%>

                                                                        <div class="form-row">
                                                                            <div class="form-group col-6 m-0">
                                                                                संरक्षित सिंचनाकरिता पाण्याची उपलब्धता 
                                                                            </div>
                                                                            <div class="form-group col-6 m-0">
                                                                                <asp:Label ID="txtAvailabilitywaterForProtectedIrrigation" runat="server" onkeyup="GetSum2();" onblur="GetSum2();" placeholder="मूल्य लक्षात"  ></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                       <%-- <div class="form-row">
                                                                            
                                                                        </div>--%>


                                                                    </div>




                                                                    <div class="form-group col-4 m-0" style="border: 1px solid #CCCCCC; padding-top: 5px; padding-bottom: 5px;">

                                                                        <label for="dob" class="col-form-label s-12" style="font-weight: bold">
                                                                            <asp:Literal ID="Literal11" Text="काढणी पश्चात व्यवस्थापन व हवामान अनुकूल मुल्य साखळीचे बळकटीकरण " runat="server"></asp:Literal></label>
                                                                        <br>

                                                                        <div class="form-row">
                                                                            <div class="form-group col-6 m-0">
                                                                                पायाभूत सुविधा निर्माण करणे
                                                                            </div>
                                                                            <div class="form-group col-6 m-0">
                                                                                <asp:Label ID="txtCreatingInfrastructure" runat="server" onkeyup="GetSum3();" onblur="GetSum3();" placeholder="मूल्य लक्षात"  ></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                       <%-- <div class="form-row">
                                                                            
                                                                        </div>--%>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-6 m-0">
                                                                                भाडे तत्वावर कृषि अवजारे केंद्र -सुविधा निर्मिती
                                                                            </div>
                                                                             <div class="form-group col-6 m-0">
                                                                                <asp:Label ID="txtAgriculturalEquipmentCenter" runat="server" onkeyup="GetSum3();" onblur="GetSum3();" placeholder="मूल्य लक्षात"  ></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                        <%--<div class="form-row">
                                                                           
                                                                        </div>--%>

                                                                        <div class="form-row">
                                                                            <div class="form-group col-6 m-0">
                                                                                हवामान अनुकूल वाणांचे पायाभूत आणि प्रमाणित बियाणे तयार करणे
                                                                            </div>
                                                                            <div class="form-group col-6 m-0">
                                                                                <asp:Label ID="txtClimatefriendlyvarieties" runat="server" onkeyup="GetSum3();" onblur="GetSum3();" placeholder="मूल्य लक्षात"  ></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                      <%--  <div class="form-row">
                                                                            
                                                                        </div>--%>

                                                                        <div class="form-row">
                                                                            <div class="form-group col-6 m-0">
                                                                                एकूण बियाणे हब-पायाभूत सुविधा विकास
                                                                            </div>
                                                                              <div class="form-group col-6 m-0">
                                                                                <asp:Label ID="txtTotalSeedHubInfrastructure" runat="server" onkeyup="GetSum3();" onblur="GetSum3();" placeholder="मूल्य लक्षात"  ></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                      <%--  <div class="form-row">
                                                                          
                                                                        </div>--%>






                                                                    </div>



                                                                </div>


                                                                <div class="form-row">
                                                                    <div class="form-group col-4 m-0" style="border: 1px solid #CCCCCC; padding-top: 5px; padding-bottom: 5px;">
                                                                        <div class="form-row">
                                                                            <div class="form-group col-6 m-0">
                                                                                एकूण एकंदर (लक्षात )
                                                                            </div>
                                                                            <div class="form-group col-6 m-0">
                                                                                <asp:Label ID="txtTotal1" runat="server" placeholder="एकूण एकंदर (लक्षात )" Style="width: 99% !important;"   ></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                       <%-- <div class="form-row">
                                                                            
                                                                        </div>--%>
                                                                    </div>
                                                                    <div class="form-group col-4 m-0" style="border: 1px solid #CCCCCC; padding-top: 5px; padding-bottom: 5px;">
                                                                        <div class="form-row">
                                                                            <div class="form-group col-6 m-0">
                                                                                एकूण एकंदर (लक्षात )
                                                                            </div>
                                                                            <div class="form-group col-6 m-0">
                                                                                <asp:Label ID="txtTotal2" runat="server" placeholder="एकूण एकंदर (लक्षात )" Style="width: 99% !important;"   ></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                        <%--<div class="form-row">
                                                                            
                                                                        </div>--%>
                                                                    </div>
                                                                    <div class="form-group col-4 m-0" style="border: 1px solid #CCCCCC; padding-top: 5px; padding-bottom: 5px;">
                                                                        <div class="form-row">
                                                                            <div class="form-group col-6 m-0">
                                                                                एकूण एकंदर (लक्षात )
                                                                            </div>
                                                                             <div class="form-group col-6 m-0">
                                                                                <asp:Label ID="txtTotal3" runat="server" placeholder="एकूण एकंदर (लक्षात )" Style="width: 99% !important;"   ></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                       <%-- <div class="form-row">
                                                                           
                                                                        </div>--%>
                                                                    </div>
                                                                </div>





                                                                <div class="form-row">

                                                                    
                                                                    
                                                                    <div class="form-group col-4 m-0">
                                                                        
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal38" Text="एकूण उपलब्ध अपधाव (TCM)" runat="server"></asp:Literal></label>
                                                                         </div>
                                                                        <div class="form-group col-4 m-0">
                                                                        <asp:Label ID="txtCom_UpscaleTCM" runat="server" placeholder="" ></asp:Label>

                                                                    </div>
                                                                    <div class="form-group col-4 m-0">
                                                                    </div>
                                                                     <div class="form-row">
                                                                    <div class="form-group col-4 m-0">
                                                                        
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal37" Text="सविस्तर प्रकल्प अहवालाची एकूण  रक्कम रु.(लक्षात )" runat="server"></asp:Literal></label>
                                                                        </div>
                                                                         <div class="form-group col-4 m-0">
                                                                        <asp:Label ID="txtCom_VillageDevelopmentPlan" runat="server" Style="width: 99% !important;"   ></asp:Label>

                                                                    </div>

                                                                </div>



                                                                <div class="form-row">
                                                                    <div class="form-group col-4 m-0">
                                                                       
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal39" Text="प्रकल्प अंमलबजवणीपूर्वी अडविलेला अपधाव  (TCM)" runat="server"></asp:Literal></label>
                                                                        <br>
                                                                        <asp:Label ID="txtCom_PreventionTCM" runat="server" placeholder="" ></asp:Label>

                                                                    </div>
                                                                    <div class="form-group col-4 m-0">
                                                                       
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal40" Text="शिल्लक अपधाव (TCM)" runat="server"></asp:Literal></label>
                                                                        <br>
                                                                        <asp:Label ID="txtCom_BalanceExclusionTCM" runat="server" ></asp:Label>

                                                                    </div>

                                                                    <div class="form-group col-4 m-0">
                                                                      
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal41" Text="प्रस्तावित कामामुळे अडविला जाणारा अपधाव (TCM)" runat="server"></asp:Literal></label>
                                                                        <br>
                                                                        <asp:Label ID="txtCom_TroubleshootTCM" runat="server" placeholder="" ></asp:Label>

                                                                    </div>
                                                                   


                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>



                                                </div>


                                                <div class="card no-b  no-r">
                                                    <div class="card-body">
                                                        <h5 class="card-title">
                                                            <asp:Literal ID="Literal6" Text="सविस्तर प्रकल्प आराखडयामध्ये प्रस्तावित मृद व जल संधारण कामाचा तपशील " runat="server"></asp:Literal></h5>

                                                    </div>
                                                    <div class="form-row">
                                                        <div class="form-group col-12">

                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:GridView ID="GrdWorkDetails" CssClass="table table-bordered table-hover" Style="" DataKeyNames="SoilAndWaterRetentionWorksID" runat="server" ShowFooter="True" AutoGenerateColumns="False" Width="100%">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="SoilAndWaterRetentionWorksGroup" HeaderText="" />
                                                                            <asp:BoundField DataField="SoilAndWaterRetentionWorksMr" HeaderText="प्रस्तावित मृद व जल संधारण कामे" />
                                                                            <asp:BoundField DataField="UnitofMess" HeaderText="परिमाण" />
                                                                            <asp:TemplateField HeaderText="१ ले वर्ष (भौतिक)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TextBox1" runat="server"  ToolTip="Physical" AutoPostBack="true" OnTextChanged="TextBox1_TextChangedPhysical" Text='<%# Bind("FYearPhysical") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="१ ले वर्ष आर्थिक (लक्षात)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TextBox2" runat="server"  AutoPostBack="true" ToolTip="Amount" OnTextChanged="TextBox1_TextChangedPhysical" Text='<%# Bind("FYearAmount") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="२ ले वर्ष (भौतिक)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TextBox3" runat="server"  AutoPostBack="true" ToolTip="Physical" OnTextChanged="TextBox1_TextChangedPhysical" Text='<%# Bind("SYearPhysical") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="२ ले वर्ष आर्थिक (लक्षात)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TextBox4" runat="server"  AutoPostBack="true" ToolTip="Amount" OnTextChanged="TextBox1_TextChangedPhysical" Text='<%# Bind("SYearAmount") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="३ ले वर्ष (भौतिक)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TextBox5" runat="server"  AutoPostBack="true" ToolTip="Physical" OnTextChanged="TextBox1_TextChangedPhysical" Text='<%# Bind("TYearPhysical") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="३ ले वर्ष आर्थिक (लक्षात)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TextBox6" runat="server"  AutoPostBack="true" ToolTip="Amount" OnTextChanged="TextBox1_TextChangedPhysical" Text='<%# Bind("TYearAmount") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="एकूण हेक्टर (भौतिक)">

                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("TotalPhysical") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="एकूण संख्या (भौतिक)">

                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("TotalPysicalNumber") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="एकूण आर्थिक (लक्षात)">

                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("TotalAmount") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                        <FooterStyle BackColor="#EBEBEB" Font-Bold="True" ForeColor="#990033" />
                                                                        <RowStyle Wrap="False" />
                                                                    </asp:GridView>

                                                                </ContentTemplate>
                                                              
                                                            </asp:UpdatePanel>


                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="card no-b  no-r">
                                                    <div class="card-body">
                                                        <h5 class="card-title">
                                                            <asp:Literal ID="Literal7" Text="कागदपत्रे" runat="server"></asp:Literal></h5>
                                                    </div>
                                                    <div class="form-row">
                                                        <div class="form-group col-12">

                                                            <div class="form-row">
                                                                <div class="form-group col-4 m-0">
                                                                    <label for="dob" class="col-form-label s-12">
                                                                        <asp:Literal ID="Literal8" Text="मंजूर व्हीडीपी / सविस्तर प्रकल्प अहवाल अपलोड करण्याची पीडीएफ प्रत" runat="server"></asp:Literal></label>
                                                                    <br>

                                                                    <asp:Label ID="Label4"  runat="server"></asp:Label>

                                                                   

                                                                </div>

                                                                <div class="form-group col-4 m-0">
                                                                  
                                                                    <label for="dob" class="col-form-label s-12">
                                                                        <asp:Literal ID="Literal3" Text="ई-टेंडरिंग करणारे कार्यालय" runat="server"></asp:Literal></label>
                                                                    <br>

                                                                    <asp:Label ID="Label3"  runat="server"></asp:Label>
                                                                </div>

                                                                 <div class="form-group col-4 m-0">
                                                                  
                                                                    <label for="dob" class="col-form-label s-12">
                                                                        <asp:Literal ID="Literal12" Text="MOM" runat="server"></asp:Literal></label>
                                                                    <br>

                                                                    <asp:Label ID="Label5"  runat="server"></asp:Label>
                                                                </div>



                                                            </div>

                                                        </div>
                                                    </div>

                                                </div>




                                                <div class="card no-b  no-r">
                                                    <div class="card-body">
                                                        <h5 class="card-title">
                                                            <asp:Literal ID="Literal26" Text="" runat="server"></asp:Literal></h5>

                                                    </div>
                                                  
                                                    <div class="form-row">
                                                       
                                                    </div>
                                                </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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

       <br /> <br /> <br /> <br />
    </form>
</body>
</html>
