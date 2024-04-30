using CommanClsLibrary.Repository.Interfaces;
using CommanClsLibrary.Repository.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
namespace CommanClsLibrary.Repository.Classes
{
    public class AdharVaultAPICalls : IAdharVaultAPICalls
    {
        public string GetAdharFromReference(string ReferenceNumber)
        {
            string URL = "https://dbt-api.mahapocra.gov.in/SharedAPI/DataVault/GetUIDFromReference/@Value";
            URL = URL.Replace("@Value", ReferenceNumber);

            string BaseURL = "https://dbt-api.mahapocra.gov.in/";
            string RemainingURL = "SharedAPI/DataVault/GetUIDFromReference/@Value";
            RemainingURL = RemainingURL.Replace("@Value", ReferenceNumber);
            URL = URL.Replace("@Value", ReferenceNumber);
            string AdharNumber = APICallAsync_GetAdhar(BaseURL, RemainingURL);


            return AdharNumber;
        }

        public string GetReferenceFromAdhar(string AdharNumber)
        {
            string URL = "https://dbt-api.mahapocra.gov.in/SharedAPI/DataVault/GetReferenceFromUID/@Value";
            string BaseURL = "https://dbt-api.mahapocra.gov.in/";
            string RemainingURL = "SharedAPI/DataVault/GetReferenceFromUID/@Value";
            RemainingURL = RemainingURL.Replace("@Value", AdharNumber);
            URL = URL.Replace("@Value", AdharNumber);
            string ReferenceNumber = APICallAsync_Reference(BaseURL, RemainingURL);

            return ReferenceNumber;
        }

        private string APICallAsync_GetAdhar(string BaseURL, string RemainingURL)
        {

            string AdharNumber = "";

            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseURL);
            //HTTP GET
            var responseTask = client.GetAsync(RemainingURL);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                AdharVaultAPIResponse apiResponse = (new JavaScriptSerializer()).Deserialize<AdharVaultAPIResponse>(result.Content.ReadAsStringAsync().Result);

                int StatusCode = apiResponse.StatusCode;

                if (StatusCode == 200)
                {
                    AdharNumber = apiResponse.UID;
                }
            }

            return AdharNumber;
        }

        private string APICallAsync_Reference(string BaseURL, string RemainingURL)
        {
            string Reference = "";
            try
            {


                var client = new HttpClient();
                client.BaseAddress = new Uri(BaseURL);
                //HTTP GET
                var responseTask = client.GetAsync(RemainingURL);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    AdharVaultAPIResponse apiResponse = (new JavaScriptSerializer()).Deserialize<AdharVaultAPIResponse>(result.Content.ReadAsStringAsync().Result);

                    int StatusCode = apiResponse.StatusCode;
                    if (StatusCode == 200)
                    {
                        Reference = apiResponse.Reference;
                    }
                }
            }
            catch (Exception)
            {
            }
            return Reference;
        }


        public AdharOTP_Response GetAdharOTP(string AdharNumber)
        {
            AdharOTP_Response obj = new AdharOTP_Response();
 
            string URL = "http://dbt-api.mahapocra.gov.in/SharedAPI/Shareddbtapi/Aadhar/GetOtp_Updated";
            string Body = "{\"SecurityKey\":\"\",\"AudharNumber\":\""+ AdharNumber + "\"}";

            try
            {
                StringContent Content = new StringContent(Body, Encoding.UTF8, "application/json");//application/json in case API need json
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(180);
                //client.DefaultRequestHeaders.Add("Authorization", AuthorizationToken);
                HttpResponseMessage response = client.PostAsync(URL, Content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string Response = response.Content.ReadAsStringAsync().Result;
                    //string Response = "{\"httpStatusCode\":200,\"message\":\"Success\",\"data\":{\"username\":\"Test\",\"userpassword\":null,\"token\":\"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IlRFU1QiLCJuYmYiOjE3MDE0MTExNDEsImV4cCI6MTcwMTQyNTU0MSwiaWF0IjoxNzAxNDExMTQxLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjU3MjU4IiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1NzI1OCJ9.FhsvhYqy5fem5FESopgP3Z8GmsQ6K3tfcucM5V9e3jg\"}}";

                    obj = JsonConvert.DeserializeObject<AdharOTP_Response>(Response);
                }
            }
            catch (Exception ex)
            {
                obj.MessageCode = "0";
                obj.Message = "Error Occured :"+ex.Message;
            }


            return obj;
        }

        public AdharOTP_Response VerifyAadharOTP(string AdharNumber, string TxnNo,string OTP)
        {
            AdharOTP_Response obj = new AdharOTP_Response();

            string URL = "http://dbt-api.mahapocra.gov.in/SharedAPI/Shareddbtapi/Aadhar/AuthrizedUsingOtp";
            string Body = "{\"SecurityKey\":\"\",\"AudharNumber\":\"" + AdharNumber + "\",\"Txn\":\""+ TxnNo + "\",\"Otp\":\""+ OTP + "\"}";

            try
            {
                StringContent Content = new StringContent(Body, Encoding.UTF8, "application/json");//application/json in case API need json
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(180);
                //client.DefaultRequestHeaders.Add("Authorization", AuthorizationToken);
                HttpResponseMessage response = client.PostAsync(URL, Content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string Response = response.Content.ReadAsStringAsync().Result;
                    //string Response = "{\"httpStatusCode\":200,\"message\":\"Success\",\"data\":{\"username\":\"Test\",\"userpassword\":null,\"token\":\"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IlRFU1QiLCJuYmYiOjE3MDE0MTExNDEsImV4cCI6MTcwMTQyNTU0MSwiaWF0IjoxNzAxNDExMTQxLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjU3MjU4IiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1NzI1OCJ9.FhsvhYqy5fem5FESopgP3Z8GmsQ6K3tfcucM5V9e3jg\"}}";

                    obj = JsonConvert.DeserializeObject<AdharOTP_Response>(Response);
                }
            }
            catch (Exception ex)
            {
                obj.MessageCode = "0";
                obj.Message = "Error Occured :" + ex.Message;
            }


            return obj;
        }
    }
}
