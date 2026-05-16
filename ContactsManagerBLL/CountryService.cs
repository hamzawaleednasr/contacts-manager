using ContactsManagerDAL;
using System.Data;

namespace ContactsManagerBLL
{
    public class CountryService
    {
        private readonly ICountryRepository _repository;

        public CountryService(string connectionString)
        {
            _repository = new CountryRepository(connectionString);
        }

        private CountryViewModel MapToViewModel(Country country)
        {
            return new CountryViewModel
            {
                Id = country.Id,
                CountryName = country.CountryName
            };
        }

        public CountryViewModel GetByID(int id)
        {
            Country country = _repository.GetByID(id);
            if (country == null) return null;
            return MapToViewModel(country);
        }

        public DataTable GetAll()
        {
            DataTable country = _repository.GetAll();

            return country;
        }

        public int Add(CountryViewModel vm)
        {
            Country country = new Country
            {
                CountryName = vm.CountryName
            };
            return _repository.Add(country);
        }

        public bool Update(CountryViewModel vm)
        {
            Country country = new Country
            {
                Id = vm.Id,
                CountryName = vm.CountryName
            };
            return _repository.Update(country);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public bool IsThere(int id)
        {
            return _repository.IsThere(id);
        }
    }
}
