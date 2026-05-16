namespace ContactsManagerBLL
{
    public class CountryViewModel
    {
        public int Id { get; set; }
        public string CountryName { get; set; }

        public CountryViewModel() { }

        public CountryViewModel(int Id, string CountryName)
        {
            this.Id = Id;
            this.CountryName = CountryName;
        }
    }
}
