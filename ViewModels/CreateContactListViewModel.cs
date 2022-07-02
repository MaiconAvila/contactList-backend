using System.ComponentModel.DataAnnotations;
namespace ContactList.ViewModels
{
    public class CreateContactListViewModel
    {
        public string Name { get; set; }
        public List<Contacts> Contacts { get; set; }
    }

    public class Contacts
    {
        public List<Phone> Phone { get; set; }
        public List<Email> Email { get; set; }
        public List<Whatsapp> Whatsapp { get; set; }
    }

    public class Phone
    {
        public string? PhoneNumber { get; set; }
    }

    public class Email
    {
        public string? EmailText { get; set; }
    }

    public class Whatsapp
    {
        public string? WhatsappText { get; set; }
    }
}
