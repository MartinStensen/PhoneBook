using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace PhoneBook
{
    public static class DeleteEntry
    {
        public static void Delete()
        {
            int id;
            Console.WriteLine("Enter the id of the entry you want to delete: ");
            var input = Console.ReadLine();
            Console.WriteLine();
            while (string.IsNullOrEmpty(input) || !int.TryParse(input, out id))
            {
                Console.WriteLine("Not a valid id, please try again (press q to cancel)");
                input = Console.ReadLine();
                if (input == "q")
                {
                    return;
                }
            }

            var success = DeleteSpecificEntry(id);
            if (success)
            {
                Console.WriteLine($@"Entry ({id}) deleted");
                PhonebookLog.LogEvent(new Log() { Action = Log.Delete, Time = DateTime.Now, User = input });
            }
        }

        public static bool DeleteSpecificEntry(int id)
        {
            if (IdLookup(id))
            {
                var existingEntries = FileHandler.GetEntries();

                FileHandler.WriteEntries(existingEntries.Where(e => e.Id != id).ToList());
                return true;
            }

            else
            {
                Console.WriteLine("The phonebook does not contain an entry with the given id");
                return false;
            }
        }

        public static bool IdLookup(int id)
        {
            var entries = new List<Person>();

            using (var streamReader = new StreamReader(FileHandler.CsvPath))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<PersonClassMap>();
                    entries = csvReader.GetRecords<Person>().ToList();
                }
            }

            var ids = entries.Select(e => e.Id);

            if (ids.Contains(id))
            {
                return true;
            }

            return false;
        }
    }
}
