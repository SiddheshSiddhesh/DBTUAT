<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommunityRegistration.aspx.cs" EnableEventValidation="false" Inherits="DBTPoCRA.Registration.CommunityRegistration" %>

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

        .AshuText {
            min-width: 60px !important;
            padding: .2rem .2rem !important;
        }

        .s-12 {
            font-size: 13px !important;
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



        function GetSum1() {
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

            document.getElementById("txtTotal1").value = total.toFixed(2);
            Total();
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

            document.getElementById("txtTotal2").value = total.toFixed(2);
            Total();
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

            document.getElementById("txtTotal3").value = total.toFixed(2);
            Total();
        }


        function Total() {
            var a = document.getElementById("txtTotal1").value;
            if (a == "") a = "0";
            var b = document.getElementById("txtTotal2").value;
            if (b == "") b = "0";
            var c = document.getElementById("txtTotal3").value;
            if (c == "") c = "0";


            var total = parseFloat(a) + parseFloat(b) + parseFloat(c);

            document.getElementById("txtCom_VillageDevelopmentPlan").value = total.toFixed(2);
        }

    </script>

    <script>

        function MakePropercase(obj) {
            //if(obj.value!="")
            //{
            //    obj.value = parseFloat(obj.value).toFixed(2);
            //}
        }

        function pageLoad(sender, args) {
            $(document).ready(function () {

                // put all your javascript functions here 



                $(function () {
                    $(".f1").keyup(function () {
                        var total = 0;
                        $(".f1").each(function () {
                            if ($(this).val() != "") {
                                total += parseFloat($(this).val());
                            }
                        });

                        $(".totaloffield td").eq(3).text(total);

                        debugger;
                        //calculate total for current row
                        var val1 = $(this).val();
                        if (val1 == "") {
                            val1 = "0";
                        }
                        var val2 = $(this).parent().parent().find(".f2").val();
                        if (val2 == "") {
                            val2 = "0";
                        }
                        var val3 = $(this).parent().parent().find(".f3").val();
                        if (val3 == "") {
                            val3 = "0";
                        }
                        var val4 = $(this).parent().parent().find(".f4").val();
                        if (val4 == "") {
                            val4 = "0";
                        }
                        var val5 = $(this).parent().parent().find(".f5").val();
                        if (val5 == "") {
                            val5 = "0";
                        }
                        var val6 = $(this).parent().parent().find(".f6").val();
                        if (val6 == "") {
                            val6 = "0";
                        }

                        var rowtotal = parseFloat(val1) + parseFloat(val3) + parseFloat(val5);

                        $(this).parent().parent().find(".t1").text(rowtotal);
                        var total1 = 0;
                        $(".f1").each(function () {
                            if ($(this).parent().parent().find(".t1").text() != "") {
                                total1 += parseFloat($(this).parent().parent().find(".t1").text());
                            }
                        });

                        $(".totaloffield td").eq(11).text(total1);

                        var rowtotal1 = parseFloat(val2) + parseFloat(val4) + parseFloat(val6);


                        var unit = $(this).parent().parent().find(".f10").text();
                        if (unit == "1") {
                            $(this).parent().parent().find(".t2").text(rowtotal1);
                            var total1 = 0;
                            $(".f1").each(function () {
                                if ($(this).parent().parent().find(".t2").text() != "") {
                                    total1 += parseFloat($(this).parent().parent().find(".t2").text());
                                }
                            });

                            $(".totaloffield td").eq(9).text(total1);
                        }
                        else {
                            $(this).parent().parent().find(".t3").text(rowtotal1);

                            var total1 = 0;
                            $(".f1").each(function () {
                                if ($(this).parent().parent().find(".t3").text() != "") {
                                    total1 += parseFloat($(this).parent().parent().find(".t3").text());
                                }
                            });

                            $(".totaloffield td").eq(10).text(total1);
                        }


                    });

                    $(".f2").keyup(function () {
                        var total = 0;
                        $(".f2").each(function () {
                            if ($(this).val() != "") {
                                total += parseFloat($(this).val());
                            }
                        });

                        $(".totaloffield td").eq(4).text(total);

                        //calculate total for current row
                        var val1 = $(this).val();
                        if (val1 == "") {
                            val1 = "0";
                        }
                        var val2 = $(this).parent().parent().find(".f1").val();
                        if (val2 == "") {
                            val2 = "0";
                        }
                        var val3 = $(this).parent().parent().find(".f3").val();
                        if (val3 == "") {
                            val3 = "0";
                        }
                        var val4 = $(this).parent().parent().find(".f4").val();
                        if (val4 == "") {
                            val4 = "0";
                        }
                        var val5 = $(this).parent().parent().find(".f5").val();
                        if (val5 == "") {
                            val5 = "0";
                        }
                        var val6 = $(this).parent().parent().find(".f6").val();
                        if (val6 == "") {
                            val6 = "0";
                        }

                        var rowtotal = parseFloat(val1) + parseFloat(val3) + parseFloat(val5);
                        $(this).parent().parent().find(".t1").text(rowtotal);
                        var total1 = 0;
                        $(".f2").each(function () {
                            if ($(this).parent().parent().find(".t1").text() != "") {
                                total1 += parseFloat($(this).parent().parent().find(".t1").text());
                            }
                        });

                        $(".totaloffield td").eq(11).text(total1);

                        var rowtotal1 = parseFloat(val2) + parseFloat(val4) + parseFloat(val6);


                        var unit = $(this).parent().parent().find(".f10").text();
                        if (unit == "1") {
                            $(this).parent().parent().find(".t2").text(rowtotal1);
                            var total1 = 0;
                            $(".f2").each(function () {
                                if ($(this).parent().parent().find(".t2").text() != "") {
                                    total1 += parseFloat($(this).parent().parent().find(".t2").text());
                                }
                            });

                            $(".totaloffield td").eq(9).text(total1);
                        }
                        else {
                            $(this).parent().parent().find(".t3").text(rowtotal1);
                            var total1 = 0;
                            $(".f2").each(function () {
                                if ($(this).parent().parent().find(".t3").text() != "") {
                                    total1 += parseFloat($(this).parent().parent().find(".t3").text());
                                }
                            });

                            $(".totaloffield td").eq(10).text(total1);
                        }


                    });

                    $(".f3").keyup(function () {
                        var total = 0;
                        $(".f3").each(function () {
                            if ($(this).val() != "") {
                                total += parseFloat($(this).val());
                            }
                        });

                        $(".totaloffield td").eq(5).text(total);

                        //calculate total for current row
                        var val1 = $(this).val();
                        if (val1 == "") {
                            val1 = "0";
                        }
                        var val2 = $(this).parent().parent().find(".f1").val();
                        if (val2 == "") {
                            val2 = "0";
                        }
                        var val3 = $(this).parent().parent().find(".f2").val();
                        if (val3 == "") {
                            val3 = "0";
                        }
                        var val4 = $(this).parent().parent().find(".f4").val();
                        if (val4 == "") {
                            val4 = "0";
                        }
                        var val5 = $(this).parent().parent().find(".f5").val();
                        if (val5 == "") {
                            val5 = "0";
                        }
                        var val6 = $(this).parent().parent().find(".f6").val();
                        if (val6 == "") {
                            val6 = "0";
                        }

                        var rowtotal = parseFloat(val1) + parseFloat(val3) + parseFloat(val5);
                        $(this).parent().parent().find(".t1").text(rowtotal);
                        var total1 = 0;
                        $(".f3").each(function () {
                            if ($(this).parent().parent().find(".t1").text() != "") {
                                total1 += parseFloat($(this).parent().parent().find(".t1").text());
                            }
                        });

                        $(".totaloffield td").eq(11).text(total1);

                        var rowtotal1 = parseFloat(val2) + parseFloat(val4) + parseFloat(val6);


                        var unit = $(this).parent().parent().find(".f10").text();
                        if (unit == "1") {
                            $(this).parent().parent().find(".t2").text(rowtotal1);
                            var total1 = 0;
                            $(".f3").each(function () {
                                if ($(this).parent().parent().find(".t2").text() != "") {
                                    total1 += parseFloat($(this).parent().parent().find(".t2").text());
                                }
                            });

                            $(".totaloffield td").eq(9).text(total1);
                        }
                        else {
                            $(this).parent().parent().find(".t3").text(rowtotal1);
                            var total1 = 0;
                            $(".f3").each(function () {
                                if ($(this).parent().parent().find(".t3").text() != "") {
                                    total1 += parseFloat($(this).parent().parent().find(".t3").text());
                                }
                            });

                            $(".totaloffield td").eq(10).text(total1);
                        }


                    });

                    $(".f4").keyup(function () {
                        var total = 0;
                        $(".f4").each(function () {
                            if ($(this).val() != "") {
                                total += parseFloat($(this).val());
                            }
                        });

                        $(".totaloffield td").eq(6).text(total);

                        //calculate total for current row
                        var val1 = $(this).val();
                        if (val1 == "") {
                            val1 = "0";
                        }
                        var val2 = $(this).parent().parent().find(".f1").val();
                        if (val2 == "") {
                            val2 = "0";
                        }
                        var val3 = $(this).parent().parent().find(".f2").val();
                        if (val3 == "") {
                            val3 = "0";
                        }
                        var val4 = $(this).parent().parent().find(".f3").val();
                        if (val4 == "") {
                            val4 = "0";
                        }
                        var val5 = $(this).parent().parent().find(".f5").val();
                        if (val5 == "") {
                            val5 = "0";
                        }
                        var val6 = $(this).parent().parent().find(".f6").val();
                        if (val6 == "") {
                            val6 = "0";
                        }

                        var rowtotal = parseFloat(val1) + parseFloat(val3) + parseFloat(val5);
                        $(this).parent().parent().find(".t1").text(rowtotal);

                        var total1 = 0;
                        $(".f4").each(function () {
                            if ($(this).parent().parent().find(".t1").text() != "") {
                                total1 += parseFloat($(this).parent().parent().find(".t1").text());
                            }
                        });

                        $(".totaloffield td").eq(11).text(total1);

                        var rowtotal1 = parseFloat(val2) + parseFloat(val4) + parseFloat(val6);


                        var unit = $(this).parent().parent().find(".f10").text();
                        if (unit == "1") {
                            $(this).parent().parent().find(".t2").text(rowtotal1);
                            var total1 = 0;
                            $(".f4").each(function () {
                                if ($(this).parent().parent().find(".t2").text() != "") {
                                    total1 += parseFloat($(this).parent().parent().find(".t2").text());
                                }
                            });

                            $(".totaloffield td").eq(9).text(total1);
                        }
                        else {
                            $(this).parent().parent().find(".t3").text(rowtotal1);
                            var total1 = 0;
                            $(".f4").each(function () {
                                if ($(this).parent().parent().find(".t3").text() != "") {
                                    total1 += parseFloat($(this).parent().parent().find(".t3").text());
                                }
                            });

                            $(".totaloffield td").eq(10).text(total1);
                        }


                    });

                    $(".f5").keyup(function () {
                        var total = 0;
                        $(".f5").each(function () {
                            if ($(this).val() != "") {
                                total += parseFloat($(this).val());
                            }
                        });

                        $(".totaloffield td").eq(7).text(total);

                        //calculate total for current row
                        var val1 = $(this).val();
                        if (val1 == "") {
                            val1 = "0";
                        }
                        var val2 = $(this).parent().parent().find(".f1").val();
                        if (val2 == "") {
                            val2 = "0";
                        }
                        var val3 = $(this).parent().parent().find(".f2").val();
                        if (val3 == "") {
                            val3 = "0";
                        }
                        var val4 = $(this).parent().parent().find(".f3").val();
                        if (val4 == "") {
                            val4 = "0";
                        }
                        var val5 = $(this).parent().parent().find(".f4").val();
                        if (val5 == "") {
                            val5 = "0";
                        }
                        var val6 = $(this).parent().parent().find(".f6").val();
                        if (val6 == "") {
                            val6 = "0";
                        }

                        var rowtotal = parseFloat(val1) + parseFloat(val3) + parseFloat(val5);
                        $(this).parent().parent().find(".t1").text(rowtotal);
                        var total1 = 0;
                        $(".f5").each(function () {
                            if ($(this).parent().parent().find(".t1").text() != "") {
                                total1 += parseFloat($(this).parent().parent().find(".t1").text());
                            }
                        });

                        $(".totaloffield td").eq(11).text(total1);

                        var rowtotal1 = parseFloat(val2) + parseFloat(val4) + parseFloat(val6);


                        var unit = $(this).parent().parent().find(".f10").text();
                        if (unit == "1") {
                            $(this).parent().parent().find(".t2").text(rowtotal1);
                            var total1 = 0;
                            $(".f5").each(function () {
                                if ($(this).parent().parent().find(".t2").text() != "") {
                                    total1 += parseFloat($(this).parent().parent().find(".t2").text());
                                }
                            });

                            $(".totaloffield td").eq(9).text(total1);
                        }
                        else {
                            $(this).parent().parent().find(".t3").text(rowtotal1);
                            var total1 = 0;
                            $(".f5").each(function () {
                                if ($(this).parent().parent().find(".t3").text() != "") {
                                    total1 += parseFloat($(this).parent().parent().find(".t3").text());
                                }
                            });

                            $(".totaloffield td").eq(10).text(total1);
                        }


                    });

                    $(".f6").keyup(function () {
                        var total = 0;
                        $(".f6").each(function () {
                            if ($(this).val() != "") {
                                total += parseFloat($(this).val());
                            }
                        });

                        $(".totaloffield td").eq(8).text(total);

                        //calculate total for current row
                        var val1 = $(this).val();
                        if (val1 == "") {
                            val1 = "0";
                        }
                        var val2 = $(this).parent().parent().find(".f1").val();
                        if (val2 == "") {
                            val2 = "0";
                        }
                        var val3 = $(this).parent().parent().find(".f2").val();
                        if (val3 == "") {
                            val3 = "0";
                        }
                        var val4 = $(this).parent().parent().find(".f3").val();
                        if (val4 == "") {
                            val4 = "0";
                        }
                        var val5 = $(this).parent().parent().find(".f4").val();
                        if (val5 == "") {
                            val5 = "0";
                        }
                        var val6 = $(this).parent().parent().find(".f5").val();
                        if (val6 == "") {
                            val6 = "0";
                        }

                        var rowtotal = parseFloat(val1) + parseFloat(val3) + parseFloat(val5);
                        $(this).parent().parent().find(".t1").text(rowtotal);
                        var total1 = 0;
                        $(".f6").each(function () {
                            if ($(this).parent().parent().find(".t1").text() != "") {
                                total1 += parseFloat($(this).parent().parent().find(".t1").text());
                            }
                        });

                        $(".totaloffield td").eq(11).text(total1);

                        var rowtotal1 = parseFloat(val2) + parseFloat(val4) + parseFloat(val6);


                        var unit = $(this).parent().parent().find(".f10").text();
                        if (unit == "1") {
                            $(this).parent().parent().find(".t2").text(rowtotal1);
                            var total1 = 0;
                            $(".f6").each(function () {
                                if ($(this).parent().parent().find(".t2").text() != "") {
                                    total1 += parseFloat($(this).parent().parent().find(".t2").text());
                                }
                            });

                            $(".totaloffield td").eq(9).text(total1);
                        }
                        else {
                            $(this).parent().parent().find(".t3").text(rowtotal1);
                            var total1 = 0;
                            $(".f6").each(function () {
                                if ($(this).parent().parent().find(".t3").text() != "") {
                                    total1 += parseFloat($(this).parent().parent().find(".t3").text());
                                }
                            });

                            $(".totaloffield td").eq(10).text(total1);
                        }



                    });



                })

            });
        }
        //if (typeof (Sys) !== 'undefined') {
        //    //On UpdatePanel Refresh
        //    var prm = Sys.WebForms.PageRequestManager.getInstance();
        //    if (prm != null) {
        //        prm.add_endRequest(function (sender, e) {
        //            if (sender._postBackSettings.panelsToUpdate != null) {
        //                alert('fdg');
        //            }
        //        });
        //    };
        //}

    </script>


    <script language="javascript" type="text/javascript">
        var size = 2;
        var id = 0;

        function ProgressBar() {
            if (document.getElementById("FileUpload1").value != "") {
        document.getElementById("divProgress").style.display = "block";
        document.getElementById("divUpload").style.display = "block";
        id = setInterval("progress()", 20);
        return true;
    }
    else {
        alert("Select a file to upload");
        return false;
    }

}

function progress() {
    size = size + 1;
    if (size > 299) {
        clearTimeout(id);
    }
    document.getElementById("divProgress").style.width = size + "pt";
    document.getElementById("lblPercentage").firstChild.data = parseInt(size / 3) + "%";
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
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="badge badge-danger badge-mini2"
                                                                            runat="server" ErrorMessage="*65756756" SetFocusOnError="True" InitialValue="0" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlGramPanchayatCode"></asp:RequiredFieldValidator>
                                                                        <cc2:DropDownListChosen ID="ddlGramPanchayatCode" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match." OnSelectedIndexChanged="ddlGramPanchayatCode_SelectedIndexChanged" AutoPostBack="true">
                                                                        </cc2:DropDownListChosen>

                                                                    </div>
                                                                    <div class="form-group col-4 m-0">
                                                                        <label for="cnic" class="col-form-label s-12">
                                                                            <i class="icon-fingerprint"></i>
                                                                            <asp:Literal ID="Literal2" Text="ग्राम कृषि संजीवनी समितीचे नाव " runat="server"></asp:Literal><em style="color: red">*</em></label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="badge badge-danger badge-mini2"
                                                                            runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="ddlVCRMC"></asp:RequiredFieldValidator>

                                                                        <cc2:DropDownListChosen ID="ddlVCRMC" runat="server" CssClass="form-control" AllowSingleDeselect="True" DataPlaceHolder="Type Here to Search..." DisableSearchThreshold="0" NoResultsText="No results match.">
                                                                        </cc2:DropDownListChosen>
                                                                    </div>
                                                                    <div class="form-group col-4 m-0">
                                                                        <label for="cnic" class="col-form-label s-12">
                                                                            .
                                                                        </label>
                                                                        <br />
                                                                        <asp:Button ID="btnMlp" CausesValidation="false" runat="server" CssClass="btn btn-info" Text="Get Data From MLP APP " OnClick="btnMlp_Click" />
                                                                    </div>

                                                                </div>
                                                                <hr />


                                                                <div class="form-row">

                                                                    <div class="form-group col-4 m-0">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" CssClass="badge badge-danger badge-mini2"
                                                                            runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtCom_PeriodofMicroplanningFrom"></asp:RequiredFieldValidator>
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal31" Text="सूक्ष्म नियोजनाचा कालावधी <br>  पासून" runat="server"></asp:Literal></label>
                                                                        <br>
                                                                        <asp:TextBox ID="txtCom_PeriodofMicroplanningFrom" runat="server" placeholder="Date in DD/MM/YYYY" class="form-control r-0 light s-12 "></asp:TextBox>
                                                                        <ajax:CalendarExtender CssClass="cal_Theme1" ID="Calendar1" runat="server" OnClientDateSelectionChanged="checkDate" TargetControlID="txtCom_PeriodofMicroplanningFrom" Format="dd/MM/yyyy"></ajax:CalendarExtender>

                                                                    </div>
                                                                    <div class="form-group col-4 m-0">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="badge badge-danger badge-mini2"
                                                                            runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtCom_PeriodofMicroplanningFromTo"></asp:RequiredFieldValidator>
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal10" Text="सूक्ष्म नियोजनाचा कालावधी <br> पर्यंत" runat="server"></asp:Literal></label>
                                                                        <br>
                                                                        <asp:TextBox ID="txtCom_PeriodofMicroplanningFromTo" runat="server" placeholder="Date in DD/MM/YYYY" class="form-control r-0 light s-12 "></asp:TextBox>
                                                                        <ajax:CalendarExtender CssClass="cal_Theme1" ID="CalendarExtender3" runat="server" OnClientDateSelectionChanged="checkDate" TargetControlID="txtCom_PeriodofMicroplanningFromTo" Format="dd/MM/yyyy"></ajax:CalendarExtender>

                                                                    </div>


                                                                    <div class="form-group col-4 m-0">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" CssClass="badge badge-danger badge-mini2"
                                                                            runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtCom_ProjectReportDate"></asp:RequiredFieldValidator>
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal32" Text="सविस्तर प्रकल्प अहवालास  ग्राम कृषि संजीवनी समितीचा <br> मान्यता दिनांक" runat="server"></asp:Literal></label>
                                                                        <br>
                                                                        <asp:TextBox ID="txtCom_ProjectReportDate" runat="server" placeholder="Date in DD/MM/YYYY" class="form-control r-0 light s-12 "></asp:TextBox>
                                                                        <ajax:CalendarExtender CssClass="cal_Theme1" ID="CalendarExtender1" OnClientDateSelectionChanged="checkDate" runat="server" TargetControlID="txtCom_ProjectReportDate" Format="dd/MM/yyyy"></ajax:CalendarExtender>

                                                                    </div>



                                                                </div>

                                                                <div class="form-row">
                                                                    <div class="form-group col-4 m-0">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" CssClass="badge badge-danger badge-mini2"
                                                                            runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtCom_DCCApprovalDate"></asp:RequiredFieldValidator>
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal33" Text="सविस्तर प्रकल्प अहवालास  जिल्हा समन्वय समितीचा <br> मान्यता दिनांक" runat="server"></asp:Literal></label>
                                                                        <br>
                                                                        <asp:TextBox ID="txtCom_DCCApprovalDate" runat="server" placeholder="Date in DD/MM/YYYY" class="form-control r-0 light s-12 "></asp:TextBox>
                                                                        <ajax:CalendarExtender CssClass="cal_Theme1" ID="CalendarExtender2" OnClientDateSelectionChanged="checkDate" runat="server" TargetControlID="txtCom_DCCApprovalDate" Format="dd/MM/yyyy"></ajax:CalendarExtender>

                                                                    </div>

                                                                    <%--<div class="form-group col-4 m-0">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" CssClass="badge badge-danger badge-mini2"
                                                                            runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtCom_For21ProjectReport"></asp:RequiredFieldValidator>
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal34" Text="सविस्तर प्रकल्प अहवालातील प्रपत्र क्र.२१ नुसार हवामान अनुकूल कृषि पद्धतीस <br> प्रोत्साहन (१+२+३ +४+५)" runat="server"></asp:Literal></label>
                                                                        <br>
                                                                        <asp:TextBox ID="txtCom_For21ProjectReport" runat="server" class="form-control r-0 light s-12 "></asp:TextBox>

                                                                    </div>

                                                                    <div class="form-group col-4 m-0">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" CssClass="badge badge-danger badge-mini2"
                                                                            runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtCom_UseOfWaterProjectReport"></asp:RequiredFieldValidator>
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal35" Text="सविस्तर प्रकल्प अहवालातील प्रपत्र क्र.२१ नुसार पाण्याचा कार्यक्षम व शाश्वत पद्धतीने <br> वापर (१+२+३+४ +५+६+७ +८)" runat="server"></asp:Literal></label>
                                                                        <br>
                                                                        <asp:TextBox ID="txtCom_UseOfWaterProjectReport" runat="server" placeholder="" class="form-control r-0 light s-12 "></asp:TextBox>

                                                                    </div>--%>
                                                                </div>



                                                                <br />
                                                                <div class="form-row">
                                                                    <div class="form-group col-4 m-0" style="border: 1px solid #CCCCCC; padding-top: 5px; padding-bottom: 5px;">

                                                                        <label for="dob" class="col-form-label s-12" style="font-weight: bold">
                                                                            <asp:Literal ID="Literal4" Text="हवामान अनुकूल कृषि पद्धतीस प्रोत्साहन" runat="server"></asp:Literal></label>
                                                                        <br>

                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                वानिकी आधारीत शेती पद्धती
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                <asp:TextBox ID="txtForestrybasedfarmingPractices" runat="server" onkeyup="GetSum1();" onblur="MakePropercase(this);GetSum1();" placeholder="मूल्य लक्षात" class="form-control r-0 light s-12 text-right "></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                एकूण  फळबाग लागवड
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                <asp:TextBox ID="txtTotalorchardPlanting" runat="server" placeholder="मूल्य लक्षात" onkeyup="GetSum1();" onblur="MakePropercase(this);GetSum1();" class="form-control r-0 light s-12 text-right "></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                एकूण क्षारपड जमिन व्यवस्थापन
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                <asp:TextBox ID="txtTotalalkalinelandmanagement" runat="server" placeholder="मूल्य लक्षात" onkeyup="GetSum1();" onblur="MakePropercase(this);GetSum1();" class="form-control r-0 light s-12 text-right "></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                एकूण संरक्षित शेती
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                <asp:TextBox ID="txtTotalprotectedfarming" runat="server" placeholder="मूल्य लक्षात" onkeyup="GetSum1();" onblur="MakePropercase(this);GetSum1();" class="form-control r-0 light s-12 text-right"></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                एकूण एकात्मिक शेती
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                <asp:TextBox ID="txtTotalIntegratedfarming" runat="server" placeholder="मूल्य लक्षात" onkeyup="GetSum1();" onblur="MakePropercase(this);GetSum1();" class="form-control r-0 light s-12 text-right"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                एकूण जमिन आरोग्य सुधारणे
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                <asp:TextBox ID="ImprovingoverallSoilhealth" runat="server" placeholder="मूल्य लक्षात" onkeyup="GetSum1();" onblur="MakePropercase(this);GetSum1();" class="form-control r-0 light s-12 text-right "></asp:TextBox>
                                                                            </div>
                                                                        </div>





                                                                    </div>



                                                                    <div class="form-group col-4 m-0" style="border: 1px solid #CCCCCC; padding-top: 5px; padding-bottom: 5px;">

                                                                        <label for="dob" class="col-form-label s-12" style="font-weight: bold">
                                                                            <asp:Literal ID="Literal5" Text="पाण्याचा कार्यक्षम व शाश्वत पद्धतीने वापर " runat="server"></asp:Literal></label>
                                                                        <br>

                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                एकूण पाण्याचा कार्यक्षम व शाश्वत पद्धतीने वापर
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                <asp:TextBox ID="txtEfficientAndsustainableuseoftotalwater" onkeyup="GetSum2();" onblur="MakePropercase(this);GetSum2();" runat="server" placeholder="मूल्य लक्षात" class="form-control r-0 light s-12 text-right"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                मुलस्थानी जलसंधारण 
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                <asp:TextBox ID="txtWaterStorageAtthebase" runat="server" onkeyup="GetSum2();" onblur="MakePropercase(this);GetSum2();" placeholder="मूल्य लक्षात" class="form-control r-0 light s-12 text-right"></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                सूक्ष्म सिंचन 
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                <asp:TextBox ID="txtFineIrrigation" runat="server" placeholder="मूल्य लक्षात" onkeyup="GetSum2();" onblur="MakePropercase(this);GetSum2();" class="form-control r-0 light s-12 text-right"></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                संरक्षित सिंचनाकरिता पाण्याची उपलब्धता 
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                <asp:TextBox ID="txtAvailabilitywaterForProtectedIrrigation" runat="server" onkeyup="GetSum2();" onblur="MakePropercase(this);GetSum2();" placeholder="मूल्य लक्षात" class="form-control r-0 light s-12 text-right"></asp:TextBox>
                                                                            </div>
                                                                        </div>


                                                                    </div>




                                                                    <div class="form-group col-4 m-0" style="border: 1px solid #CCCCCC; padding-top: 5px; padding-bottom: 5px;">

                                                                        <label for="dob" class="col-form-label s-12" style="font-weight: bold">
                                                                            <asp:Literal ID="Literal11" Text="काढणी पश्चात व्यवस्थापन व हवामान अनुकूल मुल्य साखळीचे बळकटीकरण " runat="server"></asp:Literal></label>
                                                                        <br>

                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                पायाभूत सुविधा निर्माण करणे
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                <asp:TextBox ID="txtCreatingInfrastructure" runat="server" onkeyup="GetSum3();" onblur="MakePropercase(this);GetSum3();" placeholder="मूल्य लक्षात" class="form-control r-0 light s-12 text-right"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                भाडे तत्वावर कृषि अवजारे केंद्र -सुविधा निर्मिती
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                <asp:TextBox ID="txtAgriculturalEquipmentCenter" runat="server" onkeyup="GetSum3();" onblur="MakePropercase(this);GetSum3();" placeholder="मूल्य लक्षात" class="form-control r-0 light s-12 text-right"></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                हवामान अनुकूल वाणांचे पायाभूत आणि प्रमाणित बियाणे तयार करणे
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                <asp:TextBox ID="txtClimatefriendlyvarieties" runat="server" onkeyup="GetSum3();" onblur="MakePropercase(this);GetSum3();" placeholder="मूल्य लक्षात" class="form-control r-0 light s-12 text-right"></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                एकूण बियाणे हब-पायाभूत सुविधा विकास
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                <asp:TextBox ID="txtTotalSeedHubInfrastructure" runat="server" onkeyup="GetSum3();" onblur="MakePropercase(this);GetSum3();" placeholder="मूल्य लक्षात" class="form-control r-0 light s-12 text-right"></asp:TextBox>
                                                                            </div>
                                                                        </div>






                                                                    </div>



                                                                </div>


                                                                <div class="form-row">
                                                                    <div class="form-group col-4 m-0" style="border: 1px solid #CCCCCC; padding-top: 5px; padding-bottom: 5px;">
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                एकूण एकंदर (लक्षात )
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                <asp:TextBox ID="txtTotal1" runat="server" placeholder="एकूण एकंदर (लक्षात )" Style="width: 99% !important;" Enabled="false" class="form-control r-0 light s-12 text-right"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group col-4 m-0" style="border: 1px solid #CCCCCC; padding-top: 5px; padding-bottom: 5px;">
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                एकूण एकंदर (लक्षात )
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                <asp:TextBox ID="txtTotal2" runat="server" placeholder="एकूण एकंदर (लक्षात )" Style="width: 99% !important;" Enabled="false" class="form-control r-0 light s-12 text-right"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group col-4 m-0" style="border: 1px solid #CCCCCC; padding-top: 5px; padding-bottom: 5px;">
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                एकूण एकंदर (लक्षात )
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-row">
                                                                            <div class="form-group col-12 m-0">
                                                                                <asp:TextBox ID="txtTotal3" runat="server" placeholder="एकूण एकंदर (लक्षात )" Style="width: 99% !important;" Enabled="false" class="form-control r-0 light s-12 text-right"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>









                                                                <div class="form-row">



                                                                    <div class="form-group col-4 m-0">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" CssClass="badge badge-danger badge-mini2"
                                                                            runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtCom_UpscaleTCM"></asp:RequiredFieldValidator>
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal38" Text="एकूण उपलब्ध अपधाव (TCM)" runat="server"></asp:Literal></label>
                                                                        <br>
                                                                        <asp:TextBox ID="txtCom_UpscaleTCM" runat="server" placeholder="" class="form-control r-0 light s-12 "></asp:TextBox>

                                                                    </div>
                                                                    <div class="form-group col-4 m-0">
                                                                    </div>
                                                                    <div class="form-group col-4 m-0">
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator20" CssClass="badge badge-danger badge-mini2"
                                                                            runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtCom_VillageDevelopmentPlan"></asp:RequiredFieldValidator>--%>
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal37" Text="सविस्तर प्रकल्प अहवालाची एकूण  रक्कम रु.(लक्षात )" runat="server"></asp:Literal></label>
                                                                        <br>
                                                                        <asp:TextBox ID="txtCom_VillageDevelopmentPlan" runat="server" Style="width: 99% !important;" Enabled="false" class="form-control r-0 light s-12 "></asp:TextBox>
                                                                        <asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtCom_VillageDevelopmentPlan" MinimumValue="0" MaximumValue="9999" ForeColor="Red" SetFocusOnError="true" Type="Double" runat="server" ErrorMessage="invalid"></asp:RangeValidator>
                                                                    </div>

                                                                </div>



                                                                <div class="form-row">
                                                                    <div class="form-group col-4 m-0">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" CssClass="badge badge-danger badge-mini2"
                                                                            runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtCom_PreventionTCM"></asp:RequiredFieldValidator>
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal39" Text="प्रकल्प अंमलबजवणीपूर्वी अडविलेला अपधाव  (TCM)" runat="server"></asp:Literal></label>
                                                                        <br>
                                                                        <asp:TextBox ID="txtCom_PreventionTCM" runat="server" placeholder="" class="form-control r-0 light s-12 "></asp:TextBox>

                                                                    </div>
                                                                    <div class="form-group col-4 m-0">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" CssClass="badge badge-danger badge-mini2"
                                                                            runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtCom_BalanceExclusionTCM"></asp:RequiredFieldValidator>
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal40" Text="शिल्लक अपधाव (TCM)" runat="server"></asp:Literal></label>
                                                                        <br>
                                                                        <asp:TextBox ID="txtCom_BalanceExclusionTCM" runat="server" class="form-control r-0 light s-12 "></asp:TextBox>

                                                                    </div>

                                                                    <div class="form-group col-4 m-0">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" CssClass="badge badge-danger badge-mini2"
                                                                            runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtCom_TroubleshootTCM"></asp:RequiredFieldValidator>
                                                                        <label for="dob" class="col-form-label s-12">
                                                                            <asp:Literal ID="Literal41" Text="प्रस्तावित कामामुळे अडविला जाणारा अपधाव (TCM)" runat="server"></asp:Literal></label>
                                                                        <br>
                                                                        <asp:TextBox ID="txtCom_TroubleshootTCM" runat="server" placeholder="" class="form-control r-0 light s-12 "></asp:TextBox>

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

                                                            <div style="width: 100%; overflow: auto; height: 600px;">
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:GridView ID="GrdWorkDetails" CssClass="table table-bordered table-hover" Style="" DataKeyNames="SoilAndWaterRetentionWorksID,ActivityCode" runat="server" ShowFooter="True" AutoGenerateColumns="False" Width="96%" Font-Size="14px">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="SoilAndWaterRetentionWorksGroup" HeaderText="" />
                                                                                <asp:BoundField DataField="SoilAndWaterRetentionWorksMr" HeaderText="प्रस्तावित मृद व जल संधारण कामे" ItemStyle-Wrap="False" />
                                                                                <asp:BoundField DataField="UnitofMess" HeaderText="परिमाण" />
                                                                                <asp:TemplateField HeaderText="१ ले वर्ष (भौतिक)">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="TextBox1" runat="server" Width="80px" onblur="MakePropercase(this);" class="form-control f1" ToolTip="Physical" AutoPostBack="false" OnTextChanged="TextBox1_TextChangedPhysical" Text='<%# Bind("FYearPhysical") %>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="१ ले वर्ष (लाख)">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="TextBox2" runat="server" Width="80px" onblur="MakePropercase(this);" class="form-control f2" AutoPostBack="false" ToolTip="Amount" OnTextChanged="TextBox1_TextChangedPhysical" Text='<%# Bind("FYearAmount") %>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="२ ले वर्ष (भौतिक)">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="TextBox3" runat="server" Width="80px" onblur="MakePropercase(this);" class="form-control f3" AutoPostBack="false" ToolTip="Physical" OnTextChanged="TextBox1_TextChangedPhysical" Text='<%# Bind("SYearPhysical") %>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="२ ले वर्ष आर्थिक (लाख)">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="TextBox4" runat="server" Width="80px" onblur="MakePropercase(this);" class="form-control f4" AutoPostBack="false" ToolTip="Amount" OnTextChanged="TextBox1_TextChangedPhysical" Text='<%# Bind("SYearAmount") %>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="३ ले वर्ष (भौतिक)">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="TextBox5" runat="server" Width="80px" onblur="MakePropercase(this);" class="form-control f5" AutoPostBack="false" ToolTip="Physical" OnTextChanged="TextBox1_TextChangedPhysical" Text='<%# Bind("TYearPhysical") %>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="३ ले वर्ष आर्थिक (लाख)">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="TextBox6" runat="server" Width="80px" onblur="MakePropercase(this);" class="form-control f6" AutoPostBack="false" ToolTip="Amount" OnTextChanged="TextBox1_TextChangedPhysical" Text='<%# Bind("TYearAmount") %>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="एकूण हेक्टर (भौतिक)">

                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTotalPysical" runat="server" CssClass="t2" Text='<%# Bind("TotalPhysical") %>'></asp:Label>
                                                                                        <div style="display: none;">
                                                                                            <asp:Label ID="Label3" runat="server" CssClass="f10" Text='<%# Bind("UnitofMessId") %>'></asp:Label>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="एकूण संख्या (भौतिक)">

                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTotalPysicalNumber" runat="server" CssClass="t3" Text='<%# Bind("TotalPysicalNumber") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="एकूण आर्थिक (लाख)">

                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label2" runat="server" CssClass="t1" Text='<%# Bind("TotalAmount") %>'></asp:Label>

                                                                                        <asp:HiddenField ID="hfActivityID" runat="server" Value='<%# Bind("ActivityID") %>' Visible="false"></asp:HiddenField>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                            <FooterStyle BackColor="#EBEBEB" CssClass="totaloffield" Font-Bold="True" ForeColor="#990033" />

                                                                            <RowStyle Wrap="False" />
                                                                        </asp:GridView>

                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="GrdWorkDetails" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </div>

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

                                                                <div class="form-group col-6 m-0">
                                                                    <label for="dob" class="col-form-label s-12">
                                                                        <asp:Literal ID="Literal8" Text="मंजूर व्हीडीपी / सविस्तर प्रकल्प अहवाल अपलोड करण्याची पीडीएफ प्रत अपलोड करा" runat="server"></asp:Literal></label>
                                                                    <br>


                                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:FileUpload ID="FileUpload1" class="form-control r-0 light s-12 " runat="server" />
                                                                            <asp:Label ID="LabelAnyOtherCertificate" runat="server" Text=""></asp:Label>


                                                                            <div id="divUpload" style="display: none">
                                                                                <div style="width: 300pt; text-align: center;">
                                                                                    Uploading Please wait ...
                                                                                </div>
                                                                                <div style="width: 300pt; height: 20px; border: solid 1pt gray">
                                                                                    <div id="divProgress" runat="server" style="width: 1pt; height: 20px; background-color: orange; display: none">
                                                                                    </div>
                                                                                </div>
                                                                                <div style="width: 300pt; text-align: center;">
                                                                                    <asp:Label ID="lblPercentage" runat="server" Text="Label"></asp:Label>
                                                                                </div>
                                                                                <br />
                                                                                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                                            </div>


                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="btnUPLOAD" />

                                                                        </Triggers>
                                                                    </asp:UpdatePanel>

                                                                </div>


                                                                <div class="form-group col-6 m-0">

                                                                    <label for="dob" class="col-form-label s-12">.</label>
                                                                    <br />
                                                                    <asp:Button ID="btnUPLOAD" runat="server" Text="UPLOAD" CausesValidation="false" CssClass="btn btn-danger" OnClick="btnUPLOAD_Click" OnClientClick="return ProgressBar()" />
                                                                </div>



                                                            </div>

                                                            <div class="form-row">
                                                                <div class="form-group col-6 m-0">
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="badge badge-danger badge-mini2"
                                                                        runat="server" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic" ToolTip="Required" ControlToValidate="txtCom_TroubleshootTCM"></asp:RequiredFieldValidator>--%>
                                                                    <label for="dob" class="col-form-label s-12">
                                                                        <asp:Literal ID="Literal3" Text="ई-टेंडरिंग करणारे कार्यालय" runat="server"></asp:Literal></label>
                                                                    <br>
                                                                    <asp:DropDownList ID="DropDownList1" class="form-control" runat="server">

                                                                        <asp:ListItem Text="SDAO office" Value="SDAO"> </asp:ListItem>
                                                                        <asp:ListItem Text="VCRMC" Value="VCRMC"> </asp:ListItem>
                                                                    </asp:DropDownList>
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
                                                    <div class="form-row" style="display: none;">
                                                        <%--<div class="form-group col-1 m-0"></div>--%>
                                                        <div class="form-group col-12 m-0">

                                                            <ul>

                                                                <li>
                                                                    <asp:CheckBox ID="CheckBox1" Checked="true" runat="server" /><asp:Literal ID="Literal28" Text="मी याद्वारे माझ्या मोबाइलद्वारे प्रकल्पाशी संबंधित क्रियाकलापांची माहिती प्राप्त करण्यास संमती देतो." runat="server"></asp:Literal></li>
                                                                <li>
                                                                    <asp:CheckBox ID="CheckBox2" Checked="true" runat="server" /><asp:Literal ID="Literal29" Text="समुदायाच्या वतीने मी पुरविलेल्या सर्व माहिती माझ्या ज्ञान आणि विश्वासानुसार सत्य आणि योग्य आहेत." runat="server"></asp:Literal></li>
                                                                <li>
                                                                    <asp:CheckBox ID="CheckBox3" Checked="true" runat="server" /><asp:Literal ID="Literal30" Text="मला समजले आहे की वितरण समुदायाच्या नोंदणीकृत बँक खात्यात जमा केले जाईल." runat="server"></asp:Literal></li>


                                                            </ul>
                                                        </div>
                                                        <%-- <div class="form-group col-1 m-0"></div>--%>
                                                    </div>
                                                    <div class="form-row">
                                                        <div class="form-group col-6 m-0" style="text-align: right">

                                                            <asp:Button ID="btnDraft" runat="server" Text="Save Draft" Visible="false" CssClass="btn btn-info" OnClick="btnDraft_Click" />
                                                        </div>
                                                        <div class="form-group col-6 m-0" style="text-align: left">
                                                           <%-- <asp:Button ID="btnFinalSave" runat="server" Text="Submit" Width="150px" CssClass="btn btn-success" OnClick="btnSaveAAudhar_Click" />--%>
                                                            <asp:Button ID="btnSaveAAudhar" runat="server" Text="Submit" Width="150px" CssClass="btn btn-success" OnClick="btnSaveAAudhar_Click" />
                                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" CssClass="btn btn-danger" OnClick="btnUpdate_Click" />
                                                            <br />
                                                            <br />
                                                        </div>
                                                    </div>
                                                </div>

                                                </label>

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
                <div style="position: absolute; top: 50px; left: 25%">

                    <img src="../assets/img/basic/processing.gif" />

                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <iframe id="Defib" src="~/AdminTrans/KeepAlive.aspx" frameborder="0" width="0" height="0" runat="server"></iframe>
    </form>
</body>
</html>
