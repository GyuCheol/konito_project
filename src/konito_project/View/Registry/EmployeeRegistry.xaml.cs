﻿using konito_project.Model;
using konito_project.ViewModel;
using konito_project.ViewModel.Registry;
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

namespace konito_project.View.Registry {
    /// <summary>
    /// Interaction logic for EmployeeRegister.xaml
    /// </summary>
    public partial class EmployeeRegistry : MetroWindow {
        public EmployeeRegistry(object dataContext) {
            InitializeComponent();
            DataContext = dataContext;
        }

    }
}
