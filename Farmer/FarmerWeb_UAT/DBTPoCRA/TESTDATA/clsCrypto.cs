//using java.lang;
using java.security;
using ikvm.lang;
using java.lang;
using org.bouncycastle.util.encoders;

public class clsCrypto
{
    //////SHA256 method
    public string generateSha256Hash(string rawData)
    {
        var algorithm = "SHA-256";
        byte[] hash = null;
        string res = "";
        byte[] message = ikvm.extensions.ExtensionMethods.getBytes(rawData);

         

        java.security.MessageDigest digest;
        StringBuilder sb = new StringBuilder(message.Length*2);
        try
        {
            // digest = MessageDigest.getInstance(algorithm, SECURITY_PROVIDER);
            digest = MessageDigest.getInstance(algorithm);
            digest.reset();
            hash = digest.digest(message);


            res = bytesToHex(hash);// org.bouncycastle.util.encoders.Hex.toHexString(hash);

            //foreach (byte b in hash)
            //{
            //    //String str1 = String.format("%02x", b);
            //    sb.append(String.format("%02x", b));
            //    //sb.append(b.ToString("x2"));
            //}
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }

        return res;
    }

    private  string bytesToHex(byte[] hashInBytes)
    {

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hashInBytes.Length; i++)
        {
            sb.append(java.lang.Integer.toString((hashInBytes[i] & 0xff) + 0x100, 16).Substring(1));
        }
        return sb.toString();

    }

    public string EncodetoBase64(string sesKey)
    {
        byte[] message = ikvm.extensions.ExtensionMethods.getBytes(sesKey);
        byte[]  a= Base64.encode(message);
        return System.Convert.ToBase64String(a);
    }

    //public string EncodetoBase64FromByte(byte[] message)
    //{
    //    byte[] a = Base64.encode(message);
    //    return ikvm.extensions.ExtensionMethods.toString(message);  //Base64.toBase64String(message);
    //}
}
