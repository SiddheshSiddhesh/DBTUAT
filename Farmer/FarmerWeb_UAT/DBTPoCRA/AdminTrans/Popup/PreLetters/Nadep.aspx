<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Nadep.aspx.cs" Inherits="DBTPoCRA.AdminTrans.Popup.PreLetters.Nadep" %>

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
                           <%-- <h3 style="text-align: center">प्रपत्र -३</h3>--%>
                        </td>
                    </tr>                   
                    <tr>
                        <td></td>
                        <td >
                            <h3 style="text-align: center">पूर्वसंमती पत्र </h3>
                        </td>
                        <td></td>
                    </tr>                   
                    <tr>
                        <td></td>
                        <td style="font-weight: 700">
                            (अ २ (६) (i)) व (अ २ (६) (ii))
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
                        <td colspan="3">श्री/श्रीमती&nbsp;&nbsp;<asp:Literal ID="Literal3" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td colspan="3" class="auto-style2">मु.पो.&nbsp;&nbsp;<asp:Literal ID="Literal11" runat="server"></asp:Literal>
                        तालुका&nbsp;&nbsp;<asp:Literal ID="Literal12" runat="server"></asp:Literal> जिल्हा&nbsp;&nbsp;<asp:Literal ID="Literal9" runat="server"></asp:Literal></td>
                    </tr>
                    <%--<tr>
                        <td colspan="2" style="font-weight: 700">जिल्हा&nbsp;&nbsp;<asp:Literal ID="Literal4" runat="server"></asp:Literal></td>
                        <td></td>
                    </tr>--%>
                    <tr>
                        <td>&nbsp;&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>                    
                    <tr>
                        <td colspan="3" style="text-align: justify">आपल्या दिनांक  <asp:Literal ID="Literal13" runat="server"></asp:Literal> रोजीच्या अर्जाप्रमाणे, गांडुळ खत उत्पादन युनिट / नॅडेप खत उत्पादन युनिट/सेंद्रिय निविष्ठा उत्पादन युनिट उपघटकासाठी आपली लाभार्थी म्हणून निवड करण्यात आली आहे. अटी व शर्तीची पूर्तता करण्याच्या अधिन राहून आपणांस (गांडुळ खत उत्पादन युनिट/नॅडेप खत उत्पादन युनिट/सेंद्रिय निविष्ठा उत्पादन युनिट) या बाबींच्या कामास प्रकल्पांतर्गत मार्गदर्शक सूचनेनुसार अनुज्ञेय रकमेसाठी पुढील अटी व शर्तींच्या अधिन राहुन पूर्वसंमती देण्यात येत आहे. </td>
                    </tr>
                    <tr>
                        <td colspan="3">१.	आपल्या पसंतीच्या बांधकाम व्यवसायिक/स्वत: /गवंडी कडुन गांडुळ खत उत्पादन युनिट/नॅडेप खत उत्पादन युनिट/सेंद्रिय निविष्ठा उत्पादन युनिट उभारणी करण्यास आपणास मुभा आहे. </td>
                    </tr>                   
                     <tr>
                        <td colspan="3">२.	सदरचे पूर्वसंमती पत्र मिळाल्यापासून ३० दिवसांच्या आत आपण गांडुळ खत उत्पादन युनिट अथवा नॅडेप खत उत्पादन युनिट/सेंद्रिय निविष्ठा उत्पादन युनिटची उभारणी पूर्ण करणे बंधनकारक राहील अन्यथा सदर पूर्व संमती रद्द होईल याची नोंद घ्यावी. </td>
                    </tr>
                    <tr><td colspan="3">३.
                      घटकाची अंमलबजावणी पूर्ण झाल्यानंतर अनुदान  मागणीचा परिपूर्ण प्रस्ताव https://dbt.mahapocra.gov.in या संकेत स्थळावर किंवा DBT App वर अपलोड करावा.
                   </td>
                        </tr>
                         <tr>
                        <td colspan="3">४.	सदर घटकाची अंमलबजावणी मार्गदर्शक सूचनेनुसार पूर्ण झाल्याची खात्री झाल्यानंतर आपणास शासन निर्णयानुसार (मार्गदर्शक सुचना व मंजूर मापदंड इत्यादी) अनुज्ञेय अनुदान निधी उपलब्धते नूसार मंजूर करण्यात येईल. </td>
                    </tr>
                    <tr>
                        <td colspan="3">५.	सदरचे अनुदान आपल्या आधार संलग्न बँक खात्यावर जमा करण्यात येईल. </td>
                    </tr>
                    <tr>
                        <td colspan="3">६.	आपल्या प्रकल्प प्रस्तावास मंजूरी देणे, देय अनुदानाची रक्कम/देय कालावधी इत्यादी बाबी ठरविण्याचा अंतिम अधिकार नानाजी देशमुख कृषि संजीवनी प्रकल्प/शासनाचा राहील.</td>
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
                        <td><b> उपविभागीय कृषी अधिकारी, </b></td>
                    </tr>
                    <tr>
                        <td>&nbsp;&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                         <td></td>
                        <td></td>
                        <td colspan="3"><b> उपविभाग: </b>&nbsp;&nbsp;<asp:Literal ID="Literal5" runat="server"></asp:Literal>  </td>
                        
                    </tr>
                    <tr>
                         <td></td>
                        <td></td>
                        <td colspan="3"><b> जिल्हा: </b>&nbsp;&nbsp;<asp:Literal ID="Literal6" runat="server"></asp:Literal></td>
                       
                    </tr>
             
                    <%--<tr>
                        <td colspan="3"><b> प्रत, </b></td>
                        
                    </tr>
                    <tr>
                        <td colspan="3">माहिती व योग्य त्या कार्यवाहीसाठी अग्रेषित, </td>
                       
                    </tr>
                    <tr>
                        <td colspan="3">1)	जिल्हा अधिक्षक कृषी अधिकारी <asp:Literal ID="Literal4" runat="server"></asp:Literal></td>
                        
                    </tr>
                    <tr>
                        <td colspan="3">2)	अध्यक्ष, ग्राम कृषी संजीवनी समिती <asp:Literal ID="Literal14" runat="server"></asp:Literal></td>
                       
                    </tr>--%>
                </table>
            </div>
        </center>
    </form>
</body>
</html>
