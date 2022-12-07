﻿using ForrestGump.Entities;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Threading;

namespace ForrestGump
{
    /// <summary>
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        Entities.Forrest_GumpEntities db = new Entities.Forrest_GumpEntities();
        DispatcherTimer timer = new DispatcherTimer();
        int s, m, h;

        Entities.Emploee refEmploee;
        public Home(ref Entities.Emploee authEmploe)
        {
            ConnectionWindow connection = new ConnectionWindow();
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            int id = authEmploe.Id;
            refEmploee = authEmploe;
            ListViewEmploee.ItemsSource = db.Emploees.ToList().Where(b => b.Id == id);
        }
        private void NewOrder(object sender, RoutedEventArgs e)
        {
            newOrders g = new newOrders(ref refEmploee);
            g.ShowDialog();
        }

        private void AddClient(object sender, RoutedEventArgs e)
        {
            Window g = new NewClient();
            g.ShowDialog();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            s++;
            if (s > 59)
            {
                if (m == 5)
                {
                    messageOut.Visibility = Visibility.Visible;
                }
                if (m == 10)
                {
                    s = 0;
                    m = 0;
                    timer.Stop();
                    OutTimer w = new OutTimer();
                    w.Show();
                    this.Close();
                }
                m++;
                s = 0;
                if (m > 59)
                {
                    m = 0;
                    h++;
                }
            }
            timerHome.Text = $"{h}:{m}:{s}";
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            this.Close();
        }
    }
}
