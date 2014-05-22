using System.Windows.Input;

namespace ININ.Alliances.Examples.ConferenceExample.ViewModel
{
    public static class UiCommands
    {

        public static RoutedUICommand LogInCommand = new RoutedUICommand("Log In", "LogInCommad", typeof(UiCommands));

        public static RoutedUICommand PickUpCommand = new RoutedUICommand("Pick Up", "PickUpCommand", typeof(UiCommands));
        public static RoutedUICommand DisconnectCommand = new RoutedUICommand("Disconnect", "DisconnectCommand", typeof(UiCommands));
        public static RoutedUICommand HoldCommand = new RoutedUICommand("Hold", "HoldCommand", typeof(UiCommands));
        public static RoutedUICommand DialCommand = new RoutedUICommand("Dial", "DialCommand", typeof(UiCommands));
        public static RoutedUICommand ConferenceCommand = new RoutedUICommand("Conference", "ConferenceCommand", typeof(UiCommands));
        public static RoutedUICommand MuteCommand = new RoutedUICommand("Mute", "MuteCommand", typeof(UiCommands));
    }
}
