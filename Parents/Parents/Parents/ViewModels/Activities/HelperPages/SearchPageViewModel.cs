using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Parents.ViewModels.Activities.HelperPages
{
    public class SearchPageViewModel : BaseViewModel
    {
        public SearchPageViewModel()
        {
            SearchCommand = new Command(async (object obj) =>
            {
                using (var client = new HttpClient())
                {
                    SearchResult = await client.GetStringAsync("https://www.googleapis.com/customsearch/v1?key=AIzaSyD44XPaSG0I-jqOSXCWlQCOJtQ4WiN-c4o&cx=017576662512468239146:omuauf_lfve&q=" + obj);
                }
            });
        }

        public ICommand SearchCommand { get; }
        string searchResult;

        public string SearchResult
        {
            get
            {
                return searchResult;
            }

            set
            {
                searchResult = value;
                OnPropertyChanged();
            }
        }
    }
}
