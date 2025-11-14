using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfGame
{
    public partial class MainWindow : Window
    {
        private double speed = 5;

        public MainWindow()
        {
            InitializeComponent();

            Player.Source = new BitmapImage(new Uri("https://i.redd.it/g61x0rmuvmda1.gif"));

            Canvas.SetLeft(Player, 200);
            Canvas.SetTop(Player, 200);

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            double x = Canvas.GetLeft(Player);
            double y = Canvas.GetTop(Player);

            if (e.Key == Key.Up)
                y -= speed;
            if (e.Key == Key.Down)
                y += speed;
            if (e.Key == Key.Left)
                x -= speed;
            if (e.Key == Key.Right)
                x += speed;

            Canvas.SetLeft(Player, x);
            Canvas.SetTop(Player, y);
        }
    }
}