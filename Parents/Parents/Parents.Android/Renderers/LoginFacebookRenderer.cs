using Parents.Droid.Renderers;
using Parents.Views;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(LoginFacebookView), typeof(Parents.Droid.Renderers.LoginFacebookRenderer))]

namespace Parents.Droid.Renderers
{
    using System;
    using System.Net.Http;
    using System.Runtime.Remoting.Contexts;
    using System.Threading.Tasks;
    using Android.App;
    using Models;
    using Newtonsoft.Json;
    using Xamarin.Auth;
    using Xamarin.Forms.Platform.Android;

    public class LoginFacebookRenderer : PageRenderer
    {

        public LoginFacebookRenderer()
        {
            var activity = this.Context as Android.App.Activity;

            var auth = new OAuth2Authenticator(
                clientId: "710469952485585",
                scope: "",
                authorizeUrl: new Uri(
                    "https://www.facebook.com/v2.11/dialog/oauth"),
                redirectUrl: new Uri(
                    "http://www.facebook.com/connect/login_success.html"));

            auth.Completed += async (sender, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    var accessToken =
                        eventArgs.Account.Properties["access_token"].ToString();
                    var profile = await GetFacebookProfileAsync(accessToken);
                    App.LoginFacebookSuccess(profile);
                }
                else
                {
                    App.LoginFacebookFail();
                }
            };

            activity.StartActivity(auth.GetUI(activity));
        }

        async Task<FacebookResponse> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl = "https://graph.facebook.com/v2.11/me/?fields=" +
                "name,picture.width(999),cover,age_range,devices,email," +
                "gender,is_verified,birthday,languages,work,website," +
                "religion,location,locale,link,first_name,last_name," +
                "hometown&access_token=" + accessToken;
            var httpClient = new HttpClient();
            var userJson = await httpClient.GetStringAsync(requestUrl);
            var facebookResponse =
                JsonConvert.DeserializeObject<FacebookResponse>(userJson);
            return facebookResponse;
        }
    }
}