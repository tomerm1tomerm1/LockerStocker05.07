using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using CounterFunctions;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Xamarin.Forms;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;
using System.Reflection;

namespace essentialUIKitTry
{
    class AzureApi
    {
        private static string BaseUri = "https://lockerfunctionapp.azurewebsites.net/api/";
        private static string GetUri = BaseUri + "get-locker/";
        private static string LockerFuncUri = BaseUri + "LockerFunc";

        public static async System.Threading.Tasks.Task<bool> IsAvailableAsync(Int32 locker_num)
        {

            string FuncUri = "https://lockerfunctionapp.azurewebsites.net/api/get-locker/" + locker_num;
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, FuncUri))
            {
                var json = JsonConvert.SerializeObject("");
                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    var response = await client.GetStringAsync(FuncUri);
                    Locker locker = JsonConvert.DeserializeObject<Locker>(response);
                    Console.WriteLine(response);

                    return locker.available;
                }
            }
        }

        public static async System.Threading.Tasks.Task<Locker> GetLocker(Int32 locker_num)
        {
            string FuncUri = "https://lockerfunctionapp.azurewebsites.net/api/get-locker/" + locker_num;
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, FuncUri))
            {
                var json = JsonConvert.SerializeObject("");
                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    var response = await client.GetStringAsync(FuncUri);
                    Locker locker = JsonConvert.DeserializeObject<Locker>(response);
                    Console.WriteLine(response);

                    return locker;
                }
            }
        }
        public static string GetRemainingTime(Locker locker)
        {
            return ("" + (locker.release_time - DateTimeOffset.Now.AddHours(0))).Split('.')[0];
        }




        public static async void SetOccupy(Int32 locker_num, string userKey)
        {
            var locker = new Locker();
            locker.Id = locker_num;
            locker.available = false;
            locker.locked = true;
            locker.user_key = App.m_myUserKey;

            var FuncUri = "https://lockerfunctionapp.azurewebsites.net/api/set-occupy";


            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, FuncUri))
            {
                var json = JsonConvert.SerializeObject(locker);
                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    var response = await client.PostAsync(FuncUri, stringContent);
                    var contents = await response.Content.ReadAsStringAsync();
                }
            }
        }


        public static async void TakeLockerPhoto(string id)
        {
            // Create a BlobServiceClient object which will be used to create a container client
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=storageaccountdefau8140;AccountKey=yCQK9mj77GChmhG5Ghe4cyA5ftIMiWZtm/Jg/6W8jMtBUdmoIhuLDEjllq9JCIK5o6XeNWWcfL/vOHWtNX8WKw==;EndpointSuffix=core.windows.net";
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnectionString);
            //Create a unique name for the container
            string containerName = "lockerphotos";
            // Create the container and return a container client object
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            // Get a reference to a blob
            BlobClient blobClient = containerClient.GetBlobClient("insideALocker.jpeg");
            var externalStorage = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            var lockersPicturesPath = Path.Combine(externalStorage, "Pictures//LockerStocker");
            var imagePath = Path.Combine(lockersPicturesPath, "LockerStocker_" + id + ".jpeg");

            bool exists = System.IO.Directory.Exists(lockersPicturesPath);
            if (!exists)
                System.IO.Directory.CreateDirectory(lockersPicturesPath);

            using (Stream file = File.Create(imagePath))
            {
                blobClient.DownloadTo(file);
            }


            BlobClient blobClientUpload = containerClient.GetBlobClient("LockerStocker_" + id + ".jpeg");
            FileStream upFileStream = File.OpenRead(imagePath);
            await blobClientUpload.UploadAsync(upFileStream, true);
            upFileStream.Close();
        }
    }
}

