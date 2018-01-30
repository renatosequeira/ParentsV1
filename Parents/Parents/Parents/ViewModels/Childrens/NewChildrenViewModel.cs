namespace Parents.ViewModels.Childrens
{
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using System;
    using Services;
    using Parents.Models;
    using Xamarin.Forms;
    using Plugin.Media.Abstractions;
    using Plugin.Media;
    using Parents.Helpers;
    using Parents.Resources;

    public class NewChildrenViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        DialogService dialogService;
        ApiService apiService;
        NavigationService navigationService;
        #endregion

        #region Attributes
        string _childrenFirstName;
        string _childrenLastName;
        string _childrenIdentityCard;
        DateTime _childrenBirthDate;
        string _childrenSex;

        bool _isRunning;
        bool _isEnabled;

        ImageSource _imageSource;
        MediaFile file;
        #endregion

        #region Properties
        public bool IsMale
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsMale)));
                }
            }
        }

        public string ChildrenFirstName
        {
            get
            {
                return _childrenFirstName;
            }
            set
            {
                if (_childrenFirstName != value)
                {
                    _childrenFirstName = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ChildrenFirstName)));
                }
            }
        }

        public string ChildrenLastName
        {
            get
            {
                return _childrenLastName;
            }
            set
            {
                if (_childrenLastName != value)
                {
                    _childrenLastName = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ChildrenLastName)));
                }
            }
        }

        public string ChildrenIdentityCard
        {
            get
            {
                return _childrenIdentityCard;
            }
            set
            {
                if (_childrenIdentityCard != value)
                {
                    _childrenIdentityCard = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ChildrenIdentityCard)));
                }
            }
        }

        public DateTime ChildrenBirthDate
        {
            get
            {
                return _childrenBirthDate;
            }
            set
            {
                if (_childrenBirthDate != value)
                {
                    _childrenBirthDate = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ChildrenBirthDate)));
                }
            }
        }

        public string ChildrenSex
        {
            get
            {
                return _childrenSex;
            }
            set
            {
                if (_childrenSex != value)
                {
                    _childrenSex = value;

                    var t = AppResources.Male;
                    

                    if(value == "Male" || value == "Masculino")
                    {
                        IsMale = true;
                    }
                    else
                    {
                        IsMale = false;
                    }

                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ChildrenSex)));
                }
            }
        }

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }

        public ImageSource ImageSource
        {
            set
            {
                if (_imageSource != value)
                {
                    _imageSource = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ImageSource)));
                }
            }
            get
            {
                return _imageSource;
            }
        }

        public string Image { get; set; }
        #endregion

        #region Constructors
        public NewChildrenViewModel()
        {
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();

            IsEnabled = true; //bool are disabled by default. This will enable buttons

            ImageSource = "no_image";

        }
        #endregion

        #region Commands
        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }

        async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsCameraAvailable &&
                CrossMedia.Current.IsTakePhotoSupported)
            {
                var source = await dialogService.ShowImageOptions();

                if (source == "Cancel")
                {
                    file = null;
                    return;
                }

                if (source == "From Camera")
                {
                    DateTime data = DateTime.Now;
                    TimeSpan hora = data.TimeOfDay;

                    file = await CrossMedia.Current.TakePhotoAsync(
                        new StoreCameraMediaOptions
                        {
                            Directory = "Parents",
                            Name = String.Format("Parents_{0:dd/MM/yyyy}_{1}.jpg", data, data.TimeOfDay),
                            PhotoSize = PhotoSize.Small,
                            SaveToAlbum = true
                        }
                    );
                }
                else
                {
                    file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                    });
                }
            }
            else
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
            }

            if (file != null)
            {
                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
        }

        public ICommand DeleteChildrenFirstNameCommand
        {
            get
            {
                return new RelayCommand(DeleteChildrenFirstName);
            }
        }

        void DeleteChildrenFirstName()
        {
            ChildrenFirstName = null;
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        async void Save()
        {
            if (string.IsNullOrEmpty(ChildrenFirstName))
            {
                await dialogService.ShowMessage("Error", "Please insert Children First Name");
                return;
            }

            if (string.IsNullOrEmpty(ChildrenLastName))
            {
                await dialogService.ShowMessage("Error", "Please insert Children Last Name");
                return;
            }

            if (string.IsNullOrEmpty(ChildrenIdentityCard))
            {
                await dialogService.ShowMessage("Error", "Please insert Children ID card number");
                return;
            }

            if (string.IsNullOrEmpty(ChildrenSex))
            {
                await dialogService.ShowMessage("Error", "Please insert Children Gender");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            byte[] imageArray = null;

            try
            {
                if (file != null)
                {
                    imageArray = FilesHelper.ReadFully(file.GetStream());
                    file.Dispose();
                }
            }
            catch (Exception ex)
            {
                //await dialogService.ShowMessage("Images", "Only first event will have associated image. Please add images for recurreng events, manually.");
            }

            //verificar se existe ligação à internet
            var connection = await apiService.CheckConnection();

            //se não houver ligação à internet, popup com erro e sai do método
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);

                IsRunning = false;
                IsEnabled = true;
                return;
            }

            var children = new Children
            {
                ChildrenFirstName = ChildrenFirstName,
                ChildrenLastName= ChildrenLastName,
                ChildrenIdentityCard = ChildrenIdentityCard,
                ChildrenBirthDate = ChildrenBirthDate,
                ChildrenSex = ChildrenSex,
                ChildrenFamilyDoctor= null,
                ChildrenEmail= null,
                ChildrenMobile = null,
                ChildrenAddress= null,
                CurrentSchool= null,
                SchoolContact= null,
                FirstParentId= null,
                SecondParentId= null,
                BloodInformationDescription= null,
                ImageArray= imageArray,
                IsMale = IsMale
            };

            var mainViewModel = MainViewModel.GetInstance();

            var urlAPI = Application.Current.Resources["URLAPI"].ToString();

            //se existir ligação à internet guarda token na variavel response
            var response = await apiService.Post(
                urlAPI,
                "/api",
                "/Childrens",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                children);

            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            children = (Children)response.Result;
            var childrensViewModel = ChildrensViewModel.GetInstance();
            childrensViewModel.AddChildren(children);

            await navigationService.BackOnMaster();

            IsRunning = false;
            IsEnabled = true;
        }
        #endregion
    }
}
