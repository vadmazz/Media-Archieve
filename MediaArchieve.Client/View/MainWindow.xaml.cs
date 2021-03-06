﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;
using MediaArchieve.Client.ViewModel;
using MediaArchieve.Shared;

namespace MediaArchieve.Client.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
        
        private void ButtonPower_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void listView_Click(object sender, MouseButtonEventArgs e)
        {    
            var vm = DataContext as MainWindowViewModel;
            vm.SelectedItem = (Item)(sender as ListViewItem).DataContext;
        }

        private void Maximize_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                Maximize.Visibility = Visibility.Collapsed;
                Restore.Visibility = Visibility.Visible;
            }
            else
            {
                Maximize.Visibility = Visibility.Visible;
                Restore.Visibility = Visibility.Collapsed;//todo: переписать весь код измения размеров окна
            }
        }

        private void Restore_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
        }

        private void Minimize_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}

 