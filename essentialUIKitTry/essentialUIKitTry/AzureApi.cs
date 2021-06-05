using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using CounterFunctions;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;

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

                    //Console.WriteLine(contents);
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

                    //Console.WriteLine(contents);
                    return locker;
                }
            }
        }




        public static async void SetOccupy(Int32 locker_num, string userKey)
        {
            var locker = new Locker();
            locker.Id = locker_num;
            locker.available = false;
            locker.locked = true;
            locker.user_key = userKey;

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


    }
 }

