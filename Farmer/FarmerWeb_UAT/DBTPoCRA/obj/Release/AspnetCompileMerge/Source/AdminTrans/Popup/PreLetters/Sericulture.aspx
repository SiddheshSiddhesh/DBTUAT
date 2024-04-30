<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sericulture.aspx.cs" Inherits="DBTPoCRA.AdminTrans.Popup.PreLetters.Sericulture" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <style type="text/css">
        .auto-style1 {
            width: 100%;
            border-color: #CCCCCC !important;
            border-width: 1px;
            text-align: left;
            font-family: Calibri;
            font-size: 15px;
            border-collapse: collapse;
            text-align: left;
            margin: 0px !important;
            padding: 0px !important;
        }
        
        h3, h2 {
            margin: 0px !important;
            padding: 0px !important;
        }
    </style>
    <style>
        @media print {
            .noprint {
                display: none;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div style="border: medium double #CCCCCC; width: 8in;padding-right: 15px; padding-left: 15px;">
                <table class="auto-style1" style="table-layout: fixed; text-align: left; font-family: Calibri; font-size: 15px;">
                    <tr>
                         <td colspan="3" style="background-position: center top; font-weight: 700; height: 160px; text-align:right; background-image: url('../../../assets/img/basic/HeaderImage2.jpg'); background-repeat: no-repeat; vertical-align: top;">
                            <div id="DivPrint" runat="server" > 
                            &nbsp;
                             <%--<input id="Button1" class="noprint" onclick="window.print();" type="button" value="Print" />--%>
                            <a onclick="window.print();" href="#">Print</a>
                             &nbsp;
                            <%--<asp:HyperLink ID="HyperLink1" runat="server"  OnDataBinding="HyperLink1_DataBinding">PDF</asp:HyperLink>--%>
                            <asp:LinkButton ID="LinkButton1" CssClass="noprint" CausesValidation="false" runat="server" OnClick="LinkButton1_Click"  Height="22px" Width="50px">PDF</asp:LinkButton>
                                </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="font-weight: 700">
                            <h2 style="text-align: center">नानाजी देशमुख कृषी संजीवनी प्रकल्प, महाराष्ट्र शासन </h2>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="font-weight: 700"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="font-weight: 700">
                           <%-- <h3 style="text-align: center">प्रपत्र - 3 </h3>--%>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="font-weight: 700"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="font-weight: 700">
                            <h3 style="text-align: center">पूर्वसंमती पत्र  </h3>
                        </td>
                        <td></td>
                    </tr> 
                     <tr>
                         <td colspan="3">&nbsp;&nbsp;</td>
                     </tr>                  
                     <tr>
                        <td></td>
                        <td>                       
                        </td>
                        <td> जा.क्र.&nbsp;&nbsp;<asp:Literal ID="Literal1" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>                       
                        </td>
                        <td> कार्यालय-उपविभागीय कृषी अधिकारी,&nbsp;&nbsp;<asp:Literal ID="Literal2" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>                       
                        </td>
                        <td> उपविभाग&nbsp;&nbsp;<asp:Literal ID="Literal8" runat="server"></asp:Literal>,
                             जिल्हा&nbsp;&nbsp;<asp:Literal ID="Literal10" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>                       
                        </td>
                        <td> दिनांक -<asp:Literal ID="Literal7" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td>प्रति, </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="font-weight: 700">श्री/श्रीमती&nbsp;&nbsp; <asp:Literal ID="Literal3" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="font-weight: 700" class="auto-style2">मु.पो.&nbsp;&nbsp;<asp:Literal ID="Literal11" runat="server"></asp:Literal>
                       तालुका&nbsp;&nbsp;<asp:Literal ID="Literal12" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="font-weight: 700">जिल्हा&nbsp;&nbsp;<asp:Literal ID="Literal4" runat="server"></asp:Literal></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: justify">आपल्या दिनांक  <asp:Literal ID="Literal13" runat="server"></asp:Literal> रोजीच्या अर्जाप्रमाणे रेशीम उद्योगाच्या प्रस्तावास नानाजी देशमुख कृषि संजीवनी प्रकल्पांतर्गत मार्गदर्शक सूचनेनुसार अनुज्ञेय रकमेसाठी पूर्वसंमती देण्यात येत आहे. आपण सादर केलेल्या हमीपत्रात यापूर्वी या घटकासाठी <asp:Literal ID="Literal14" runat="server"></asp:Literal> लाभ घेतला नसल्याचे नमूद केलेले आहे.सदरचे पूर्वसंमती पत्र दिल्याच्या तारखेपासून <span style="color:red"> तीस</span> दिवसाच्या आत ग्राम कृषि संजीवनी मार्फत नियुक्त खरेदी समिती समवेत खरेदी करणे आवश्यक आहे. दिलेल्या   मुदतीत खरेदी न केल्यास आपणास दिलेली पूर्वसंमती आपोआप रद्द समजण्यात यावी.सदर प्रकल्प उभारणीसाठी लाभार्थ्याने सर्व खर्च अगोदर करावयाचा आहे. खरेदी प्रक्रिया पूर्ण झाल्यानंतर अनुदान मागणीचा परिपूर्ण प्रस्ताव <span style="color:red">15</span> दिवसात https://dbt.mahapocra.gov.in या संकेतस्थळावर किंवा DBT app वर अपलोड करावा. याबाबतचा अनुदान मागणी प्रस्ताव   <span style="color:red"> १ </span> महिन्याच्या आत सादर न केल्यास आपणास अनुदान मागणीचा हक्क राहणार नाही याची नोंद  घ्यावी. प्रकल्प उभारणी मार्गदर्शक सूचनेनुसार पूर्ण झाल्याची खात्री झाल्यानंतर आपणास  शासन निर्णयानुसार ( मार्गदर्शक सुचना व मंजूर मापदंड इत्यादी ) अनुज्ञेय अनुदान निधी उपलब्धते नूसार मंजूर करण्यात येईल.तसेच सदरचे अनुदान आपल्या आधार संलग्न बँक खात्यावर  जमा करण्यात येईल. आपल्या प्रकल्प प्रस्तावास मंजूरी देणे, देय अनुदानाची रक्कम/ देय कालावधी इत्यादी बाबी ठरविण्याचा  अंतिम अधिकार नानाजी देशमुख कृषि संजीवनी प्रकल्प / शासनाचा राहील     </td>
                    </tr>
                       <tr>
                        <td>&nbsp;</td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: justify">घटक अंमलबजावणीबाबत सविस्तर मार्गदर्शक सूचना www.mahapocra .gov.in या संकेतस्थळावर उपलब्ध आहेत. अधिक माहितीसाठी संबधित कृषि सहाय्यक यांचेकडे संपर्क साधावा.</td>
                        
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td> उपविभागीय कृषी अधिकारी, </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td> उपविभाग:&nbsp;&nbsp;<asp:Literal ID="Literal5" runat="server"></asp:Literal> </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td> जिल्हा:&nbsp;&nbsp;<asp:Literal ID="Literal6" runat="server"></asp:Literal></td>
                    </tr>
             
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
            </div>
        </center>
    </form>
</body>
</html>


