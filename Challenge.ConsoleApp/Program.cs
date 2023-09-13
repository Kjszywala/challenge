using Microsoft.VisualBasic.FileIO;

namespace Challenge.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Specify the path to the CSV file you want to read.
            //string csvFilePath = args[0];
            string csvFilePath = "C:\\Users\\kamil.szywala\\Desktop\\Challenge\\Challenge.Api\\Challenge.ConsoleApp\\postcodes.csv";

            // Create a TextFieldParser instance to read the CSV file.
            using (TextFieldParser parser = new TextFieldParser(csvFilePath))
            {
                // Set the field type to delimited, indicating that fields are separated by a delimiter (comma).
                parser.TextFieldType = FieldType.Delimited;

                // Set the delimiter used in the CSV file (comma in this case).
                parser.SetDelimiters(",");

                // Continue reading until the end of the CSV file is reached.
                while (!parser.EndOfData)
                {
                    // Read a line from the CSV file and split it into fields based on the delimiter.
                    string[] fields = parser.ReadFields();

                    // Iterate through the fields in the current row and print them.
                    foreach (string field in fields)
                    {
                        Console.Write(field + " | ");
                    }

                    // Move to the next line in the CSV file.
                    Console.WriteLine();
                }
            }
        }
    }
}