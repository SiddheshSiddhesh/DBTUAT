<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeedProduction.aspx.cs" Inherits="DBTPoCRA.AdminTrans.Popup.PreLetters.SeedProduction" %>

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
            <div style="border: medium double #CCCCCC; width: 8in;padding-right: 15px; padding-left: 15px;">
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
                        <td colspan="3" style="font-weight: 700"><%--<h4 style="text-align: center">प्रपत्र-२</h4>--%><br />
                            <h4 style="text-align: center"> पूर्वसंमती पत्र</h4><br />
                            <h4 style="text-align: center">(प्रकल्प उपक्रम सांकेतांक-B3.1.1) </h4>        
                            
                        </td>
                    </tr>                    
                    <tr>
                         <td colspan="3">&nbsp;&nbsp;</td>
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
                            <td> जिल्हा <asp:Literal ID="Literal10" runat="server"></asp:Literal></td>
                         </tr>  
                         <tr>
                             <td></td>
                             <td></td>
                             <td>दिनांक - <asp:Literal ID="Literal6" runat="server"></asp:Literal></td>
                         </tr>
                             <tr>
                         <td colspan="3">&nbsp;&nbsp;</td>
                     </tr> 
                          <tr>
                            <td colspan="3"><b>प्रति,</b></td>                        
                          </tr> 
                             <tr>
                                 <td colspan="3"><b>श्री/श्रीमती</b> <asp:Literal ID="Literal12" runat="server"></asp:Literal>
                             <b>मु.पो.</b> <asp:Literal ID="Literal13" runat="server"></asp:Literal><b> तालूका </b><asp:Literal ID="Literal14" runat="server"></asp:Literal>
                                     <b> जिल्हा </b><asp:Literal ID="Literal15" runat="server"></asp:Literal></td>
                             </tr>       
                              <tr>
                                  <td>&nbsp;&nbsp;</td>
                                  <td>&nbsp;</td>
                                  <td>&nbsp;</td>
                              </tr>
                             <tr>
                                 <td colspan="3" style="text-align:justify;">आपल्या दिनांक <asp:Literal ID="Literal19" runat="server"></asp:Literal>रोजीचा अर्ज ग्राम कृषि संजीवनी समितीने (VCRMC) दि <asp:Literal ID="Literal20" runat="server"></asp:Literal>रोजीच्या <asp:Literal ID="Literal1" runat="server"></asp:Literal>  क्रमांकाच्या ठरावाने मंजूर केला आहे. आपल्या अर्जानुसार, आपले  नावावर  8-अ प्रमाणे
                                     <asp:Literal ID="Literal4" runat="server"></asp:Literal> हे. क्षेत्र असून यापैकी सर्व्हे नं./गट नं. <asp:Literal ID="Literal5" runat="server"></asp:Literal> मधील <asp:Literal ID="Literal9" runat="server"></asp:Literal> हे. क्षेत्रावर <asp:Literal ID="Literal11" runat="server"></asp:Literal> या पिकाच्या 
                                      <asp:Literal ID="Literal16" runat="server"></asp:Literal> या वाणाच्या प्रमाणित/पायाभूत बियाण्याच्या उत्पादनासाठी २०१८-१९ मध्ये बीज प्रमाणीकरण यंत्रणेकडे केलेल्या नोंदणीच्या अधिन राहून पूर्वसंमती देण्यात येत आहे.
                                 </td>
                             </tr>
                             <tr>
                         <td colspan="3">&nbsp;&nbsp;</td>
                     </tr> 
                             <tr>
                                 <td colspan="3" style="text-align:justify;">हवामान अनुकुल वाणाचे पायाभूत किंवा प्रमाणित बियाण्यांचे उत्पादन घेण्यासाठी बिजोत्पादक संस्था व बीज प्रमाणीकरण यंत्रणा यांचे मार्गदर्शनानुसार  कारवाही करण्यात यावी. बिजोत्पादनासाठी खरेदी केलेल्या पैदासकार/पायाभूत बियाणेकरिता अदा केलेली किंमत( महाबीजचे दरानुसार) आणि प्रमाणीकरण यंत्रणेचे शुल्क या दोन बाबींवर प्रत्यक्ष झालेल्या खर्चाच्या अधिन राहून आपला प्रस्ताव अनुदानासाठी पात्र राहिल याची नोंद घ्यावी. आपला प्रस्ताव मंजूर झाल्यास आपणास शासन निर्णयानुसार (मार्गदर्शक सुचना व मंजूर मापदंड इत्यादी) अनुज्ञेय अनुदान निधी उपलब्धतेनूसार मंजूर करण्यात येईल. तसेच सदरचे अनुदान आपल्या बँक खात्यावर DBT द्वारे जमा करण्यात येईल. आपल्या प्रकल्प प्रस्तावास मंजूरी देणे, देय अनुदानाची रक्कम/देय कालावधी इत्यादी बाबी ठरविण्याचा अंतिम अधिकार नानाजी देशमुख कृषि संजीवनी प्रकल्प/शासनाचा राहिल.</td>
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
                                    <td><b>उपविभाग</b> <asp:Literal ID="Literal2" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td><b>जिल्हा:</b> <asp:Literal ID="Literal3" runat="server"></asp:Literal> </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                             <tr>                                 
                                 <td colspan="3"><b>प्रत,</b></td>
                             </tr>
                             <tr>
                                 <td colspan="3">माहिती व योग्य त्या कार्यवाहीसाठी अग्रेषित, </td>
                             </tr>
                             <tr>
                                 <td colspan="3">1)	जिल्हा अधिक्षक कृषी अधिकारी <asp:Literal ID="Literal17" runat="server"></asp:Literal></td>
                             </tr>
                             <tr>
                                 <td colspan="3">2)	अध्यक्ष, ग्राम कृषी संजीवनी समिती <asp:Literal ID="Literal18" runat="server"></asp:Literal></td>
                             </tr>
                             </table><br />
                         </td> </tr>
               </table>
            </div>
        </center>
    </form>
</body>
</html>
