using System;
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
            if (sceneSize.Width < 10 || sceneSize.Height < 10)
                sceneSize = new Size(700, 550);

            controller = new CController(1.1, 0, sceneSize);
            player = new CPlayer(0.45);

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

            Redraw();
            tbScore.Text = controller.getPoints().ToString("0");
        }

        private void Redraw()
        {
            gameCanvas.Children.Clear();

            foreach (var obj in controller.getObjects())
            {
                gameCanvas.Children.Add(obj.getSprite());
            }
        }

        private void gameCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isRunning || !player.CanClick) return;

            Point mousePos = e.GetPosition(gameCanvas);

            player.mouseClick(mousePos);

            double oldScore = controller.getPoints();
            controller.mouseClick(player, mousePos);
            double newScore = controller.getPoints();

            if (newScore > oldScore)
                lbLog.Items.Insert(0, $"+{(newScore - oldScore):0.0} points");
            else
                lbLog.Items.Insert(0, "Miss / Bonus");

            if (lbLog.Items.Count > 12)
                lbLog.Items.RemoveAt(lbLog.Items.Count - 1);

            Redraw();
            tbScore.Text = controller.getPoints().ToString("0");
        }
    }
}