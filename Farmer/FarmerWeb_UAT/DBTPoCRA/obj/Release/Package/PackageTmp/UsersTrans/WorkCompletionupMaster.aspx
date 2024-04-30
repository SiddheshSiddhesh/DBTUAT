<%@ Page Title="Work Completion Master" Language="C#" MasterPageFile="~/UsersTrans/UserMaster.Master" AutoEventWireup="true" CodeBehind="WorkCompletionupMaster.aspx.cs" Inherits="DBTPoCRA.UsersTrans.WorkCompletionupMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">

        <div class="col-md-4">
            <label for="mobile" class="col-form-label s-12"></label>
            <br />

         </div>
        <div class="col-md-4">
            <label for="mobile" class="col-form-label s-12"></label>
            <br />

         </div>
        <div class="col-md-4" style="text-align: right">
            <label for="mobile" class="col-form-label s-12"></label>
            <br />
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
     </div>
    <hr />

    <div class="row">

        <div class="col-md-12">
            <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" Width="100%"
                CssClass="table table-bordered table-hover data-tables"
                SelectedRowStyle-BackColor="#F3F3F3">
                <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                <Columns>

                    <asp:TemplateField HeaderText="Sr. No">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <%-- <asp:BoundField HeaderText="Application Code" DataField="ApplicationCode">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>--%>
                    <asp:BoundField DataField="ApplicationDate" HeaderText="Application Date">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>

                    <asp:BoundField HeaderText="Activity" DataField="ActivityName">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Activity Code" DataField="ActivityCode">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>

                    <asp:BoundField HeaderText="Request No." DataField="RequestNo">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="Request Amount">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("TotalAmtByBen") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Approved Amount">
                        <ItemTemplate>
                            <asp:Label ID="Label15" Text='<%# Bind("FinalAmtApproved") %>' runat="server" ></asp:Label>
                            <br />
                            <asp:Label ID="Label2" Text='<%# Bind("ApplicationStatus") %>' runat="server" ></asp:Label>
                            
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text='<%# Bind("btnFarmer") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>
                <PagerStyle Font-Bold="True" />

                <SelectedRowStyle BackColor="#F3F3F3"></SelectedRowStyle>
            </asp:GridView>
        </div>
      </div>
    <hr />
</asp:Content>
