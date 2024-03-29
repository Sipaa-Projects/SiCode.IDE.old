﻿using SiCodeIDE;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SiCode.IDE.OptionsPages
{
    /// <summary>
    /// Logique d'interaction pour OptionalComponentsPage.xaml
    /// </summary>
    public partial class OptionalComponentsPage : Page
    {
        public OptionalComponentsPage()
        {
            InitializeComponent();
            if (Configuration.Theme == 1)
            {
                tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                tb1.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            }
        }
    }
}
