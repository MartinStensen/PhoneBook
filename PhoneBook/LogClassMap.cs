using CsvHelper.Configuration;

namespace PhoneBook
{
    public class LogClassMap : ClassMap<Log>
    {
        public LogClassMap()
        {
            Map(l => l.Action).Name("event");
            Map(l => l.Time).Name("time");
            Map(l => l.User).Name("user");
        }
    }
}