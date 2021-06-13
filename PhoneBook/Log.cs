using System;
using System.Runtime.CompilerServices;

namespace PhoneBook
{
    public class Log
    {
        public string User { get; set; }
        public DateTime Time { get; set; }
        public string Action { get; set; }

        public static readonly string Add = "Added new entry";
        public static readonly string Edit = "Edited entry";
        public static readonly string Delete = "Deleted entry";
    }
}