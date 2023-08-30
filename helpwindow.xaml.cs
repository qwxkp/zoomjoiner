using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using vladnigger;

namespace Zoomjoiner
{
    /// <summary>
    /// Interaction logic for helpwindow.xaml
    /// </summary>
    public partial class helpwindow : Window
    {
        public helpwindow()
        {
            InitializeComponent();

            void Close12_Click(object sender, RoutedEventArgs e)
            {
                
            }

            void Border_MouseDown12(object sender, MouseButtonEventArgs e)
            {
                if (e.ButtonState == MouseButtonState.Pressed)
                    DragMove();
            }





        }
        void Close12_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Close12_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Support_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://t.me/skeden");
        }

        private void Border_MouseDown12(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();

        }
    }
}





