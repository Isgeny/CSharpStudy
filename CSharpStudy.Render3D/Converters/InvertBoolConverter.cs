using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CSharpStudy.Render3D.Converters
{
    public class InvertBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                bool x => !x,
                _ => DependencyProperty.UnsetValue,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                bool x => !x,
                _ => DependencyProperty.UnsetValue,
            };
        }
    }
}