using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
//using smarx.WazStorageExtensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NoSQLWorker
{
    class genId
    {
        public static int generate(string table)
        {
            int id;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
    ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("azurepos");
            if (container.CreateIfNotExists())
            {
                container.SetPermissions(
    new BlobContainerPermissions
    {
        PublicAccess = BlobContainerPublicAccessType.Off
    });
            }
            CloudBlockBlob blob = container.GetBlockBlobReference(table);

            if (Exists(blob))
            {
                var leaseId = GetLease(blob, 20);
                string text = blob.DownloadText(null, AccessCondition.GenerateLeaseCondition(leaseId));
                id = Int32.Parse(text) + 1;
                blob.UploadText(id.ToString(), null, AccessCondition.GenerateLeaseCondition(leaseId));
                blob.ReleaseLease(AccessCondition.GenerateLeaseCondition(leaseId));
            }
            else
            {
                try
                {
                    blob.UploadText("");
                    string leaseId = GetLease(blob, 20);
                    id = 1;
                    blob.UploadText(id.ToString(), null, AccessCondition.GenerateLeaseCondition(leaseId));
                    blob.ReleaseLease(AccessCondition.GenerateLeaseCondition(leaseId));
                } catch (StorageException)
                {
                    var leaseId = GetLease(blob, 20);
                    string text = blob.DownloadText(null, AccessCondition.GenerateLeaseCondition(leaseId));
                    id = Int32.Parse(text) + 1;
                    blob.UploadText(id.ToString(), null, AccessCondition.GenerateLeaseCondition(leaseId));
                    blob.ReleaseLease(AccessCondition.GenerateLeaseCondition(leaseId));
                }
            }
            return id;






        }

        private static string GetLease(CloudBlockBlob blob, int p)
        {
            bool gotLease = false;
            string leaseId = "0";
            do
            {
                try
                {
                    leaseId = blob.AcquireLease(new TimeSpan(0, 0, 20), null);
                    gotLease = true;
                }
                catch (StorageException) //e)
                {
                    //if (!e.Message.Contains("(409)"))
                    //{
                    //    throw;
                    //}
                    //else
                   // {
                        System.Threading.Thread.Sleep(20); //TODO: random
                    //}

                }
            } while (gotLease == false);
            return leaseId;
        }
        public static bool Exists(CloudBlockBlob blob)
            {
                try
                {
                    blob.FetchAttributes();
                    return true;
                }
                catch (StorageException e)
                {
                    if (e.Message.Contains("(404)"))
                    {
                        return false;
                    }
                    else
                    {
                        throw;
                    }
                }
           
        }
    }
}
