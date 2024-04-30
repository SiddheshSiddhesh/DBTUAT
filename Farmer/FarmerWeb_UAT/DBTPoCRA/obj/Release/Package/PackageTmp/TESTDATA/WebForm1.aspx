<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="DBTPoCRA.TESTDATA.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../assets/js/jquery.min.js"></script>

    <%--<script>
        var s = "";
        function Update() {
            $("#<%=GridView1.ClientID %> tr").each(function () {
                var id = $(this).find("td:eq(0)").text();
                var val = $(this).find("td:eq(1)").text();
                //etc...
                //var a = "  ";
                RefreshAll(id, val, $(this).find("td:eq(1)"));
                //$(this).find("td:eq(1)").text(" Done - "+val)
            });
            alert("Updated Sucessfully");
        }

        function RefreshAll(id,val,obj) {
            //debugger;
            // s = s + str;
            $.ajax({
                url: '<%=ResolveUrl("~/APPData/CommanAPI.asmx/checkUserNameAvail") %>',
                data: "{ 'id': '" + id + "','val': '" + val + "'}",
               dataType: "json",
               type: "POST",
               contentType: "application/json; charset=utf-8",
               success: function (data) {
                   obj.text('Done');
               },
               error: function (response) {
                   debugger;
                   alert("error- "+response);
               },
               failure: function (response) {
                   alert(response.responseText);
               }
           });

       }
    </script>--%>

    <script src="//translate.google.com/translate_a/element.js?cb=googleTranslateElementInit"></script>
    <script>
        function googleTranslateElementInit() {
            new google.translate.TranslateElement({
                pageLanguage: 'en',
                includedLanguages: 'mr',
                autoDisplay: false
            }, 'google_translate_element');
            var a = document.querySelector("#google_translate_element select");
            a.selectedIndex = 1;
            a.dispatchEvent(new Event('change'));
        }
    </script>

    <script type="text/javascript">

        // Load the Google Transliterate API
        google.load("elements", "1", {
            packages: "transliteration"
        });

        function onLoad() {
            var options = {
                sourceLanguage:
                google.elements.transliteration.LanguageCode.ENGLISH,
                destinationLanguage:
                [google.elements.transliteration.LanguageCode.KANNADA],
                transliterationEnabled: true
            };

            // Create an instance on TransliterationControl with the required
            // options.
            var control =
            new google.elements.transliteration.TransliterationControl(options);

            // Enable transliteration in the textbox with id
            // 'transliterateTextarea'.
            control.makeTransliteratable(['TextBox1']);
        }
        google.setOnLoadCallback(onLoad);
    </script>
</head>
<body>
    <div>
        
        
        <%-- <script type="text/javascript">
            function googleTranslateElementInit() {
                new google.translate.TranslateElement
                ({
                    pageLanguage: 'en',
                    includedLanguages: 'mr',
                    layout: google.translate.TranslateElement.InlineLayout.SIMPLE
                },
                'google_translate_element');
            }
        </script>
        <script type="text/javascript"
            src="//translate.google.com/translate_a/element.js?cb=googleTranslateElementInit">  
        </script>--%>
    </div>

    <form id="form1" runat="server">
        <asp:TextBox ID="TextBox1" Text="Ram Ram" runat="server"></asp:TextBox>
        <div id="google_translate_element">

            <asp:Literal ID="Literal1" runat="server"></asp:Literal>

            <%-- <asp:Button ID="btnfocus" runat="server" OnClick="btnfocus_Click" Text="Add Focus" />
            &nbsp;&nbsp;
       
            <a href="#" onclick="Update();">UPDATE </a>
            <br />
            <br />--%>
            <%--<asp:GridView ID="GridView1" runat="server" Font-Size="7px" DataKeyNames="RegistrationID" AutoGenerateColumns="False" Width="100%">
                <Columns>


                    <asp:BoundField HeaderText="ID" DataField="RegistrationID"></asp:BoundField>
                    <asp:BoundField HeaderText="NAME" DataField="RegisterName"></asp:BoundField>

                   
             
                    <asp:TemplateField HeaderText="A">

                        <ItemTemplate>
                            <asp:TextBox ID="TextBox1" CssClass="txtclass" Height="10px" runat="server" Text='<%# Bind("RegisterNameMr") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>--%>
            <br />
            <br />
            <br />
             <asp:FileUpload ID="fu" CssClass="form-control" runat="server" />
            &nbsp;
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
            <br />
        </div>
    </form>
</body>
</html>
