using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Parents.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Parents.Services
{
    public class FacebookServices
    {

        public async Task<FacebookResponse> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl =
                "https://graph.facebook.com/v2.11/me/?fields=name,picture.width(999),cover,age_range,devices,email,gender,is_verified,birthday,languages,work,website,religion,location,locale,link,first_name,last_name,hometown&access_token="
                + accessToken;

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            var facebookProfile = JsonConvert.DeserializeObject<FacebookResponse>(userJson);

            return facebookProfile;
        }
    }
}
