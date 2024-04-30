<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreSanctionletter.aspx.cs" Inherits="DBTPoCRA.AdminTrans.Popup.PreSanctionletter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
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
            <div style="border: medium double #CCCCCC; width: 8in; height:11in;padding-right: 10px; padding-left: 10px;">
                
                <table class="auto-style1" style="width: 7in; table-layout: fixed; text-align: left; font-family: Calibri; font-size: 15px;">
                   <tr>
                        <td></td>
                        <td></td>
                        <td style="text-align: right;">  <a onclick="window.print();" href="#">Print</a></td>
                    </tr>
                     <tr>
                        <td colspan="3">
                         <asp:Image ID="Image1" Width="100%" ImageUrl="~/assets/img/basic/HeaderImage2.jpg" runat="server"></asp:Image>
                        </td>
                    </tr>
                   
                   
                    <tr>
                        <td></td>
                        <td style="font-weight: 700">
                            <h3 style="text-align: center">पूर्वसंमती पत्र</h3>
                        </td>
                        <td></td>
                    </tr>
                     <tr>
                        <td colspan="3" style="text-align: center"> <span style="font-weight: 700"> (प्रकल्प उपक्रम सांकेतांक : 
                            <asp:Literal ID="Literal22" runat="server"></asp:Literal> )</span> 
                        </td>
                    </tr>
                     <tr>
                        <td colspan="3" style="text-align: right"> 
                            <table class="auto-style1" style="width: 100%">
                                <tr>
                                    <td style="width:70%">जा.क्र.- </td>
                                    <td style="text-align:left; width: 10px;"> </td>
                                    <td style="text-align:left;"> <span > <asp:Literal ID="Literal6" runat="server"></asp:Literal> </span>    </td>
                                </tr>
                                <tr>
                                    <td>कार्यालय-</td>
                                    <td style="text-align:left;"></td>
                                    <td style="text-align:left;">तालुका कृषि अधिकारी </td>
                                </tr>
                                <tr>
                                    <td>उपविभाग-</td>
                                    <td style="text-align:left;"> </td>
                                    <td style="text-align:left;"> <span > <asp:Literal ID="Literal7" runat="server"></asp:Literal> </span> </td>
                                </tr>
                                <tr>
                                    <td>जिल्हा-</td>
                                    <td style="text-align:left;"> </td>
                                    <td style="text-align:left;"> <span > <asp:Literal ID="Literal23" runat="server"></asp:Literal> </span> </td>
                                </tr>
                                <tr>
                                    <td>दिनांक-</td>
                                    <td style="text-align:left;">
                                        </td>
                                    <td style="text-align:left;">
                                        <span > <asp:Literal ID="Literal5" runat="server"></asp:Literal> </span> 
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    
                   
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>प्रति,</td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3" ><asp:Literal ID="Literal4" runat="server"></asp:Literal></td>
                    </tr>
                    
                    <tr>
                        <td colspan="2" ><asp:Literal ID="Literal3" runat="server"></asp:Literal></td>
                        <td></td>
                    </tr>
                  
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                   
                  
                    <tr>
                        <td colspan="3" style="text-align: justify">

                            <p> 
                            आपला दि. <asp:Literal ID="Literal10" runat="server"></asp:Literal>  रोजीचा अर्ज ग्राम कृषि संजीवनी समितीने दि. <asp:Literal ID="Literal11" runat="server"></asp:Literal> रोजीच्या ठरावामध्ये मंजूर केलेला आहे. आपल्या अर्जानुसार, आपले नावावर ८-अ प्रमाणे . <asp:Literal ID="Literal13" runat="server"></asp:Literal>  हे क्षेत्र आहे. <asp:Literal ID="Literal12" runat="server"></asp:Literal> या बाबीसाठी घटकाच्या मार्गदर्शक सूचनेच्या आधीन राहून अनुज्ञेय रक्कमेसाठी पूर्वसंमती देण्यात येत आहे. <br /> 
                            
                            </p>
                              <p> 
                            पूर्वसंमती पत्र दिल्याच्या तारखेपासून <asp:Literal ID="Literal14" runat="server"></asp:Literal> दिवसांच्या मुदतीत सदर बाबीची अंमलबजावणी करणे आवश्यक आहे. सदर प्रकल्प उभारणीसाठी लाभार्थ्याने सर्व खर्च अगोदर स्वत: करावयाचा आहे.
                                  लाभार्थी शेतकऱ्यांनी निविष्ठांची खरेदी अधिकृत विक्रेत्याकडून कॅशलेस पध्दतीने किंवा रोखीने करावी. सदर खरेदी रोखीने केल्यास GST क्रमांक असलेली पावती सादर करावी. (शा.नि.क्र. संकीर्ण -1617/प्र.क्र. 31(भाग-1)/18/17-अ, दि. 04 जानेवारी 2019). 
                                   घटकाची अंमलबजावणी पूर्ण करून आवश्यक कागदपत्र https://dbt.mahapocra.gov.in या संकेतस्थळावर किंवा DBT APP वर अपलोड करून अनुदान मागणी करणे आवश्यक आहे. काम पूर्ण न करता फक्त कामाची देयके अपलोड केल्यास निधी अदायगी मध्ये विलंब होण्याची शक्यता नाकारता येत नाही. तसेच विहित मुदतीत घटकाची अंमलबजावणी पूर्ण न झाल्यास आपणास दिलेले पूर्वसंमती पत्र आपोआप रद्द समजण्यात यावे.
                           
                                   </p>
                             <p> 

सदर घटकाची अंमलबजावणी मार्गदर्शक सूचनेनुसार पूर्ण झाल्याची खात्री झाल्यानंतर आपला प्रस्ताव प्रत्यक्ष मोका तपासणीनुसार देय अनुदानासाठी पात्र राहिल. आपला प्रस्ताव मंजूर झाल्यानंतर मार्गदर्शक सुचना व मंजूर मापदंड/प्रत्यक्ष खर्चानुसार अनुज्ञेय अनुदान निधी उपलब्धतेनूसार मंजूर करण्यात येईल. सदरचे अनुदान आपल्या आधार संलग्न बँक खात्यावर DBT द्वारे जमा करण्यात येईल. आपल्या प्रस्तावास मंजूरी देणे, देय अनुदानाची रक्कम/देय कालावधी इत्यादी बाबी ठरविण्याचा अंतिम अधिकार नानाजी देशमुख कृषि संजीवनी प्रकल्प/शासनाचा राहील.<br />
</p>
                             <p> 
घटक अंमलबजावणीबाबत सविस्तर मार्गदर्शक सूचना www.mahapocra.gov.in या संकेतस्थळावर उपलब्ध आहेत. अधिक माहितीसाठी संबधित कृषि सहाय्यक/ समूह सहाय्यक यांचेकडे संपर्क साधावा.
                                 </p>


                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    
                   
                    <tr>
                        <td></td>
                        <td></td>
                        <td style="height: 40px"></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="2" style="text-align: right; font-weight: bold;">तालुका कृषि अधिकारी,<br />
उपविभाग: <asp:Literal ID="Literal1" runat="server"></asp:Literal>
           जिल्हा: <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                        </td>
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
