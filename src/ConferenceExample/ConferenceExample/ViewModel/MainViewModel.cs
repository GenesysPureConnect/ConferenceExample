using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ININ.Alliances.Examples.ConferenceExample.Model;
using ININ.IceLib;
using ININ.IceLib.Connection;

namespace ININ.Alliances.Examples.ConferenceExample.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public delegate void PleaseScrollToEndHandler();
        public event PleaseScrollToEndHandler PleaseScrollToEnd;



        private static MainViewModel _instance = null;
        private static object _instanceLocker = new object();

        private const string LogTimestampFormat = "H:mm:ss_fffffff";

        private string _cicUsername = "";
        private SecureString _cicPassword = new SecureString();
        private string _cicStation;
        private string _cicServer = "";
        private ImageSource _cicConnectionStateImage;
        private readonly Session _session = new Session();
        private string _connectionState;
        private bool _isConnected;
        private bool _isConnectionInProgress;
        private string _log = "Welcome to the Conference Example!" + Environment.NewLine;
        private QueueViewModel _queueViewModel;



        #region Public Properties

        public static MainViewModel Instance
        {
            get { return _instance ?? (_instance = new MainViewModel()); }
        }

        public QueueViewModel QueueViewModel
        {
            get { return _queueViewModel; }
            set
            {
                if (value == null && _queueViewModel != null)
                    _queueViewModel.Dispose();

                _queueViewModel = value;
                OnPropertyChanged();
            }
        }

        public string CicUsername
        {
            get { return _cicUsername; }
            set
            {
                _cicUsername = value;
                OnPropertyChanged();
            }
        }

        public SecureString CicPassword
        {
            get { return _cicPassword; }
            set
            {
                _cicPassword = value ?? new SecureString();
                // Seal the password value
                if (!_cicPassword.IsReadOnly()) _cicPassword.MakeReadOnly();
                OnPropertyChanged();
            }
        }

        public string CicStation
        {
            get { return _cicStation; }
            set
            {
                _cicStation = value;
                OnPropertyChanged();
            }
        }

        public string CicServer
        {
            get { return _cicServer; }
            set
            {
                _cicServer = value;
                OnPropertyChanged();
            }
        }
        
        public string ConnectionStateString
        {
            get { return _connectionState; }
            set
            {
                _connectionState = value;
                OnPropertyChanged();
            }
        }

        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                OnPropertyChanged();
                OnPropertyChanged("ConnectButtonText");
            }
        }

        public bool IsConnectionInProgress
        {
            get { return _isConnectionInProgress; }
            set
            {
                _isConnectionInProgress = value;
                OnPropertyChanged();
                OnPropertyChanged("ConnectButtonText");
            }
        }

        public ImageSource CicConnectionStateImage
        {
            get { return _cicConnectionStateImage; }
            set
            {
                _cicConnectionStateImage = value;
                OnPropertyChanged();
            }
        }

        public string ConnectButtonText
        {
            get { return IsConnected ? "Disconnect" : IsConnectionInProgress ? "Connecting..." : "Connect"; }
        }

        public string Log
        {
            get { return _log; }
            private set
            {
                _log = value; 
                OnPropertyChanged();
                if (PleaseScrollToEnd != null) PleaseScrollToEnd();
            }
        }

        #endregion


        
        public MainViewModel()
        {
            // Load the settings into this view model. This loads the authentication credentials
            HelperModel.LoadSettings(this);

            // Register command bindings for this view model
            CommandBindings.Add(new CommandBinding(UiCommands.LogInCommand, LogIn_Executed, LogIn_CanExecute));

            _session.ConnectionStateChanged += SessionOnConnectionStateChanged;

            UpdateCicConnectionStateImage(ConnectionState.None);
        }



        #region IceLib

        private void SessionOnConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
        {
            try
            {
                // Update state items
                UpdateCicConnectionStateImage(e.State);
                ConnectionStateString = e.State + " (" + e.Message + ")";
                IsConnectionInProgress = e.State == ConnectionState.Attempting;
                IsConnected = e.State == ConnectionState.Up;
                LogMessage("Connection state: " + ConnectionStateString + " [" + e.Reason + "]");

                if (e.State == ConnectionState.Up)
                {
                    // Initialize when connection comes up
                    Context.Send(s =>
                    {
                        try
                        {
                            QueueViewModel = new QueueViewModel(_session);
                        }
                        catch (Exception ex)
                        {
                            LogMessage(ex);
                        }
                    }, null);
                }
                else
                {
                    // Ensure things are cleaned up otherwise
                    Context.Send(s =>
                    {
                        try
                        {
                            QueueViewModel = null;
                        }
                        catch (Exception ex)
                        {
                            LogMessage(ex);
                        }
                    }, null);
                }
            }
            catch (Exception ex)
            {
                LogMessage(ex);
            }
        }

        #endregion



        #region Commanding

        private void LogIn_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                // Always true; this is controlled via properties and style triggers
                e.CanExecute = true;
                e.Handled = true;
            }
            catch (Exception ex)
            {
                LogMessage(ex);
            }
        }

        private void LogIn_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                // Disconnect or Connect on login button press
                if (IsConnected || IsConnectionInProgress)
                {
                    Disconnect();
                }
                else
                {
                    HelperModel.SaveSettings(this);
                    LogMessage("Attempting to connect to " + CicServer + " as " + CicUsername);
                    _session.ConnectAsync(new SessionSettings(),
                        new HostSettings(new HostEndpoint(CicServer)),
                        new ICAuthSettings(CicUsername, CicPassword),
                        new WorkstationSettings(CicStation, SupportedMedia.Call), 
                        null, null);
                }
            }
            catch (Exception ex)
            {
                LogMessage(ex);
            }
        }

        #endregion



        #region Private Methods

        private void UpdateCicConnectionStateImage(ConnectionState state)
        {
            Context.Send(s =>
            {
                try
                {
                    // Determine which state image to show
                    var imageName = "";
                    switch (state)
                    {
                        case ConnectionState.None:
                            imageName = "bullet_square_glass_grey";
                            break;
                        case ConnectionState.Up:
                            imageName = "bullet_square_glass_green";
                            break;
                        case ConnectionState.Down:
                            imageName = "bullet_square_glass_red";
                            break;
                        case ConnectionState.Attempting:
                            imageName = "bullet_square_glass_yellow";
                            break;
                        default:
                            imageName = "bullet_square_glass_grey";
                            break;
                    }

                    // Load image
                    var newImage =
                            new BitmapImage(new Uri(String.Format("img/{0}.png", imageName),
                                UriKind.Relative));
                    newImage.Freeze();
                    // -> to prevent error: "Must create DependencySource on same Thread as the DependencyObject"
                    CicConnectionStateImage = newImage;
                }
                catch (Exception ex)
                {
                    Tracing.TraceException(ex, ex.Message);
                }
            }, null);
        }

        public override void Dispose()
        {
            Disconnect();

            base.Dispose();
        }

        #endregion



        #region Public Methods

        public void Disconnect()
        {
            try
            {
                // Disconnect and clean up
                if (_session.ConnectionState == ConnectionState.Up) _session.Disconnect();
            }
            catch (Exception ex)
            {
                LogMessage(ex);
            }
        }

        public void LogMessage(string message, [CallerMemberName] string callerName = null)
        {
            if (!string.IsNullOrEmpty(callerName)) callerName = "[" + callerName + "] -> ";
            var formattedMessage = string.Format("{0} - {1}{2}{3}", DateTime.Now.ToString(LogTimestampFormat), callerName, message, Environment.NewLine);
            Tracing.TraceAlways(formattedMessage);
            Context.Send(s =>
            {
                Log += formattedMessage;
            }, null);
        }

        public void LogMessage(Exception ex, string message = "", [CallerMemberName] string callerName = null)
        {

            if (string.IsNullOrEmpty(message))
                message = ex.Message;
            else
                message += " (" + ex.Message + ")";

            if (!string.IsNullOrEmpty(callerName)) callerName = "[" + callerName + "] -> ";
            var formattedMessage = string.Format("{0} - ERROR: {1}{2}{3}", DateTime.Now.ToString(LogTimestampFormat), callerName, message, Environment.NewLine);
            Tracing.TraceException(ex, formattedMessage);
            Context.Send(s =>
            {
                Log += formattedMessage;
            }, null);

        }

        #endregion


    }
}
