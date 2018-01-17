using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace bing_Maps0002
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int counter = 0;
        public MainWindow()
        {
            InitializeComponent();
            //Bing Maps Code 
            myMap.Focus();
            myMap.MouseDoubleClick += new MouseButtonEventHandler(myMap_MouseDoubleClick);
        }
        //Mouse click events     
        public void myMap_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            Point mousePosition = e.GetPosition(this);
            Location pinLocation = myMap.ViewportPointToLocation(mousePosition);

            Pushpin pin = new Pushpin();
            pin.Location = pinLocation;
            pin.Content = counter += 1;
            pin.MouseDown += new MouseButtonEventHandler(pin_MouseDown);

            MessageBox.Show(pinLocation.ToString());
            String pinList = pinLocation.ToString();
            Double pinList2 = pinLocation.Longitude;
            myMap.Children.Add(pin);

            for (int ix = 0; ix < pinList.Length; ix++)
            {
                String pinList1 = pinLocation.ToString() + ix;

                string pfaad = @"C:\Test\" + pin.Content;
                //Create the file.
                using (FileStream fs = File.Create(pfaad))
                {
                    AddText(fs, pinList1);
                }
            }
        }
        /// <summary>
        /// https://msdn.microsoft.com/de-de/library/system.text.encoding.utf8(v=vs.110).aspx
        /// Encoding.UTF8-Eigenschaft
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="value"></param>

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
        /// <summary>
        /// setzt die pins auf der bing karte
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            var pushPinContent = 0;
            var pushPin = sender as Pushpin;
            if (pushPin != null && pushPin.GetType() == typeof(Pushpin))
                pushPinContent = Convert.ToInt32(pushPin.Content);

            myMap.Heading = (double)pushPinContent;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
