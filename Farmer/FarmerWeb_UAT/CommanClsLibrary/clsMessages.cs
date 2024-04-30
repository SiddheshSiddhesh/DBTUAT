using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace CommanClsLibrary
{
    public static class clsMessages
    { 
        static public void Sucessmsg(Literal lbl, String str)
        {
            lbl.Text = "";
            if (str.Trim() == "S")
            {
                lbl.Text="<div class='isasuccess1'>Record Saved Successully.</div> ";
            }
            if (str.Trim() == "A")
            {
                lbl.Text = "<div class='isasuccess1'>Record added Successfully.</div> ";
            }
            if (str.Trim() == "CAN")
            {
                lbl.Text = "<div class='isasuccess1'>Unit Cancelled Successfully.</div> ";
            }
            else if (str.Trim() == "U")
            {
                lbl.Text = "<div class='isasuccess1'>Record Updated Successfully. </div> ";
            }
            else if (str.Trim() == "D")
            {
                lbl.Text = "<div class='isasuccess1'>Record Deleted Successfully.</div> ";
            }
            else if (str.Trim() == "C")
            {
                lbl.Text = "<div class='isasuccess1'>Password Changed Successfully.</div> ";
            }
            else if (str.Trim() == "ES")
            {
                lbl.Text = "<div class='isasuccess1'>Your Login Details send to your email id.</div> ";
            }
            else if (str.Trim() == "VS")
            {
                lbl.Text = "<div class='isasuccess1'>Your verification code send to your email id.</div> ";
            }
            else if (str.Trim() == "EMS")
            {
                lbl.Text = "<div class='isasuccess1'>Email Send Successfully.</div> ";
            }

            String s = " <div id='MsgDiv' class='alert alert-success'> <button class='close' type='button' onclick='ResetMsg(MsgDiv);' data-dismiss='alert'><span aria-hidden='true'>×</span></button>" +
                               " <p class='text-small'>" + lbl.Text.Trim()+ "</p>" +
                           " </div>";

            lbl.Text = s;
        }
        static public void Sucessmsg2(Literal lbl, String str)
        {            
            lbl.Text = "<div class='isasuccess1'>" + str + "</div> ";

            String s = " <div id='MsgDiv' class='alert alert-success'> <button class='close' type='button' onclick='ResetMsg(MsgDiv);' data-dismiss='alert'><span aria-hidden='true'>×</span></button>" +
                              " <p class='text-small'>" + str.Trim() + "</p>" +
                          " </div>";

            lbl.Text = s;
            
        }
        static public void Errormsg(Literal lbl, String str)
        {            
            lbl.Text = "<div class='isaerror'>" + str + "</div> ";
            String s = " <div id='MsgDiv' class='alert alert-danger'> <button class='close' type='button' onclick='ResetMsg(MsgDiv);' data-dismiss='alert'><span aria-hidden='true'>×</span></button>" +
                              " <p class='text-small'>" + str.Trim() + "</p>" +
                          " </div>";

            lbl.Text = s;
        }
        static public void Warningmsg(Literal lbl, String str)
        {            
            lbl.Text = "<div class='isawarning'>" + str + "</div> ";
            String s = " <div id='MsgDiv' class='alert alert-warning'> <button class='close' type='button' onclick='ResetMsg(MsgDiv);' data-dismiss='alert'><span  aria-hidden='true'>×</span></button>" +
                             " <p class='text-small'>" + str.Trim() + "</p>" +
                         " </div>";

            lbl.Text = s;
        }
    }
}
