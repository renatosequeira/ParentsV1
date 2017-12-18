using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parents.Converter.Activities
{
    public class ActivitiesPriorityConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueAsString = value.ToString();
            switch (valueAsString)
            {
                case ("Urgent"):
                    {
                        return Color.FromHex("#EF3D60");
                    }
                case ("Normal"):
                    {
                        return Color.FromHex("#231F20");
                    }
                default:
                    {
                        return Color.FromHex("#231F20");
                    }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Only one way bindings are supported with this converter");
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
