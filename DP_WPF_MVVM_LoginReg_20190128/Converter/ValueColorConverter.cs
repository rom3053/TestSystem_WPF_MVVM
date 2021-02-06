using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DP_WPF_MVVM_LoginReg_20190128.Converter
{
    public class ValueColorConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Double num = (Double)value;
            if (num >= 90)
            {
                return "BlueViolet";
            }
            else if (num >= 75)
            {
                return "MediumSeaGreen";
            }
            else if (num >=60)
            {
                return "Peru";
            }
            else if (num < 60)
            {
                return "Red";
            }

            return "Black";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
