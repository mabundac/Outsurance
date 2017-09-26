using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Outsurance.FileParser;
using System.Reflection;

namespace Outsurance.Test
{
    [TestClass]
    public class OutsuranceTest
    {
        private static string fileName = string.Format("{0}", "Outsurance - data.csv");
        private static string filePath = string.Format("{0}{1}", @"C:\", fileName);

        [TestMethod]
        public void Check_if_the_file_exists()
        {
            var fileExists = File.Exists(filePath);

            Assert.IsTrue(fileExists);
            Assert.AreEqual(fileExists, true);
        }

        [TestMethod]
        public void Check_if_the_addresses_has_a_house_number()
        {
            var addresses = CsvFileParser.GetSortedAddresses(filePath);

            var doesAllAdressesHaveHouseNumber = true;

            foreach (var item in addresses)
            {
                if(item.HouseNumber == string.Empty)
                {
                    doesAllAdressesHaveHouseNumber = false;
                }
            }

            Assert.AreEqual(doesAllAdressesHaveHouseNumber, true);
            Assert.IsTrue(doesAllAdressesHaveHouseNumber);
        }

        [TestMethod]
        public void Check_if_the_file_is_not_empty()
        {
            var fileNotEmpty = true;

            if(new FileInfo(filePath).Length == 0)
            {
                fileNotEmpty = false;
            }

            Assert.IsTrue(fileNotEmpty);
            Assert.AreEqual(fileNotEmpty, true);
        }

        [TestMethod]
        public void Check_if_the_line_in_a_file_has_all_fields()
        {
            var rows = CsvFileParser.ParseCsvFile(filePath);

            var doesAllRowsHaveAllFields = true;

            foreach (var item in rows)
            {
                if (item.FirstName == string.Empty || item.LastName == string.Empty || item.Address == string.Empty || item.PhoneNumber == string.Empty)
                {
                    doesAllRowsHaveAllFields = false;
                }
            }

            Assert.AreEqual(doesAllRowsHaveAllFields, true);
            Assert.IsTrue(doesAllRowsHaveAllFields);
        }
    }
}
