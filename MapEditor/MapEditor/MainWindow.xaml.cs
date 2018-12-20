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
using Hexagonal;
using System.IO;


namespace MapEditor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Model hexModel;
        private Board board;
        private GraphicsEngine engine;
        private const int HEX_SIDE = 50;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (hexModel == null)
            {
                hexModel = new Model();
            }
            board = new Board(50, 50, HEX_SIDE, HexOrientation.Flat);
            engine = new GraphicsEngine(board);
            ((SpecialCanvas)canvas).Engine = engine;
        }
        private void clusterPreview_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (board == null)
            {
                return;
            }
            Point mouseClick = new Point(e.GetPosition(canvas).X - board.XOffset,
                 e.GetPosition(canvas).Y - board.YOffset);
            Hex clickedHex = board.FindHexMouseClick((int)mouseClick.X, (int)mouseClick.Y);

            if (clickedHex == null)
            {
               // Console.WriteLine("No hex was clicked.");
                board.BoardState.ActiveHex = null;
            }

            else
            {
                //Console.WriteLine("Hex was clicked."+ clickedHex.Col + clickedHex.Row);
                if (board.MapData != null)
                {
                    if (clickedHex.Row >= 0 && clickedHex.Row < board.MapData.width &&
                        clickedHex.Col >= 0 && clickedHex.Col < board.MapData.height)
                    {

                        board.BoardState.ActiveHex = clickedHex;
                        board.resetArea((int)clickedHex.Row, (int)clickedHex.Col);
                        ((SpecialCanvas)canvas).Engine = engine;
                    }
                }
            }
            
        }

        //npc设定
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            

                board.setSelectHexState(1);
                //selcetHex.HexState.setBackgroundColor();
       
             ((SpecialCanvas)canvas).Engine = engine;
        }

        //可否通行设定
        private void button1_Click(object sender, RoutedEventArgs e)
        {
           
        }

        //新建地图
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("new map~~~~");
            NewMap map = new NewMap();
            map.NewMapEvent += createNewMap;
            map.ShowDialog();


        }
        public void createNewMap(string name,int width,int height)
        {
            board = new Board(50, 50, HEX_SIDE, HexOrientation.Flat);
            engine = new GraphicsEngine(board);
            MapData data = new MapData(name, width, height);
            board.setMapData(data);

            ((SpecialCanvas)canvas).Engine = engine;
        }

        //打开地图
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Console.WriteLine("url is " + AppDomain.CurrentDomain.BaseDirectory);

            string rootPath = AppDomain.CurrentDomain.BaseDirectory;
            rootPath = rootPath.Substring(0, rootPath.LastIndexOf(@"\"));
            rootPath = rootPath.Substring(0, rootPath.LastIndexOf(@"\"));
            rootPath = rootPath.Substring(0, rootPath.LastIndexOf(@"\"));
            rootPath = rootPath.Substring(0, rootPath.LastIndexOf(@"\"));
            rootPath = rootPath.Substring(0, rootPath.LastIndexOf(@"\"));
            rootPath = rootPath + @"\MapData";
            dlg.InitialDirectory = rootPath;
            dlg.DefaultExt = ".json";
            dlg.Filter = "Text documents (.json)|*.json";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                MapData data = new MapData(filename);
                board = new Board(50, 50, HEX_SIDE, HexOrientation.Flat);
                board.setMapData(data);
                engine = new GraphicsEngine(board);                            
                ((SpecialCanvas)canvas).Engine = engine;
            }
        }

        //保存地图
        private void button_Click(object sender, RoutedEventArgs e)
        {
            // 如果文件不存在，创建文件； 如果存在，覆盖文件 
            string filename = board.MapData.name;
            StreamWriter sw1 = new StreamWriter("..\\..\\..\\..\\MapData/"+ filename + ".json");
            //board.writeDataToFile(sw1);
            board.MapData.writeDataToFile(sw1);
            sw1.Close();
        }

        public void TestDele(int a,int b,int c)
        {
            Console.WriteLine("huidiaochenggong!!!!!!!!");
        }
    }
}
