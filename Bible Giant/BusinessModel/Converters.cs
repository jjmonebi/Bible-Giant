using Bible_Giant.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using WinRTMultibinding.Foundation.Interfaces;

namespace Bible_Giant.BusinessModel
{
    public class LifeLineToIsEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, String culture)
        {
            if (((LifeLine)value) == LifeLine.None)
            {
                return true;
            }
            return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, String culture)
        {
            throw new NotImplementedException("Cannot convert back");
        }
    }
    public class PercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, String culture)
        {
            return value.ToString() + "%";
        }
        public object ConvertBack(object value, Type targetType, object parameter, String culture)
        {
            throw new NotImplementedException("Cannot convert back");
        }
    }
    public class FafOptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, String culture)
        {
            return "I think the answer is " + value.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, String culture)
        {
            throw new NotImplementedException("Cannot convert back");
        }
    }
    public class LifeLineToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, String culture)
        {
            LifeLine lifeline = (LifeLine)values[0]; string tag = values[1].ToString();
            if (lifeline == LifeLine.AskDAudience && tag == "2")
            {
                return Visibility.Visible;
            }
            else if (lifeline == LifeLine.FoneAFriend && tag == "3")
            {
                return Visibility.Visible;
            }
            else
                return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, String culture)
        {
            throw new NotImplementedException("Cannot convert back");
        }
    }
    public class AudienceSizeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, String culture)
        {
            int audienceGrade = (int)values[0]; double height = (double)values[1];
            double Height = System.Convert.ToDouble(audienceGrade) / 100 * height * 0.4;
            return Height;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, String culture)
        {
            throw new NotImplementedException("Cannot convert back");
        }
    }
}
