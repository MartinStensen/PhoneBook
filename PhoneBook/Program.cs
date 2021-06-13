using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using CsvHelper;
using CsvHelper.Configuration.Attributes;



namespace PhoneBook
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (!File.Exists(FileHandler.CsvPath))
                {
                    Console.WriteLine("Creating new phonebook");
                    FileHandler.CreatePhonebook();
                }

                if (!File.Exists(PhonebookLog.LogPath))
                {
                    Console.WriteLine("Creating new logfile");
                    PhonebookLog.CreateLogFile();
                }

                Console.WriteLine("Phonebook");

                File.Copy(FileHandler.CsvPath, FileHandler.CsvBackupPath, true);

                while (true)
                {
                    Console.WriteLine("\n- type v to view phonebook, type q to quit, type h for a list of commands");
                    Console.WriteLine();
                    var option = Console.ReadLine();
                    switch (option)
                    {
                        case "h":
                            Console.WriteLine("commands:");
                            Console.WriteLine("v - view entries");
                            Console.WriteLine("q - quit");
                            Console.WriteLine("a - add new entry");
                            Console.WriteLine("d - delete entry");
                            Console.WriteLine("e - edit entry");
                            Console.WriteLine("l - view log");
                            break;
                        case "v":
                            ViewEntries.View();
                            break;
                        case "q":
                            return;
                        case "a":
                            AddEntry.Add();
                            break;
                        case "d":
                            DeleteEntry.Delete();
                            break;
                        case "e":
                            EditEntry.Edit();
                            break;
                        case "l":
                            PhonebookLog.ViewLog();
                            break;
                        default:
                            Console.WriteLine("- Unknown command, please try again (h to view list of valid commands)");
                            break;
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Sorry, an unexpected error occured");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
