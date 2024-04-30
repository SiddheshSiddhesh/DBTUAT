<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreSanctionletterMarathi.aspx.cs" Inherits="DBTPoCRA.AdminTrans.Popup.PreLetters.PreSanctionletterMarathi" %>

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
        
        h3, h2,h4,h5 {
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
                <table class="auto-style1" style="; table-layout: fixed; text-align: left; font-family: Calibri; font-size: 15px;">
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
                        <td colspan="3" ><h5 style="text-align: center">महाराष्ट्र शासन - कृषि विभाग </h5>
                            <h5 style="text-align: center">नानाजी देशमुख कृषि संजीवनी प्रकल्प</h5>
                            <h4 style="text-align: center">३० - अ,ब आर्केड, वर्ल्ड ट्रेड सेंटर, कफ परेड, मुंबई ४०० ००५ </h4>                     
                           
                        </td>
                    </tr> 
                     <tr>
                         <td colspan="3">&nbsp;&nbsp;</td>
                     </tr>  
                    <tr>
                        <td>दूरध्वनी क्र.०२२-२२१६३३५१/२</td>
                        <td></td>
                        <td>ई-मेल : mahapocra@gmail.com</td>
                    </tr>  
                    <hr />               
                    <tr>
                        <td >जा क्र. नादेकृसंप्र- <asp:Literal ID="Literal23" runat="server"></asp:Literal></td>                         
                        <td></td>
                        <td>दि.-<asp:Literal ID="Literal22" runat="server"></asp:Literal></td>    <%-- .0९.2018--%>
                    </tr>
                     <tr>
                         <td colspan="3">&nbsp;&nbsp;</td>
                     </tr>  
                  <tr>
                      <td colspan="3"><%--<h3 style="text-align: center">प्रपत्र-3</h3>--%>
                          <h2 style="text-align: center">पूर्वसंमती पत्र</h2>
                          <h4 style="text-align: center">(नानाजी देशमुख कृषि संजीवनी प्रकल्प अंतर्गत हरितगृह/शेडनेटगृह/ प्लॅस्टीक टनेल /लागवड साहित्य 
या घटकासाठी द्यावयाच्या पुर्वसंमती पत्राचा नमूना)  
</h4>
                          <h4 style="text-align: center">(A2.4.1 / A2.4.3 / A2.4.4 / A2.4.5 / A2.4.6)</h4>

                      </td>
                  </tr>
                     <tr>
                         <td colspan="3">&nbsp;&nbsp;</td>
                     </tr>  
                    <tr>
                       <td colspan="3" style="text-align: justify">कार्यालय -उपविभागीयकृषि अधिकारी,&nbsp;   <asp:Literal ID="Literal7" runat="server"></asp:Literal> </td>                                               
                       
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: justify">                  
                        जिल्हा &nbsp;&nbsp;<asp:Literal ID="Literal2" runat="server"></asp:Literal></td>
                    </tr>                   
                    <tr>
                        <td colspan="3" style="text-align: justify">दिनांक -  <asp:Literal ID="Literal1" runat="server"></asp:Literal></td>
                       
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: justify">प्रति,</td>
                       
                    </tr>                   
                    <tr>
                        <td colspan="3" style="text-align: justify">श्री/श्रीमती&nbsp;&nbsp;<asp:Literal ID="Literal3" runat="server"></asp:Literal></td>                        
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: justify">मु.पो.&nbsp;&nbsp;<asp:Literal ID="Literal8" runat="server"></asp:Literal>,
                            तालूका ,&nbsp;&nbsp;<asp:Literal ID="Literal9" runat="server"></asp:Literal>
                             जिल्हा &nbsp;&nbsp;<asp:Literal ID="Literal15" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr> <td colspan="3">&nbsp;</td></tr>
                    <tr>
                        <td colspan="3" style="text-align: justify">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; आपला दिनांक  <asp:Literal ID="Literal4" runat="server"></asp:Literal> रोजीचा अर्ज ग्राम कृषि संजीवनी समितीने (व्हीसीआरएमसी) दि <asp:Literal ID="Literal5" runat="server"></asp:Literal> रोजीच्या  <asp:Literal ID="Literal6" runat="server"></asp:Literal> क्रमांकाच्या ठरावाने मंजूर केला आहे. आपल्या अर्जानुसार, आपले  नावावर  8-अ प्रमाणे  <asp:Literal ID="Literal10" runat="server"></asp:Literal> हे. क्षेत्र असून यापैकी सर्व्हे नं./गट नं.  <asp:Literal ID="Literal11" runat="server"></asp:Literal>मधील  <asp:Literal ID="Literal12" runat="server"></asp:Literal> हे. क्षेत्रावर हरितगृह (उच्च तंत्रज्ञान हरितगृह/नैसर्गिक वायुवीजन हरितगृह/ प्लॅस्टीक टनेल/ लागवड साहित्य) Model <asp:Literal ID="Literal13" runat="server"></asp:Literal> /शेडनेटगृह Model <asp:Literal ID="Literal14" runat="server"></asp:Literal>उभारणीसाठी/लागवड साहित्यासाठी नानाजी देशमुख कृषि संजीवनी प्रकल्प अंतर्गत पूर्वसंमती देण्यात येत आहे. आपण सादर केलेल्या हमीपत्रात या पूर्वी  <asp:Literal ID="Literal16" runat="server"></asp:Literal> या घटकासाठी  <asp:Literal ID="Literal17" runat="server"></asp:Literal> चौ.मी. क्षेत्रासाठी लाभ घेतल्याचे नमूद केलेले आहे. मार्गदर्शक सूचनांनूसार व आपल्या मागणीनूसार आता  <asp:Literal ID="Literal18" runat="server"></asp:Literal> या घटकासाठी   <asp:Literal ID="Literal19" runat="server"></asp:Literal> चौ.मी. क्षेत्रासाठी पूर्वसंमती देण्यात येत आहे. 
सदरचे पूर्वसंमतीपत्र दिल्याच्या तारखेपासून पंधरा दिवसाच्या आत आपण प्रकल्पाचे काम सुरु करावे. यासाठी सुयोग्य सेवा पुरवठादार/ठेकेदार आपण निवडावा. पंधरा दिवसात प्रकल्पाचे काम सुरु न केल्यास आपणास दिलेली पूर्वसंमती आपोआप रद्द समजण्यात यावी. पायासाठी खड्डे खोदकाम झालेनंतर व उभारणी साहित्य प्रकल्पस्थळी पुरवठा झाल्यानंतर संबंधित कृषि सहाय्यक/समूह सहाय्यक यांना तपासणीसाठी कळवावे. दोन महिन्याच्या आत हरितगृह/शेडनेटगृह/ प्लॅस्टीक टनेल/ लागवड साहित्य यांचे काम पुर्ण करुन अनुदान मागणीचा परिपूर्ण प्रस्ताव उपविभागीय कृषि अधिकारी यांचे कार्यालयात त्वरित सादर करावा. 
</td>
                    </tr>
                     <tr> <td colspan="3">&nbsp;</td></tr>
                    <tr>
                        <td colspan="3" style="text-align: justify">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; सदर घटकाचा लाभसाठी आवश्यक असलेले प्रशिक्षण घेणे अनिवार्य आहे. आपण उभारणी करु इच्छित असलेल्या हरितगृह/शेडनेटगृहाच्या मॉडेलनुसार उभारणी करण्यासाठी आवश्यक असलेल्या साहित्याचे तांत्रिक निकष, अनिवार्य साहित्याची यादी व आराखडा सोबत जोडले आहे. उभारणीसाठी वापरावयाचे साहित्य सोबत जोडलेल्या तांत्रिक निकषाप्रमाणे, व BIS मानांकित असणे आवश्यक आहे. अनुज्ञेय अनुदान निधी उपलब्धतेनूसार मंजूर करण्यात येईल. तसेच सदरचे अनुदान आपल्या बँक खात्यावर DBT द्वारे जमा करण्यात येईल. आपल्या प्रकल्प प्रस्तावास मंजूरी देणे, देय अनुदानाची रक्कम/देय कालावधी इत्यादी बाबी ठरविण्याचा अंतिम अधिकार नानाजी देशमुख कृषि संजीवनी प्रकल्पाचा राहिल.</td>
                    </tr>
                    <tr><td colspan="3">&nbsp;</td></tr>
                    <tr>
                        <td colspan="3" style="text-align: justify">सोबत:
                            <br />
                         1.	आराखडा (Design)
                            <br />
                            2.	अनिवार्य साहित्याची यादी (सिंचनासाठी कंट्रोल हेडसह)
                            <br />
                            3.	General Specifications
                            <br />
                            4.	Specifications of GI Pipes
                            <br />
                            5.	Specifications of other Items
                            <br />
                            6.	Specifications of climate control equipments (CCPH मॉडेलसाठी फक्त)
                        </td>
                    </tr>
                    <tr><td colspan="3">&nbsp;</td></tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>उपविभागीय कृषि अधिकारी,
                            <br />
                            उपविभाग:&nbsp;&nbsp;  <asp:Literal ID="Literal20" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>जिल्हा:&nbsp;&nbsp;  <asp:Literal ID="Literal21" runat="server"></asp:Literal></td>
                        <td></td>
                        <td></td>
                    </tr>
                    </table>                             
               
            </div>
        </center>
    </form>
</body>
</html>