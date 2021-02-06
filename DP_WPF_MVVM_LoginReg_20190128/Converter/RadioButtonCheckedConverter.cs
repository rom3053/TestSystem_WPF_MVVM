using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DP_WPF_MVVM_LoginReg_20190128.Converter
{
    public class RadioButtonCheckedConverter : IValueConverter

    {

        public object Convert(object value, Type targetType, object parameter,

            System.Globalization.CultureInfo culture)

        {

            return ((string)parameter == (string)value);

        }



        public object ConvertBack(object value, Type targetType, object parameter,

            System.Globalization.CultureInfo culture)

        {

            return (bool)value ? parameter : null;

        }

    }

}

