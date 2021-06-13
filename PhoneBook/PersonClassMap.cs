using CsvHelper.Configuration;

namespace PhoneBook
{
    public class PersonClassMap : ClassMap<Person>
    {
        public PersonClassMap()
        {
            Map(p => p.Id).Name("id");
            Map(p => p.FirstName).Name("first_name");
            Map(p => p.MiddleName).Name("middle_name");
            Map(p => p.LastName).Name("last_name");
            Map(p => p.PhoneNumber).Name("phone_number");
        }
    }
}