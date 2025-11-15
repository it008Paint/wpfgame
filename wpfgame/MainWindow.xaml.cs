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
        enum Direction { Up, Down, Left, Right }
        Direction lastDirection = Direction.Right;
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
            {
                lastDirection = Direction.Up;
                y -= speed;
            }
                
            if (e.Key == Key.Down)
            {
                lastDirection = Direction.Down;
                y += speed;
            }
                
            if (e.Key == Key.Left)
            {
                lastDirection = Direction.Left;
                x -= speed;
            }
                
            if (e.Key == Key.Right)
            {
                lastDirection = Direction.Right;
                x += speed;
            }
            
            if (e.Key == Key.A)
            {
                Shoot();
            }

            Canvas.SetLeft(Player, x);
            Canvas.SetTop(Player, y);
        }

        private void shoot(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) Shoot();
        }

        private void Shoot()
        {
            Image fireball = new Image
            {
                Width = 30,
                Height = 30,
                Source = new BitmapImage(new Uri("pack://application:,,,/Images/fireball.png"))
            };
            double playerX = Canvas.GetLeft(Player);
            double playerY = Canvas.GetTop(Player);
            GameCanvas.Children.Add(fireball);
            Direction fireballDirection = lastDirection;


            if (fireballDirection == Direction.Up || fireballDirection == Direction.Right)
            {
                Canvas.SetLeft(fireball, playerX + 20);
                Canvas.SetTop(fireball, playerY + 20);
            }
            else if (fireballDirection == Direction.Down)
            {
                Canvas.SetLeft(fireball, playerX + 40);
                Canvas.SetTop(fireball, playerY + 20);
            }
            else
            {
                Canvas.SetLeft(fireball, playerX + 20);
                Canvas.SetTop(fireball, playerY + 40);
            }


            DispatcherTimer t = new DispatcherTimer();
            t.Interval = TimeSpan.FromMilliseconds(40);
            t.Tick += (s, e) =>
            {
                double x = Canvas.GetLeft(fireball);
                double y = Canvas.GetTop(fireball);
                RotateTransform rotateTransform = new RotateTransform();

                switch (fireballDirection)
                {
                    case Direction.Up: 
                        Canvas.SetTop(fireball, y - 10);
                        rotateTransform.Angle = -90;
                        fireball.RenderTransform = rotateTransform;
                        break;
                    case Direction.Down: 
                        Canvas.SetTop(fireball, y + 10);
                        rotateTransform.Angle = 90;
                        fireball.RenderTransform = rotateTransform;
                        break;
                    case Direction.Left: 
                        Canvas.SetLeft(fireball, x - 10);
                        rotateTransform.Angle = 180;
                        fireball.RenderTransform = rotateTransform;
                        break;
                    case Direction.Right: 
                        Canvas.SetLeft(fireball, x + 10); 
                        break;
                }
            };
            t.Start();
        }
    }
}