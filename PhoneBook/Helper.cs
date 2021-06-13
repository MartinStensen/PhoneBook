using System.Linq;

namespace PhoneBook
{
    public static class Helper
    {
        public static bool IsDigitsOnly(string str)
        {
            return str.All(c => c >= '0' && c <= '9');
        }
    }
}