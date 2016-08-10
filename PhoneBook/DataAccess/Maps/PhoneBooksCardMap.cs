using FluentNHibernate.Mapping;

namespace PhoneBook.DataAccess
{
    public class PhoneBooksCardMap : ClassMap<PhoneBooksCard>
    {
        public PhoneBooksCardMap()
        {
            Table("phonebook");

            Id(x => x.Id)
                .Column("Id")
                .GeneratedBy.Identity();

            Map(x => x.Phone)
                .Column("Phone")
                .Not.Nullable();

            Map(x => x.Name)
                .Column("Name")
                .Not.Nullable();
        }
    }
}