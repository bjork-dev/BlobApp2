using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace FileUploader.Models
{
    public class BlobStorage : IStorage
    {
        private readonly string connection;

        public BlobStorage(string conn)
        {
            connection = conn;
        }

        public Task Initialize()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connection);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("public");
            return containerClient.CreateIfNotExistsAsync();
        }

        public Task Save(Stream fileStream, string name)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connection);
            
            // Get the container (folder) the file will be saved in
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("public");

            // Get the Blob Client used to interact with (including create) the blob
            BlobClient blobClient = containerClient.GetBlobClient(name);

            // Upload the blob
            return blobClient.UploadAsync(fileStream);
        }

        public async Task<IEnumerable<string>> GetNames()
        {
            List<string> names = new List<string>();

            BlobServiceClient blobServiceClient = new BlobServiceClient(connection);

            // Get the container the blobs are saved in
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("public");

            // This gets the info about the blobs in the container
            AsyncPageable<BlobItem> blobs = containerClient.GetBlobsAsync();

            await foreach (var blob in blobs)
            {
                names.Add(blob.Name);
            }
            return names;
        }

        public Task<Stream> Load(string name)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connection);

            // Get the container the blobs are saved in
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("public");

            // Get a client to operate on the blob so we can read it.
            BlobClient blobClient = containerClient.GetBlobClient(name);
            
            return blobClient.OpenReadAsync();
        }
    }
}