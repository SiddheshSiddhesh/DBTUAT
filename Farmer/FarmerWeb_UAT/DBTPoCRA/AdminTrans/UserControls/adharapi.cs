
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Modes.Gcm;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using Org.BouncyCastle.Crypto.Modes;
//using javax.crypto.KeyGenerator;

namespace DBTPoCRA.AdminTrans.UserControls
{
    public partial class AudharAPi : System.Web.UI.Page
    {



        public void GenerateXml()
        {
            string struid = "999941057058";

            XmlDocument XDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = XDoc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            XDoc.AppendChild(xmlDeclaration);


            XmlElement XElemRoot = XDoc.CreateElement("Otp");


            XElemRoot.SetAttribute("xmlns", "http://www.uidai.gov.in/authentication/uid-auth-request/1.0");

            XElemRoot.SetAttribute("uid", struid);
            XElemRoot.SetAttribute("ac", "public");
            XElemRoot.SetAttribute("sa", "SNDKS23054");
            XElemRoot.SetAttribute("ver", "1.6");
            XElemRoot.SetAttribute("txn", "UKC:public:" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            XElemRoot.SetAttribute("ts", "2018-09-30T05:01:11");
            XElemRoot.SetAttribute("lk", "MBni88mRNM18dKdiVyDYCuddwXEQpl68dZAGBQ2nsOlGMzC9DkOVL5s");
            XElemRoot.SetAttribute("type", "A");
            XDoc.AppendChild(XElemRoot);

            XmlElement aa = XDoc.CreateElement("Opts");
            aa.SetAttribute("ch", "00");
            XElemRoot.AppendChild(aa);



            //XmlElement Xsource = XDoc.CreateElement("Uses");
            //Xsource.SetAttribute("bio", "n");
            //Xsource.SetAttribute("bt", "n");
            //Xsource.SetAttribute("otp", "n");

            //Xsource.SetAttribute("pa", "n");
            //Xsource.SetAttribute("pfa", "n");
            //Xsource.SetAttribute("pi", "n");

            //Xsource.SetAttribute("pin", "n");

            //XElemRoot.AppendChild(Xsource);

            //XmlElement Xsource0 = XDoc.CreateElement("Meta");

            //Xsource0.SetAttribute("fdc", "NC");
            //Xsource0.SetAttribute("idc", "NA");
            //Xsource0.SetAttribute("lot", "P");
            //Xsource0.SetAttribute("lov", "560103");
            //Xsource0.SetAttribute("pip", "127.0.0.1");
            //Xsource0.SetAttribute("udc", "UKC:SampleClient");
            //XElemRoot.AppendChild(Xsource0);

            //XmlElement Xsource1 = XDoc.CreateElement("Meta");

            //Xsource1.SetAttribute("fdc", "NC");
            //Xsource1.SetAttribute("idc", "NA");
            //Xsource1.SetAttribute("lot", "P");
            //Xsource1.SetAttribute("lov", "560103");
            //Xsource1.SetAttribute("pip", "127.0.0.1");
            //Xsource1.SetAttribute("udc", "UKC:SampleClient");
            //XElemRoot.AppendChild(Xsource1);

            //XmlElement Xsource2 = XDoc.CreateElement("Skey");

            //Xsource2.SetAttribute("ci", "20150922");
            //Xsource2.InnerText = SSKEY();
            //XElemRoot.AppendChild(Xsource2);

            //XmlElement Xsource3 = XDoc.CreateElement("Data");
            //Xsource3.InnerText = EncryptPID();
            //XElemRoot.AppendChild(Xsource3);

            //XmlElement Xsource4 = XDoc.CreateElement("Hmac");
            //Xsource4.InnerText = Hmac;
            //XElemRoot.AppendChild(Xsource4);

            XDoc.Save(Server.MapPath("~/TESTDATA/test.xml").ToString());
            SendForAuthentication();


        }
        public string PutIntoQuotes(string value)
        {
            return "\"" + value + "\"";
        }

        public void Auth(String otp)
        {

            //String SSKEYS = "Cn47aR2iHVpb5bXIvjKJD2Hqbpt1IU/PGB370riZj7xei/7R/Z3m9sPEilhw5lDIz4gqeFbbgxRYWDricBVcxU4mX6fKLCmxEdVza5NPB3AAftKKVObW5nmF+knsU064XnJHSsNHqvkKAGKq9FsIMhR/fw3lR8OZSkZNQKoITZNe034H3Zq5azpNZkRdhhBaCKnZ0Qt//QPuZHTaQVQhcrHVGiUT0g8IJqqpwWpsbxzLG/gvleqMeluoUnWSfhF7tvY4w6Nwnip3aMyf747w7C3OxMC+/17FDAhOvYag7x9DGWfYxaQcUGnBOaqJRixgYZWSvp3f/SYlC7U4wl5Ukw==";
            //String PidS= "9DL2JceHjkQcln4OsPstZVXTTPSgSw6RyXOkW0/Myq47Y9EIsWoNL5e04t4K0mlandZOgfnwhyjmMgZltO2bwzwcAxr4k9D3CGN3O2IoC3Y=";
            //String Hmacs = "Wy3MEM9kihrV0zyyE5ZQXVExu6VMzBeibidnxyw7FIY8HAMa+JPQ9whjdztiKAt2";


            byte[] inputData = Encoding.UTF8.GetBytes("<Pid ts=\"2017-07-24T13:13:54\" ver=\"2.0\"><Pv otp=\"252525\"/></Pid>");// Encoding.UTF8.GetBytes(original);
            String ts = DateTime.Now.ToString("YYYY-MM-dd'T'hh:mm:ss");
            byte[] sessionKey = generateSessionKey();

            byte[] cipherTextWithTS = encrypt(inputData, sessionKey, ts);

            byte[] srcHash = generateHash(inputData);

           // byte[] iv = generateIv(ts);

           // byte[] aad = generateAad(ts);

           // byte[] encSrcHash = encryptDecryptUsingSessionKey(true, sessionKey, iv, aad, srcHash);

           // byte[] decryptedText = decrypt(cipherTextWithTS, sessionKey, encSrcHash);





            string url = "https://auaqa.maharashtra.gov.in/aua/rest/authreqv2";

            String Txn = "UKC:public:" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

            String str = "<Auth xmlns=" + PutIntoQuotes("http://aua.maharashtra.gov.in/auth/gom-auth-request") + ">";
            str += "<Txn>" + Txn + "</Txn>";
            str += "<Ver>2.5</Ver>";
            str += "<SubAUACode>SNDKS23054</SubAUACode>";
            str += "<ReqType>auth</ReqType>";
            str += "<DeviceId>UDC-AGRIGOM-0001</DeviceId>";
            str += "<UID>899348693919</UID>";
            str += "<Uses pi =" + PutIntoQuotes("n") + " pa =" + PutIntoQuotes("n") + " pfa =" + PutIntoQuotes("n") + " bio =" + PutIntoQuotes("n") + " bt =" + PutIntoQuotes("n") + " pin=" + PutIntoQuotes("n") + " otp=" + PutIntoQuotes("y") + "/>";
            str += "<Meta rdsId =" + PutIntoQuotes("") + " rdsVer =" + PutIntoQuotes("") + " dpId =" + PutIntoQuotes("") + " dc =" + PutIntoQuotes("") + " mi =" + PutIntoQuotes("") + " mc =" + PutIntoQuotes("") + " />";
            str += "<Skey ci =" + PutIntoQuotes("20201030") + ">" + System.Convert.ToBase64String(sessionKey) + "</Skey>";
            str += "<Data type =" + PutIntoQuotes("X") + ">" + System.Convert.ToBase64String(cipherTextWithTS) + "</Data>";
            str += "<Hmac>" + System.Convert.ToBase64String(srcHash) + "</Hmac> <type>A</type>";
            str += "<rc>Y</rc>";
            str += "</Auth>";

            String Conn = str;// sr1.ReadToEnd();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(Conn);
            req.Method = "POST";
            req.ContentType = "text/xml";
            req.ContentLength = requestBytes.Length;
            Stream requestStream = req.GetRequestStream();
            requestStream.Write(requestBytes, 0, requestBytes.Length);
            requestStream.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
            string backstr = sr.ReadToEnd();
            ////////////////XmlDocument xdocl = new XmlDocument();
        }

        public void SendForAuthentication()
        {

            // SingXML(); "">

            String Txn = "UKC:public:" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

            String str = "<Auth xmlns=" + PutIntoQuotes("http://aua.maharashtra.gov.in/auth/gom-auth-request") + ">";
            str += "<Txn>" + Txn + "</Txn>";
            str += "<Ver>2.5</Ver>";
            str += "<SubAUACode>SNDKS23054</SubAUACode>";
            str += "<ReqType>otp</ReqType>";
            str += "<DeviceId>UDC-AGRIGOM-0001</DeviceId>";
            str += "<UID>899348693919</UID>";
            str += "<type>A</type>";
            str += "<Ch>01</Ch>";
            str += "</Auth> ";

            string url = "https://auaqa.maharashtra.gov.in/aua/rest/authreqv2";

            //StreamReader sr1 = new StreamReader(Server.MapPath("~/TESTDATA/test-signed.xml").ToString());
            String Conn = str;// sr1.ReadToEnd();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(Conn);
            req.Method = "POST";
            req.ContentType = "text/xml";
            req.ContentLength = requestBytes.Length;
            Stream requestStream = req.GetRequestStream();
            requestStream.Write(requestBytes, 0, requestBytes.Length);
            requestStream.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
            string backstr = sr.ReadToEnd();
            XmlDocument xdocl = new XmlDocument();
            xdocl.LoadXml(backstr);
            xdocl.Save(Server.MapPath("~/TESTDATA/test1.xml").ToString());
            sr.Close();
            res.Close();
        }

        String Hmac;
        private String EncryptPID(String otp)
        {
            //GenratePID(otp);
            //string original = File.ReadAllText(Server.MapPath("~/TESTDATA/pid.xml").ToString());//"<Pid ts="+PutIntoQuotes("2018-10-27T18:27:31") +" ver="+PutIntoQuotes("2.0")+"><Pv otp="+PutIntoQuotes("2524")+" /></Pid>";


            //GenratePID(otp);
            //string original = File.ReadAllText(Server.MapPath("~/TESTDATA/pid.xml").ToString());//"<Pid ts="+PutIntoQuotes("2018-10-27T18:27:31") +" ver="+PutIntoQuotes("2.0")+"><Pv otp="+PutIntoQuotes("2524")+" /></Pid>";

            //AESCipher aesCipher = new AESCipher();
            //String ts = xc.toString();
            //byte[] sK = aesCipher.generateSessionKey();
            //System.out.println("Skey Print" + sK.length);
            //System.out.println("sessionKey " + Base64.encodeBase64String(sK));
            //cipherTextWithTS = aesCipher.encrypt(pidXmlBytes, sK, ts);

            byte[] inputData = Encoding.UTF8.GetBytes("<Pid ts=\"2017-07-24T13:13:54\" ver=\"2.0\"><Pv otp=\"252525\"/></Pid>");// Encoding.UTF8.GetBytes(original);
            String ts = DateTime.Now.ToString("YYYY-MM-dd'T'hh:mm:ss");
            byte[] sessionKey = generateSessionKey();

            byte[] cipherTextWithTS = encrypt(inputData, sessionKey, ts);

            byte[] srcHash = generateHash(inputData);

            byte[] iv = generateIv(ts);

            byte[] aad = generateAad(ts);

            byte[] encSrcHash = encryptDecryptUsingSessionKey(true, sessionKey, iv, aad, srcHash);

            byte[] decryptedText = decrypt(cipherTextWithTS, sessionKey, encSrcHash);

            //byte[] encrypted;

            //RijndaelManaged myRijndael = new RijndaelManaged();

            ////string getHashSha256from_Original = GetSHA256(original);

            ////encrypted = EncryptStringToBytes_Aes(getHashSha256from_Original, SKey1, myRijndael.IV);

            //Hmac = GetSHA256(original);
            //byte[] Hmac1;
            //Hmac1 = EncryptStringToBytes(Hmac, SKey1, myRijndael.IV);
            //Hmac = System.Convert.ToBase64String(Hmac1);

            //return System.Convert.ToBase64String(encryptUsingSessionKey(pidBytes, SKey1));

            //byte[] encrypted;

            //RijndaelManaged myRijndael = new RijndaelManaged();

            //string getHashSha256from_Original = GetSHA256(original);

            //encrypted = EncryptStringToBytes_Aes(getHashSha256from_Original, SKey1, myRijndael.IV);

            //Hmac = GetSHA256(original);
            //byte[] Hmac1;
            //Hmac1 = EncryptStringToBytes(Hmac, SKey1, myRijndael.IV);
            //Hmac = System.Convert.ToBase64String(Hmac1);
            //return System.Convert.ToBase64String(encrypted);



            //byte[] encrypted;

            //RijndaelManaged myRijndael = new RijndaelManaged();

            ////AesManaged myAes = new AesManaged();
            //encrypted = EncryptStringToBytes(original, SKey1, myRijndael.IV);

            //Hmac = GetSHA256(original);
            //byte[] Hmac1;
            //Hmac1 = EncryptStringToBytes(Hmac, SKey1, myRijndael.IV);
            //Hmac = System.Convert.ToBase64String(Hmac1);
            //return System.Convert.ToBase64String(encrypted);

            return "";

        }

        public static int IV_SIZE_BITS = 96;
        public static int AAD_SIZE_BITS = 128;
        public byte[] decrypt(byte[] inputData, byte[] sessionKey, byte[] encSrcHash)
        {

            byte[] bytesTs = copyOfRange(inputData, 0, 19);

            String ts = Encoding.UTF8.GetString(bytesTs, 0, bytesTs.Length);

            byte[] cipherData = copyOfRange(inputData, bytesTs.Length, inputData.Length);

            byte[] iv = this.generateIv(ts);

            byte[] aad = this.generateAad(ts);

            byte[] plainText = this.encryptDecryptUsingSessionKey(false, sessionKey, iv, aad, cipherData);

            byte[] srcHash = this.encryptDecryptUsingSessionKey(false, sessionKey, iv, aad, encSrcHash);

            //System.out.println("Decrypted HAsh in cipher text: "+byteArrayToHexString(srcHash));

            Boolean result = this.validateHash(srcHash, plainText);

            if (!result) {

                throw new Exception("Integrity Validation Failed : " + "The original data at client side and the decrypted data at server side is not identical");

            } else {

               // System.out.println("Hash Validation is Successful!!!!!");

                return plainText;

            }

        }
        private Boolean validateHash(byte[] srcHash, byte[] plainTextWithTS)
        {

            byte[] actualHash = this.generateHash(plainTextWithTS);

            //System.out.println("Hash of actual plain text in cipher hex:--->"+byteArrayToHexString(actualHash));

            //		boolean tr =  Arrays.equals(srcHash, actualHash);

            if (Encoding.UTF8.GetString(srcHash, 0, srcHash.Length)==(Encoding.UTF8.GetString(actualHash, 0, actualHash.Length)))
            {

                return true;

            }
            else
            {

                return false;

            }

        }
    byte[] copyOfRange(byte[] src, int start, int end)
        {
            int len = end - start;
            byte[] dest = new byte[len];
            // note i is always from 0
            for (int i = 0; i < len; i++)
            {
                dest[i] = src[start + i]; // so 0..n = 0+x..n+x
            }
            return dest;
        }
        public byte[] generateHash(byte[] message)
        {

            byte[] hash = null;

            try
            {

                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // ComputeHash - returns byte array  
                    byte[] bytes = sha256Hash.ComputeHash(message);
                    hash = bytes;

                }
                //HashAlgorithm sha = new SHA1CryptoServiceProvider();
                //byte[] result = sha.ComputeHash(message);
                //MessageDigest digest = MessageDigest.getInstance(algorithm, SECURITY_PROVIDER);

                //digest.reset();

                //HMAC_SIZE = digest.getDigestLength();

                //hash = digest.digest(message);

            }
            catch (Exception e)
            {

                throw new Exception(

                        "SHA-256 Hashing algorithm not available");

            }

            return hash;

        }
        public static int AES_KEY_SIZE_BITS = 256;
        public byte[] generateSessionKey()
        {

            //KeyGenerator kgen = KeyGenerator.getInstance("AES", JCE_PROVIDER);
            //var kgen = KeyGenerator.getInstance("AES", JCE_PROVIDER);
            //kgen.init(AES_KEY_SIZE_BITS);

            //SecretKey key = kgen.generateKey();

            //byte[] symmKey = key.getEncoded();

            //return symmKey;



            byte[] symmKey, iv;

            using (AesManaged aes = new AesManaged())
            {
                aes.GenerateIV();
                aes.GenerateKey();
                aes.Mode = CipherMode.ECB;
                symmKey = aes.Key;
                iv = aes.IV;

                aes.Clear();
            }



            //RijndaelManaged sessionKey = new RijndaelManaged();
            //sessionKey.KeySize = 256;
            byte[] encryptedtext;
            //sessionKey.GenerateKey();
            X509Certificate2 uidCert = new X509Certificate2(Server.MapPath("~/AdminTrans/UserControls/uidai_auth_preprod.cer").ToString(), "public", X509KeyStorageFlags.DefaultKeySet);

            RSACryptoServiceProvider RSA = (RSACryptoServiceProvider)uidCert.PublicKey.Key;
            {
                RSAParameters RSAKeyInfo = RSA.ExportParameters(false);
                encryptedtext = RSAEncrypt(symmKey, RSAKeyInfo, false);
            }

            //return Convert.ToBase64String(encryptedtext);
           // Array.Resize(ref encryptedtext, 32);
            return encryptedtext;

        }
        public byte[] generateIv(String ts)
        {

            return getLastBits(ts, IV_SIZE_BITS / 8);

        }
        private byte[] getLastBits(String ts, int bits)
        {

            byte[] tsInBytes = Encoding.UTF8.GetBytes(ts);

            return copyOfRange(tsInBytes, tsInBytes.Length - bits, tsInBytes.Length);

        }
        public byte[] generateAad(String ts)
        {

            return getLastBits(ts, AAD_SIZE_BITS / 8);

        }
        public byte[] encrypt(byte[] inputData, byte[] sessionKey, String ts)
        {

            byte[] iv = this.generateIv(ts);

            //System.out.println("iv "+ new String(iv));

            byte[] aad = this.generateAad(ts);
            // System.out.println("aad "+ new String(aad));

            byte[] cipherText = this.encryptDecryptUsingSessionKey(true, sessionKey, iv, aad, inputData);

            //System.out.println("cipher text *********** ---> "+Base64.encodeBase64String(cipherText));

            byte[] tsInBytes = Encoding.UTF8.GetBytes(ts);

            //byte[] packedCipherData = new byte[cipherText.length + tsInBytes.length];

            //System.arraycopy(tsInBytes, 0, packedCipherData, 0, tsInBytes.length);

            //System.arraycopy(cipherText, 0, packedCipherData, tsInBytes.length, cipherText.length);

            return cipherText;

        }

        public static int AUTH_TAG_SIZE_BITS = 16;// 128;

        public byte[] encryptDecryptUsingSessionKey(Boolean cipherOperation, byte[] skey, byte[] iv, byte[] aad, byte[] data)
        {

            //      AEADParameters aeadParam = new AEADParameters(new KeyParameter(skey), AUTH_TAG_SIZE_BITS, iv, aad);
            var parameters = new AeadParameters(new KeyParameter(skey), AUTH_TAG_SIZE_BITS, iv, aad);
            //      GCMBlockCipher gcmb = new GCMBlockCipher(new AESEngine());
            var decryptCipher = new GcmBlockCipher(new AesEngine());
            //      gcmb.init(cipherOperation, aeadParam);
            decryptCipher.Init(cipherOperation, parameters);
            //int outputSize = gcmb.getOutputSize(data.length);
            int outputSize = decryptCipher.GetOutputSize(data.Length);
            //      byte[] result = new byte[outputSize];
            byte[] result = new byte[outputSize];
            //      int processLen = gcmb.processBytes(data, 0, data.length, result, 0);
            int processLen = decryptCipher.ProcessBytes(data, 0, data.Length, result, 0);
            //      gcmb.doFinal(result, processLen);
            decryptCipher.DoFinal(result, processLen);
            return result;

            //AeadParameters aeadParam = new AeadParameters(new KeyParameter(skey), AUTH_TAG_SIZE_BITS, iv, aad);

            //PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(new AesEngine());
            //cipher.Init(cipherOperation, aeadParam);

            //int outputSize = cipher.GetOutputSize(data.Length);

            //byte[] tempOP = new byte[outputSize];
            //int processLen = cipher.ProcessBytes(data, 0, data.Length, tempOP, 0);
            //int outputLen = cipher.DoFinal(tempOP, processLen);

            //byte[] result = new byte[processLen + outputLen];
            //tempOP.CopyTo(result, 0);
            //return result;

        }
        public byte[] encryptUsingSessionKey(byte[] data, byte[] skey)
        {
            PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(new AesEngine(), new Pkcs7Padding());
            cipher.Init(true, new KeyParameter(skey));

            int outputSize = cipher.GetOutputSize(data.Length);

            byte[] tempOP = new byte[outputSize];
            int processLen = cipher.ProcessBytes(data, 0, data.Length, tempOP, 0);
            int outputLen = cipher.DoFinal(tempOP, processLen);

            byte[] result = new byte[processLen + outputLen];
            tempOP.CopyTo(result, 0);
            return result;
        }
        public string EncodeStringToBase64(string stringToEncode)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(stringToEncode));
        }

        //private String EncryptPID()
        //{
        //    GenratePID();
        //    using (AesCryptoServiceProvider myAes = new AesCryptoServiceProvider())
        //    {
        //        string original = File.ReadAllText(Server.MapPath("~/pid.xml").ToString());
        //        byte[] encrypted = EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);
        //        string Encrypted = "";
        //        Hmac = GetSHA256(Encrypted);
        //        return System.Convert.ToBase64String(encrypted);
        //    }
        //}

        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;

            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }

        public string skeyencryption(string toencrypt, string filepath)
        {
            X509Store store = new X509Store(StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certcollection = store.Certificates;
            X509Certificate2 x509 = new X509Certificate2(filepath);
            RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)x509.PublicKey.Key;

            byte[] bytestoEncrypt = ASCIIEncoding.ASCII.GetBytes(toencrypt);
            byte[] encryptedBytes = rsa.Encrypt(bytestoEncrypt, false);

            return Convert.ToBase64String(encryptedBytes);

        }

        public string pidencryptionaes256(string boimetric, string key)
        {

            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key); // 256-AES key
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(boimetric);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB; // http://msdn.microsoft.com/en-us/library/system.security.cryptography.ciphermode.aspx
            rDel.Padding = PaddingMode.PKCS7; // better lang support
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);

        }


        private byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }

                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream. 
            return encrypted;
        }

        private void GenratePID(String otp)
        {

            string strotp = otp;
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement XElemRoot = xmlDoc.CreateElement("Pid");
            XElemRoot.SetAttribute("ts", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
            XElemRoot.SetAttribute("ver", "2.0");
            xmlDoc.AppendChild(XElemRoot);
            XmlElement Xsource = xmlDoc.CreateElement("Pv");
            Xsource.SetAttribute("otp", strotp);
            XElemRoot.AppendChild(Xsource);
            xmlDoc.Save(Server.MapPath("~/TESTDATA/pid.xml").ToString());

        }


        private string GetSHA256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            using (SHA256Managed algorithm = new SHA256Managed())
            {
                byte[] buffer2 = algorithm.TransformFinalBlock(bytes, 0, bytes.Length);
                byte[] hash = algorithm.Hash;
                return Convert.ToBase64String(algorithm.Hash);
            }


        }

        private void SingXML()
        {
            try
            {

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Server.MapPath("~/TESTDATA/test.xml").ToString());
                xmlDoc.PreserveWhitespace = true;

                //X509Certificate2 uidCert = new X509Certificate2(Server.MapPath("~/AdminTrans/UserControls/uidai_auth_stage.cer").ToString(), "public", X509KeyStorageFlags.DefaultKeySet);

                X509Certificate2 uidCert = new X509Certificate2(Server.MapPath("~/AdminTrans/UserControls/Staging_Signature_PrivateKey.p12").ToString(), "public", X509KeyStorageFlags.DefaultKeySet);
                using (RSACryptoServiceProvider rsaKey = (RSACryptoServiceProvider)uidCert.PrivateKey)
                {
                    SignXml(xmlDoc, uidCert);
                }

                xmlDoc.Save(Server.MapPath("~/TESTDATA/test-signed.xml").ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {

            }

        }

        private void SignXml(XmlDocument XDoc, X509Certificate2 uidCert)
        {

            RSACryptoServiceProvider rsaKey = (RSACryptoServiceProvider)uidCert.PrivateKey;

            if (XDoc == null)
                throw new ArgumentException("XDoc");
            if (rsaKey == null)
                throw new ArgumentException("Key");

            SignedXml signedXml = new SignedXml(XDoc);
            signedXml.SigningKey = rsaKey;
            Reference reference = new Reference();
            reference.Uri = "";
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);
            signedXml.AddReference(reference);
            KeyInfo keyInfo = new KeyInfo();
            KeyInfoX509Data clause = new KeyInfoX509Data();
            clause.AddSubjectName(uidCert.Subject);
            clause.AddCertificate(uidCert);
            keyInfo.AddClause(clause);
            signedXml.KeyInfo = keyInfo;
            signedXml.ComputeSignature();
            XmlElement xmlDigitalSignature = signedXml.GetXml();
            System.Console.WriteLine(signedXml.GetXml().InnerXml);
            XDoc.DocumentElement.AppendChild(XDoc.ImportNode(xmlDigitalSignature, true));


        }

        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

        public byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                //X509Store store = new X509Store(StoreLocation.CurrentUser);
                //store.Open(OpenFlags.ReadOnly);
                //X509Certificate2Collection certcollection = store.Certificates;
                //XmlDocument xmlDoc = new XmlDocument();
                //xmlDoc.PreserveWhitespace = true;
                //X509Certificate2 uidCert = new X509Certificate2(@"D:\uidai-auth-client-1.6-bin\uidai-auth-client-1.6-bin\uidai-auth-client-1.6-bin/uidai_auth_stage.cer", "public", X509KeyStorageFlags.DefaultKeySet);

                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportParameters(RSAKeyInfo);

                    encryptedData = rsa.Encrypt(DataToEncrypt, DoOAEPPadding);
                }


                return encryptedData;
            }

            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }

        }

        byte[] SKey1 = null;

        private String SSKEY()
        {

            //AesManaged sessionKey = new AesManaged();

            //sessionKey.KeySize = 256;
            //SKey1 = sessionKey.Key;
            //byte[] SkeyIV = sessionKey.IV;
            //byte[] encryptedtext;
            //sessionKey.GenerateKey();

            //var str = System.Text.Encoding.Default.GetString(sessionKey.Key);

            //X509Store store = new X509Store(StoreLocation.CurrentUser);
            //store.Open(OpenFlags.ReadOnly);
            //X509Certificate2Collection certcollection = store.Certificates;
            //X509Certificate2 x509 = new X509Certificate2(Server.MapPath("~/AdminTrans/UserControls/uidai_auth_preprod.cer"));
            //RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)x509.PublicKey.Key;

            //byte[] bytestoEncrypt = ASCIIEncoding.ASCII.GetBytes(str);
            //byte[] encryptedBytes = rsa.Encrypt(bytestoEncrypt, false);

            //return Convert.ToBase64String(encryptedBytes);

            RijndaelManaged sessionKey = new RijndaelManaged();
            sessionKey.KeySize = 256;
            byte[] encryptedtext;
            sessionKey.GenerateKey();
            X509Certificate2 uidCert = new X509Certificate2(Server.MapPath("~/AdminTrans/UserControls/uidai_auth_preprod.cer").ToString(), "public", X509KeyStorageFlags.DefaultKeySet);

            RSACryptoServiceProvider RSA = (RSACryptoServiceProvider)uidCert.PublicKey.Key;
            {
                RSAParameters RSAKeyInfo = RSA.ExportParameters(false);
                encryptedtext = RSAEncrypt(sessionKey.Key, RSAKeyInfo, false);
            }
            SKey1 = sessionKey.Key;
            return Convert.ToBase64String(encryptedtext);

        }

        //private String SSKEY()
        //{
        //    RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        //    RijndaelManaged sessionKey = new RijndaelManaged();
        //    sessionKey.KeySize = 256;
        //    byte[] encryptedtext;
        //    encryptedtext = RSAEncrypt(sessionKey.Key, RSA.ExportParameters(false), false);
        //    SKey1 = sessionKey.Key;
        //    return System.Convert.ToBase64String(encryptedtext);
        //}

    }
}
