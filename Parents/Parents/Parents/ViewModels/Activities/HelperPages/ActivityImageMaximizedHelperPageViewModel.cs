using GalaSoft.MvvmLight.Command;
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

        bool _saveButtonVisibility;
        #endregion

        #region Properties
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
        #endregion

        #region Constructors

        public ActivityImageMaximizedHelperPageViewModel(Activity _activity)
        {
            this.activity = _activity;
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();
            SaveButtonVisibility = false;

            ActivityImage = _activity.ActivityImageFullPath;
            ImageTitle = String.Format("{0}", _activity.ActivityDescription.ToUpper());

            MessagingCenter.Subscribe<ActivityRepeatHelperPageView, string>(this, "activityImageForMaximization", (s, a) =>
            {
                ActivityImage = a.ToString();
            });
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
                    SaveButtonVisibility = true;
                }
            }
            get
            {
                return _imageSource;
            }
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


        #endregion
    }
}
