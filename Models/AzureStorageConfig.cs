using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace FileUploader.Models
{
    public class AzureStorageConfig
    {
        public string ConnectionString { get; set; }
    }

 
}