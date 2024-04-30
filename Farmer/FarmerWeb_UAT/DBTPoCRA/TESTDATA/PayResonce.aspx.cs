using CommanClsLibrary;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBTPoCRA.TESTDATA
{
    public partial class PayResonce : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ReadReport();
        }

        public void ReadReport()
        {
            MyClass cla = new MyClass();
            Dictionary<String, List<Dictionary<String, String>>> AllFilesText = GetAllFileText();


            foreach (KeyValuePair<String, List<Dictionary<String, String>>> ls in AllFilesText)
            {
                int x = 0;
                String filename = ls.Key;
               // UpdateLogRead(" Read :: " + filename);
                List<Dictionary<String, String>> a = ls.Value;

                foreach (Dictionary<String, String> l in ls.Value)
                {
                    #region ""
                    using (SqlConnection connection = new SqlConnection(clsSettings.strCoonectionString))
                    {
                        connection.Open();

                        SqlCommand command = connection.CreateCommand();
                        SqlTransaction transaction;
                        transaction = connection.BeginTransaction("CTransaction");
                        command.Connection = connection;
                        command.Transaction = transaction;

                        try
                        {  
                            String DisbursementID = "";
                            String AaDharNumber = "";
                            String NameOnAudhar = "";
                            String DisbursedAmt = "";
                            String DisbursedDate = "";
                            String DisbursementChildID = "";
                            String Status = "";
                            String BankTransactionID = "";
                            String Remarks = "";
                            String IINNumber = "";
                            String AccountNo = "";
                             
                            foreach (KeyValuePair<String, String> entry in l)
                            {
                                #region
                                String k = entry.Key;
                                String v = entry.Value;

                                if (k.Trim() == "DisbursementID")
                                {
                                    DisbursementID = v.Trim();
                                } 
                                 
                                if (k.Trim() == "UserCreditReference")
                                {
                                    DisbursementChildID = v.Trim();
                                }
                                 
                                if (k.Trim() == "BeneficiaryAadhaarNumber")
                                {
                                    AaDharNumber = v.Trim();
                                }                                 
                                if (k.Trim() == "BeneficiaryName")
                                {
                                    NameOnAudhar = v.Trim();
                                }
                                
                                if (k.Trim() == "Amount")
                                {
                                    DisbursedAmt = (Convert.ToDouble(v.Trim()) / 100).ToString("0.00");
                                }
                                DisbursedDate = cla.mdy(cla.SvrDate());

                                //DisbursementChildID = str[6].ToString().Trim();
                                //Status = str[8].ToString().Trim();//SuccessFlag
                                if (k.Trim() == "SuccessFlag")
                                {
                                    Status = v.Trim();
                                }
                                //BankTransactionID = str[9].ToString().Trim();//ItemSequenceNumber
                                if (k.Trim() == "ItemSequenceNumber")
                                {
                                    BankTransactionID = v.Trim();
                                }

                                //Remarks = str[10].ToString().Trim();
                                if (k.Trim() == "Narration")
                                {
                                    Remarks = v.Trim();
                                }

                                if (Status.Trim().ToUpper() != "Paid".ToUpper())
                                {
                                    if (k.Trim() == "ReasonCode")
                                    {
                                        Remarks = Remarks + "  " + v.Trim();
                                    }
                                }


                                // IINNumber = "";
                                if (k.Trim() == "DestinationBankIIN")
                                {
                                    IINNumber = v.Trim();
                                }
                                //AccountNo = "";
                                if (k.Trim() == "DestinationBank")
                                {
                                    AccountNo = v.Trim();
                                }

                                // ls.Add("filename", filename);
                                // ls.Add("DisbursementID", DisbursementID);


                                #endregion
                            }

                            x++;

                            AaDharNumber = AaDharNumber.Remove(0, 3);
                            String RegistrationID = cla.GetExecuteScalar("SELECT RegistrationID from Tbl_T_Disbursement_Details_Child  where AadharNumber='" + AaDharNumber + "'", command);

                            String strQuery = " INSERT INTO Tbl_T_Disbursement_Log (DisbursementID ,RegistrationID ,DisbursementChildID ,AaDharNumber ,NameOnAudhar ,DisbursedDate,DisbursedStatus ,BankTransactionID ,DisbursedAmt,Remarks)";
                            strQuery += "VALUES(" + DisbursementID + "," + RegistrationID + "," + DisbursementChildID + ",'" + AaDharNumber + "','" + NameOnAudhar + "','" + (DisbursedDate) + "','" + Status + "','" + BankTransactionID.Trim() + "'," + DisbursedAmt + ",'" + Remarks.Trim() + "')";
                            cla.ExecuteCommand(strQuery, command);

                            //UpdateLog(strQuery);

                            DataTable dt = cla.GetDataTable("SELECT top 1 ApplicationID,WorkReportID FROM Tbl_T_Disbursement_Details_Child where DisbursementChildID=" + DisbursementChildID.Trim() + "", command);
                            if (Status.Trim().ToUpper() == "Failed".ToUpper())
                            {
                                // in case of failed ................

                                cla.ExecuteCommand("UPDATE Tbl_T_Disbursement_Details_Child SET IsDeleted='1' , DisbursedAmount=0 , DisbursedDate='" + (DisbursedDate) + "' , DisbursedStatus='" + Status + "' , DisTransactionID='" + BankTransactionID.Trim() + "' ,BankRemark='" + Remarks + "' , IINNumber='" + IINNumber.Trim() + "' ,AccountNo='" + AccountNo + "'   where DisbursementChildID=" + DisbursementChildID.Trim() + " and DisbursementID=" + DisbursementID.Trim() + "  ", command);
                                cla.ExecuteCommand("UPDATE Tbl_T_ApplicationPaymentVoucher SET DisbursementID =NULL , IsRepayment='1'  WHERE (ApplicationID=" + dt.Rows[0]["ApplicationID"].ToString().Trim() + " AND WorkReportID=" + dt.Rows[0]["WorkReportID"].ToString().Trim() + ")", command);
                                cla.ExecuteCommand("UPDATE Tbl_T_Application_WorkReport SET IsPaymentDone=NULL, ApplicationStatusID=1 where WorkReportID=" + dt.Rows[0]["WorkReportID"].ToString().Trim().Trim() + " ", command);
                            }
                            //if (Status.Trim().ToUpper() == "Open Entry".ToUpper())
                            //{

                            //    // in case of failed ................
                            //    // cla.ExecuteCommand("UPDATE Tbl_T_Disbursement_Details_Child SET IsDeleted='1' , DisbursedAmount=0 , DisbursedDate='" + (DisbursedDate) + "' , DisbursedStatus='" + Status + "' , DisTransactionID='" + BankTransactionID.Trim() + "' ,BankRemark='" + Remarks + "' , IINNumber='" + IINNumber.Trim() + "' ,AccountNo='" + AccountNo + "'   where DisbursementChildID=" + DisbursementChildID.Trim() + " and DisbursementID=" + DisbursementID.Trim() + "  ", command);
                            //    // cla.ExecuteCommand("UPDATE Tbl_T_ApplicationPaymentVoucher SET DisbursementID =NULL , IsRepayment='1'  WHERE (ApplicationID=" + dt.Rows[0]["ApplicationID"].ToString().Trim() + " AND WorkReportID=" + dt.Rows[0]["WorkReportID"].ToString().Trim() + ")", command);
                            //}
                            else
                            {

                                cla.ExecuteCommand("UPDATE Tbl_T_Disbursement_Details_Child SET DisbursedAmount=" + DisbursedAmt + " , DisbursedDate='" + (DisbursedDate) + "', DisbursedStatus='" + Status + "', DisTransactionID='" + BankTransactionID.Trim() + "' , BankRemark='" + Remarks + "' , IINNumber='" + IINNumber.Trim() + "' ,AccountNo='" + AccountNo + "'  where DisbursementChildID=" + DisbursementChildID.Trim() + " and DisbursementID=" + DisbursementID.Trim() + "  ", command);
                                cla.ExecuteCommand("UPDATE Tbl_T_Application_WorkReport SET IsPaymentDone='1' , ApplicationStatusID=15  where WorkReportID=" + dt.Rows[0]["WorkReportID"].ToString().Trim().Trim() + " ", command);
                            }


                            // UpdateLogRead(Status);


                            cla.ExecuteCommand("UPDATE  Tbl_T_Disbursement_Details SET ReportFileName='" + filename + "', DisbursedDate='" + cla.mdy(cla.SvrDate()) + "' where DisbursementID=" + DisbursementID + "", command);



                            transaction.Commit();




                        }
                        catch (Exception ex)
                        {

                            //UpdateLogRead("ReadReport :: " + ex.ToString() + "   :::   " + command.CommandText.Trim());
                            try
                            {
                                transaction.Rollback();

                            }
                            catch
                            {

                            }
                            // cla.SendMail(ex.ToString());
                        }
                        finally
                        {

                            connection.Close();
                            command.Dispose();
                        }

                    }

                    #endregion
                }
                if (x > 0)
                {
                    string sourceFile = @"" + filename;
                    if (File.Exists(sourceFile))
                    {
                        string targetPath = @"C:\KOTAK\LOG\" + filename;
                        System.IO.File.Copy(sourceFile, targetPath, true);
                        File.Delete(@"" + filename);
                    }
                }
                //--End of file 


            }




        }

        private Dictionary<String, List<Dictionary<String, String>>> GetAllFileText()
        {
            //UpdateLog(" File Checking");
            //Dictionary<String, List<String>> AllFilesText = new Dictionary<string, List<String>>();
            string path = @"D:\KOTAK";
            DirectoryInfo di = new DirectoryInfo(path);

            //List<Dictionary<String, List<Dictionary<String, String>>>> lstResponce = new List<Dictionary<string, List<Dictionary<string, string>>>>();

            Dictionary<String, List<Dictionary<String, String>>> lp = new Dictionary<string, List<Dictionary<string, string>>>();
            Parallel.ForEach(di.GetFiles("*.txt", System.IO.SearchOption.TopDirectoryOnly), file =>
            {

                String filename = file.FullName;
                //UpdateLogRead(filename);


                if (new MyClass().GetExecuteScalar("select DisbursementID from Tbl_T_Disbursement_Details where ReportFileName='" + filename + "'").Trim().Length == 0)
                {
                    //ls.Add("filename", filename);
                    using (TextFieldParser tfp = new TextFieldParser(filename))
                    {
                        List<Dictionary<String, String>> ll = new List<Dictionary<string, string>>();
                        tfp.TextFieldType = FieldType.FixedWidth;
                        String DisbursementID = "";
                        int x = 0;
                        while (!tfp.EndOfData)
                        {

                            Dictionary<String, String> ls = new Dictionary<string, string>();
                            #region
                            if (x == 0)
                            {
                                tfp.SetFieldWidths(new int[] { 2, 7, 40, 14, 9, 9, 15, 3, 13, 9, 13, 8, 10, 10, 15 });
                                string[] fields = tfp.ReadFields();
                                //3 User Credit Reference 
                                for (int m = 0; m != fields.Length; m++)
                                {
                                    if (m == 3)
                                    {
                                        DisbursementID = fields[m].Trim();

                                    }
                                }
                            }
                            else
                            {
                                tfp.SetFieldWidths(new int[] { 2, 9, 2, 3, 15, 40, 9, 7, 20, 13, 13, 10, 10, 1, 1, 2, 20 });
                                string[] fields = tfp.ReadFields();
                                for (int y = 0; y != fields.Length; y++)
                                {
                                    #region " add"
                                    if (y == 0)
                                    {
                                        //APBS Transaction Code
                                        ls.Add("APBSTransactionCode", fields[y].ToString());
                                        ls.Add("DisbursementID", DisbursementID);
                                    }
                                    if (y == 1)
                                    {
                                        //Destination Bank IIN
                                        ls.Add("DestinationBankIIN", fields[y].ToString());
                                    }
                                    if (y == 2)
                                    {
                                        //Destination Account Type
                                        ls.Add("DestinationAccountType", fields[y].ToString());
                                    }
                                    if (y == 3)
                                    {
                                        //Ledger Folio Number
                                        ls.Add("LedgerFolioNumber", fields[y].ToString());
                                    }
                                    if (y == 4)
                                    {
                                        //Beneficiary Aadhaar Number
                                        ls.Add("BeneficiaryAadhaarNumber", fields[y].ToString());
                                    }
                                    if (y == 5)
                                    {
                                        //Beneficiary account holder's name
                                        ls.Add("BeneficiaryName", fields[y].ToString());
                                    }
                                    if (y == 6)
                                    {
                                        //Sponsor Bank IIN
                                        ls.Add("SponsorBankIIN", fields[y].ToString());
                                    }
                                    if (y == 7)
                                    {
                                        //User number
                                        ls.Add("Usernumber", fields[y].ToString());
                                    }
                                    if (y == 8)
                                    {
                                        //User Name /Narration
                                        ls.Add("Narration", fields[y].ToString());
                                    }
                                    if (y == 9)
                                    {
                                        //User Credit reference
                                        ls.Add("UserCreditReference", fields[y].ToString());
                                    }
                                    if (y == 10)
                                    {
                                        //Amount
                                        ls.Add("Amount", fields[y].ToString());
                                    }
                                    if (y == 11)
                                    {
                                        //Item sequence Number
                                        ls.Add("ItemSequenceNumber", fields[y].ToString());
                                    }
                                    if (y == 12)
                                    {
                                        //Checksum
                                        ls.Add("Checksum", fields[y].ToString());
                                    }
                                    if (y == 13)
                                    {
                                        //Success Flag
                                        if (fields[y].ToString().Trim() == "1")
                                        {
                                            ls.Add("SuccessFlag", "Paid");
                                        }
                                        //else if (fields[y].ToString().Trim() == "3")
                                        //{
                                        //    ls.Add("SuccessFlag", "Open Entry");
                                        //}
                                        else
                                        {
                                            ls.Add("SuccessFlag", "Failed");
                                        }
                                    }
                                    if (y == 14)
                                    {
                                        //Filler
                                        ls.Add("Filler", fields[y].ToString());
                                    }
                                    if (y == 15)
                                    {
                                        //Reason Code
                                        String Reason = "";
                                        if (fields[y].ToString().Trim() == "1")
                                        {
                                            Reason = "Account closed";
                                        }
                                        else if (fields[y].ToString().Trim() == "51")
                                        {
                                            Reason = "KYC Documents Pending";
                                        }
                                        else if (fields[y].ToString().Trim() == "52")
                                        {
                                            Reason = "Documents Pending for Account Holder turning Major";
                                        }
                                        else if (fields[y].ToString().Trim() == "53")
                                        {
                                            Reason = "Account inoperative";
                                        }
                                        else if (fields[y].ToString().Trim() == "54")
                                        {
                                            Reason = "Dormant account";
                                        }
                                        else if (fields[y].ToString().Trim() == "57")
                                        {
                                            Reason = "Amount Exceeds limit set on Account by Bank for Credit per Transaction";
                                        }
                                        else if (fields[y].ToString().Trim() == "58")
                                        {
                                            Reason = "Account reached maximum Credit limit set on account by Bank";
                                        }
                                        else if (fields[y].ToString().Trim() == "60")
                                        {
                                            Reason = "Account Holder Expired";
                                        }
                                        else if (fields[y].ToString().Trim() == "62")
                                        {
                                            Reason = "Account Under Litigation";
                                        }
                                        else if (fields[y].ToString().Trim() == "64")
                                        {
                                            Reason = "Aadhaar Number not Mapped to Account Number";
                                        }
                                        else if (fields[y].ToString().Trim() == "68")
                                        {
                                            Reason = "A/c Blocked or Frozen";
                                        }
                                        else if (fields[y].ToString().Trim() == "71")
                                        {
                                            Reason = "Invalid account Type (NRE/PPF/CC/Loan/FD)";
                                        }
                                        else if (fields[y].ToString().Trim() == "69")
                                        {
                                            Reason = "Customer Insolvent / Insane";
                                        }
                                        else if (fields[y].ToString().Trim() == "10")
                                        {
                                            Reason = "Unclaimed/DEAF accounts";
                                        }
                                        else if (fields[y].ToString().Trim() == "96")
                                        {
                                            Reason = "Aadhaar number not mapped to IIN (Not Linked)";
                                        }
                                        else
                                        {
                                            Reason = "NA";
                                        }

                                        if (fields[y].ToString().Trim() != "00")
                                            ls.Add("ReasonCode", fields[y].ToString() + " : " + Reason);
                                        else ls.Add("ReasonCode", "");
                                    }
                                    if (y == 16)
                                    {
                                        //Destination Bank account number
                                        ls.Add("DestinationBank", fields[y].ToString());
                                    }

                                    #endregion
                                }

                            }
                            x++;
                            #endregion
                            if (x > 1)
                                ll.Add(ls);
                        }

                        lp.Add(filename, ll);
                    }
                }
                else
                {
                    //UpdateLogRead("Exist :: " + filename);
                    try
                    {
                        string sourceFile = @"" + filename;
                        if (File.Exists(sourceFile))
                        {
                            //F:\agent2-2.1.6-x64\FLAMe-2.1.6\Output\APB-CR-KKBK-KKBKH2HUSER1-23072020-000055-RES.txt
                            //string aa = sourceFile.Replace(@"F:\agent2-2.1.6-x64\FLAMe-2.1.6\Output\", "");
                            //string targetPath = @"F:\agent2-2.1.6-x64\FLAMe-2.1.6\Output\Readed\" + aa;// @"C:\KOTAK\LOG\" + filename.Replace(path, "");
                            //System.IO.File.Copy(sourceFile, targetPath, true);
                            //File.Delete(@"" + filename);
                        }
                    }
                    catch (Exception ex) {

                        //UpdateLogRead("Exist Delete Error  :: " + ex.ToString());
                    }
                }
            });
            return lp;
        }
    }
}