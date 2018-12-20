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
using System.Windows.Shapes;

namespace MapEditor
{
    /// <summary>
    /// NewMap.xaml 的交互逻辑
    /// </summary>
    public partial class NewMap : Window
    {
        public delegate void NewMapDelegate(string name,int width, int height);
        public event NewMapDelegate NewMapEvent;
        //public User user;
        public NewMap()
        {
            //窗口居中
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.Width = 300;
            this.Height = 350;
        }

        public void Commit(object sender, RoutedEventArgs e)
        {
            //业务逻辑。。。。
           Console.WriteLine("commit");
           if (NewMapEvent != null)
            {
                Console.WriteLine("send event ");
                string name = this.name.Text;
                int width = int.Parse(this.width.Text);
                int height = int.Parse(this.height.Text);
                NewMapEvent(name,width,height);
           }

            DialogResult = true;
            Close();            
          
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
