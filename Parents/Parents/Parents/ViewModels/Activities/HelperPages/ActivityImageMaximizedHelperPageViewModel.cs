using GalaSoft.MvvmLight.Command;
using Parents.Helpers;
using Parents.Models;
using Parents.Services;
using Parents.Views.Activities.HelpersPages;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Parents.ViewModels.Activities.HelperPages
{
    public class ActivityImageMaximizedHelperPageViewModel : INotifyPropertyChanged
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
        string _activityImage;
        Activity activity;
        string _imageTitle;
        ImageSource _imageSource;
        MediaFile file;
        bool _isRunning;
        bool _isEnabled;
        bool _saveButtonVisibility;
        #endregion

        #region Properties
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

        public bool SaveButtonVisibility
        {
            get
            {
                return _saveButtonVisibility;
            }
            set
            {
                if (_saveButtonVisibility != value)
                {
                    _saveButtonVisibility = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(SaveButtonVisibility)));
                }
            }
        }

        public string ImageTitle
        {
            get
            {
                return _imageTitle;
            }
            set
            {
                if (_imageTitle != value)
                {
                    _imageTitle = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ImageTitle)));
                }
            }
        }

        public string ActivityImage
        {
            get
            {
                return _activityImage;
            }
            set
            {
                if (_activityImage != value)
                {
                    _activityImage = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActivityImage)));
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
        #endregion

        #region Constructors

        public ActivityImageMaximizedHelperPageViewModel(Activity _activity)
        {
            this.activity = _activity;
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();
            SaveButtonVisibility = true;

            ImageSource = _activity.ActivityImageFullPath;
            ImageTitle = String.Format("{0}", _activity.ActivityDescription.ToUpper());

            MessagingCenter.Subscribe<EditActivityViewModel, string>(this, "activityImageForMaximization", (s, a) =>
            {
                ImageSource = a.ToString();
            });
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
                    file = await CrossMedia.Current.TakePhotoAsync(
                        new StoreCameraMediaOptions
                        {
                            Directory = "Parents",
                            Name = "test.jpg",
                            PhotoSize = PhotoSize.Medium,
                        }
                    );
                }
                else
                {
                    file = await CrossMedia.Current.PickPhotoAsync();
                }
            }
            else
            {
                file = await CrossMedia.Current.PickPhotoAsync();
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

        public ICommand DeleteImageCommand
        {
            get
            {
                return new RelayCommand(DeleteImage);
            }
        }

        async void DeleteImage()
        {
            ActivityImage = "no_image";
        }

        public ICommand SaveNewImageCommand
        {
            get
            {
                return new RelayCommand(SaveNewImage);
            }
        }

        async void SaveNewImage()
        {
            //ActivityRepeat = CheckSelectedDays(_selectedDays);

            IsRunning = true;
            IsEnabled = false;

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

            byte[] imageArray = null;

            if (file != null)
            {
                imageArray = FilesHelper.ReadFully(file.GetStream());
                file.Dispose();
            }

            string childIdentityCard = Application.Current.Properties["childrenIdentityCard"] as string;
            int childId = Convert.ToInt32(Application.Current.Properties["childrenId"]);

            activity.ImageArray = imageArray;

            var mainViewModel = MainViewModel.GetInstance();

            //se existir ligação à internet guarda token na variavel response
            var response = await apiService.Put(
                "http://api.parents.outstandservices.pt",
                "/api",
                "/Activities",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                activity);


            //se a resposta (Token) for nulo ou estiver vazia, significa que o email ou a pass estão errados
            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            var activityViewModel = ActivitiesViewModel.GetInstance();
            activityViewModel.UpdateActivity(activity);

            MessagingCenter.Send(this, "newActivityImage", activity.ActivityImageFullPath);

            //activityViewModel.ReloadActivities();

            IsRunning = false;
            IsEnabled = true;

            await navigationService.BackOnMaster();
        }
        #endregion
    }
}
