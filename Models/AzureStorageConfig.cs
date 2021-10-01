using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Threading.Tasks;

namespace FileUploader.Models
{
    public class AzureStorageConfig
    {
        public string ConnectionString { get; set; }
        public string FileContainerName { get; set; }
    }
}