using Outsurance.FileParser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsurance.FileParser
{
    public static class CsvFileParser
    {
        public static IEnumerable<FileLine> ParseCsvFile(string fileName, bool skipFirstLine = true)
        {
            var fileLines = new List<FileLine>();
            var NoOfRowsToSkip = skipFirstLine ? 1 : 0;
            var lineNumber = 0;

            var fileData = File.ReadAllLines(fileName).Skip(NoOfRowsToSkip);

            foreach(var item in fileData)
            {
                lineNumber++;
                var values = item.Split(',');
                var fileLine = new FileLine()
                {
                   LineNumber = lineNumber,
                   FirstName = values[0],
                   LastName = values[1],
                   Address = values[2],
                   PhoneNumber = values[3]
                };

                fileLines.Add(fileLine);
            }

            return fileLines;
        }
        public static IEnumerable<FrequencyModel> GenerateFirstAndLastNameFrequency(string fileName)
        {
            var frequencies = new List<FrequencyModel>();
            var fileData = ParseCsvFile(fileName);

            var oddw = new Dictionary<string, int>();

            foreach(var item in fileData)
            {
                if(!frequencies.Any(x=>x.Key.ToLower().Equals(item.FirstName.ToLower())))
                {
                    var freq = new FrequencyModel()
                    {
                      Frequency = 1,
                      Key = item.FirstName
                    };

                    frequencies.Add(freq);
                }
                else
                {
                    var freq = frequencies.FirstOrDefault(x => x.Key.ToLower() == item.FirstName.ToLower());
                    freq.Frequency++;
                }

                if (!frequencies.Any(x => x.Key.ToLower().Equals(item.LastName.ToLower())))
                {
                    var freq = new FrequencyModel()
                    {
                        Frequency = 1,
                        Key = item.LastName
                    };

                    frequencies.Add(freq);
                }
                else
                {
                    var freq = frequencies.FirstOrDefault(x => x.Key.ToLower() == item.LastName.ToLower());
                    freq.Frequency++;
                }
            }

            frequencies = frequencies.OrderByDescending(x => x.Frequency).ThenBy(x => x.Key).ToList();

            return frequencies;
        }
        public static IEnumerable<AddressModel> GetSortedAddresses(string fileName)
        {
            var addresses = new List<AddressModel>();
            var fileData = ParseCsvFile(fileName);
            addresses = (from item in fileData
                        select new AddressModel
                        {
                            FullAddress = item.Address,
                            HouseNumber = GetHouseNumberFromAddress(item.Address),
                            StreetName = GetStreetNameFromAddress(item.Address)
                        }).ToList();

            addresses = addresses.OrderBy(x => x.StreetName).ToList();

            return addresses;
        }
        private static string GetHouseNumberFromAddress(string address)
        {
            var houseNumber = string.Empty;
            var intVal = 0;

            var addressValue = address.Substring(0, address.IndexOf(" "));
            if (int.TryParse(addressValue, out intVal))
                houseNumber = intVal.ToString();

            return houseNumber;
        }
        private static string GetStreetNameFromAddress(string address)
        {
            var streetName = string.Empty;
            streetName = address.Substring(address.IndexOf(" ") + 1);

            return streetName;
        }
    }
}
