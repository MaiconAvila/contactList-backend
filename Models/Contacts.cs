namespace ContactList.Models
{
    public class Contacts
    {
        public int Id { get; set; }
        public List<Phone> Phone { get; set; }
        public List<Email> Email { get; set; }
        public List<Whatsapp> Whatsapp { get; set; }
    }
}
