using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace vladnigger
{
    /// <summary>
    /// Interaction logic for helpwindow.xaml
    /// </summary>
    public partial class helpwindow : Window
    {

        public helpwindow()
        {


            InitializeComponent();

           

            void Border_MouseDown12(object sender, MouseButtonEventArgs e)
            {
                if (e.ButtonState == MouseButtonState.Pressed)
                    DragMove();
            }

            

            void Close12_Click(object sender, RoutedEventArgs e)
            {

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





