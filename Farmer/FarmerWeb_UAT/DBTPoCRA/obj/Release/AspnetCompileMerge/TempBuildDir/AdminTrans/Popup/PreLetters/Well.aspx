<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Well.aspx.cs" Inherits="DBTPoCRA.AdminTrans.Popup.PreLetters.Well" %>

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
                        <td colspan="3" style="font-weight: 700"><%--<h4 style="text-align: center">प्रपत्र -३</h4>--%><br />
                            <h4 style="text-align: center"> पूर्वसंमती पत्र</h4><br />
                            <h4 style="text-align: center">(प्रकल्प उपक्रम संकेतांक  A 3.3.4 ) </h4>        
                            
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
                            <td>उपविभाग <asp:Literal ID="Literal9" runat="server"></asp:Literal> , जिल्हा <asp:Literal ID="Literal10" runat="server"></asp:Literal></td>
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
                            <td colspan="3">प्रति,</td>                        
                          </tr> 
                             <tr>
                                 <td colspan="3">श्री/श्रीमती <asp:Literal ID="Literal12" runat="server"></asp:Literal>
                             मु.पो. <asp:Literal ID="Literal13" runat="server"></asp:Literal> तालूका <asp:Literal ID="Literal14" runat="server"></asp:Literal> जिल्हा <asp:Literal ID="Literal15" runat="server"></asp:Literal></td>
                             </tr>       
                              <tr>
                                  <td>&nbsp;</td>
                                  <td>&nbsp;</td>
                                  <td>&nbsp;</td>
                              </tr>
                             <tr>
                                 <td colspan="3" style="text-align:justify;">आपल्या दिनांक <asp:Literal ID="Literal11" runat="server"></asp:Literal> रोजीच्या अर्जाप्रमाणे पाणी साठवण संरचनाची निर्मिती या घटकांतर्गत विहीर उपघटकासाठी आपली लाभार्थी म्हणून निवड करण्यात आली आहे. संदर्भ क्र.
                                     <asp:Literal ID="Literal16" runat="server"></asp:Literal> अन्वये भूजल सर्वेक्षण व विकास यंत्रणा  यांचेकडून प्राप्त प्रमाणकांच्या आधारे प्रस्तावित स्थळ निश्चिती योग्य असलेबाबत प्रमाणित करण्यात आलेले आहे.अटी व शर्तीची पूर्तता करण्याच्या अधिन राहून आपणांस  विहिरीच्या कामास प्रकल्पांतर्गत मार्गदर्शक सूचनेनुसार अनुज्ञेय रकमेसाठी पूर्वसंमती देण्यात येत आहे. सदरचे पूर्वसंमती पत्र मिळाल्यापासून ३० दिवसांच्या आत काम सुरु करणे बंधनकारक राहील अन्यथा सदर पूर्व संमती रद्द होईल याची नोंद घ्यावी. नविन विहीर खोदणे,बांधकाम व इतर तत्सम बाबींची पूर्तता करण्यासाठी कमाल १ वर्षाचा कालावधी देण्यात येत आहे.
                                 </td>
                             </tr>
                             <tr>
                         <td colspan="3">&nbsp;&nbsp;</td>
                     </tr> 
                             <tr>
                                 <td colspan="3" style="text-align:justify;">
                                     सदर घटकाची अंमलबजावणी मार्गदर्शक सूचनेनुसार पूर्ण झाल्याची खात्री झाल्यानंतर आपणास शासन निर्णयानुसार (मार्गदर्शक सुचना व मंजूर मापदंड इत्यादी) अनुज्ञेय अनुदान निधी उपलब्धते नूसार मंजूर करण्यात येईल. तसेच सदरचे अनुदान आपल्या राष्ट्रीयकृत बँकखात्यावर २ टप्प्यात <b>(पहिला टप्पा- विहिरीचे खोदकाम पूर्ण झाल्यावर व दुसरा टप्पा- खोदकाम व बांधकाम पूर्ण करून विहिरीची ७/१२ वर नोंद झाल्यावर )</b> जमा करण्यात येईल. आपल्या प्रकल्प प्रस्तावास मंजूरी देणे, देय अनुदानाची रक्कम/देय कालावधी इत्यादी बाबी ठरविण्याचा अंतिम अधिकार नानाजी देशमुख कृषि संजीवनी प्रकल्प/शासनाचा राहील.
                                 </td>
                             </tr>
                             <tr>
                                 <td>&nbsp;&nbsp;</td>
                                 <td>&nbsp;</td>
                                 <td>&nbsp;</td>
                             </tr>
                                     <tr>
                                      <td></td>                        
                                      <td></td>
                                      <td><b>उपविभागीय कृषि अधिकारी,</b></td>
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
                                    <td colspan="3">1)	जिल्हा अधिक्षक कृषी अधिकारी <asp:Literal ID="Literal4" runat="server"></asp:Literal></td>                                
                                </tr>
                                <tr>
                                    <td colspan="3">2)	अध्यक्ष, ग्राम कृषी संजीवनी समिती <asp:Literal ID="Literal5" runat="server"></asp:Literal></td>
                                </tr>
                        
                         </table><br />
                         </td> </tr>
               </table>
            </div>
        </center>
    </form>
</body>
</html>