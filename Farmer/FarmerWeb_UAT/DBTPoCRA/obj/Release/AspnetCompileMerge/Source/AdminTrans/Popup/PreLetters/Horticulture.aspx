<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Horticulture.aspx.cs" Inherits="DBTPoCRA.AdminTrans.Popup.PreLetters.Horticulture" %>

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
                        <td >
                            <%--<h3 style="text-align: center">प्रपत्र - 3 </h3>--%>
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
                         <td colspan="3">&nbsp;</td>
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
                        <td colspan="3" style="font-weight: 700">श्री/श्रीमती&nbsp;&nbsp;<asp:Literal ID="Literal3" runat="server"></asp:Literal></td>
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
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>&nbsp;&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: justify">
                            
                            
                             
                            
                            उपरोक्त विषयान्वये आपल्या दिनांक <asp:Literal ID="Literal13" runat="server"></asp:Literal> च्या अर्जास अनुसरून आपणास कळविण्यात येते की, दिनांक <asp:Literal ID="Literal9" runat="server"></asp:Literal> रोजी झालेल्या ग्राम कृषी संजीवनी समितीच्या बैठकीत नानाजी देशमुख कृषी संजीवनी प्रकल्पांतर्गत फळबाग घटकासाठी आपली लाभार्थी म्हणून निवड करण्यात आली आहे. पुढील अटी व शर्तीची पूर्तता करण्याच्या अधीन राहून आपणास मागणी केलेल्या<asp:Literal ID="Literal15" runat="server"></asp:Literal>(हे. आर.) क्षेत्रावर<asp:Literal ID="Literal16" runat="server"></asp:Literal> ह्या फळपिकाची  लागवड करण्यास पूर्वसंमती देण्यात येत आहे.
	                        <br />
                            सदर पूर्वसंमतीपत्र मिळाल्यापासून ४५ दिवसांच्या आत आपण फळबाग लागवड पूर्ण करावी.

                            
                            <br />
                            १.	निश्चित केलेल्या ठिकाणी वर नमूद केलेल्या क्षेत्रापर्यंत लाभ अनुज्ञेय राहील.
                            <br />
                            २.	फळबाग लागवड झाल्यावरच https://dbt.mahapocra.gov.in या संकेतस्थळावर किंवा DBT app वर अनुदानाची मागणी करता येईल.
                            <br />
                            ३.	फळबागेसाठी कोणतीही आगाऊ रक्कम मिळणार नाही.
                            <br />
                            ४.	फळबागेसाठी पूर्वमशागत, खड्डे भरणी, आंतरमशागत व काटेरी झाडांचे कुंपण इ. बाबींची व्यवस्था शेतकऱ्यास स्वखर्चाने करावी लागेल.
                            <br />
                            ५.	खड्डे खोदणी, कलम लागवड, पिक संरक्षण, नांग्या भरणी व ठिबक सिंचन इ. बाबींना लाभ अनुज्ञेय राहील.
                            <br />
                            ६.	कृषी सहाय्यक यांनी प्रस्तावित जागेवर आखणी करून दिल्यावर व मंजूर फळबाग लागवड मार्गदर्शक सुचानेतील निकषाप्रमाणे करावी. 
                            <br />
                            ७.	 फळबाग लागवड झालेनंतर मार्गदर्शक सूचनेनुसार रक्कम रु. <asp:Literal ID="Literal14" runat="server"></asp:Literal> अनुदान देय आहे.

                            </td>
                    </tr>

                    <tr>
                        <td colspan="3">आपल्या प्रकल्प प्रस्तावास मंजूरी देणे, देय अनुदानाची रक्कम/देय कालावधी इत्यादी बाबी ठरविण्याचा अंतिम अधिकार नानाजी देशमुख कृषि संजीवनी प्रकल्प/शासनाचा राहील. </td>
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
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp; &nbsp;</td>
                        <td>&nbsp;</td>
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
