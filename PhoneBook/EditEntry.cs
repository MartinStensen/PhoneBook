using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    public static class EditEntry
    {
        public static void Edit()
        {
            int id;
            Console.WriteLine("Enter the id of the entry you want to edit: ");
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

            var entries = FileHandler.GetEntries();
            
            var personList = entries.Where(e => e.Id == id).ToList();
            if (personList.Count == 0)
            {
                Console.WriteLine("Couldn't find entry with given id");
                return;
            }

            var person = personList[0];

            Console.WriteLine($"firstname ({person.FirstName}) press Enter to skip");
            var newFirstname = Console.ReadLine();
            if (newFirstname.Length > 0)
            {
                Console.WriteLine($@"New first name: {newFirstname}");
                person.FirstName = newFirstname;
            }

            Console.WriteLine($"middlename ({person.MiddleName}) press Enter to skip");
            var newMiddlename = Console.ReadLine();
            if (newMiddlename.Length > 0)
            {
                Console.WriteLine($"New first name: {newMiddlename}");
                person.MiddleName = newMiddlename;
            }

            Console.WriteLine($"surname ({person.LastName}) press Enter to skip");
            var newSurname = Console.ReadLine();
            if (newSurname.Length > 0)
            {
                Console.WriteLine($"New first name: {newSurname}");
                person.LastName = newSurname;
            }

            Console.WriteLine($"phonenumber ({person.PhoneNumber}) press Enter to skip");
            var newPhonenumber = Console.ReadLine();
            if (newPhonenumber.Length > 0)
            {
                Console.WriteLine($"New first name: {newPhonenumber}");
                person.PhoneNumber = newPhonenumber;
            }

            DeleteEntry.DeleteSpecificEntry(person.Id);
            FileHandler.WriteEntry(person);
            PhonebookLog.LogEvent(new Log() { Action = Log.Edit, Time = DateTime.Now, User = person.Id.ToString() });
            FileHandler.SortPhonebook();
        }
    }
}
