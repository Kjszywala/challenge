using Challenge.ConsoleApp.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Net.Http.Json;

namespace Challenge.ConsoleApp
{
	internal class Program
	{
		public const string URI = "/api/PostCodes/";
		public const string URL = "https://localhost:7008/swagger/v1/swagger.json";
		static async Task Main(string[] args)
        {
            try
			{
				switch (args.Length)
				{
					case 0:
						await Console.Out.WriteLineAsync("Please provide the path to a CSV file.");
						break;
					case 1:
						if (IsCsvFile(args[0])) 
						{
							await MigratePostcodes(args[0]);
							break;
						}
						else
						{
							var postCodes = await GetPostCodesFromPartialString(args[0]);
							Console.WriteLine(FormatJson(postCodes)); 
							break;
						}
					case 3:

						break;
					default:
						await Console.Out.WriteLineAsync("Too many parameters given.");
						break;
				}
			}
			catch (Exception ex)
			{
				await Console.Out.WriteLineAsync(ex.Message);
			}
		}

		/// <summary>
		/// Migrate all post codes from csv file to database.
		/// </summary>
		/// <param name="csvFilePath">Path to the file</param>
		/// <returns></returns>
        static async Task MigratePostcodes(string csvFilePath)
        {
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
							Console.WriteLine("Response: " + response.StatusCode);
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="partialString"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		static async Task<string> GetPostCodesFromPartialString(string partialString)
		{
			try
			{
				var _httpClient = new HttpClient();
				_httpClient.BaseAddress = new Uri(URL);
				var response = await _httpClient.GetAsync(URI + partialString);
				response.EnsureSuccessStatusCode();

				// Deserialize the JSON response into a list of Postcode objects
				var data = await response.Content.ReadAsStringAsync();

				return data;
			}
			catch (Exception ex)
			{
				throw new Exception($"Endpoint: {URI}\nFailed to retrieve data from API. Task<T> GetAsync(int Id)", ex);
			}
		}

		/// <summary>
		/// Check if the given file is csv format.
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		static bool IsCsvFile(string filePath)
		{
			return !File.Exists(filePath) || filePath.Contains("csv") ? false : true;
		}

		/// <summary>
		/// Format to Json.
		/// </summary>
		/// <param name="json"></param>
		/// <returns></returns>
		static string FormatJson(string json)
		{
			try
			{
				var parsedJson = JToken.Parse(json);
				return parsedJson.ToString(Newtonsoft.Json.Formatting.Indented);
			}
			catch
			{
				// If parsing fails, return the original JSON string
				return json;
			}
		}
	}
}