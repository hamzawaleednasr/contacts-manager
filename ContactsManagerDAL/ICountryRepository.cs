using System.Data;

namespace ContactsManagerDAL
{
    public interface ICountryRepository
    {
        Country GetByID(int id);
        DataTable GetAll();
        int Add(Country contact);
        bool Update(Country contact);
        bool Delete(int id);
        bool IsThere(int id);
    }
}
