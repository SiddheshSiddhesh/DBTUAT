using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;

/// <summary>
/// Summary description for MyCertificatePolicy
/// </summary>
public class MyCertificatePolicy
{
	public MyCertificatePolicy()
	{
		//
		// TODO: Add constructor logic here
		//
    }

    public bool CheckValidationResult(ServicePoint sp, X509Certificate cert, WebRequest req, int problem)
    {

        return true;

    }
}