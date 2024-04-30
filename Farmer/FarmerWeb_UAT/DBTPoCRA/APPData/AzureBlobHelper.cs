using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DBTPoCRA.APPData
{
    public class AzureBlobHelper
    {
       
        public String UploadData(string strDirectoryName, string strFileName, byte[] Imagebits)
        {
            String ret = "";
            try {
                CloudStorageAccount storageaccount = CloudStorageAccount.Parse(System.Configuration.ConfigurationManager.AppSettings["StorageConnectionString"].ToString());
                CloudBlobClient client = storageaccount.CreateCloudBlobClient();
                CloudBlobContainer container = client.GetContainerReference("admintrans");
                container.CreateIfNotExists();
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });

                ret= CreateBlockBlob(container, strDirectoryName, strFileName, Imagebits);
            }
            catch(Exception ex)
            {
                ret = ex.Message;

            }

            return ret;
        }


        private static String CreateBlockBlob(CloudBlobContainer container, string strDirectoryName, string strFileName, byte[] Imagebits)
        {
            String ret = "Error";
            CloudBlobDirectory directory = container.GetDirectoryReference(strDirectoryName);
            CloudBlockBlob blockblob = directory.GetBlockBlobReference(strFileName);
            if (!blockblob.Exists())
            {
                using (Stream stream = new MemoryStream(Imagebits))
                {
                    blockblob.UploadFromStream(stream);
                }
            }
            else
            {
                blockblob.DeleteIfExists();
                //update
                using (Stream stream = new MemoryStream(Imagebits))
                {
                    blockblob.UploadFromStream(stream);
                }
            }

            if (blockblob.Exists())
            {
                ret = "";
            }

            return ret;
        }


        public static String checkFileExistence(string strDirectoryName, string strFileName)
        {
            String ret = "";
            try
            {
                CloudStorageAccount storageaccount = CloudStorageAccount.Parse(System.Configuration.ConfigurationManager.AppSettings["StorageConnectionString"].ToString());
                CloudBlobClient client = storageaccount.CreateCloudBlobClient();
                CloudBlobContainer container = client.GetContainerReference("admintrans");
                container.CreateIfNotExists();
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });

                ret = checkExistence(container, strDirectoryName, strFileName);
            }
            catch (Exception ex)
            {
                ret = ex.Message;

            }

            return ret;
        }
        private static String checkExistence(CloudBlobContainer container, string strDirectoryName, string strFileName)
        {
            String ret = "";
            CloudBlobDirectory directory = container.GetDirectoryReference(strDirectoryName);
            CloudBlockBlob blockblob = directory.GetBlockBlobReference(strFileName);
            if (!blockblob.Exists())
            {
                ret = "Not Found";
            }
            else
            {
                ret = "Found";
            }

          

            return ret;
        }



        //private static async Task CreateBlockBlob(CloudBlobContainer container, string strDirectoryName, string strFileName, byte[] Imagebits)
        //{
        //    CloudBlobDirectory directory = container.GetDirectoryReference(strDirectoryName);
        //    CloudBlockBlob blockblob = directory.GetBlockBlobReference(strFileName);
        //    if (!blockblob.Exists())
        //    {
        //        using (Stream stream = new MemoryStream(Imagebits))
        //        {
        //            await blockblob.UploadFromStreamAsync(stream);
        //        }
        //    }
        //}







        public void ActionResult( HttpPostedFileBase photo)
        {

            try
            {
                if (photo != null && photo.ContentLength > 0)
                {
                    // extract only the fielname
                    var fileName = Path.GetFileName(photo.FileName);
                   // doct.Image = fileName.ToString();

                    StorageCredentials storageCredentials = new StorageCredentials("dbtpocradata", "7ZY9ok0uGgwnepFwe6iEsW+0bHZ3skcRa3ctc7R8PQQaCble8a3o+QrzzKDhdOcjvT2ZFLvaZhlKOdVlss1pAg==");
                    CloudStorageAccount cloudStorageAccount = new CloudStorageAccount(storageCredentials, useHttps: true);
                    CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                    CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("images");


                    string imageName = Guid.NewGuid().ToString() + "-" + Path.GetExtension(photo.FileName);

                    CloudBlockBlob BlockBlob = cloudBlobContainer.GetBlockBlobReference(imageName);

                    BlockBlob.Properties.ContentType = photo.ContentType;
                    BlockBlob.UploadFromStreamAsync(photo.InputStream);
                    string imageFullPath = BlockBlob.Uri.ToString();

                    var memoryStream = new MemoryStream();


                    photo.InputStream.CopyTo(memoryStream);
                    memoryStream.ToArray();



                    memoryStream.Seek(0, SeekOrigin.Begin);
                    using (var fs = photo.InputStream)
                    {
                        BlockBlob.UploadFromStreamAsync(memoryStream);
                    }

                }
            }
            catch (Exception ex)
            {
                Util.LogError(ex);
            }


            
        }
    }
}