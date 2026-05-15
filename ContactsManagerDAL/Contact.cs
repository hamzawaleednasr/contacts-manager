using System;

namespace ContactsManagerDAL
{
    public class Contact
    {
        public int Id { get; set; }
        public int CountryID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }
        public DateTime BirthDate { get; set; }

        public Contact() { }

        private Contact(int id, int countryID, string firstName, string lastName, string phone, string email, string address, string imagePath, DateTime birthDate)
        {
            this.Id = id;
            this.CountryID = countryID;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Phone = phone;
            this.Email = email;
            this.Address = address;
            this.ImagePath = imagePath;
            this.BirthDate = birthDate;
        }

    }
}
