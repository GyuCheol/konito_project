﻿using MahApps.Metro.Controls;
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

namespace konito_project.View.ResultInput
{
    /// <summary>
    /// Interaction logic for PurchaseSalesInput.xaml
    /// </summary>
    public partial class PurchaseSalesInput : MetroWindow {
        public PurchaseSalesInput() {
            InitializeComponent();
        }

        public PurchaseSalesInput(object dataContext) : this() {
            DataContext = dataContext;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}