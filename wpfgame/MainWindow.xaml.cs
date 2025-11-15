using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfGame
{
    public partial class MainWindow : Window
    {
        private double speed = 5;
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();

            Player.Source = new BitmapImage(new Uri("pack://application:,,,/Images/player.gif"));

            Canvas.SetLeft(Player, 200);
            Canvas.SetTop(Player, 200);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            Random r = new Random();
            int getrand = r.Next(500, 3000);
            timer.Interval = TimeSpan.FromMilliseconds(getrand);

            double x = r.Next(0, (int)GameCanvas.ActualWidth-100);
            double y = r.Next(0, (int)GameCanvas.ActualHeight-100);
            Image img = new Image();
            img.Width = 100;
            img.Height = 100;

            Uri imageUri = new Uri("pack://application:,,,/Images/zombie.gif");
            img.Source = new BitmapImage(imageUri);
            
            GameCanvas.Children.Add(img);
            Canvas.SetLeft(img,x);
            Canvas.SetTop(img,y);
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