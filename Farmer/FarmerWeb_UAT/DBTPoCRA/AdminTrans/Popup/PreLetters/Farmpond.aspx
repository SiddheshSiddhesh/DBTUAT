<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Farmpond.aspx.cs" Inherits="DBTPoCRA.AdminTrans.Popup.PreLetters.Farmpond" %>

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
                            <h4 style="text-align: center">(पुर्वसंमती पत्राचा नमूना) </h4>        
                            
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
                            <td colspan="3">प्रति,</td>                        
                          </tr> 
                             <tr>
                                 <td colspan="3">श्री/श्रीमती <asp:Literal ID="Literal12" runat="server"></asp:Literal></td>
                             </tr> 
                             <tr>
                                 <td colspan="3">मु.पो. <asp:Literal ID="Literal13" runat="server"></asp:Literal> तालूका <asp:Literal ID="Literal14" runat="server"></asp:Literal> जिल्हा <asp:Literal ID="Literal15" runat="server"></asp:Literal></td>
                             </tr>       
                              <tr>
                                  <td>&nbsp;&nbsp;</td>
                                  <td>&nbsp;</td>
                                  <td>&nbsp;</td>
                              </tr>
                             <tr>
                                 <td colspan="3"><b>विषय:-</b> शेततळे पूर्वसंमती /कार्यारंभ आदेशाबाबत <asp:Literal ID="Literal16" runat="server"></asp:Literal></td>
                             </tr>       
                             <tr>
                                 <td colspan="3" style="text-align: justify">उपरोक्त विषयावरील आपल्या दिनांक <asp:Literal ID="Literal17" runat="server"></asp:Literal> च्या मागणीपत्रास अनुसरून आपणास कळविण्यात येते की, दिनांक <asp:Literal ID="Literal18" runat="server"></asp:Literal> रोजी झालेल्या ग्राम कृषी संजीवनी समितीच्या बैठकीत नानाजी देशमुख कृषी संजीवनी प्रकल्पांतर्गत सामुदायिक शेततळे व अस्तरीकरण घटकासाठी आपली लाभार्थी म्हणून निवड करण्यात आली आहे. पुढील अटी व शर्तीची पूर्तता करण्याच्या अधीन राहून आपणास
                                    <%-- <asp:Literal ID="Literal19" runat="server"></asp:Literal> X <asp:Literal ID="Literal20" runat="server"></asp:Literal> X <asp:Literal ID="Literal21" runat="server"></asp:Literal> इतक्या --%>मागणी केलेल्या आकारमानाचे शेततळे व <%-- <asp:Literal ID="Literal22" runat="server"></asp:Literal> चौ.मी.--%>
                                     अस्तरीकरणास (इनलेट आउटलेट विरहित) कार्यारंभ करण्यास पूर्वसंमती देण्यात येत आहे. सदरचे शेततळे श्री. <asp:Literal ID="Literal23" runat="server"></asp:Literal> यांचे मालकीचे <asp:Literal ID="Literal24" runat="server"></asp:Literal> सर्व्हे नं. मध्ये घेणेत यावे.
                                 </td>
                             </tr>
                             <tr>
                                 <td colspan="3" style="text-align: justify">पूर्वसंमती पत्र मिळाल्यापासून ६० दिवसांच्या आत आपण सामुदायिक शेततळ्याचे व अस्तरीकरणाचे काम पूर्ण करावे.</td>
                             </tr>
                         </table>&nbsp;<br />
                         <table class="auto-style1" border="1">
                     
                       <tr>
                           <td style="width:20px;">
                               १.
                           </td>
                           <td colspan="2">निवडलेला व मंजूर शेततळ्याचा प्रकार- इनलेट आउटलेट सह / इनलेट आउटलेट विरहीत </td>                           
                       </tr>
                       <tr>
                           <td style="width:20px;">
                               २.
                           </td>
                           <td colspan="2">निश्चित केलेल्या ठिकाणी शेततळे घेणे बंधनकारक राहील. </td>                           
                       </tr>
                       <tr>
                           <td style="width:20px;">
                               ३.
                           </td>
                           <td colspan="2">निश्चित केलेल्या आकारमानाचेच शेततळे घेणे बंधनकारक राहील. </td>                           
                       </tr>
                       <tr>
                           <td style="width:20px;">
                               ४.
                           </td>
                           <td colspan="2">काम ६० दिवसात पूर्ण करणे बंधनकारक राहील. </td>                           
                       </tr>
                       <tr>
                           <td style="width:20px;">
                               ५.
                           </td>
                           <td colspan="2">काम पूर्ण झाल्यावर अनुदानाची मागणी https://dbt.mahapocra.gov.in या संकेतस्थळावर किंवा DBT app वर करता येईल.</td>                           
                       </tr>
                       <tr>
                           <td style="width:20px;">
                               ६.
                           </td>
                           <td colspan="2">कामासाठी कोणतीही आगाऊ रक्कम मिळणार नाही. </td>                           
                       </tr>
                       <tr>
                           <td style="width:20px;">
                               ७.
                           </td>
                           <td colspan="2">मजूर/मशिनच्या मालकास शासनाकडून परस्पर पेमेंट केले जाणार नाही. </td>                           
                       </tr>
                       <tr>
                           <td style="width:20px;">
                               ८.
                           </td>
                           <td colspan="2">इनलेट आउटलेट सह असलेल्या शेततळ्याच्या बांधावर व पाण्याच्या प्रवाहाच्या भागामध्ये स्थानिक प्रजातीच्या वनस्पतींची लागवड करणे बंधनकारक राहील.</td>                           
                       </tr>
                       <tr>
                           <td style="width:20px;">
                               ९.
                           </td>
                           <td colspan="2">शेततळ्याची निगा/दुरुस्तीची जबाबदारी शेतकऱ्यावर राहील. </td>                           
                       </tr>
                       <tr>
                           <td style="width:20px;">
                               १०.
                           </td>
                           <td colspan="2">पावसाळ्यामध्ये शेततळ्यात गाळ वाहून येणार नाही अथवा साठणार नाही याची व्यवस्था करावी लागेल. </td>                           
                       </tr>
                       <tr>
                           <td style="width:20px;">
                               ११.
                           </td>
                           <td colspan="2">इनलेट आउटलेट विरहित सामुदायिक शेततळे अस्तरीकरणासाठी BIS Standard मधील ५०० मायक्रॉन रीइनफोर्सड एचडीपीई जिओ मेंबरेन फिल्म IS 15351:2015 Type II या दर्जाची प्लास्टिक फिल्म वापरणे बंधनकारक आहे. </td>                           
                       </tr>
                       <tr>
                           <td style="width:20px;">
                               १२.
                           </td>
                           <td colspan="2">अस्तरीकरणासाठी उपलब्ध प्लास्टिक फिल्म IS 15351:2015 Type II उत्पादकाची निवड करण्याची आपणास मुभा आहे. अनुदान पात्रतेसाठी वरील दर्जा असलेले प्लास्टिक फिल्म च्या खरेदीचे बिल व अस्तरीकरण बसविण्याचे बिल जीएसटीसह सादर करणे आवश्यक आहे. </td>                           
                       </tr>
                       <tr>
                           <td style="width:20px;">
                               १३.
                           </td>
                           <td colspan="2">शेततळ्यातून पिकांना पाणी देण्याची व्यवस्था शेतकऱ्यास स्वखर्चाने करावी लागेल. </td>                           
                       </tr>
                       <tr>
                           <td style="width:20px;">
                               १४.
                           </td>
                           <td colspan="2">कृषी पर्यवेक्षक यांनी प्रस्तावित जागेवर आखणी करून दिल्यावर व मंजूर अंदाजपत्रकानुसार काम करावे.  </td>                           
                       </tr>
                       <tr>
                           <td style="width:20px;">
                               १५.
                           </td>
                           <td colspan="2">पूर्वसंमतीच्या तारखेपासून ३० दिवसात काम सुरु न झाल्यास हे आदेश आपोआप रद्द ठरतील. </td>                           
                       </tr>
                       <tr>
                           <td style="width:20px;">
                               १६.
                           </td>
                           <td colspan="2">मंजूर अंदाजपत्रकानुसार व तांत्रिकदृष्ट्या शेततळ्याचे योग्य काम झालेनंतर रक्कम रु. <asp:Literal ID="Literal1" runat="server"></asp:Literal> अनुदान देय आहे. </td>                           
                       </tr>
                     <tr>
                        <td>&nbsp;</td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: justify">घटक अंमलबजावणीबाबत सविस्तर मार्गदर्शक सूचना www.mahapocra .gov.in या संकेतस्थळावर उपलब्ध आहेत. अधिक माहितीसाठी संबधित कृषि सहाय्यक यांचेकडे संपर्क साधावा.</td>
                        
                    </tr>   
                                           
                       </table>
                 &nbsp;<br />            
            
                         <table class="auto-style1" style="table-layout: fixed; text-align: left; font-family: Calibri; font-size: 15px;">
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
                                    <td>&nbsp;&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
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
                         </td> </tr>
               </table>
            </div>
        </center>
    </form>
</body>
</html>
