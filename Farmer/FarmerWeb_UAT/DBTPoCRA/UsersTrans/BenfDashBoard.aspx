<%@ Page Title="" Language="C#" MasterPageFile="~/UsersTrans/UserMaster.Master" AutoEventWireup="true" CodeBehind="BenfDashBoard.aspx.cs" Inherits="DBTPoCRA.UsersTrans.BenfDashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="card mb-3">
                <div class="card-header  btn  badge-primary"> REGISTRATION  STATUS  </div>
                <div class="slimScrollDiv">
                    <div class="card-body slimScroll">
                        <div class="table-responsive">
                            <table class="table table-hover">
                                <tbody>
                                    <tr class="no-b">

                                        <td>
                                            <h6>NAME </h6>
                                            <small class="text-muted">Mobile Phones</small>
                                        </td>
                                        <td>
                                            <h6>MOBILE NO. </h6>
                                            <small class="text-muted">Mobile Phones</small>
                                        </td>
                                        <td>
                                            <h6>LAST LOGIN  </h6>
                                            <small class="text-muted">Mobile Phones</small>

                                        </td>
                                        <td>
                                            <h6>REGISTRATION STATUS  </h6>
                                            <span class="badge badge-success">Delivered</span>
                                        </td>
                                        <td>
                                            <span class="badge badge-primary">Update Profile</span>
                                        </td>
                                    </tr>

                                </tbody>
                            </table>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card mb-3">
                <div class="card-header  btn  badge-success">APPLICATION STATUS </div>
                <div class="slimScrollDiv">
                    <div class="card-body">
                        <div class="table-responsive">
                            <table class="table table-hover">
                                <tbody>
                                    <tr class="no-b">
                                      
                                        <td>
                                            <h6>Apple Product</h6>
                                            <small class="text-muted">Mobile Phones</small>
                                        </td>
                                        <td>$250</td>
                                        <td>
                                            <span class="badge badge-success">Delivered</span>
                                        </td>
                                        <td>
                                            <span>
                                                <i class="icon icon-data_usage"></i>5 days ago</span>
                                            <br>
                                            <span>
                                                <i class="icon icon-timer"></i>5 September, 2017</span>
                                        </td>
                                        <td>
                                            <a class="btn-fab btn-fab-sm btn-primary text-white">
                                                <i class="icon-eye"></i>
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                       
                                        <td>
                                            <h6>Apple Product</h6>
                                            <small class="text-muted">Mobile Phones</small>
                                        </td>
                                        <td>$250</td>
                                        <td>
                                            <span class="badge badge-success">Delivered</span>
                                        </td>
                                        <td>
                                            <span>
                                                <i class="icon icon-data_usage"></i>5 days ago</span>
                                            <br>
                                            <span>
                                                <i class="icon icon-timer"></i>5 September, 2017</span>
                                        </td>
                                        <td>
                                            <a class="btn-fab btn-fab-sm btn-primary text-white">
                                                <i class="icon-eye"></i>
                                            </a>
                                        </td>
                                    </tr>
                                   
                                </tbody>
                            </table>
                        </div>
                    </div>
                   
                </div>
            </div>
        </div>
    </div>
</asp:Content>
