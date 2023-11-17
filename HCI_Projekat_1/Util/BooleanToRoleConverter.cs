using HCI_Projekat_1.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HCI_Projekat_1
{
    public class BooleanToRoleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                bool isManager = (bool)value;
                return isManager ? LanguageUtil.GetTranslation("Manager") : LanguageUtil.GetTranslation("Worker");
            }
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is String)
            {
                String str = (String)value;
                return str == "Manager";
            }
            return false;
        }
    }
}
