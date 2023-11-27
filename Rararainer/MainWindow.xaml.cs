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

namespace Rararainer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : AdonisUI.Controls.AdonisWindow
    {
        private String hwid;
        private List<String> servers;
        private API.Client apiClient = new API.Client();
        private MemoryWriter.Writer memoryWriter;

        public MainWindow()
        {
            this.hwid = libc.hwid.HwId.Generate();
            InitializeComponent();
            this.label_hwid.Content = this.hwid;
            try
            {
                this.servers = apiClient.getServers(this.hwid);
                this.list_servers.ItemsSource = this.servers;
                this.list_servers.SelectedIndex = 0;
                this.memoryWriter = new MemoryWriter.Writer(this.servers.First(), this.hwid);
            } catch
            {
                Clipboard.SetText(this.hwid);
                AdonisUI.Controls.MessageBox.Show("Not subscribed. HWID Copied. Closing application", "Error");
                this.Close();
            }

        }

        private void slider_atkSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = (Slider) sender;
            this.label_atkSpeed.Content = "(" + slider.Value.ToString() + ")";
        }

        private void slider_longRange_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = (Slider) sender;
            this.label_longRange.Content = "("+ slider.Value.ToString() + ")";
        }

        private void button_copy_hwid_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(this.hwid);
            AdonisUI.Controls.MessageBox.Show("HWID Copied!", "Success");
        }

        private void list_servers_Selected(object sender, RoutedEventArgs e)
        {

            if (this.memoryWriter == null) return;
            ComboBox cb = (ComboBox) sender;
            this.memoryWriter.changeServer((String) cb.SelectedValue);
        }
    }
}
