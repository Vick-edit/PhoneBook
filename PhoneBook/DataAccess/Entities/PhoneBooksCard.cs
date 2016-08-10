namespace PhoneBook.DataAccess
{
    public class PhoneBooksCard
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Phone { get; set; }

        public static string KeyFieldName { get { return "Name"; } }
    }
}