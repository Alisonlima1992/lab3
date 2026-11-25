using System;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ClickOrLoose
{
    public partial class MainWindow : Window
    {
        private CController controller;
        private CPlayer player;
        private DispatcherTimer timer;
        private bool isRunning = false;
        private double totalTime = 0;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (isRunning)
            {
                StopGame();
                return;
            }

            StartGame();
        }

        private void StartGame()
        {
            gameCanvas.Children.Clear();
            lbLog.Items.Clear();
            tbScore.Text = "0";
            totalTime = 0;
            tbTime.Text = "0.0s";

            Size sceneSize = new Size(gameCanvas.ActualWidth, gameCanvas.ActualHeight);
            if (sceneSize.Width == 0 || sceneSize.Height == 0)
                sceneSize = new Size(700, 550);

            controller = new CController(1.2, 0, sceneSize);
            player = new CPlayer(0.4);

            isRunning = true;
            btnStart.Content = "Stop";
            tbStatus.Text = "Playing...";
            timer.Start();
        }

        private void StopGame()
        {
            timer.Stop();
            isRunning = false;
            btnStart.Content = "Start";
            tbStatus.Text = "Stopped";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!isRunning) return;

            double delta = 0.1;
            totalTime += delta;
            tbTime.Text = totalTime.ToString("0.0") + "s";

            player.update(delta);
            controller.update(delta);

            RedrawObjects();
            tbScore.Text = controller.getPoints().ToString("0");
        }

        private void RedrawObjects()
        {
            gameCanvas.Children.Clear();

            foreach (var obj in controller.getObjects())
            {
                Ellipse sprite = obj.getSprite();
                if (!gameCanvas.Children.Contains(sprite))
                    gameCanvas.Children.Add(sprite);
            }
        }

        private void gameCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isRunning || !player.CanClick) return;

            Point mousePos = e.GetPosition(gameCanvas);
            player.mouseClick(mousePos);

            double oldPoints = controller.getPoints();
            controller.mouseClick(mousePos);
            double newPoints = controller.getPoints();

            if (newPoints > oldPoints)
            {
                double gained = newPoints - oldPoints;
                lbLog.Items.Insert(0, $"+{gained:0.0} points");
            }
            else
            {
                lbLog.Items.Insert(0, "Miss");
            }

            if (lbLog.Items.Count > 15)
                lbLog.Items.RemoveAt(lbLog.Items.Count - 1);

            RedrawObjects();
            tbScore.Text = controller.getPoints().ToString("0");
        }
    }
}