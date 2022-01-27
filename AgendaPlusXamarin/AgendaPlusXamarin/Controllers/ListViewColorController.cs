using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AgendaPlusXamarin.Controllers
{
    class ListViewColorController : IValueConverter
    {
        /// <summary>
        /// metodo que permite convertir un string a hexadecimal
        /// </summary>
        /// <param name="hexColor"></param>
        /// <returns>Color</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string hexColor = value.ToString();

                if (hexColor.IndexOf('#') != -1)
                {
                    hexColor = hexColor.Replace("#", "");
                }

                if (hexColor.Length == 6)
                {
                    hexColor = "FF" + hexColor;
                }

                byte alpha = 0;
                byte red = 0;
                byte green = 0;
                byte blue = 0;

                if (hexColor.Length == 8)
                {
                    alpha = byte.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                    red = byte.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                    green = byte.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
                    blue = byte.Parse(hexColor.Substring(6, 2), NumberStyles.AllowHexSpecifier);
                }

                return Color.FromRgba(red, green, blue, alpha);
            }
            return "#F0001";
        }



        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
