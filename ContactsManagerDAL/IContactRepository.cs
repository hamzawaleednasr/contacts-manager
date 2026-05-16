using System.Data;

namespace ContactsManagerDAL
{
    public interface IContactRepository
    {
        Contact GetByID(int id);
        DataTable GetAll();
        int Add(Contact contact);
        bool Update(Contact contact);
        bool Delete(int id);
    }
}
