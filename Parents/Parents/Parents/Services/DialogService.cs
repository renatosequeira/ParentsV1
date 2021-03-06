﻿using Parents.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Parents.Services
{
    public class DialogService
    {
        public async Task ShowMessage(string title, string message)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }

        public async Task<bool> ShowConfirm(string title, string message)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, AppResources.Yes, AppResources.No);
            
        }

        public async Task<string> ShowImageOptions()
        {
            var txtInput = new Entry { Text = "" };
            return await Application.Current.MainPage.DisplayActionSheet(
                "Where do you take the image?",
                "Cancel",
                null,
                "From Gallery",
                "From Camera");
        }

    }
}
