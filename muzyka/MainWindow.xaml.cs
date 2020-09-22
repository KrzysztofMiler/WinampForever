using Microsoft.Win32;
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

namespace muzyka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        MediaPlayer mediaPlayer = new MediaPlayer();
        string filename;
     

        private void BT_click_Play(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Play();
        }        

        private void BT_click_Pause(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void BT_click_Stop(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
        }

        private void BT_Cilck_Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog//otwieram okno dialogowe z win do pobierania
            {
                Multiselect = false,
                DefaultExt = ".mp3"

            };
            bool? dialogOK = fileDialog.ShowDialog();
            if (dialogOK == true)
            {
                filename = fileDialog.FileName;
                TBFileName.Text = fileDialog.SafeFileName;
                mediaPlayer.Open(new Uri(filename));

            }
        }
    }
}
