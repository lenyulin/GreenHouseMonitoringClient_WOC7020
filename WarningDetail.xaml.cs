using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace GreenHouseMonitoringClient
{
    public sealed partial class WarningDetail : UserControl
    {
        public WarningDetail(string id, string msg, BitmapImage img)
        {
            this.InitializeComponent();
            this.id.Text = id;
            this.msg.Text = msg;
            //this.pic.Source = img;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
