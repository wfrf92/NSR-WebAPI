// IContactRepository.cs
using System.Threading.Tasks;

public interface IContactService
{
    Task<bool> SaveContactForm(ContactForm contactForm);
}
