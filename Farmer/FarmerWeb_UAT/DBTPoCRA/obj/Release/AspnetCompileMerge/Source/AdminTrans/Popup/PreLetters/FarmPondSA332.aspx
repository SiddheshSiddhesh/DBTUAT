﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FarmPondSA332.aspx.cs" Inherits="DBTPoCRA.AdminTrans.Popup.PreLetters.FarmPondSA332" %>

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
            padding-right: 10px !important;
            padding-left: 10px !important;
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
            <div style="border: medium double #CCCCCC; width: 8in; padding-right: 15px; padding-left: 15px;">
             <table class="auto-style1" style="table-layout: fixed; text-align: left; font-family: Calibri; font-size: 15px; ">
              
                    <tr>
                        <td colspan="3" style="background-position: center top; font-weight: 700; height: 160px; text-align:right; background-image: url('../../../assets/img/basic/HeaderImage2.jpg'); background-repeat: no-repeat; vertical-align: top;">
                            <div id="Div1" runat="server" > 
                            &nbsp;
                             <%--<input id="Button1" class="noprint" onclick="window.print();" type="button" value="Print" />--%>
                            <a onclick="window.print();" href="#">Print</a>
                             &nbsp;
                            <%--<asp:HyperLink ID="HyperLink1" runat="server"  OnDataBinding="HyperLink1_DataBinding">PDF</asp:HyperLink>--%>
                            <asp:LinkButton ID="LinkButton2" CssClass="noprint" CausesValidation="false" runat="server" OnClick="LinkButton1_Click"  Height="22px" Width="50px">PDF</asp:LinkButton>
                                </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="font-weight: 700">
                           <%-- <h2 style="text-align: center">नानाजी देशमुख कृषी संजीवनी प्रकल्प, महाराष्ट्र शासन </h2>--%>
                        </td>
                    </tr>                   
                    <tr>
                        <td></td>
                        <td >
                          
                        </td>
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
                         <td colspan="3" style="text-align:center">&nbsp;घटक- शेततळे इनलेट व आउटलेट शिवाय (अस्तरीकरणासह) </td>
                     </tr>  
                    
                     <tr>
                         <td colspan="3" style="text-align:center">&nbsp; (प्रकल्प उपक्रम सांकेतांक- A3.3.2/A)</td>
                     </tr>                 
                    <tr>
                        <td></td>
                        <td>                       
                        </td>
                        <td> <%--जा.क्र.&nbsp;&nbsp;<asp:Literal ID="Literal1" runat="server"></asp:Literal>--%></td>
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
                        <td> उपविभाग&nbsp;&nbsp;<asp:Literal ID="Literal3" runat="server"></asp:Literal>,
                             जिल्हा&nbsp;&nbsp;<asp:Literal ID="Literal4" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>                       
                        </td>
                        <td> दिनांक -<asp:Literal ID="Literal5" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td>प्रति, </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="font-weight: 700">श्री/श्रीमती&nbsp;&nbsp;<asp:Literal ID="Literal6" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="font-weight: 700" class="auto-style2">मु.पो.&nbsp;&nbsp;<asp:Literal ID="Literal7" runat="server"></asp:Literal>-<asp:Literal ID="Literal23" runat="server"></asp:Literal>,
                        तालुका&nbsp;&nbsp;<asp:Literal ID="Literal8" runat="server"></asp:Literal>,</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="font-weight: 700">जिल्हा&nbsp;&nbsp;<asp:Literal ID="Literal9" runat="server"></asp:Literal></td>
                        <td></td>
                    </tr>
                   <tr>
                      <td colspan="3">
                      <table border="1" class="table-responsive" style=" border: 1px solid black; border-collapse: collapse;">
                       <tr>
                        <td>१ </td>
                        <td>आपल्या अर्जाचा दिनांक </td>
                        <td><asp:Literal ID="Literal10" runat="server"></asp:Literal></td>
                    </tr>
                       <tr>
                        <td> २</td>
                        <td>  मागणी अर्जाचा क्रमांक</td>
                        <td><asp:Literal ID="Literal21" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td>३</td>
                        <td>मागणी केलेला घटक</td>
                        <td><asp:Literal ID="Literal11" runat="server"></asp:Literal></td>
                    </tr>
                      <tr>
                        <td> ४</td>
                        <td> मागणी केलेल्या घटकाचा कोड</td>
                        <td><asp:Literal ID="Literal22" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td>५</td>
                        <td>ग्राम कृषि संजीवनी समिती (VCRMC) ने लाभार्थीच्या अर्जांला मान्यता देण्याचा दिनांक</td>
                        <td><asp:Literal ID="Literal12" runat="server"></asp:Literal></td>
                    </tr>
                     <tr>
                        <td>६</td>
                        <td>अर्जानुसार 8-अ चा क्रमांक व क्षेत्र (हे. आर.) </td>
                        <td><asp:Literal ID="Literal13" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td>७</td>
                        <td>७/१२ चा गट क्र./सर्व्हे नं.</td>
                        <td><asp:Literal ID="Literal14" runat="server"></asp:Literal></td>
                    </tr>
                           <tr>
                        <td>८</td>
                        <td>७/१२ चे क्षेत्र (हे. आर.)</td>
                        <td><asp:Literal ID="Literal19" runat="server"></asp:Literal>.<asp:Literal ID="Literal20" runat="server">  </asp:Literal>                 
                        </td>
                    </tr>
                    <tr>
                        <td>९</td>
                        <td>स्थळ पाहणी करून कृषि सहाय्यकाने शिफारस केल्याचा दिनांक </td>
                        <td><asp:Literal ID="Literal15" runat="server"></asp:Literal></td>
                    </tr>
                   
                          <tr>
                        <td>१०</td>
                        <td>घटक राबविण्यासाठीचा कालावधी (दिवस)</td>
                        <td><asp:Literal ID="Literal16" runat="server"></asp:Literal></td>
                    </tr>
                          <tr>
                        <td>११</td>
                         <td> पूर्व संमती दिलेल्या शेततळ्याचे आकारमान </td>
                        <td><asp:Literal ID="Literal26" runat="server"> &nbsp;या  प्रमाणे कार्यवाही करण्यात यावी.</asp:Literal></td>
                    </tr>
                      </table>    
                       </td>
                    </tr>
                    <tr>
                       <td colspan="3" style="text-align: justify"><br />
                         &nbsp;&nbsp;&nbsp; वरील अर्जाच्या अनुषंगाने आपणास कळविण्यात येते कि, ग्राम कृषि संजीवनी समितीने (VCRMC) वरील ठरावान्वये आपली लाभार्थी म्हणून निवड केलेली आहे. सदर निवड आपण प्रकल्पाच्या अटी व शर्तीच्या पूर्तता करण्याचे अधिन राहून करण्यात आली आहे. आपल्या मागणीनुसार शेततळे इनलेट व आउटलेट शिवाय (अस्तरीकरणासह) या बाबीसाठी प्रकल्पांतर्गत मार्गदर्शक सूचनेनुसार अनुज्ञेय रकमेसाठी पूर्वसंमती देण्यात येत आहे. सदर प्रकल्प उभारणीसाठी लाभार्थ्याने सर्व खर्च अगोदर करावयाचा आहे. सदरचे पूर्वसंमतीपत्र दिल्याच्या तारखेपासून दिलेल्या मुदतीत शेततळ्याचे काम पूर्ण करून आवश्यक कागदपत्र https://dbt.mahapocra.gov.in या संकेतस्थळावर किंवा DBT app वर अपलोड करून अनुदान मागणी करणे आवश्यक आहे. काम पूर्ण न करता फक्त कामाची देयके अपलोड केल्यास निधी अदायगी मध्ये विलंब होण्याची शक्यता नाकारता येत नाही. तसेच विहित मुदतीत काम पूर्ण न झाल्यास पूर्वसंमती पत्र आपोआप रद्द समजण्यात यावे.</td></tr>
                      <tr>
                       
                        <td colspan="3" style="text-align: justify"><br />
                          &nbsp;&nbsp;&nbsp; निश्चित केलेल्या ठिकाणी निश्चित केलेल्या आकारमानाचेच शेततळे घेणे बंधनकारक राहील. सदर कामासाठी कोणतीही आगाऊ रक्कम मिळणार नाही. मजूर/मशिनच्या मालकास शासनाकडून परस्पर पेमेंट केले जाणार नाही. इनलेट आउटलेट शिवाय असलेल्या शेततळ्याच्या बांधावर स्थानिक प्रजातीच्या वनस्पतींची लागवड करणे बंधनकारक राहील. शेततळ्याची निगा/दुरुस्तीची जबाबदारी शेतकऱ्यावर राहील. पावसाळ्यामध्ये शेततळ्यात गाळ वाहून येणार नाही अथवा साठणार नाही याची व्यवस्था करावी लागेल. शेततळे अस्तरीकरणासाठी BIS Standard मधील ५०० मायक्रॉन रीइनफोर्सड एचडीपीई जिओ मेंबरेन फिल्म IS 15351:2015 Type II या दर्जाची प्लास्टिक फिल्म वापरणे बंधनकारक आहे. अस्तरीकरणासाठी तांत्रिक निकषानुसार आवश्यक दर्जाच्या फिल्म उत्पादकाची/पुरवठादाराची निवड करण्याची आपणास मुभा आहे. अनुदान पात्रतेसाठी वरील दर्जा असलेले प्लास्टिक फिल्म च्या खरेदीचे बिल व अस्तरीकरण बसविण्याचे बिल जीएसटीसह संकेतस्थळावर अपलोड  करणे आवश्यक आहे. </td></tr>     <tr>
                     <td colspan="3" style="text-align: justify"><br />
                           &nbsp;&nbsp;&nbsp; शेततळ्याचे काम मार्गदर्शक सूचनेनुसार विहित तांत्रिक निकषाप्रमाणे व मंजूर अंदाजपत्रकानुसार असेल तरच आपला प्रस्ताव प्रत्यक्ष मोका तपासणीनुसार देय अनुदानासाठी पात्र राहिल. आपला प्रस्ताव मंजूर झाल्यानंतर मार्गदर्शक सुचना व मंजूर मापदंड/प्रत्यक्ष खर्चानुसार अनुज्ञेय अनुदान निधी उपलब्धतेनूसार मंजूर करण्यात येईल. सदरचे अनुदान आपल्या आधार संलग्न बँक खात्यावर DBT द्वारे जमा करण्यात येईल. आपल्या प्रस्तावास मंजूरी देणे, देय अनुदानाची रक्कम/देय कालावधी इत्यादी बाबी ठरविण्याचा अंतिम अधिकार नानाजी देशमुख कृषि संजीवनी प्रकल्प/शासनाचा राहिल.</td></tr>  <tr>
                     <td colspan="3" style="text-align: justify"><br />
                           &nbsp;&nbsp;&nbsp;  घटक अंमलबजावणीबाबत सविस्तर मार्गदर्शक सूचना www.mahapocra.gov.in या संकेतस्थळावर उपलब्ध आहेत. अधिक माहितीसाठी संबधित कृषि सहाय्यक यांचेकडे संपर्क साधावा.  </td>
                    </tr>
                      <tr>
                        <td>&nbsp;</td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: justify">   </td>     
                    </tr> 
                   
                
                    <tr>
                        <td></td>
                        <td></td><br />
                        <td><asp:Literal ID="litName" runat="server"></asp:Literal>, </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td><br />
                        <td> उपविभाग:&nbsp;&nbsp;<asp:Literal ID="Literal17" runat="server"></asp:Literal> </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td><br />
                        <td> जिल्हा:&nbsp;&nbsp;<asp:Literal ID="Literal18" runat="server"></asp:Literal></td>
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
