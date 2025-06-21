namespace CareerHub.Models
{
    public class LanguagesSearchViewModel
    {
        public string Name { get; set; }

        public bool CancelSearch { get; set; }

        public LanguagesSearchViewModel() { }

        public LanguagesSearchViewModel(string name)
        {
            Name = name;

            CancelSearch = false;

            if(!string.IsNullOrEmpty(name))
                CancelSearch = true;
        }
    }
}
