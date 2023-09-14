using Challenge.ConsoleApp.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Net.Http.Json;

namespace Challenge.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Specify the path to the CSV file.
            //string csvFilePath = args[0];
            string csvFilePath = "C:\\Users\\kamil.szywala\\Desktop\\Challenge\\Challenge.Api\\Challenge.ConsoleApp\\postcodes.csv";
            string URI = "/api/PostCodes/";
            string URL = "https://localhost:7008/swagger/v1/swagger.json";

            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<PostCode>();

                foreach (var record in records)
                {
                    var postCode = new PostCode()
                    {
                        Country = record.Country,
                        CountryString = record.CountryString,
                        Eastings = record.Eastings,
                        Latitude = record.Latitude,
                        Longitude = record.Longitude,
                        Northings = record.Northings,
                        Postcode = record.Postcode,
                        Region = record.Region,
                        Town = record.Town,
                    };
                    using (var httpClient = new HttpClient())
                    {
                        // Set the base address of your API
                        httpClient.BaseAddress = new Uri(URL);

                        // Send the GET request
                        var response = await httpClient.PostAsJsonAsync(URI, postCode);

                        // Check the response status and handle it accordingly
                        if (response.IsSuccessStatusCode)
                        {
                            // Request was successful
                            var responseBody = await response.Content.ReadAsStringAsync();
                            Console.WriteLine("Response: " + responseBody);
                        }
                        else
                        {
                            // Request failed
                            Console.WriteLine("Error: " + response.StatusCode);
                        }
                    }

                    Console.WriteLine($"{record.Postcode}, {record.Country}, {record.CountryString}");
                }
            }
        }
    }
}