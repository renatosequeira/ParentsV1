namespace Parents.ViewModels
{
    using Parents.Models;
    using System.Collections.ObjectModel;
    using Services;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using System.Collections;
    using Refractored.FabControl;

    public class ChildrensViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Attributes
        ObservableCollection<Children> _childrens;
        #endregion

        #region Properties
        public ObservableCollection<Children> Childrens {
            get
            {
                return _childrens;
            }
            set
            {
                if(_childrens != value)
                {
                    _childrens = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Childrens)));
                }
            }
        }
        #endregion

        #region Constructors
        public ChildrensViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            LoadChildrens();
        }
        #endregion

       

        #region Methods
        async void LoadChildrens()
        {
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetList<Children>(
                "http://api.parents.outstandservices.pt",
                "/api",
                "/Childrens",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var childrens = (List<Children>)response.Result;

            Childrens = new ObservableCollection<Children>(childrens.OrderBy(c => c.ChildrenFirstName));
        }
        #endregion

       
    }
}
