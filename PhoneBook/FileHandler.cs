using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace PhoneBook
{
    public class FileHandler
    {
        public static readonly string CsvPath = Path.Combine(Environment.CurrentDirectory, $"phonebook.csv");
        public static readonly string CsvBackupPath = Path.Combine(Environment.CurrentDirectory, $"phonebook_backup.csv");
        
        public static void CreatePhonebook()
        {
            using (var streamWriter = new StreamWriter(CsvPath))
            {
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    csvWriter.Context.RegisterClassMap<PersonClassMap>();
                    csvWriter.WriteHeader<Person>();
                    csvWriter.NextRecord();
                }
            }
        }

        public static void WriteEntry(Person person)
        {
            var persons = new List<Person>()
            {
                person
            };
            
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,

            };

            using (var stream = File.Open(CsvPath, FileMode.Append))
            {
                using (var streamWriter = new StreamWriter(stream))
                {
                    using (var csvWriter = new CsvWriter(streamWriter, config))
                    {
                        csvWriter.Context.RegisterClassMap<PersonClassMap>();
                        csvWriter.WriteRecords(persons);
                    }
                }
            }
        }

        public static void WriteEntries(List<Person> persons)
        {
            using (var streamWriter = new StreamWriter(CsvPath))
            {
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    csvWriter.Context.RegisterClassMap<PersonClassMap>();
                    csvWriter.WriteRecords(persons);
                }
            }
        }

        public static List<Person> GetEntries()
        {
            using (var streamReader = new StreamReader(CsvPath))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<PersonClassMap>();
                    var records = csvReader.GetRecords<Person>().ToList();
                    return records;
                }
            }
        }

        public static void SortPhonebook()
        {
            var entries = GetEntries();
            WriteEntries(entries.OrderBy(e => e.Id).ToList());
        }
    }
}