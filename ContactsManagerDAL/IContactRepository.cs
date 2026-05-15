using System;
using System.Collections.Generic;

namespace ContactsManagerDAL
{
    public interface IContactRepository
    {
        Contact GetByID(int id);
        List<Contact> GetAll();
        int Add(Contact contact);
        bool Update(Contact contact);
        bool Delete(int id);
    }
}
