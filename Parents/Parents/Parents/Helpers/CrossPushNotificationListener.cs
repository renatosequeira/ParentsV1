using PushNotification.Plugin.Abstractions;
using System.Diagnostics;
using PushNotification.Plugin;
using Newtonsoft.Json.Linq;

namespace Parents.Helpers
{
    //Class to handle push notifications listens to events such as registration, unregistration, message arrival and errors.
    public class  CrossPushNotificationListener : IPushNotificationListener
    {

        public void OnMessage(JObject values, DeviceType deviceType)
        {
            Debug.WriteLine("Message Arrived");
        }

        public void OnRegistered(string token, DeviceType deviceType)
        {
            //Debug.WriteLine(string.Format("Push Notification - Device Registered - Token : {0}", Token));
        }

        public void OnUnregistered(DeviceType deviceType)
        {
            Debug.WriteLine("Push Notification - Device Unnregistered");
       
        }

        public void OnError(string message, DeviceType deviceType)
        {
            Debug.WriteLine(string.Format("Push notification error - {0}",message));
        }

        public bool ShouldShowNotification()
        {
            return true;
        }
    }
}
