using HCI_Projekat_1.Models;
using HCI_Projekat_1.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Projekat_1.Forms.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {

        private List<Theme> themes;
        public SettingsPage()
        {
            InitializeComponent();
            themes=ThemeUtil.GetThemes();
            themeCombo.DataContext=themes;
        }

        private void themeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var theme=themeCombo.SelectedItem as Theme;
            ThemeUtil.ChangeTheme(new Uri(theme.Path));
        }
    }
}
