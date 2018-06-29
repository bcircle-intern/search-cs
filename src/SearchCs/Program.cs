using System;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SearchCs {

    class Program {
        static string ToJsonString(Request request) {
            var serializer = new DataContractJsonSerializer(typeof(Request));
            using (var stream = new MemoryStream()) {
                serializer.WriteObject(stream, request);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        static string[] ToArray(string jsonString) {
            var serializer = new DataContractJsonSerializer(typeof(string[]));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString))) {
                var obj = serializer.ReadObject(stream) as string[];
                return obj;
            }
        }

        static void ShowOutput(string[] data) {
            foreach (var item in data) {
                Console.WriteLine(item);
            }
        }

        static async Task<string[]> SendRequest(Request request) {
            var url = "http://localhost:5000/api/search/searchFile";
            using (var client = new HttpClient()) {
                var requestJson = ToJsonString(request);
                var response = await client.PostAsync(url, new StringContent(requestJson, Encoding.UTF8, "application/json"));
                var returnJson = await response.Content.ReadAsStringAsync();
                var returnObject = ToArray(returnJson);
                return returnObject;
            }
        }

        // START HERE
        static async Task Main(string[] args) {
            var request = new Request {
                Path = "/Users/wk/Intern/SearchCs",
                Pattern = "*.json"
            };

            var result = await SendRequest(request);
            ShowOutput(result);
        }
    }
}
