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
    public static class AddEntry
    {
        public static void Add()
        {
            Console.WriteLine("Adding new entry: \n");
            Console.WriteLine("Enter first name (Enter to skip)");
            var firstname = Console.ReadLine();
            Console.WriteLine("\nEnter middle name (Enter to skip)");
            var middlename = Console.ReadLine();
            Console.WriteLine("\nEnter surname (Enter to skip)");
            var lastname = Console.ReadLine();
            Console.WriteLine("\nEnter phone number (Enter to skip)");
            var phonenumber = Console.ReadLine();
            
            while (!string.IsNullOrEmpty(phonenumber) && !Helper.IsDigitsOnly(phonenumber))
            {
                Console.WriteLine("Not a valid phonenumber, must contain digits only");
                Console.WriteLine("Please try again: (q to cancel)");
                phonenumber = Console.ReadLine();
                if (phonenumber == "q")
                {
                    return;
                }
;           }

            var names = new List<string>() {firstname, middlename, lastname};

            if (names.All(n => n == "") && !string.IsNullOrEmpty(phonenumber))
            {
                firstname = phonenumber;
            }

            if (names.All(n => n == "") && string.IsNullOrEmpty(phonenumber))
            {
                Console.WriteLine("All fields are empty, no entry added");
                return;
            }

            var id = FindNewId();
            var person = new Person()
            {
                Id = id,
                FirstName = firstname,
                MiddleName = middlename,
                LastName = lastname,
                PhoneNumber = phonenumber
            };

            FileHandler.WriteEntry(person);
            PhonebookLog.LogEvent(new Log() { Action = Log.Add, Time = DateTime.Now, User = person.Id.ToString() });
        }

        public static int FindNewId()
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

            if (entries.Count == 0)
            {
                return 0;
            }

            return entries[entries.Count - 1].Id + 1;
        }
    }
}
