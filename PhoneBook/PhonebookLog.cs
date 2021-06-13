using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using CsvHelper;
using CsvHelper.Configuration;

namespace PhoneBook
{
    public static class PhonebookLog
    {
        public static readonly string LogPath = Path.Combine(Environment.CurrentDirectory, $"phonebook_log.csv");

        public static void CreateLogFile()
        {
            using (var streamWriter = new StreamWriter(LogPath))
            {
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    csvWriter.Context.RegisterClassMap<LogClassMap>();
                    csvWriter.WriteHeader<Log>();
                    csvWriter.NextRecord();
                }
            }
        }

        public static void LogEvent(Log logevent)
        {
            var logs = new List<Log>()
            {
                logevent
            };

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,

            };

            using (var stream = File.Open(LogPath, FileMode.Append))
            {
                using (var streamWriter = new StreamWriter(stream))
                {
                    using (var csvWriter = new CsvWriter(streamWriter, config))
                    {
                        csvWriter.Context.RegisterClassMap<LogClassMap>();
                        csvWriter.WriteRecords(logs);
                    }
                }
            }
        }

        public static void ViewLog()
        {
            var records = new List<Log>();
            using (var streamReader = new StreamReader(LogPath))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<LogClassMap>();
                    records = csvReader.GetRecords<Log>().ToList();
                }
            }

            Console.WriteLine();

            foreach (var log in records)
            {
                Console.WriteLine($"{log.Action} ({log.User}) - {log.Time}");
            }
        }
    }
}