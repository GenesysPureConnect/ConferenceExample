using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ININ.Alliances.Examples.ConferenceExample.ViewModel;

namespace ININ.Alliances.Examples.ConferenceExample.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isScrollAtEnd = true;

        public MainWindow()
        {
            DataContext = MainViewModel.Instance;
            MainViewModel.Instance.PleaseScrollToEnd += () => { if (_isScrollAtEnd) Log.ScrollToEnd(); };

            InitializeComponent();

            CicPasswordBox.Password = Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(MainViewModel.Instance.CicPassword));
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            MainViewModel.Instance.Disconnect();
        }

        private void CicPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.CicPassword = CicPasswordBox.SecurePassword;
        }

        private void InteractionsListView_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var listView = sender as ListView;
                if (listView == null || listView.SelectedItem == null) return;
                var row =
                    listView.ItemContainerGenerator.ContainerFromItem(listView.SelectedItem) as ListViewItem;
                if (row != null && !row.IsMouseOver)
                    row.IsSelected = false;
            }
            catch (Exception ex)
            {
                MainViewModel.Instance.LogMessage(ex);
            }
        }

        private void Log_OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            try
            {
                _isScrollAtEnd = Math.Abs(Log.VerticalOffset - Log.ScrollableHeight) < 32;
            }
            catch (Exception ex)
            {
                MainViewModel.Instance.LogMessage(ex);
            }
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                // This can be null when the application shuts down
                if (MainViewModel.Instance.QueueViewModel != null)
                    MainViewModel.Instance.QueueViewModel.SelectedInteraction = e.NewValue as InteractionViewModel;
            }
            catch (Exception ex)
            {
                MainViewModel.Instance.LogMessage(ex);
            }
        }
    }
}
