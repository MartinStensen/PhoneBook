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
    public static class ViewEntries
    {
        public static void View()
        {
            var records = FileHandler.GetEntries();

            Console.WriteLine();
            Console.WriteLine("id, firstname, middlename, lastname, phonenumber");
            Console.WriteLine();

            foreach (var record in records)
            {
                Console.Write($"{record.Id} - ");

                if (!string.IsNullOrEmpty(record.FirstName))
                {
                    Console.Write($"{record.FirstName} ");
                }

                if (!string.IsNullOrEmpty(record.MiddleName))
                {
                    Console.Write($"{record.MiddleName} ");
                }

                if (!string.IsNullOrEmpty(record.LastName))
                {
                    Console.Write($"{record.LastName} - ");
                }

                Console.WriteLine(record.PhoneNumber);
            }

            Console.WriteLine();
        }
    }
}
