﻿using konito_project.ViewModel.Query;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
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

namespace konito_project.View.Query {
    /// <summary>
    /// Interaction logic for EmployeeQuery.xaml
    /// </summary>
    public partial class EmployeeQuery : MetroWindow {
        public EmployeeQuery() {
            InitializeComponent();
            DataContext = new EmpQueryViewModel();
        }
    }
}
