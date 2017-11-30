using Parents.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Parents.Services;

namespace Parents.ViewModels.Childrens
{
    public class EditChildrenViewModel : INotifyPropertyChanged
    {
        #region MyRegion
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion

        #region Properties
        public string ChildrenFirstName { get; set; }
        public string ChildrenImage { get; set; }
        public string ChildrenLastName { get; set; }
        #endregion

        private Children children;

        #region Constructors
        public EditChildrenViewModel(Children children)
        {
            this.children = children;
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            ChildrenFirstName = children.ChildrenFirstName;
            //ChildrenImage = children.ChildrenImageFullPath;
            ChildrenLastName = children.ChildrenLastName;
        } 
        #endregion


    }
}
