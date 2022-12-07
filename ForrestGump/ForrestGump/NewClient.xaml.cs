﻿using ForrestGump.Entities;
using System;
using System.Linq;
using System.Windows;

namespace ForrestGump
{
    /// <summary>
    /// Логика взаимодействия для NewClient.xaml
    /// </summary>
    public partial class NewClient : Window
    {
        string error;
        public NewClient()
        {
            InitializeComponent();
        }
        Entities.Forrest_GumpEntities db = new Forrest_GumpEntities();
        private void AddNewClient(object sender, RoutedEventArgs e)
        {
            error = "";
            if (lastNameBx.Text != "" && nameBx.Text != "" && middleNameBx.Text != "" && passportDataBx.Text != ""
                && dateDp.Text != "" && addresBx.Text != "" && emailBx.Text != "" && phoneBx.Text != "")
            {

                Entities.Client client = new Entities.Client()
                {
                    Name = nameBx.Text,
                    LastName = lastNameBx.Text,
                    MiddleName = middleNameBx.Text,
                    PassportData = passportDataBx.Text,
                    Email = emailBx.Text,
                    Address = addresBx.Text,
                    Number = phoneBx.Text,
                    DateOfBirth = Convert.ToDateTime(dateDp.Text)
                };
                Client authClient = null;
                using (Entities.Forrest_GumpEntities context = new Entities.Forrest_GumpEntities())
                {
                    authClient = context.Clients.Where(b => b.Number == phoneBx.Text).FirstOrDefault(); 
                    if(authClient != null) { error += "Такой номер уже зарегистрирован"; }
                    authClient = null;

                    authClient = context.Clients.Where(b => b.PassportData == passportDataBx.Text).FirstOrDefault();
                    if (authClient != null) { error += "\nТакой человек уже зарегистрирован"; }
                    authClient = null;

                    authClient = context.Clients.Where(b => b.Email == emailBx.Text).FirstOrDefault();
                    if (authClient != null) { error += "\nТакой Email уже зарегистрирован"; }
                    
                    if(error=="")
                    {
                        context.Clients.Add(client);
                        context.SaveChanges();
                        MessageBox.Show("Клиент добален");
                    }
                    else { errorMessages.Text = error; }
                }
            }
            else
            {
                 MessageBox.Show("Заполните поля");
            }

        }
    }
}
