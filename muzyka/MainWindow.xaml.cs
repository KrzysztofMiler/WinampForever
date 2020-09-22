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

        List<string> files = new List<string>();
        List<string> paths= new List<string>();

        private void BT_click_Play(object sender, RoutedEventArgs e)
        {
            play();
                      
        }

        private void play()
        {
            if (paths.Count != 0)
            {
                mediaPlayer.Open(new Uri(paths.First()));
                mediaPlayer.Play();
                NowPlaying.Text = files.First();
                paths.RemoveAt(0);//1 eleem
                files.RemoveAt(0);
                listBoxSongsUpdate();//refresh kolejki
                mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
            }
            else
            {
                mediaPlayer.Play();//de facto jest aby nie wywaliło program
            }
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            play();
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
                Multiselect = true,
                DefaultExt = ".mp3"

            };           

            bool? dialogOK = fileDialog.ShowDialog();//czy odpali okno
            if (dialogOK == true)
            {
                filename = fileDialog.FileName;
                TBFileName.Text = fileDialog.SafeFileName;//ten tekścik na wyszukaniu ustawia


                files.Add(fileDialog.SafeFileName);
                paths.Add(fileDialog.FileName);

                listBoxSongsUpdate();
                              

                //mediaPlayer.Open(new Uri(filename));//możliwe że nadmiarowe

            }
        }

        private void listBoxSongsUpdate() { 
        listBoxSongs.Items.Clear();//aby nie dublowało itp

                for (int i = 0; i<files.Count; i++)
                {
                    listBoxSongs.Items.Add(files[i]);

                }
        }

        private void BT_click_Shuffle(object sender, RoutedEventArgs e)
        {//BUG POTRAAFI USÓWAĆ RODZAJ PIOSENKI
            Random random = new Random();

            List<String> filesInst = files.ToList();
            List<String> pathInst = paths.ToList();
             

            files.Clear();
            paths.Clear();

            for (int i = 0; i < filesInst.Capacity; i++)
            {
                int ctn = random.Next(0, filesInst.Count-1);//tutaj potrafi usunąc
                
                                
                files.Add(filesInst.ElementAt(ctn));
                paths.Add(pathInst.ElementAt(ctn));

                filesInst.RemoveAt(ctn);
                pathInst.RemoveAt(ctn);
            }
            listBoxSongsUpdate();
        }
    }
}
