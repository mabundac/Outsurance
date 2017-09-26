using Outsurance.FileParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Outsurance.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = string.Format("{0}", "Outsurance - data.csv");
            var filePath = string.Format("{0}{1}", @"C:\", fileName);

            Console.WriteLine("Started at {0} ", DateTime.Now);
            Console.WriteLine("Reading a file in directory {0}", filePath);

            WriteFirstAndLastNameFrequenciesOutput(filePath);
            WriteAddressesOutput(filePath);

            Console.WriteLine(string.Empty);
            Console.WriteLine("Finished at {0} ", DateTime.Now);
            Console.ReadKey();
        }
        static void WriteFirstAndLastNameFrequenciesOutput(string filePath)
        {
            var frequencies = CsvFileParser.GenerateFirstAndLastNameFrequency(filePath);
            Console.WriteLine(string.Empty);
            var header = "Word \t Frequency";
            Console.WriteLine(header);

            var csv = new StringBuilder();

            Console.WriteLine("-----------------------------------");
            foreach (var item in frequencies)
            {
                var line = string.Format("{0} \t {1}", item.Key, item.Frequency.ToString());

                var csvLine = string.Format("{0},{1}", item.Key, item.Frequency.ToString());

                csv.Append(csvLine);
                csv.Append("\n");
                Console.WriteLine(line);
            }

            var fileName = string.Format("{0}", "Outsurance - data.csv");
            var outputFfileName = string.Format("{0}", "Outsurance - Frequencies.csv");
            var outputFfilePath = filePath.Replace(fileName, outputFfileName);

            File.WriteAllText(outputFfilePath, csv.ToString());

            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Format("Frequencies output written in directory: {0}", outputFfilePath));
        }
        static void WriteAddressesOutput(string filePath)
        {
            var addresses = CsvFileParser.GetSortedAddresses(filePath);
            Console.WriteLine(string.Empty);
            var csv = new StringBuilder();

            var header = "Address";
            Console.WriteLine(header);
            Console.WriteLine("-----------------------------------");
            foreach (var item in addresses)
            {

                var csvLine = string.Format("{0}", item.FullAddress);
                csv.Append(csvLine);
                csv.Append("\n");


                Console.WriteLine(item.FullAddress);
            }

            var fileName = string.Format("{0}", "Outsurance - data.csv");
            var outputFfileName = string.Format("{0}", "Outsurance - Addresses.csv");
            var outputFfilePath = filePath.Replace(fileName, outputFfileName);

            File.WriteAllText(outputFfilePath, csv.ToString());

            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Format("Addresses output written in directory: {0}", outputFfilePath));
        }
    }
}
