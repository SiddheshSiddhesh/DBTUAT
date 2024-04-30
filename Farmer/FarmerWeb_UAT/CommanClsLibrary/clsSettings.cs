using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;



namespace CommanClsLibrary
{
    public static class clsSettings
    {

        public static String PublicKey = "MjA0OCE8UlNBS2V5VmFsdWU+PE1vZHVsdXM+OFZucTE0d215NkJTcDRrQm1rZW1ob2ZiTFFrMFZqaHMzdXFNY1hlTVMyUXp5Mzl2TFMzNUY4WCtsMzhmeHVoSjlEL1dyaTVqT2o5SUFtelkweG8zWlZsd3pBeUdvYnAzc2lXUE1UWStRRTBhWXo4NWhqa0JCRU54STdoSDBIWGVkL0ZXTExPemtZbXZRS0s4NHJwYlRXSjZhYndDeWtwaFA0V1dQUkVPVGNZOU1MZjRlZUpBYThSc0NIdFhUNGE3YXpJdisrT3RSSEJkbVF4Y2paYml2cm5scStUQXdrbGV6SmlyeHJHc0NCSFFIRXZRbWJsNGUwV28wZGg1OFQ3VXdYNXlSU1gvalNLRFEwcGZDUXpYcjlvOFVyS2o1S2hwc2FVYkN5NmVHbmpVbWliM3Ira3kwVkZSRWpRL0lVZFdMMzNIRWVRRkd1SFRyY0hTV004QjhRPT08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjxQPi9oSGxPMThXZzVzSFZYZzY3MkxlMlRGcVVHcHJxUWcwQjJ5Nmg1a3pBWkc1L0NrcDE2Rit4eUZtSXlLR1BWazhTSTNlRmdmcy9mckFxSDJtMUduVGdzYWkweFR0Z1dHTEFEbGNRcGhnZzg0NUZ0eWxuZWtUK2NyRUxaSUV6OVU4K1FBMGREeTMzY1c0RlNrYkQxN0UyMjVOQWVuVDFSZ2hQTTNabVliQUQvcz08L1A+PFE+OHk5SmhRRldocmYyeE1yUXVacEJONXMxcGpuWDBFU2Mxbk5jOC9OWUdrc2tlTTRQVTlMM00yTEF3MkJIMXZzVWxZVmdlMGtkNG43OXlheVdqK0tveXNjNUhyNHNKQ3JqeDBXWFdKdTgzR3UzZGpiSUQ1NWZNSFQzQTBvUC9OK0JYM3RWTldyQlJCZUtSZWo0S3ZpMzhVNStXSUM4dEhGUkVoOHZrM2dwMWdNPTwvUT48RFA+WTNZdC95Z2ljRExvbEJVVlBWRy9XOWQwZnphcUh1b1BORGprYk9FVWpyQzExbmxtMy90ZzNpTzlFRlFicVRpZDJPbHczNzhLa0FMeUdRUFhvNkFxSlRBTjNnUjdWZ01SbjJ6VktWenRxNnUzdmJOelFuWlVVNTJGWHpHaW9Kb2gzSGl3RURFVTZjVFZZN05MM0tDRkFFSVY4NEhPWThXbWZWODRxRm1RSnJFPTwvRFA+PERRPmJ2bHVVRzdxNEhHRkFBc1pzd2tzcDhhQmRnakJibjVSSUIvbkZFQkJQVVo3TDFIQmR0dzRDTkFRN1ZlN2tPUmxZVkpMVkJkcXR0aUMwZ3liYksvZm5TSEs5RGVPaHphM1dWRnRubmI3ZVk4Q1dzVG5DUkdabU5CWGhMM0FqQUltMUw3QWhLN2g0VVBveXJ4a3U1OWl1WkM3WVlIbUcyWDJ2QkZWTWJQTlM2Yz08L0RRPjxJbnZlcnNlUT5nQUIrQTcrSTB3OGJJcDJldlhXWWVGeUYxSDlhWG03QjlGbUxCOW5xbjhpTXJFT2F2TjZqTXhUYzRSMUxiM2pRREN3dGpiNkh2TGlBM2RBeEN0M3NTYVVTODZ4WGx4b3pqbmlHRk5DRTgvRXB5MkxnT1c3a0NxbmVxL0U5KzFGODg5bG84cVlvK0tvQTRBOUE4TmRmTWk1M0VBb252elZBU0dsRTlFTHJWU0k9PC9JbnZlcnNlUT48RD5NS0JCSkhSa1RKcGNqTXladWcrOWlWVk4zMkQvWG0rMzM2RzU4aEozL2FpT2x1MXg1TlJpWDF2UTA0MFZSZW4vSEdyUVFmK3Vsa214cEd0eHMvYVFPcWN1aHEzdlloNURoMnp4MEtSeEhsdHFlWngvWm4rTnFPSTlwVWkzajFSd0loSkpubkFHT2NoWHV0b08zRTlYbmR1OWRoZ2ZKOHFjbFVQQTVVVE9jUFNmcC84cGdsZVBNcWFpU3ZrWlVoakZQM1dPZ0EwUzYwMERXcXhidU80NmY3N0trbHdQbE1LR2JlSWVNSE5tV1IwQ3ByZzRpOWhCWCsxeUlZcXBiYXlUdU4rQXFYbTdtSENkNEF6ckg3ZXZoZDR6Skxnc090TkRtUnZieHVtWXVybzd6NTg1M1pGV21SVjZrc1FVUnVPN2xaUkZOK0VtQUVOTHA5aUwxL1RGRVE9PTwvRD48L1JTQUtleVZhbHVlPg==";
        public static String PrivateKey = "MjA0OCE8UlNBS2V5VmFsdWU+PE1vZHVsdXM+OFZucTE0d215NkJTcDRrQm1rZW1ob2ZiTFFrMFZqaHMzdXFNY1hlTVMyUXp5Mzl2TFMzNUY4WCtsMzhmeHVoSjlEL1dyaTVqT2o5SUFtelkweG8zWlZsd3pBeUdvYnAzc2lXUE1UWStRRTBhWXo4NWhqa0JCRU54STdoSDBIWGVkL0ZXTExPemtZbXZRS0s4NHJwYlRXSjZhYndDeWtwaFA0V1dQUkVPVGNZOU1MZjRlZUpBYThSc0NIdFhUNGE3YXpJdisrT3RSSEJkbVF4Y2paYml2cm5scStUQXdrbGV6SmlyeHJHc0NCSFFIRXZRbWJsNGUwV28wZGg1OFQ3VXdYNXlSU1gvalNLRFEwcGZDUXpYcjlvOFVyS2o1S2hwc2FVYkN5NmVHbmpVbWliM3Ira3kwVkZSRWpRL0lVZFdMMzNIRWVRRkd1SFRyY0hTV004QjhRPT08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjxQPi9oSGxPMThXZzVzSFZYZzY3MkxlMlRGcVVHcHJxUWcwQjJ5Nmg1a3pBWkc1L0NrcDE2Rit4eUZtSXlLR1BWazhTSTNlRmdmcy9mckFxSDJtMUduVGdzYWkweFR0Z1dHTEFEbGNRcGhnZzg0NUZ0eWxuZWtUK2NyRUxaSUV6OVU4K1FBMGREeTMzY1c0RlNrYkQxN0UyMjVOQWVuVDFSZ2hQTTNabVliQUQvcz08L1A+PFE+OHk5SmhRRldocmYyeE1yUXVacEJONXMxcGpuWDBFU2Mxbk5jOC9OWUdrc2tlTTRQVTlMM00yTEF3MkJIMXZzVWxZVmdlMGtkNG43OXlheVdqK0tveXNjNUhyNHNKQ3JqeDBXWFdKdTgzR3UzZGpiSUQ1NWZNSFQzQTBvUC9OK0JYM3RWTldyQlJCZUtSZWo0S3ZpMzhVNStXSUM4dEhGUkVoOHZrM2dwMWdNPTwvUT48RFA+WTNZdC95Z2ljRExvbEJVVlBWRy9XOWQwZnphcUh1b1BORGprYk9FVWpyQzExbmxtMy90ZzNpTzlFRlFicVRpZDJPbHczNzhLa0FMeUdRUFhvNkFxSlRBTjNnUjdWZ01SbjJ6VktWenRxNnUzdmJOelFuWlVVNTJGWHpHaW9Kb2gzSGl3RURFVTZjVFZZN05MM0tDRkFFSVY4NEhPWThXbWZWODRxRm1RSnJFPTwvRFA+PERRPmJ2bHVVRzdxNEhHRkFBc1pzd2tzcDhhQmRnakJibjVSSUIvbkZFQkJQVVo3TDFIQmR0dzRDTkFRN1ZlN2tPUmxZVkpMVkJkcXR0aUMwZ3liYksvZm5TSEs5RGVPaHphM1dWRnRubmI3ZVk4Q1dzVG5DUkdabU5CWGhMM0FqQUltMUw3QWhLN2g0VVBveXJ4a3U1OWl1WkM3WVlIbUcyWDJ2QkZWTWJQTlM2Yz08L0RRPjxJbnZlcnNlUT5nQUIrQTcrSTB3OGJJcDJldlhXWWVGeUYxSDlhWG03QjlGbUxCOW5xbjhpTXJFT2F2TjZqTXhUYzRSMUxiM2pRREN3dGpiNkh2TGlBM2RBeEN0M3NTYVVTODZ4WGx4b3pqbmlHRk5DRTgvRXB5MkxnT1c3a0NxbmVxL0U5KzFGODg5bG84cVlvK0tvQTRBOUE4TmRmTWk1M0VBb252elZBU0dsRTlFTHJWU0k9PC9JbnZlcnNlUT48RD5NS0JCSkhSa1RKcGNqTXladWcrOWlWVk4zMkQvWG0rMzM2RzU4aEozL2FpT2x1MXg1TlJpWDF2UTA0MFZSZW4vSEdyUVFmK3Vsa214cEd0eHMvYVFPcWN1aHEzdlloNURoMnp4MEtSeEhsdHFlWngvWm4rTnFPSTlwVWkzajFSd0loSkpubkFHT2NoWHV0b08zRTlYbmR1OWRoZ2ZKOHFjbFVQQTVVVE9jUFNmcC84cGdsZVBNcWFpU3ZrWlVoakZQM1dPZ0EwUzYwMERXcXhidU80NmY3N0trbHdQbE1LR2JlSWVNSE5tV1IwQ3ByZzRpOWhCWCsxeUlZcXBiYXlUdU4rQXFYbTdtSENkNEF6ckg3ZXZoZDR6Skxnc090TkRtUnZieHVtWXVybzd6NTg1M1pGV21SVjZrc1FVUnVPN2xaUkZOK0VtQUVOTHA5aUwxL1RGRVE9PTwvRD48L1JTQUtleVZhbHVlPg==";

        public static String ENCKEY = "b14ca5898a4e4133bbce2ea2315a1916";  //

        #region "LIVE "

        //public static String strCoonectionString = @"Data Source=tcp:pocradb.database.windows.net,1433;Initial Catalog=Pocra_Live;Persist Security Info=False;User ID=GITI_PoCRAUAT;Password=Ashu@751985;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=800; Max Pool Size=1000;Pooling=true;";
        //public static String APIKEY = "GITIAPPS@751985";
        //public static String CUSTCODE = "POCRA";
        //public static String FORMATCODE = "POCRAUPLD";
        //public static String BaseUrl = "https://dbtpocradata.blob.core.windows.net";
        //public static String StrCommanMessgae = "";// "निवडणूक आचारसंहिता लागू झाली असल्या  मुळे प्रकल्पासाठी नोंदणी आणि अर्जाची सुविधा थांबवण्यात आली आहेत. यापूर्वी नोंदणी केलेले लाभार्थी सिस्टममध्ये लॉग इन करू शकतात आणि त्यांच्या अर्ज बाबतची सद्य स्थिती तपासू शकतात.धन्यवाद.";
        //public static Boolean IsRegistrationStart = true;
        //public static string ServerURL = "https://dbtpocradata.blob.core.windows.net";

        //public static string KUA = "https://kuanew25.maharashtra.gov.in:8443/kua25/KUA/rest/kycreq";
        //public static string AUA = "https://auanew25.maharashtra.gov.in:8080/aua25/aua/rest/authreqv2";

        #endregion



        #region "UAT" 
        public static String strCoonectionString = @"Data Source=tcp:pocradb.database.windows.net,1433;Initial Catalog=Pocra_Live_Clone;Persist Security Info=False;User ID=GITI_PoCRAUAT;Password=Ashu@751985;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=800; Max Pool Size=1000;Pooling=true;";
        public static String APIKEY = "GITIAPPS@751985";
        public static String CUSTCODE = "POCRA";
        public static String FORMATCODE = "POCRAUPLD";
        public static String BaseUrl = "https://pocrauat.blob.core.windows.net";
        public static String StrCommanMessgae = "";// " निवडणूक आचारसंहिता लागू झाली असल्या  मुळे प्रकल्पासाठी नोंदणी आणि अर्जाची सुविधा थांबवण्यात आली आहेत. यापूर्वी नोंदणी केलेले लाभार्थी सिस्टममध्ये लॉग इन करू शकतात आणि त्यांच्या अर्ज बाबतची सद्य स्थिती तपासू शकतात.धन्यवाद.";
        public static Boolean IsRegistrationStart = true;
        public static string ServerURL = "https://pocrauat.blob.core.windows.net";


        //From Live
        public static string KUA = "https://kuanew25.maharashtra.gov.in:8443/kua25/KUA/rest/kycreq";
        public static string AUA = "https://auanew25.maharashtra.gov.in:8080/aua25/aua/rest/authreqv2";
        #endregion
    }
}
