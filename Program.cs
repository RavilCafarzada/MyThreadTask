using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    static async Task Main()
    {
        string path = "https://jsonplaceholder.typicode.com/posts";
        var response = await GetInfoAsync(path);

        // Convert to objects
        var myObjects = JsonConvert.DeserializeObject<MyObject[]>(response);

        // Specify the IDs
        var specifiedIds = new List<int> { 1, 10, 100 };

        // Filtered the specified IDs
        var filteredObjects = myObjects.Where(obj => specifiedIds.Contains(obj.Id)).ToList();

        string filePath = "mydata.json";

        File.WriteAllText(filePath, JsonConvert.SerializeObject(filteredObjects, Formatting.Indented));

        Console.WriteLine($"Filtered objects with specified IDs have been written to {filePath}");
    }

    static async Task<string> GetInfoAsync(string path)
    {
        using (HttpClient client = new HttpClient())
        {
            string result = await client.GetStringAsync(path);
            return result;
        }
    }
}

class MyObject
{
    public int UserId { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}
