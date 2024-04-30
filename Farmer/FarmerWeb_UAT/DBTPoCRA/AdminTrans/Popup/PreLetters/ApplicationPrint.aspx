<%@ Page Language="C#" AutoEventWireup="true"  EnableEventValidation="false" CodeBehind="ApplicationPrint.aspx.cs" Inherits="DBTPoCRA.AdminTrans.Popup.PreLetters.ApplicationPrint" %>

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

        h5, h2, h4 {
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
            <div runat="server" id="divExport" style="border: medium double #CCCCCC; width: 8in; padding-right: 15px; padding-left: 15px;">
                <table class="auto-style1">
                    <tr>
                        <td style="background-position: center top; font-weight: 700; height: 160px; text-align:right; background-image: url('../../../assets/img/basic/HeaderImage2.jpg'); background-repeat: no-repeat; vertical-align: top;">
                            <div id="DivPrint" runat="server" > 
                            
                             <%--<input id="Button1" class="noprint" onclick="window.print();" type="button" value="Print" />--%>
                            <a onclick="window.print();" href="#">Print</a>
                             
                            <%--<asp:HyperLink ID="HyperLink1" runat="server"  OnDataBinding="HyperLink1_DataBinding">PDF</asp:HyperLink>--%>
                            <asp:LinkButton ID="LinkButton1" CssClass="noprint" CausesValidation="false" runat="server" OnClick="LinkButton1_Click"  Height="22px" Width="50px">PDF</asp:LinkButton>
                                </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: 700"><h5 style="text-align: center">प्रकल्पांतर्गत लाभासाठी निवड होणेकरिता अर्ज</h5><br />
                            <h3 style="text-align: center">जागतिक बँक अर्थसहाय्यित</h3><br />
                            <h2 style="text-align: center">नानाजी देशमुख कृषी संजीवनी प्रकल्प</h2>                           
                        
                            <h4 style="text-align: center">(प्रत्येक घटकासाठी स्वतंत्र अर्ज करावा) </h4>
                        </td>
                    </tr> 
                     <tr>
                         <td colspan="3">&nbsp;</td>
                     </tr>                   
                    <tr>
                        <td style="text-align: justify">प्रति,</td>                        
                    </tr>
                  
                    <tr>
                        <td style="text-align: justify">
                            <table class="auto-style1">
                                <tr>
                                    <td style="width: 30%">अध्यक्ष, ग्राम कृषि संजीवनी समिती</td>
                                    <td style="border-style: none none dotted none; border-width: 1px; border-color: #333333">  <asp:Literal ID="Literal7" runat="server"></asp:Literal> </td>
                                    <td style="width: 30%">(अर्जदाराचे गाव)</td>
                                    <td style="border-style: none none dotted none; border-width: 1px; border-color: #333333"> <asp:Literal ID="Literal1" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td>गावसमुह क्रमांक</td>
                                    <td style="border-style: none none dotted none; border-width: 1px; border-color: #333333">  <asp:Literal ID="Literal9" runat="server"></asp:Literal>
                                    </td>
                                    <td>तालुका</td>
                                    <td style="border-style: none none dotted none; border-width: 1px; border-color: #333333">  <asp:Literal ID="Literal15" runat="server"></asp:Literal>                  
                   
                                    </td>
                                </tr>
                                <tr>
                                    <td>जिल्हा</td>
                                    <td style="border-style: none none dotted none; border-width: 1px; border-color: #333333">  <asp:Literal ID="Literal2" runat="server"></asp:Literal></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>                        
                    </tr>
                  
                    
                    <tr>
                        <td style="text-align: justify"></td>
                       
                    </tr>
                  
                    
                    <tr>
                        <td style="text-align: justify"><strong>महोदय/महोदया,</strong></td>
                       
                    </tr>
                  
                    
                    <tr>
                        <td style="text-align: justify">&nbsp;</td>
                       
                    </tr>
                    <tr>
                        <td style="text-align: justify">जागतिक बँक अर्थसहाय्यित नानाजी देशमुख कृषि संजीवनी प्रकल्पांतर्गत मी, माझ्या मालकीहक्काचे शेत जमिनीमध्ये हवामान बदलास पुरक पध्दतीने शेती करण्यासाठी गुंतवणूक करणेस तयार आहे, त्याकरिता  या प्रकल्पांतर्गत खालील घटकाचा लाभ मिळावा,ही विनंती.</td>
                       
                    </tr>
                    <tr> <td></td></tr>
                    <tr>
                        <td style="text-align: justify; font-weight: bold;">मागणी केलेल्या घटकाचे नाव- <asp:Literal ID="Literal3" runat="server"></asp:Literal></td>                        
                    </tr>
                    <tr>
                        <td style="text-align: justify; font-weight: bold;">घटकाचा तपशील .- <asp:Literal ID="Literal8" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td style="text-align: justify; font-weight: bold;">&nbsp;</td>
                    </tr>
                    </table>
                   <table class="auto-style1" style="width: 100%; text-align: left; font-family: Calibri; font-size: 15px;" border="1" >
                       <tr>
                       <th style="width:7%">अ.क्र.</th>
                       <th style="width:40%">बाब</th>
                       <th style="width:50%">तपशील</th>
                           </tr>
                       <tr>
                           <td>
                               1
                           </td>
                           <td>अर्जदाराचे संपूर्ण नाव 

                           </td>
                           <td>
                                <asp:Literal ID="Literal11" runat="server"></asp:Literal>
                           </td>
                       </tr>
                       <tr>
                           <td>2</td>
                           <td>पूर्ण पत्ता </td>
                           <td>घर क्र. <asp:Literal ID="Literal12" runat="server"></asp:Literal> गल्ली क्र. /भागाचे नाव <asp:Literal ID="Literal16" runat="server"></asp:Literal><br />
                               गाव <asp:Literal ID="Literal17" runat="server"></asp:Literal> पोस्ट <asp:Literal ID="Literal18" runat="server"></asp:Literal><br />
                               तालुका <asp:Literal ID="Literal19" runat="server"></asp:Literal> जिल्हा <asp:Literal ID="Literal20" runat="server"></asp:Literal><br />
                              पिन क्र.- <asp:Literal ID="Literal21" runat="server"></asp:Literal> 
                           </td>
                       </tr>
                       <tr>
                           <td>3</td>
                           <td>भ्रमणध्वनी क्रमांक-  <br />
                              
                           </td>
                           <td > <asp:Literal ID="Literal22" runat="server"></asp:Literal><br />
                               <asp:Literal ID="Literal23" Visible="false" runat="server"></asp:Literal>
                           </td>
                       </tr>
                       <tr>
                           <td>4</td>
                           <td>स्त्री/पुरुष(लागू असेल तेथे √  अशी खूण करावी)</td>
                           <td><%-- स्त्री     <asp:CheckBox ID="gender1" runat="server" />
                               पुरुष     <asp:CheckBox ID="gender2" runat="server" />--%>
                               <asp:Literal ID="Literal6" runat="server"></asp:Literal>
                              
                             </td>
                       </tr>
                       <tr>
                           <td>5</td>
                           <td>जात प्रवर्ग(लागू असेल तेथे √ अशी खूण करावी)</td>
                           <td>
                               <asp:Literal ID="Literal26" runat="server"></asp:Literal>
                              
                             </td>
                       </tr>
                       <tr>
                           <td>6</td>
                           <td>अर्जदार दिव्यांग आहे काय?( लागू तेथे √  खूण करावी)</td>
                           <td><%--<asp:CheckBoxList ID="physicallyhandi" runat="server" RepeatDirection="Horizontal">
                               <asp:ListItem>होय </asp:ListItem>
                               <asp:ListItem>नाही</asp:ListItem>
                               </asp:CheckBoxList>--%>
                                <asp:Literal ID="Literal13" runat="server"></asp:Literal>
                           </td>
                       </tr>
                       <tr>
                           <td>7</td>
                           <td>आधारकार्ड क्र. </td>
                           <td><asp:Literal ID="Literal24" runat="server"></asp:Literal></td>
                       </tr>
                     <%--  <tr>
                           <td>8</td>
                           <td>स्वत:च्या नावावरील एकूण क्षेत्र (8-अ प्रमाणे)</td>
                          
                       </tr>--%>
                       <tr>
                           <td>8</td>
                           <td>ज्या क्षेत्रावर घटक राबविणार आहे त्या क्षेत्राचा सर्व्हे क्रमांक-</td>
                            <td>खाते क्र <asp:Literal ID="Literal25" runat="server"></asp:Literal> <br />
                               <%--एकूण जमिनधारणा --%>        
                              <asp:Literal ID="Literal4" runat="server"></asp:Literal> (हेक्टर) 
                             <asp:Literal ID="Literal10" runat="server"></asp:Literal> (आर) 

                           </td>
                       </tr>
                       <tr>
                           <td>9</td>
                           <td>यापूर्वी याच घटकाचा लाभ आपण अथवा आपल्या कुटुंबातील  अन्य सदस्यांनी या किंवा इतर योजनेतून घेतला आहे काय? असल्यास लाभ घेतलेली योजना,अनुदान रक्कम व लाभ घेतलेचे वर्षे इ.तपशील .</td>
                           <td></td>
                       </tr>
                       <tr>
                           <td>10</td>
                           <td>अनुदान वजा जाता उवर्रित रक्कम कशा प्रकारे उभारणी करणार ? ( लागू असेल तेथे  √  अशी खूण करावी)</td>
                           <td><%--स्वतः<asp:CheckBox ID="self" runat="server" />
                               बँक कर्जाव्दारे<asp:CheckBox ID="banklone" runat="server"/>--%>
                               <asp:Literal ID="Literal14" runat="server"></asp:Literal>
                           </td>
                       </tr>
                   </table>    
                <table class="auto-style1">
                    <tr>
                        <td colspan="3" style="text-align: justify">&nbsp;&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: justify">सदर घटकासाठी माझी निवड झाल्यास आवश्यक कागदपत्रांची पूर्तता करुन विहीत मुदतीत प्रकल्पाच्या मार्गदर्शक सूचनेनुसार तसेच प्रकल्प आधिकाऱ्यांच्या तांत्रिक देखरेखीखाली काम पूर्ण करीन व त्यानंतरच मला अनुदान देय राहील. तसेच सदरचे काम मी विहित मुदतीत व मार्गदर्शक सुचनेप्रमाणे पूर्ण न केलेस अनुदान मिळणार नाही, याची मला जाणीव आहे.</td>
                    </tr>
                     <tr> <td colspan="3">&nbsp;&nbsp; </td></tr>
                    <tr>
                        <td colspan="3" style="text-align: justify">सदर बाबीचा लाभ घेत असताना वा त्यानंतर माझेकडुन गैरव्यवहार झाल्याचे आढळून आल्यास अथवा लाभांकित बाबीचा माझ्याकडुन गैरवापर झाल्याचे सिद्ध झाल्यास संबधित घटकासाठी मिळालेले अनुदान माझ्याकडून सव्याज वसुल केले जाईल याची मला स्पष्ट जाणीव आहे.</td>
                    </tr>
                     <tr> <td colspan="3">&nbsp;&nbsp; </td></tr>
                    <tr>
                        <td colspan="3" style="text-align: justify">प्रकल्पाचे अधिकारी आणि इतर प्राधिकृत व्यक्तींना  घटक तपासणीसाठी माझी संमती आहे तसेच, त्यांना या तपासणी कामासाठी सहकार्य करेन.</td>
                    </tr>
                     <tr> <td colspan="3">&nbsp;</td></tr>
                    <tr>
                        <td colspan="3" style="text-align: justify">वरील सर्व माहिती माझ्या समजुतीप्रमाणे बरोबर व खरी आहे. </td>
                    </tr>
                    <tr><td colspan="3" style="text-align: justify">स्थळ:- (गावाचे नाव) <asp:Literal ID="Literal5" runat="server"></asp:Literal></td></tr>

                    <tr><td>दिनांक -<asp:Literal ID="Literal27" runat="server"></asp:Literal></td></tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td style="font-weight: 700; text-align: center;">अर्जदाराची सही </td></tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td style="font-weight: 700; text-align: center;">&nbsp;</td></tr>
                    <tr>
                        <td colspan="3">1 . या मध्ये अनुदान मागणी केलेल्या घटकाचा तपशील उदा. शेततळ्याचा आकार, हरितगृहाचा/शेडनेटचा आकार, फळबाग लागवड असल्यास फळपिक नाव, क्षेत्र इ. तसेच ठिबक, तुषार असल्यास- पिकाचे नाव-क्षेत्र इ. तपशील नमूद करावा.
                            <br />
                            2.&nbsp; प्रकल्पाच्या विविध घटकातील माहितीची देवाण-घेवाणीसाठी भ्रमणध्वनी क्रमांकाचा प्रकल्पाच्या कामकाजासाठी वापर करण्यास माझी सहमती आहे.
                            <br />
                            3 . दारिद्रय रेषेखालील दाखला सेाबत जोडण्याची आवश्यता नाही. तथापि ग्राम कृषि संजीवनी समिती  लाभार्थी निवड करतेवेळी अर्जदार दारिद्रय रेषेखालील  असल्याबाबत खात्री करुन अर्जाबाबत  उचित निर्णय घेऊ शकेल .
                            <br />
                            4 . मी, वरिल मुद्दा क्रमांक 8 मध्ये नमूद केलेला आधार क्रमांक धारक, सदर प्रकल्पांतर्गत लाभार्थी निवडविषयक कामकाजाकरिता आधार कायद्यांतर्गत माझे घेतलेले बेाटांचे ठसे/ बुबुळांच्या प्रतिमा UIDAI पेार्टलवरील माहितीशी पडताळणी / प्रमाणीकरण करणेसाठी वापर करणेस याव्दारे सहमती देत आहे. सदर  प्रकल्पांतर्गत मला असेही अवगत करण्यात आलेले आहे की, सदरची माहिती (बायोमेट्रिक ) प्रकल्पांतर्गत केाठेही जतन अथवा समाईकीकरण (share) केली जाणार नसून ती फक्त सीआयडीआर (Central Identities Data Repository) कडे पडताळणी करिता सादर केली जाईल.
                            <br />
                            5.&nbsp; कुटुंब या व्याख्येत पती, पत्नी व १८ वर्षाखालील मुलांचा समावेश राहील.
</td>
                        </tr>
                </table>                   
               
            </div>
        </center>
    </form>
</body>
</html>
