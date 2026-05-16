using ContactsManagerBLL;
using System;
using System.Configuration;
using System.Data;

namespace ContactsConsoleUI
{
    internal class Program
    {

        private static void _PrintMainMenu()
        {
            Console.WriteLine("════════════════════════════════════");
            Console.WriteLine("          CONTACTS MANAGER          ");
            Console.WriteLine("════════════════════════════════════");
            Console.WriteLine("  -- Contacts --");
            Console.WriteLine("\t[1] List all contacts.");
            Console.WriteLine("\t[2] Find contact.");
            Console.WriteLine("\t[3] Add contact.");
            Console.WriteLine("\t[4] Update contact.");
            Console.WriteLine("\t[5] Delete contact.");
            Console.WriteLine("\t[6] Is contact there.");
            Console.WriteLine("  -- Countries --");
            Console.WriteLine("\t[7]  List all countries.");
            Console.WriteLine("\t[8]  Find country.");
            Console.WriteLine("\t[9]  Add country.");
            Console.WriteLine("\t[10] Update country.");
            Console.WriteLine("\t[11] Delete country.");
            Console.WriteLine("\t[12] Is country there.");
            Console.WriteLine("════════════════════════════════════");
            Console.WriteLine("\t[0] Exit.");
            Console.WriteLine("════════════════════════════════════");
        }

        private static int _ReadUserInput()
        {
            string choice;
            int input;

            while (true)
            {
                Console.Write("Choose a number: ");
                choice = Console.ReadLine();

                if (int.TryParse(choice, out input) && input >= 0 && input <= 12)
                    break;

                Console.WriteLine("Invalid input, enter a valid number between 0 and 12");
            }

            return input;
        }

        private static void _PrintContact(ContactViewModel c)
        {
            Console.WriteLine("\n┌─────────────────────────────────────┐");
            Console.WriteLine("│           CONTACT DETAILS           │");
            Console.WriteLine("├─────────────────────────────────────┤");
            Console.WriteLine($"│  ID         : {c.Id,-22}│");
            Console.WriteLine($"│  First Name : {c.FirstName,-22}│");
            Console.WriteLine($"│  Last Name  : {c.LastName,-22}│");
            Console.WriteLine($"│  Phone      : {c.Phone,-22}│");
            Console.WriteLine($"│  Email      : {c.Email,-22}│");
            Console.WriteLine($"│  Address    : {c.Address,-22}│");
            Console.WriteLine($"│  Birth Date : {c.BirthDate.ToString("yyyy-MM-dd"),-22}│");
            Console.WriteLine($"│  Country ID : {c.CountryID,-22}│");
            Console.WriteLine("└─────────────────────────────────────┘");
        }

        private static ContactViewModel _ReadContact()
        {
            ContactViewModel vm = new ContactViewModel();

            Console.Write("First Name : "); vm.FirstName = Console.ReadLine();
            Console.Write("Last Name  : "); vm.LastName = Console.ReadLine();
            Console.Write("Phone      : "); vm.Phone = Console.ReadLine();
            Console.Write("Email      : "); vm.Email = Console.ReadLine();
            Console.Write("Address    : "); vm.Address = Console.ReadLine();
            Console.Write("Image Path : "); vm.ImagePath = Console.ReadLine();
            Console.Write("Country ID : "); vm.CountryID = int.Parse(Console.ReadLine());

            Console.Write("Birth Date (yyyy-MM-dd) : ");
            vm.BirthDate = DateTime.Parse(Console.ReadLine());


            return vm;
        }

        private static void _PrintCountry(CountryViewModel c)
        {
            Console.WriteLine("\n┌─────────────────────────────────────┐");
            Console.WriteLine("│           COUNTRY DETAILS           │");
            Console.WriteLine("├─────────────────────────────────────┤");
            Console.WriteLine($"│  ID           : {c.Id,-20}│");
            Console.WriteLine($"│  Country Name : {c.CountryName,-20}│");
            Console.WriteLine("└─────────────────────────────────────┘");
        }

        private static CountryViewModel _ReadCountry()
        {
            CountryViewModel vm = new CountryViewModel();
            Console.Write("Country Name : ");
            vm.CountryName = Console.ReadLine();
            return vm;
        }

        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            ContactService contactService = new ContactService(connectionString);
            CountryService countryService = new CountryService(connectionString);

            while (true)
            {
                Console.Clear();

                _PrintMainMenu();
                int input = _ReadUserInput();

                if (input == 0) break;

                if (input == 1)
                {
                    Console.Clear();

                    DataTable contacts = contactService.GetAll();

                    if (contacts == null || contacts.Rows.Count == 0)
                    {
                        Console.WriteLine("No contacts found.");
                    }
                    else
                    {
                        Console.WriteLine($"\n{"ID",-5} {"First Name",-15} {"Last Name",-15} {"Phone",-15} {"Email",-25}");
                        Console.WriteLine(new string('-', 75));

                        foreach (DataRow c in contacts.Rows)
                            Console.WriteLine($"{c["ContactID"],-5} {c["FirstName"],-15} {c["LastName"],-15} {c["Phone"],-15} {c["Email"],-25}");
                    }

                    Console.Write("\nPress any key to return menu . . .");
                    Console.ReadKey(true);
                }
                else if (input == 2)
                {
                    Console.Clear();

                    int ContactID;

                    Console.Write("Enter the contact id: ");
                    ContactID = Convert.ToInt32(Console.ReadLine());

                    ContactViewModel contact = contactService.GetByID(ContactID);

                    if (contact == null)
                    {
                        Console.WriteLine($"\nContact with {ContactID} is not found, try again.");
                    }
                    else
                    {
                        _PrintContact(contact);
                    }

                    Console.Write("\nPress any key to return menu . . .");
                    Console.ReadKey(true);
                }
                else if (input == 3)
                {
                    Console.Clear();

                    ContactViewModel vm = _ReadContact();
                    int newId = contactService.Add(vm);

                    if (newId == -1)
                    {
                        Console.WriteLine("\nFailed to add the contact, please try again.");
                    }
                    else
                    {
                        Console.WriteLine($"\nContact added successfully, with id: {newId}");
                    }

                    Console.Write("\nPress any key to return menu . . .");
                    Console.ReadKey(true);
                }
                else if (input == 4)
                {
                    Console.Clear();

                    int ContactID;

                    Console.Write("Enter contact id to update: ");
                    ContactID = Convert.ToInt32(Console.ReadLine());

                    ContactViewModel contact = contactService.GetByID(ContactID);

                    if (contact == null)
                    {
                        Console.WriteLine($"\nContact with {ContactID} is not found, try again.");
                    }
                    else
                    {
                        _PrintContact(contact);

                        int update = 0;

                        Console.Write("\nAre you sure update this contact? [Yes:1/No:0]: ");
                        update = Convert.ToInt32(Console.ReadLine());

                        if (update == 1)
                        {
                            int tempID = contact.Id;
                            contact = _ReadContact();
                            contact.Id = tempID;

                            bool isUpdated = contactService.Update(contact);

                            if (isUpdated)
                                Console.WriteLine("\nContact Updated Successfully!");
                            else
                                Console.WriteLine("\nAn error accured, try again.");
                        }
                        else
                        {
                            Console.WriteLine("\nUpdate canceled.");
                        }
                    }

                    Console.WriteLine("\nPress any key to return menu . . .");
                    Console.ReadKey(true);
                }
                else if (input == 5)
                {
                    Console.Clear();

                    int ContactID;

                    Console.Write("Enter contact id to delete: ");
                    ContactID = Convert.ToInt32(Console.ReadLine());

                    ContactViewModel contact = contactService.GetByID(ContactID);

                    if (contact == null)
                    {
                        Console.WriteLine($"\nContact with {ContactID} is not found, try again.");
                    }
                    else
                    {
                        _PrintContact(contact);

                        int delete = 0;

                        Console.Write("\nAre you sure delete this contact? [Yes:1/No:0]: ");
                        delete = Convert.ToInt32(Console.ReadLine());

                        if (delete == 1)
                        {
                            bool isDeleted = contactService.Delete(contact.Id);

                            if (isDeleted)
                                Console.WriteLine("\nContact Deleted Successfully!");
                            else
                                Console.WriteLine("\nAn error accured, try again.");
                        }
                        else
                        {
                            Console.WriteLine("\nDelete canceled.");
                        }
                    }

                    Console.WriteLine("\nPress any key to return menu . . .");
                    Console.ReadKey(true);
                }
                else if (input == 6)
                {
                    Console.Clear();

                    Console.Write("Enter the contact id: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    bool IsFound = contactService.IsThere(id);

                    if (IsFound)
                    {
                        Console.WriteLine("\nYes, The contact is there :-)");
                    }
                    else
                    {
                        Console.WriteLine("\nNo, The contact is not there :-(");
                    }

                    Console.Write("\nPress any key to return menu . . .");
                    Console.ReadKey(true);
                }
                else if (input == 7)
                {
                    Console.Clear();

                    DataTable countries = countryService.GetAll();

                    if (countries == null || countries.Rows.Count == 0)
                    {
                        Console.WriteLine("No countries found.");
                    }
                    else
                    {
                        Console.WriteLine($"\n{"ID",-5} {"Country Name",-30}");
                        Console.WriteLine(new string('-', 35));

                        foreach (DataRow r in countries.Rows)
                            Console.WriteLine($"{r["CountryID"],-5} {r["CountryName"],-30}");
                    }

                    Console.Write("\nPress any key to return menu . . .");
                    Console.ReadKey(true);
                }
                else if (input == 8)
                {
                    Console.Clear();

                    Console.Write("Enter the country id: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    CountryViewModel country = countryService.GetByID(id);

                    if (country == null)
                    {
                        Console.WriteLine($"\nCountry with ID {id} is not found.");
                    }
                    else
                    {
                        _PrintCountry(country);
                    }

                    Console.Write("\nPress any key to return menu . . .");
                    Console.ReadKey(true);
                }
                else if (input == 9)
                {
                    Console.Clear();

                    CountryViewModel vm = _ReadCountry();
                    int newId = countryService.Add(vm);

                    if (newId == -1)
                        Console.WriteLine("\nFailed to add the country, please try again.");
                    else
                        Console.WriteLine($"\nCountry added successfully, with id: {newId}");

                    Console.Write("\nPress any key to return menu . . .");
                    Console.ReadKey(true);
                }
                else if (input == 10)
                {
                    Console.Clear();

                    Console.Write("Enter country id to update: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    CountryViewModel country = countryService.GetByID(id);

                    if (country == null)
                    {
                        Console.WriteLine($"\nCountry with ID {id} is not found.");
                    }
                    else
                    {
                        _PrintCountry(country);

                        Console.Write("\nAre you sure update this country? [Yes:1/No:0]: ");
                        int update = Convert.ToInt32(Console.ReadLine());

                        if (update == 1)
                        {
                            int tempID = country.Id;
                            country = _ReadCountry();
                            country.Id = tempID;

                            bool isUpdated = countryService.Update(country);

                            if (isUpdated)
                                Console.WriteLine("\nCountry Updated Successfully!");
                            else
                                Console.WriteLine("\nAn error occured, try again.");
                        }
                        else
                        {
                            Console.WriteLine("\nUpdate canceled.");
                        }
                    }

                    Console.Write("\nPress any key to return menu . . .");
                    Console.ReadKey(true);
                }
                else if (input == 11)
                {
                    Console.Clear();

                    Console.Write("Enter country id to delete: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    CountryViewModel country = countryService.GetByID(id);

                    if (country == null)
                    {
                        Console.WriteLine($"\nCountry with ID {id} is not found.");
                    }
                    else
                    {
                        _PrintCountry(country);

                        Console.Write("\nAre you sure delete this country? [Yes:1/No:0]: ");
                        int delete = Convert.ToInt32(Console.ReadLine());

                        if (delete == 1)
                        {
                            bool isDeleted = countryService.Delete(country.Id);

                            if (isDeleted)
                                Console.WriteLine("\nCountry Deleted Successfully!");
                            else
                                Console.WriteLine("\nAn error occured, try again.");
                        }
                        else
                        {
                            Console.WriteLine("\nDelete canceled.");
                        }
                    }

                    Console.Write("\nPress any key to return menu . . .");
                    Console.ReadKey(true);
                }
                else if (input == 12)
                {
                    Console.Clear();

                    Console.Write("Enter the country id: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    bool isFound = countryService.IsThere(id);

                    Console.WriteLine(isFound
                        ? "\nYes, The country is there :-)"
                        : "\nNo, The country is not there :-(");

                    Console.Write("\nPress any key to return menu . . .");
                    Console.ReadKey(true);
                }
            }
        }
    }
}
