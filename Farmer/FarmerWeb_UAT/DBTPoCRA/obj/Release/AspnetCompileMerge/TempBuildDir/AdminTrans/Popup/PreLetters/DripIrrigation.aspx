<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DripIrrigation.aspx.cs" Inherits="DBTPoCRA.AdminTrans.Popup.PreLetters.DripIrrigation" %>

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

        h4, h2, h3 {
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
                 <table class="auto-style1" style=" text-align: left; font-family: Calibri; font-size: 15px;">
                     <tr><td> 
                         <table class="auto-style1" style="  text-align: left; font-family: Calibri; font-size: 15px;">
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
                        <td colspan="3" style="font-weight: 700"><%--<h4 style="text-align: center">प्रपत्र -३</h4>--%><br />
                            <h4 style="text-align: center"> पूर्वसंमती पत्र</h4><br />
                            <h4 style="text-align: center">(प्रकल्प उपक्रम सांकेतांक- A3.7.1 व A3.7.2) </h4>        
                            
                        </td>
                    </tr>  
                             <tr>
                                 <td colspan="3">&nbsp;</td>
                             </tr>                  
                    
                    </table>
                         <table class="auto-style1" style="table-layout: fixed; text-align: left; font-family: Calibri; font-size: 15px;">
                          <tr>
                            <td></td>
                            <td></td>
                            <td> जा.क्र. <asp:Literal ID="Literal7" runat="server"></asp:Literal></td>
                         </tr>                   
                          <tr>
                            <td></td>
                            <td></td>
                            <td> कार्यालय- उपविभागीय कृषि अधिकारी, <asp:Literal ID="Literal8" runat="server"></asp:Literal></td>
                         </tr> 
                          <tr>
                            <td></td>
                            <td></td>
                            <td>उपविभाग <asp:Literal ID="Literal9" runat="server"></asp:Literal> , जिल्हा <asp:Literal ID="Literal10" runat="server"></asp:Literal></td>
                         </tr>  
                         <tr>
                             <td></td>
                             <td></td>
                             <td>दिनांक - <asp:Literal ID="Literal6" runat="server"></asp:Literal></td>
                         </tr>
                          <tr>
                            <td colspan="3">प्रति,</td>                        
                          </tr> 
                             <tr>
                                 <td colspan="3">श्री/श्रीमती <asp:Literal ID="Literal12" runat="server"></asp:Literal></td>
                                 </tr>
                             <tr>
                                 <td colspan="3">
                             मु.पो. <asp:Literal ID="Literal13" runat="server"></asp:Literal> तालूका <asp:Literal ID="Literal14" runat="server"></asp:Literal> जिल्हा <asp:Literal ID="Literal15" runat="server"></asp:Literal></td>
                             </tr>       
                              <tr>
                                  <td>&nbsp;&nbsp;</td>
                                  <td>&nbsp;</td>
                                  <td>&nbsp;</td>
                              </tr>
                             <tr>
                                 <td colspan="3" style="text-align:justify;">आपल्या दिनांक <asp:Literal ID="Literal4" runat="server"></asp:Literal> रोजीचा अर्ज ग्राम कृषि संजीवनी समितीने (VCRMC) दि <asp:Literal ID="Literal5" runat="server"></asp:Literal> रोजीच्या <asp:Literal ID="Literal16" runat="server"></asp:Literal> क्रमांकाच्या ठरावाने मंजूर केला आहे. सदरबाबत कृषि सहाय्यक यांना दि.<asp:Literal ID="Literal19" runat="server"></asp:Literal> रोजी प्रत्यक्ष भेट देऊन पूर्व संमती देणेबाबत शिफारस केलेली आहे. आपल्या अर्जानुसार, आपले  नावावर  
                                     सर्व्हे नं./गट नं. <asp:Literal ID="Literal11" runat="server"></asp:Literal> मधील <asp:Literal ID="Literal20" runat="server"></asp:Literal>.<asp:Literal ID="Literal17" runat="server"></asp:Literal> हे. क्षेत्रावर <asp:Literal ID="Literal18" runat="server"></asp:Literal> या पिकासाठी ना.दे.कृ.सं.प्र.सूक्ष्म सिंचन योजना २०१८-१९ मध्ये अनुदानावर ठिबक /तुषार संच बसविण्यासाठी पूर्वसंमती देण्यात येत आहे.  
                                 </td>
                             </tr>
                             <tr>
                                 <td colspan="3" style="text-align:justify;">सन २०१८-१९ या वर्षासाठी मा. आयुक्त, कृषि यांनी मान्यता दिलेल्या सूक्ष्म सिंचन संच उत्पादकापैकी आपल्या पसंतीनुसार कोणत्याही उत्पादकाकडून व कंपनी प्रतिनिधीने दिलेल्या आराखड्यानुसार संच बसविणे आवश्यक आहे. सदरचे पूर्वसंमतीपत्र दिल्याच्या तारखेपासून दोन महिन्याच्या (६० दिवस) आत संच बसवून व तो कार्यान्वित करून अनुदान मागणीचा  प्रस्ताव या कार्यालयास सादर करणे आपणास बंधनकारक आहे. विहित मुदतीत संच न बसवल्यास पूर्वसंमती पत्र आपोआप रद्द समजण्यात येईल .</td>
                             </tr>
                             <tr>
                                 <td colspan="3">&nbsp;</td>
                             </tr>
                             <tr>
                                 <td colspan="3" style="text-align:justify;">ठिबक व तुषार संच बसविण्यासाठी आवश्यक  साहित्य विहित  तांत्रिक निकषाप्रमाणे व BIS मानांकित असेल तरच आपला प्रस्ताव अनुदानासाठी पात्र राहिल याची नोंद घ्यावी. आपला प्रस्ताव मंजूर झाल्यास आपणास मार्गदर्शक सुचना व मंजूर मापदंडानुसार अनुज्ञेय अनुदान निधी उपलब्धतेनूसार मंजूर करण्यात येईल. तसेच सदरचे अनुदान आपल्या बँक खात्यावर DBT द्वारे जमा करण्यात येईल. आपल्या प्रकल्प प्रस्तावास मंजूरी देणे, देय अनुदानाची रक्कम/देय कालावधी इत्यादी बाबी ठरविण्याचा अंतिम अधिकार नानाजी देशमुख कृषि संजीवनी प्रकल्प/शासनाचा राहिल.</td>
                             </tr>                             
                             <tr>
                                 <td>&nbsp;&nbsp;</td>
                                 <td>&nbsp;</td>
                                 <td>&nbsp;</td>
                             </tr>
                                     <tr>
                                      <td></td>                        
                                      <td></td>
                                      <td><b><asp:Literal ID="litName" runat="server"></asp:Literal>,</b></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td><b>उपविभाग:</b> <asp:Literal ID="Literal2" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td><b>जिल्हा:</b> <asp:Literal ID="Literal3" runat="server"></asp:Literal> </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                                         </table><br />
                         </td> </tr>
               </table>
            </div>
        </center>
    </form>
</body>
</html>
