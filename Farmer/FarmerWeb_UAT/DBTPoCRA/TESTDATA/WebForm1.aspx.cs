
using CommanClsLibrary;
using DBTPoCRA.APPData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

namespace DBTPoCRA.TESTDATA
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        MyClass cla = new MyClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!IsPostBack)
            {
                Literal1.Text = cla.GetExecuteScalar("Select  top 1 RegisterName,RegisterNameMr,RegistrationID from Tbl_M_RegistrationDetails where IsDeleted is null and RegisterNameMr is null ");
                //GridView1.DataSource = cla.GetDataTable("Select  top 1 RegisterName,RegisterNameMr,RegistrationID from Tbl_M_RegistrationDetails where IsDeleted is null and RegisterNameMr is null  ");
                //GridView1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AzureBlobHelper fileRet = new AzureBlobHelper();
            Session["FPORegistrationID"] = "705";
            ViewState["FPOApplicationID"] = "738";
            String PathUp = "DocMasters/MemberDoc/FPO/" + Session["FPORegistrationID"].ToString().Trim().ToString().Trim() + "";

         
            String FPORegistrationID = Session["FPORegistrationID"].ToString().Trim();
            PathUp = "DocMasters/MemberDoc/FPO/" + FPORegistrationID.ToString().Trim() + "";
            String UpFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            String UploadedPath = "/admintrans/DocMasters/MemberDoc/FPO/" + FPORegistrationID.ToString().Trim() + "/" + UpFileName.Trim() + "" + System.IO.Path.GetExtension(fu.FileName.ToString());
            byte[] bin = fu.FileBytes;
            String fileName = UpFileName.Trim() + System.IO.Path.GetExtension(fu.FileName.ToString());
            String ret = fileRet.UploadData(PathUp, fileName, bin);
            if (ret.Trim().Length > 0)
            {
               // Util.ShowMessageBox(this.Page, "Error", "Please upload " + DocName + " ", "error");
                return;
            }
            else
            {
                if (cla.ExecuteCommand(" INSERT INTO FPO_A_FPOApplication_Documents (FPOApplicationID,NameOfDocuments,Document_Path) VALUES (" + ViewState["FPOApplicationID"].ToString().Trim() + ",'Technical sanction for construction (Approval from Warehousing Corporation or PWD or Zilla Parishad or Agricultural University or Government Institution for the design of construction projects like construction of warehouse and seed production center)','" + UploadedPath + "') ").Trim().Length == 0)
                {
                    //cla.ExecuteCommand("UPDATE FPO_A_FPOApplication SET Self_contribution_Amount='" + txtSelfcontribution.Text.Trim() + "',Proposed_Bank_Loan_Amount='" + txtProposedBabkLoanAmt.Text.Trim() + "' ,Estimated_grant_Amount='" + txtEstimatedGrant.Text.Trim() + "' ,Name_of_Proposed_Bank='" + txtProposedBankName.Text.Trim() + "' ,Previously_Borrowed_Amount='" + txtPreviouslyBorrowedAmt.Text.Trim() + "' ,Balance_Repayment_Amount='" + txtBalanceRepayment.Text.Trim() + "'  where FPOApplicationID=" + ViewState["FPOApplicationID"].ToString().Trim() + " ");
                    //String a = clsSettings.BaseUrl + UploadedPath;
                    //FillGridView();
                    Util.ShowMessageBox(this.Page, "Success", "FileUploaded Sucessfully", "success");
                }

            }

        }

            
        
        //protected void btnfocus_Click(object sender, EventArgs e)
        //{
        //    foreach(GridViewRow gr in GridView1.Rows)
        //    {
        //        TextBox TextBox1 = (TextBox)gr.FindControl("TextBox1");
        //        TextBox1.Focus();

        //    }
        //}


        //protected void Update_Click(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow gr in GridView1.Rows)
        //    {
        //        String RegistrationID = GridView1.DataKeys[gr.RowIndex]["RegistrationID"].ToString();
        //        TextBox TextBox1 = (TextBox)gr.FindControl("TextBox1");
        //        TextBox1.Focus();
        //        Label lbl = (Label)gr.FindControl("TextBox2");
        //        cla.ExecuteCommand("UPDATE Tbl_M_RegistrationDetails SET RegisterNameMr=N'"+ lbl.Text.Trim()+ "' where RegistrationID="+ RegistrationID + " ");
        //    }
        //}





        ////////SHA256 method
        //public String generateSha256Hash(byte[] message)
        //{
        //    String algorithm = "SHA-256";
        //    byte[] hash = null;

        //    java.security.MessageDigest digest;
        //    StringBuilder sb = new StringBuilder();
        //    try
        //    {
        //        // digest = MessageDigest.getInstance(algorithm, SECURITY_PROVIDER);
        //        digest = java.security.MessageDigest.getInstance(algorithm);
        //        digest.reset();
        //        hash = digest.digest(message);
        //        foreach (byte b in hash)
        //        {
        //            sb.Append(String.Format("%02x", b));
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        //e.printStackTrace();
        //    }

        //    return sb.ToString();
        //}

        //static string ComputeSha256Hash(string source)
        //{


        //    using (SHA256 sha256Hash = SHA256.Create())
        //    {

        //        string hash = GetHash(sha256Hash, source);

        //        //Console.WriteLine($"The SHA256 hash of {source} is: {hash}.");

        //        //Console.WriteLine("Verifying the hash...");

        //        //if (VerifyHash(sha256Hash, source, hash))
        //        //{
        //        //    Console.WriteLine("The hashes are the same.");
        //        //}
        //        //else
        //        //{
        //        //    Console.WriteLine("The hashes are not same.");
        //        //}

        //        return hash;
        //    }

        //    //// Create a SHA256   
        //    //using (SHA256 sha256Hash = SHA256.Create())
        //    //{



        //    //    // ComputeHash - returns byte array  
        //    //    byte[] bytes = sha256Hash.ComputeHash(System.Text.Encoding.Unicode.GetBytes(rawData));

        //    //    //String myString = new string(System.Text.Encoding.Unicode.GetChars(bytes));

        //    //    //Convert byte array to a string
        //    //    StringBuilder builder = new StringBuilder();
        //    //    for (int i = 0; i < bytes.Length; i++)
        //    //    {
        //    //        builder.Append(bytes[i].ToString("X2"));
        //    //    }
        //    //    return builder.ToString();
        //    //    //return BitConverter.ToString(bytes);
        //    //}
        //}
        //public string PutIntoQuotes(string value)
        //{
        //    return "\"" + value + "\"";
        //}


        //private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        //{

        //    UnicodeEncoding unicode = new UnicodeEncoding();
        //    Byte[] encodedBytes = unicode.GetBytes(input);
        //    // Convert the input string to a byte array and compute the hash.
        //    byte[] data = hashAlgorithm.ComputeHash(encodedBytes);

        //    return unicode.GetString(data);

        //    // Create a new Stringbuilder to collect the bytes
        //    // and create a string.
        //    //var sBuilder = new StringBuilder();

        //    //// Loop through each byte of the hashed data 
        //    //// and format each one as a hexadecimal string.
        //    //for (int i = 0; i < data.Length; i++)
        //    //{
        //    //    sBuilder.Append(data[i].ToString("x2"));
        //    //}

        //    //// Return the hexadecimal string.
        //    //return sBuilder.ToString();

        //    //string hex = "";
        //    //foreach (byte x in data)
        //    //{
        //    //    hex += String.Format("{0:x2}", x);
        //    //}

        //    //return hex;
        //}

        //// Verify a hash against a string.
        //private static bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
        //{
        //    // Hash the input.
        //    var hashOfInput = GetHash(hashAlgorithm, input);

        //    // Create a StringComparer an compare the hashes.
        //    StringComparer comparer = StringComparer.OrdinalIgnoreCase;

        //    return comparer.Compare(hashOfInput, hash) == 0;
        //}



        //private byte[] GetKey()
        //{
        //    return MOLCryptoEngine.MOLSecurity.GenerateKey(256);
        //}
        //private byte[] GetIV()
        //{
        //    byte[] keys = null;

        //    keys = MOLSecurity.GenerateIV(128, "uidaimaharashtra");// Encoding.ASCII.GetBytes("uidaimaharashtra"); //Convert.ToByte("uidaimaharashtra");
        //    return keys;
        //}

    }






}
