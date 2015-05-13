using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FamBudg
{
    public partial class Options : Form
    {
        /*
         * Переименование имён столбцов.
         * Настройка сервера БД. Возможность локальной БД. В будущем - различных СУБД (нужно ли).
         * Настройка внешнего вида, расположения окон.
         */
        public Options(FamilyBudget f)
        {           
            InitializeComponent();
        }
    }
}
