using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ForrestGump
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly Entities.Forrest_GumpEntities DataBase = new Entities.Forrest_GumpEntities();

        public static ConnectionWindow Emploee_id = new ConnectionWindow();

    }
}
