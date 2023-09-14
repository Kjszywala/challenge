using Challenge.ConsoleApp.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Challenge.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Specify the path to the CSV file you want to read.
            //string csvFilePath = args[0];
            string csvFilePath = "C:\\Users\\kamil.szywala\\Desktop\\Challenge\\Challenge.Api\\Challenge.ConsoleApp\\postcodes.csv";

            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<PostCode>(); // Replace MyClass with your own class that matches your CSV structure.

                foreach (var record in records)
                {
                    Console.WriteLine($"{record.Postcode}, {record.Country}, {record.CountryString}"); // Replace Field1, Field2, Field3 with your CSV field names.
                }
            }
        }
    }
}