using lab2.Models;
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
        private CPlayer player = null!;
        private CController bonusController = null!;
        private CEnemy enemy = null!;
        private DispatcherTimer timer = null!;
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
            totalTime = 0;

            player = new CPlayer(0.6);
            enemy = new CEnemy(100, 15);

            Size sceneSize = new Size(gameCanvas.ActualWidth, gameCanvas.ActualHeight);
            if (sceneSize.Width < 10 || sceneSize.Height < 10)
                sceneSize = new Size(720, 580);

            bonusController = new CController(sceneSize);

            isRunning = true;
            btnStart.Content = "Стоп";
            tbStatus.Text = "Игра идёт";
            timer.Start();

            UpdateUI();
        }

        private void StopGame()
        {
            timer.Stop();
            isRunning = false;
            btnStart.Content = "Старт";
            tbStatus.Text = "Стоп";
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (!isRunning) return;

            double delta = 0.1;
            totalTime += delta;

            player.update(delta);
            bonusController.update(delta);

            Redraw();
            UpdateUI();
        }

        private void Redraw()
        {
            gameCanvas.Children.Clear();

            foreach (var obj in bonusController.getObjects())
            {
                gameCanvas.Children.Add(obj.getSprite());
            }
        }

        private void UpdateUI()
        {
            tbTime.Text = totalTime.ToString("0.0") + "с";
            tbScore.Text = player.Gold.ToString("0");
            tbDamage.Text = player.Damage.ToString("0");
            tbCooldown.Text = player.Cooldown.ToString("0.00") + "с";

            pbEnemyHealth.Maximum = enemy.MaxHealth;
            pbEnemyHealth.Value = enemy.Health;
            tbEnemyHealth.Text = $"{enemy.Health:0} / {enemy.MaxHealth:0}";
        }

        private void gameCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isRunning) return;

            Point mousePos = e.GetPosition(gameCanvas);

            string bonusResult = bonusController.mouseClick(player, mousePos);

            if (bonusResult != null)
            {
                lbLog.Items.Insert(0, bonusResult);
            }
            else
            {
                if (player.TryClick())
                {
                    bool isDead = enemy.TakeDamage(player.Damage);
                    lbLog.Items.Insert(0, $"-{player.Damage} урона");

                    if (isDead)
                    {
                        player.AddGold(enemy.GoldReward);
                        lbLog.Items.Insert(0, $"+{enemy.GoldReward} золота");
                        enemy.Reset(enemy.MaxHealth * 1.2, enemy.GoldReward * 1.15);
                    }
                }
                else
                {
                    lbLog.Items.Insert(0, "Перезарядка...");
                }
            }

            if (lbLog.Items.Count > 14)
                lbLog.Items.RemoveAt(lbLog.Items.Count - 1);

            UpdateUI();
        }

        private void btnUpgradeCooldown_Click(object sender, RoutedEventArgs e)
        {
            if (!isRunning) return;

            if (player.TryUpgradeCooldown(40))
                lbLog.Items.Insert(0, "Перезарядка улучшена!");
            else
                lbLog.Items.Insert(0, "Не хватает золота");

            UpdateUI();
        }
    }
}