using System;
using Azure.Storage.Blobs;
using System.IO;

namespace BlobCRUD
{
    class Program
    {
        static string connectionString = "";
        
        
        
        static string containerName = "storagequeueazye2";
        static string blobName = "File11.txt";

        static void createBlob()
        {
            Console.WriteLine("Please create a new container, enter container name");
            containerName = Console.ReadLine();

            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.CreateBlobContainer(containerName);

            var files = Directory.GetFiles(@"C:\Temp\");
            foreach (var file in files)
            {

                using (MemoryStream stream = new MemoryStream(File.ReadAllBytes(file)))
                {
                    containerClient.UploadBlob(Path.GetFileName(file), stream);
                }
                Console.WriteLine(file, " Uploaded!");

            }
        }

        static void deleteAllBlobFiles()
        {
            BlobContainerClient containerClient1 = new BlobContainerClient(connectionString, containerName);
            var allBlobs = containerClient1.GetBlobs();
            foreach (var blob in allBlobs)
            {
                var blobClients = containerClient1.GetBlobClient(blob.Name);
                blobClients.Delete();
                Console.WriteLine(blob.Name + " is deleted");
            }
        }

        static void deleteBlob()
        {
            BlobClient blobClient = new BlobClient(connectionString, containerName, blobName);
            blobClient.Delete();
            Console.WriteLine("Blob Deleted!!");
        }

        static void getBlob()
        {
            BlobContainerClient containerClient2 = new BlobContainerClient(connectionString, containerName);
            var allBlobss = containerClient2.GetBlobs();
        }

        static void readAllFiles()
        {
            BlobContainerClient containerClient2 = new BlobContainerClient(connectionString, containerName);
            var allBlobss = containerClient2.GetBlobs();

         
            foreach (var blobs in allBlobss)
            {

                Console.WriteLine("Blob Name is " + blobs.Name);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Connecting to the blob service");

            Console.WriteLine("Please select what would you like to do with the service?");
            Console.WriteLine("Press 1 to upload all files from the temp folder");
            Console.WriteLine("Press 2 to Delete");
            Console.WriteLine("Press 3 to Delete all files ");
            Console.WriteLine("Press 4 to Read all files ");

            int number = Convert.ToInt32(Console.ReadLine());

            

         


            //-----------------------------------------------------------------



          

            switch (number)
            {
                case 1:
                    createBlob();
                    break;
                case 2:
                    deleteBlob();
                    break;
                case 3:
                    deleteAllBlobFiles();
                    break;
                case 4:
                    readAllFiles();
                    break;

                default:
                    Console.WriteLine("Not a valid selection");
                    break;
            }

           

           

            Console.ReadLine();
        }
    }
}
