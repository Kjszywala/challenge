using Challenge.ConsoleApp.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Net.Http.Json;
using System.Text;

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
						// Validate latitude
						if (!ValidateInputs.ValidateLatitude(args[0]))
						{
							await Console.Out.WriteLineAsync("Invalid latitude value.");
							break;
						}
						if (!ValidateInputs.ValidateLongitude(args[1]))
						{
							await Console.Out.WriteLineAsync("Invalid longtitude value.");
							break;
						}
						if (!ValidateInputs.ValidateMaxDistanceInKilometers(args[2]))
						{
							await Console.Out.WriteLineAsync("Invalid distance value.");
							break;
						}
						await GetPstcodesNearLocation(Double.Parse(args[0]), Double.Parse(args[1]), Double.Parse(args[2]));
						break;
					default:
						await Console.Out.WriteLineAsync("Wrong number of parameters given.");
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
		/// Return postcodes near a specific location (latitude / longitude).
		/// </summary>
		/// <param name="latitude"></param>
		/// <param name="longitude"></param>
		/// <param name="maxDistanceInKilometers"></param>
		/// <returns></returns>
		static async Task GetPstcodesNearLocation(double latitude, double longitude, double maxDistanceInKilometers)
		{
			using (HttpClient httpClient = new HttpClient())
			{
				httpClient.BaseAddress = new Uri(URL);

				// Create a model with the required data
				var requestModel = new
				{
					Latitude = latitude,
					Longitude = longitude,
					MaxDistanceInKilometers = maxDistanceInKilometers
				};

				// Serialize the model to JSON
				string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(requestModel);

				// Create a StringContent with the JSON data
				StringContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

				try
				{
					// Send the POST request
					HttpResponseMessage response = await httpClient.PostAsync(URI + "near", content);

					// Check if the request was successful
					if (response.IsSuccessStatusCode)
					{
						// Read and parse the response content
						string jsonResponse = await response.Content.ReadAsStringAsync();
                        // Handle the jsonResponse (e.g., deserialize it to a list of strings)
                        // var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(jsonResponse);
                        // Do something with the result
                        await Console.Out.WriteLineAsync(FormatJson(jsonResponse));
                    }
					else
					{
						Console.WriteLine($"HTTP Error: {response.StatusCode}");
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Exception: {ex.Message}");
				}
			}
		}

		/// <summary>
		/// Check if the given file is csv format.
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		static bool IsCsvFile(string filePath)
		{
			return !File.Exists(filePath) || !filePath.Contains("csv") ? false : true;
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