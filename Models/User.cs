namespace ContactList.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Contacts> Contacts { get; set; }
    }
}
