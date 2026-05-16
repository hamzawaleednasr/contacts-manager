using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManagerDAL
{
    public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }

        public Country() { }

        public Country(int Id, string CountryName)
        {
            this.Id = Id;
            this.CountryName = CountryName;
        }
    }
}
