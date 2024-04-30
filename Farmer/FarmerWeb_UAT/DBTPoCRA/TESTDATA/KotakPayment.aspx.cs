using CommanClsLibrary;
using DBTPoCRA.AdminTrans.UserControls;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBTPoCRA.TESTDATA
{
    public partial class KotakPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                MyClass cla = new MyClass();
                DataTable dt = cla.GetDataTable("SELECT Top 1  DisFileName, FileSrNumber ,DisbursementID FROM  Tbl_T_Disbursement_Details WHERE  (IsFileCreated IS NULL) AND IsDeleted IS NULL ");
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                   // UpdateLog("TRY" + dt.Rows[x]["FileSrNumber"].ToString());
                    CreateExcel(dt.Rows[x]["DisbursementID"].ToString(), dt.Rows[x]["DisFileName"].ToString());

                }

            }
            catch (Exception ex)
            {
               // UpdateLog(ex.ToString());
            }
            //List<Dictionary<String, String>> lstResponce = new List<Dictionary<string, string>>();
            //int x = 0;
            //using (TextFieldParser tfp = new TextFieldParser(@"c:\DATA\B.txt"))
            //{
            //    tfp.TextFieldType = FieldType.FixedWidth;

            //    while (!tfp.EndOfData)
            //    {
            //        if (x == 0)
            //        {
            //            tfp.SetFieldWidths(new int[] { 2, 7, 40, 14, 9, 9, 15, 3, 13, 9, 13, 8, 10, 10, 15 });
            //            string[] fields = tfp.ReadFields();
            //        }
            //        else
            //        {
            //            tfp.SetFieldWidths(new int[] { 2, 9, 2, 3, 15, 40, 9, 7, 20, 13, 13, 10, 10, 1, 1, 2, 20 });
            //            string[] fields = tfp.ReadFields();
            //            Dictionary<String, String> ls = new Dictionary<string, string>();
            //            for (int y = 0; y != fields.Length; y++)
            //            {
            //                #region " add"
            //                if (y == 0)
            //                {
            //                    //APBS Transaction Code
            //                    ls.Add("APBSTransactionCode", fields[y].ToString());
            //                }
            //                if (y == 1)
            //                {
            //                    //Destination Bank IIN
            //                    ls.Add("DestinationBankIIN", fields[y].ToString());
            //                }
            //                if (y == 2)
            //                {
            //                    //Destination Account Type
            //                    ls.Add("DestinationAccountType", fields[y].ToString());
            //                }
            //                if (y == 3)
            //                {
            //                    //Ledger Folio Number
            //                    ls.Add("LedgerFolioNumber", fields[y].ToString());
            //                }
            //                if (y == 4)
            //                {
            //                    //Beneficiary Aadhaar Number
            //                    ls.Add("BeneficiaryAadhaarNumber", fields[y].ToString());
            //                }
            //                if (y == 5)
            //                {
            //                    //Beneficiary account holder's name
            //                    ls.Add("BeneficiaryName", fields[y].ToString());
            //                }
            //                if (y == 6)
            //                {
            //                    //Sponsor Bank IIN
            //                    ls.Add("SponsorBankIIN", fields[y].ToString());
            //                }
            //                if (y == 7)
            //                {
            //                    //User number
            //                    ls.Add("Usernumber", fields[y].ToString());
            //                }
            //                if (y == 8)
            //                {
            //                    //User Name /Narration
            //                    ls.Add("Narration", fields[y].ToString());
            //                }
            //                if (y == 9)
            //                {
            //                    //User Credit reference
            //                    ls.Add("UserCreditReference", fields[y].ToString());
            //                }
            //                if (y == 10)
            //                {
            //                    //Amount
            //                    ls.Add("Amount", fields[y].ToString());
            //                }
            //                if (y == 11)
            //                {
            //                    //Item sequence Number
            //                    ls.Add("ItemSequenceNumber", fields[y].ToString());
            //                }
            //                if (y == 12)
            //                {
            //                    //Checksum
            //                    ls.Add("Checksum", fields[y].ToString());
            //                }
            //                if (y == 13)
            //                {
            //                    //Success Flag
            //                    if (fields[y].ToString().Trim() == "1")
            //                    {
            //                        ls.Add("SuccessFlag", "Paid");
            //                    }
            //                    else if (fields[y].ToString().Trim() == "3")
            //                    {
            //                        ls.Add("SuccessFlag", "Open Entry");
            //                    }
            //                    else
            //                    {
            //                        ls.Add("SuccessFlag", "Failed");
            //                    }
            //                }
            //                if (y == 14)
            //                {
            //                    //Filler
            //                    ls.Add("Filler", fields[y].ToString());
            //                }
            //                if (y == 15)
            //                {
            //                    //Reason Code
            //                    String Reason = "";
            //                    if (fields[y].ToString().Trim() == "1")
            //                    {
            //                        Reason = "Account closed";
            //                    }
            //                    else if (fields[y].ToString().Trim() == "51")
            //                    {
            //                        Reason = "KYC Documents Pending";
            //                    }
            //                    else if (fields[y].ToString().Trim() == "52")
            //                    {
            //                        Reason = "Documents Pending for Account Holder turning Major";
            //                    }
            //                    else if (fields[y].ToString().Trim() == "53")
            //                    {
            //                        Reason = "Account inoperative";
            //                    }
            //                    else if (fields[y].ToString().Trim() == "54")
            //                    {
            //                        Reason = "Dormant account";
            //                    }
            //                    else if (fields[y].ToString().Trim() == "57")
            //                    {
            //                        Reason = "Amount Exceeds limit set on Account by Bank for Credit per Transaction";
            //                    }
            //                    else if (fields[y].ToString().Trim() == "58")
            //                    {
            //                        Reason = "Account reached maximum Credit limit set on account by Bank";
            //                    }
            //                    else if (fields[y].ToString().Trim() == "60")
            //                    {
            //                        Reason = "Account Holder Expired";
            //                    }
            //                    else if (fields[y].ToString().Trim() == "62")
            //                    {
            //                        Reason = "Account Under Litigation";
            //                    }
            //                    else if (fields[y].ToString().Trim() == "64")
            //                    {
            //                        Reason = "Aadhaar Number not Mapped to Account Number";
            //                    }
            //                    else if (fields[y].ToString().Trim() == "68")
            //                    {
            //                        Reason = "A/c Blocked or Frozen";
            //                    }
            //                    else if (fields[y].ToString().Trim() == "71")
            //                    {
            //                        Reason = "Invalid account Type (NRE/PPF/CC/Loan/FD)";
            //                    }
            //                    else if (fields[y].ToString().Trim() == "69")
            //                    {
            //                        Reason = "Customer Insolvent / Insane";
            //                    }
            //                    else if (fields[y].ToString().Trim() == "10")
            //                    {
            //                        Reason = "Unclaimed/DEAF accounts";
            //                    }
            //                    else
            //                    {
            //                        Reason = "NA";
            //                    }

            //                    if (fields[y].ToString().Trim() != "00")
            //                        ls.Add("ReasonCode", fields[y].ToString() + " : " + Reason);
            //                    else ls.Add("ReasonCode", "");
            //                }
            //                if (y == 16)
            //                {
            //                    //Destination Bank account number
            //                    ls.Add("DestinationBank", fields[y].ToString());
            //                }

            //                #endregion
            //            }
            //            lstResponce.Add(ls);
            //        }
            //        x++;

            //    }
            //}


            //String PathUp = "~/DocMasters/ErroLog";
            //String path = System.Web.HttpContext.Current.Server.MapPath(PathUp);
            //if (!Directory.Exists(path))
            //{
            //    // Try to create the directory.
            //    DirectoryInfo di = Directory.CreateDirectory(path);
            //}
            //path = path + "/" + DateTime.Now.ToString("MM/dd/yyyy").Replace("/", "") + ".txt";

            //String filepath = path;
            //DataTable dt = new DataTable();


            //int[] widths = new int[] { 2, 7, 40, 14, 9, 9, 15, 3, 13, 9, 13, 8, 10, 10, 3 };
            ////APBS transaction Code,UserNumber,Username,UserCreditReferenceNo,APBS tape input,Sponsor Bank IIN,User’s bank Account number,Ledger Folio number,User defined limit for individual credit,items,Total items,Total amount (Balancing Debit amount),Settlement date (DDMMYYYY),Item Sequence Number,Checksum,Filler
            //String UserCreditReferenceNo = "101";
            //String TotalItems = "1";
            //String TotalAmoint = "500";


            //TotalItems = TotalItems.PadRight(9);
            //TotalAmoint = TotalAmoint.PadRight(9);
            //string[] headers = new string[] { "33", "999ZN99", "POCRA", UserCreditReferenceNo.Trim(), "", "000607420", "7413126488", "", "", TotalItems.Trim(), TotalAmoint.Trim(), "06022020", "", "", "" };

            ////string[] headers = new string[] { "33", "999ZN99", "POCRA", "101", "", "000607420", "7413126488", "", "", "000000001", "0000000050000", "06022020", "", "", "" };
            //using (FixedWidthWriter writer = new FixedWidthWriter(widths, filepath))
            //{
            //    writer.WriteLine(headers);
            //}



            //int[] widths2 = new int[] { 2, 9, 2, 3, 15, 40, 9, 7, 20, 13, 13, 10, 10, 1, 2, 9 };
            //List<string[]> lst = FixedWidthWriter.ConvertTable(dt);
            //using (FixedWidthWriter writer2 = new FixedWidthWriter(widths2, filepath, true))
            //{
            //    foreach (string[] a in lst)
            //    {
            //        writer2.WriteLine(a);
            //    }
            //}


            ////string[] headers2 = new string[] { "77", "", "", "", "000785215852125", "", "000607420", "999ZN99", "Test Payment from Pocra", "1", "0000000050000", "", "", "", "","" };

            ////string[] headers3 = new string[] { "77", "", "", "", "000785215852126", "", "000607420", "999ZN99", "Test Payment from Pocra", "2", "0000000010000", "", "", "", "" };
            ////string[] headers4 = new string[] { "77", "", "", "", "000785215852127", "", "000607420", "999ZN99", "Test Payment from Pocra", "3", "0000000010000", "", "", "", "" };
            ////string[] headers5 = new string[] { "77", "", "", "", "000785215852128", "", "000607420", "999ZN99", "Test Payment from Pocra", "4", "0000000010000", "", "", "", "" };
            ////string[] headers6 = new string[] { "77", "", "", "", "000785215852129", "", "000607420", "999ZN99", "Test Payment from Pocra", "5", "0000000010000", "", "", "", "" };
            ////using (FixedWidthWriter writer2 = new FixedWidthWriter(widths2, filepath,true))
            ////{
            ////    writer2.WriteLine(headers2);
            ////    //writer2.WriteLine(headers3);
            ////    //writer2.WriteLine(headers4);
            ////    //writer2.WriteLine(headers5);
            ////    //writer2.WriteLine(headers6);
            ////}



        }

        public void CreateExcel(String DisbursementID, String DisFileName)
        {
            MyClass cla = new MyClass();
            DataTable dt = cla.GetDataTable("select * from dbo.GeTDataForPayment(" + DisbursementID + ")");// cla.GetDataTable("SELECT  DisbursementChildID, UserReferenceNo, AaDharNumber, NameOnAudhar , RequestAmount, Convert(nvarchar(20),RequestDate,103) as RequestDate, 'A' as PaymentProductCode FROM  Tbl_T_Disbursement_Details_Child WHERE  (IsFileCreated IS NULL) and DisbursementID=" + DisbursementID + "");

            if (dt.Rows.Count > 0)
            {
                //UpdateLog(DisFileName.Trim() + "Inprocess");
                String filepath = @"E:\Kotak\Out/" + DisFileName + ".txt";


                List<string[]> lstPay = FixedWidthWriter.ConvertTable(dt);

                String SettlementDate = cla.SvrDate().Replace("/", "");
                String UserNumber = System.Configuration.ConfigurationManager.AppSettings["UserNumber"].ToString();
                String UserName = System.Configuration.ConfigurationManager.AppSettings["UserName"].ToString();
                String SponsorBankIIN = System.Configuration.ConfigurationManager.AppSettings["SponsorBankIIN"].ToString();
                String UsersBankAccountNumber = System.Configuration.ConfigurationManager.AppSettings["UsersBankAccountNumber"].ToString();

                int[] widths = new int[] { 2, 7, 40, 14, 9, 9, 15, 3, 13, 9, 13, 8, 10, 10, 3 };
                //APBS transaction Code,UserNumber,Username,UserCreditReferenceNo,APBS tape input,Sponsor Bank IIN,User’s bank Account number,Ledger Folio number,User defined limit for individual credit,items,Total items,Total amount (Balancing Debit amount),Settlement date (DDMMYYYY),Item Sequence Number,Checksum,Filler
                String UserCreditReferenceNo = DisbursementID.ToString();
                String TotalItems = dt.Rows.Count.ToString();
                String TotalAmoint = dt.Compute("Sum(RequestAmount)", "").ToString();


                TotalItems = TotalItems.PadRight(9);
                TotalAmoint = TotalAmoint.PadRight(9);
                string[] headers = new string[] { "33", UserNumber, UserName, UserCreditReferenceNo.Trim(), "", SponsorBankIIN, UsersBankAccountNumber, "", "", TotalItems.Trim(), TotalAmoint.Trim(), SettlementDate, "", "", "" };

                using (FixedWidthWriter writer = new FixedWidthWriter(widths, filepath))
                {
                    writer.WriteLine(headers);
                }





                //String DisFileName = dt.Rows[0]["DisFileName"].ToString();
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["con"].ToString()))
                {
                    connection.Open();

                    SqlCommand command = connection.CreateCommand();
                    SqlTransaction transaction;
                    transaction = connection.BeginTransaction("CTransaction");
                    command.Connection = connection;
                    command.Transaction = transaction;

                    try
                    {



                        //String str = "";
                        List<String> lst = new List<string>();


                        //foreach (DataRow dr in dt.Rows)
                        //{
                        //    //Pocra2,032305003693,899348693919,ASHUTOSH PANDEY,10,16/01/2019,2
                        //    str = DisbursementID.ToString() + ",032305003693," + dr["AaDharNumber"].ToString() + "," + dr["NameOnAudhar"].ToString() + "," + dr["RequestAmount"].ToString() + "," + dr["RequestDate"].ToString() + "," + dr["DisbursementChildID"].ToString().Trim() + "";


                        //    cla.ExecuteCommand("UPDATE Tbl_T_Disbursement_Details_Child SET   IsFileCreated ='1'  WHERE (DisbursementChildID =" + dr["DisbursementChildID"].ToString() + ")", command);
                        //    lst.Add(str);
                        //}

                        int[] widths2 = new int[] { 2, 9, 2, 3, 15, 40, 9, 7, 20, 13, 13, 10, 10, 1, 2, 9 };

                        using (FixedWidthWriter writer2 = new FixedWidthWriter(widths2, filepath, true))
                        {
                            foreach (string[] a in lstPay)
                            {
                                writer2.WriteLine(a);
                            }
                        }


                        cla.ExecuteCommand("UPDATE Tbl_T_Disbursement_Details_Child SET   IsFileCreated ='1'  WHERE (DisbursementID =" + DisbursementID.ToString() + ")", command);
                        cla.ExecuteCommand("UPDATE Tbl_T_Disbursement_Details SET   IsFileCreated ='1', FileCreateDateTime =Getdate() WHERE (DisbursementID =" + DisbursementID.ToString() + ")", command);




                        //UpdateLog(DisFileName.Trim() + "Done");


                        // transaction.Commit();
                        try
                        {
                            transaction.Rollback();

                        }
                        catch
                        {

                        }


                    }
                    catch (Exception exHandle)
                    {
                        //UpdateLog(exHandle.ToString());

                        try
                        {
                            transaction.Rollback();

                        }
                        catch
                        {

                        }
                       // cla.SendMail(exHandle.ToString());
                    }
                    finally
                    {
                        transaction.Dispose();
                        command.Dispose();
                    }
                }


            }



        }
    }
}