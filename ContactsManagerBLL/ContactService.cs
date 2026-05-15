using System.Collections.Generic;
using ContactsManagerDAL;

namespace ContactsManagerBLL
{
    public class ContactService
    {
        private readonly IContactRepository _repository;

        public ContactService(string connectionString)
        {
            _repository = new ContactRepository(connectionString);
        }

        private ContactViewModel MapToViewModel(Contact contact)
        {
            return new ContactViewModel
            {
                Id = contact.Id,
                CountryID = contact.CountryID,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Phone = contact.Phone,
                Email = contact.Email,
                Address = contact.Address,
                ImagePath = contact.ImagePath,
                BirthDate = contact.BirthDate
            };
        }

        public ContactViewModel GetByID(int id)
        {
            Contact contact = _repository.GetByID(id);
            if (contact == null) return null;
            return MapToViewModel(contact);
        }

        public List<ContactViewModel> GetAll()
        {
            List<Contact> contacts = _repository.GetAll();
            List<ContactViewModel> result = new List<ContactViewModel>();

            foreach (Contact c in contacts)
                result.Add(MapToViewModel(c));

            return result;
        }

        public int Add(ContactViewModel vm)
        {
            Contact contact = new Contact
            {
                CountryID = vm.CountryID,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Phone = vm.Phone,
                Email = vm.Email,
                Address = vm.Address,
                ImagePath = vm.ImagePath,
                BirthDate = vm.BirthDate
            };
            return _repository.Add(contact);
        }

        public bool Update(ContactViewModel vm)
        {
            Contact contact = new Contact
            {
                Id = vm.Id,
                CountryID = vm.CountryID,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Phone = vm.Phone,
                Email = vm.Email,
                Address = vm.Address,
                ImagePath = vm.ImagePath,
                BirthDate = vm.BirthDate
            };
            return _repository.Update(contact);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
