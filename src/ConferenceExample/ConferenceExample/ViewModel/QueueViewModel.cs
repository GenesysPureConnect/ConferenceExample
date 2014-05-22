using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ININ.IceLib;
using ININ.IceLib.Connection;
using ININ.IceLib.Interactions;

namespace ININ.Alliances.Examples.ConferenceExample.ViewModel
{
    public class QueueViewModel : ViewModelBase
    {
        private Session _session;
        private InteractionQueue _queue;

        private readonly string[] _queueAttributes =
        {
            InteractionAttributeName.InteractionId,
            InteractionAttributeName.InteractionType,
            InteractionAttributeName.State,
            InteractionAttributeName.StateDescription,
            InteractionAttributeName.UserQueueNames,
            InteractionAttributeName.RemoteAddress,
            InteractionAttributeName.RemoteId,
            InteractionAttributeName.RemoteName,
            InteractionAttributeName.LocalAddress,
            InteractionAttributeName.LocalId,
            InteractionAttributeName.LocalName,
            InteractionAttributeName.ConferenceId,
            InteractionAttributeName.Muted,
            InteractionAttributeName.Capabilities,
            InteractionAttributeName.StationQueueNames
        };

        private ObservableCollection<InteractionViewModel> _interactions = new ObservableCollection<InteractionViewModel>();
        private InteractionViewModel _selectedInteraction;
        private string _dialString = "3172222222";


        #region Public Properties

        public ObservableCollection<InteractionViewModel> Interactions
        {
            get { return _interactions; }
            set
            {
                _interactions = value;
                OnPropertyChanged();
            }
        }

        public ReadOnlyCollection<Interaction> SelectedInteractions
        {
            get
            {
                return
                    new ReadOnlyCollection<Interaction>(
                        Interactions.Where(interaction => interaction.IsSelected && !interaction.IsDisconnected)
                            .Select(interaction => interaction.Interaction).ToList());
            }
        }

        public InteractionViewModel SelectedInteraction
        {
            get { return _selectedInteraction; }
            set
            {
                _selectedInteraction = value;
                OnPropertyChanged();
                RaisePermissionProperties();
            }
        }

        public string DialString
        {
            get { return _dialString; }
            set
            {
                _dialString = value;
                OnPropertyChanged();
                OnPropertyChanged("CanDial");
            }
        }

        public bool CanPickup
        {
            get
            {
                try
                {
                    if (SelectedInteraction == null || SelectedInteraction.IsDisconnected ||
                        SelectedInteractions.Count > 1) return false;
                    return (SelectedInteraction.Interaction.Capabilities & InteractionCapabilities.Pickup) > 0;
                }
                catch (NotCachedException)
                {
                    // Supressing this error because it can be thrown in a race condition when WPF updates on an item that is being removed
                }
                catch (Exception ex)
                {
                    MainViewModel.Instance.LogMessage(ex);
                }
                return false;
            }
        }

        public bool CanMute
        {
            get
            {
                try
                {
                    if (SelectedInteraction == null || SelectedInteraction.IsDisconnected ||
                        SelectedInteractions.Count > 1) return false;
                    return (SelectedInteraction.Interaction.Capabilities & InteractionCapabilities.Mute) > 0;
                }
                catch (NotCachedException)
                {
                    // Supressing this error because it can be thrown in a race condition when WPF updates on an item that is being removed
                }
                catch (Exception ex)
                {
                    MainViewModel.Instance.LogMessage(ex);
                }
                return false;
            }
        }

        public bool CanHold
        {
            get
            {
                try
                {
                    if (SelectedInteraction == null || SelectedInteraction.IsDisconnected ||
                        SelectedInteractions.Count > 1) return false;
                    return (SelectedInteraction.Interaction.Capabilities & InteractionCapabilities.Hold) > 0;
                }
                catch (NotCachedException)
                {
                    // Supressing this error because it can be thrown in a race condition when WPF updates on an item that is being removed
                }
                catch (Exception ex)
                {
                    MainViewModel.Instance.LogMessage(ex);
                }
                return false;
            }
        }

        public bool CanConference
        {
            get
            {
                try
                {
                    if (SelectedInteractions.Count < 2) return false;
                    return
                        SelectedInteractions.All(
                            interaction => (interaction.Capabilities & InteractionCapabilities.Conference) > 0);
                }
                catch (NotCachedException)
                {
                    // Supressing this error because it can be thrown in a race condition when WPF updates on an item that is being removed
                }
                catch (Exception ex)
                {
                    MainViewModel.Instance.LogMessage(ex);
                }
                return false;
            }
        }

        public bool CanDisconnect
        {
            get
            {
                try
                {
                    if (SelectedInteraction == null || SelectedInteraction.IsDisconnected ||
                        SelectedInteractions.Count > 1) return false;
                    return (SelectedInteraction.Interaction.Capabilities & InteractionCapabilities.Disconnect) > 0;
                }
                catch (NotCachedException)
                {
                    // Supressing this error because it can be thrown in a race condition when WPF updates on an item that is being removed
                }
                catch (Exception ex)
                {
                    MainViewModel.Instance.LogMessage(ex);
                }
                return false;
            }
        }

        public bool CanDial
        {
            get { return !string.IsNullOrEmpty(DialString); }
        }

        #endregion


        public QueueViewModel(Session session)
        {
            try
            {
                // Initialize objects
                //Interactions = new ObservableCollection<InteractionViewModel>();

                // Set command bindings
                CommandBindings.Add(new CommandBinding(UiCommands.PickUpCommand,
                    PickUp_Executed,
                    (sender, e) =>
                    {
                        e.CanExecute = true;
                        e.Handled = true;
                    }));
                CommandBindings.Add(new CommandBinding(UiCommands.DisconnectCommand,
                    Disconnect_Executed,
                    (sender, e) =>
                    {
                        e.CanExecute = true;
                        e.Handled = true;
                    }));
                CommandBindings.Add(new CommandBinding(UiCommands.MuteCommand,
                    Mute_Executed,
                    (sender, e) =>
                    {
                        e.CanExecute = true;
                        e.Handled = true;
                    }));
                CommandBindings.Add(new CommandBinding(UiCommands.HoldCommand,
                    Hold_Executed,
                    (sender, e) =>
                    {
                        e.CanExecute = true;
                        e.Handled = true;
                    }));
                CommandBindings.Add(new CommandBinding(UiCommands.ConferenceCommand,
                    Conference_Executed,
                    (sender, e) =>
                    {
                        e.CanExecute = true;
                        e.Handled = true;
                    }));
                CommandBindings.Add(new CommandBinding(UiCommands.DialCommand,
                    Dial_Executed,
                    (sender, e) =>
                    {
                        e.CanExecute = true;
                        e.Handled = true;
                    }));

                // Set objects
                _session = session;
                _queue = new InteractionQueue(InteractionsManager.GetInstance(_session),
                    new QueueId(QueueType.MyInteractions, _session.UserId));
                _queue.QueueContentsChanged += QueueOnQueueContentsChanged;
                _queue.StartWatching(_queueAttributes);
            }
            catch (Exception ex)
            {
                MainViewModel.Instance.LogMessage(ex);
            }
        }



        #region Public Methods

        public void RaisePermissionProperties()
        {
            try
            {
                OnPropertyChanged("CanConference");
                OnPropertyChanged("CanDisconnect");
                OnPropertyChanged("CanHold");
                OnPropertyChanged("CanMute");
                OnPropertyChanged("CanPickup");
            }
            catch (Exception ex)
            {
                MainViewModel.Instance.LogMessage(ex);
            }
        }

        public override void Dispose()
        {
            try
            {
                if (_queue.IsWatching()) _queue.StopWatching();
            }
            catch (Exception ex)
            {
                MainViewModel.Instance.LogMessage(ex);
            }
            finally
            {
                base.Dispose();
            }
        }

        #endregion



        #region Private Methods

        private InteractionViewModel FindInteractionById(long id, IList<InteractionViewModel> conferenceList = null)
        {
            return conferenceList != null
                ? conferenceList.FirstOrDefault((item) => item.InteractionId == id)
                : Interactions.FirstOrDefault((item) => item.InteractionId == id);
        }

        #endregion



        #region Commanding

        private void PickUp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (!CanPickup) return;

                MainViewModel.Instance.LogMessage("Executing action: Pickup");
                SelectedInteraction.Pickup();
            }
            catch (Exception ex)
            {
                MainViewModel.Instance.LogMessage(ex);
            }
        }

        private void Disconnect_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (!CanDisconnect) return;

                MainViewModel.Instance.LogMessage("Executing action: Disconnect");
                SelectedInteraction.Disconnect();
            }
            catch (Exception ex)
            {
                MainViewModel.Instance.LogMessage(ex);
            }
        }

        private void Mute_Executed(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            try
            {
                if (!CanMute) return;

                MainViewModel.Instance.LogMessage("Executing action: Toggle Mute");
                SelectedInteraction.Mute();
            }
            catch (Exception ex)
            {
                MainViewModel.Instance.LogMessage(ex);
            }
        }

        private void Hold_Executed(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            try
            {
                if (!CanHold) return;

                MainViewModel.Instance.LogMessage("Executing action: Toggle Hold");
                SelectedInteraction.Hold();
            }
            catch (Exception ex)
            {
                MainViewModel.Instance.LogMessage(ex);
            }
        }

        private void Conference_Executed(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            try
            {
                if (!CanConference) return;

                var interactionsForConference = SelectedInteractions.ToArray();
                //var interactionsForConference = Interactions.Where(i => !i.IsDisconnected).Select(i=>i.Interaction).ToArray();
                MainViewModel.Instance.LogMessage("Making a conference with interactions: " +
                                                  interactionsForConference.Select(i => i.InteractionId.Id.ToString())
                                                      .Aggregate((current, next) => current + ", " + next));
                InteractionsManager.GetInstance(_session).MakeNewConference(interactionsForConference);
            }
            catch (Exception ex)
            {
                MainViewModel.Instance.LogMessage(ex);
            }
        }

        private void Dial_Executed(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            try
            {
                if (!CanDial) return;

                MainViewModel.Instance.LogMessage("Executing action: Dial (" + DialString + ")");
                InteractionsManager.GetInstance(_session)
                    .MakeCall(new CallInteractionParameters(DialString, CallMadeStage.None));

                //DialString = "";
            }
            catch (Exception ex)
            {
                MainViewModel.Instance.LogMessage(ex);
            }
        }

        #endregion



        #region IceLib Events

        private void QueueOnQueueContentsChanged(object sender, QueueContentsChangedEventArgs e)
        {
            try
            {
                //MainViewModel.Instance.LogMessage("Queue contents changed");

                // Add interactions
                foreach (var interaction in e.ItemsAdded)
                {
                    MainViewModel.Instance.LogMessage(string.Format("[{0}] Interaction Added", interaction.Interaction.InteractionId.Id));
                    Context.Send(s =>
                    {
                        try
                        {
                            Interactions.Add(new InteractionViewModel(interaction.Interaction));

                            // Select the interaction if there is only one non-disconnected one
                            var candidates = Interactions.Where(i => !i.IsDisconnected).ToList();
                            if (candidates.Count() == 1)
                                SelectedInteraction = candidates[0];
                        }
                        catch (Exception ex)
                        {
                            MainViewModel.Instance.LogMessage(ex);
                        }
                    }, null);
                }

                // Update interactions
                foreach (var interaction in e.ItemsChanged)
                {
                    var existingInteraction = FindInteractionById(interaction.Interaction.InteractionId.Id);

                    if (existingInteraction != null)
                    {
                        // Update properties
                        MainViewModel.Instance.LogMessage(
                            string.Format("[{0}] Interaction Changed with attributes: {1}",
                                interaction.Interaction.InteractionId.Id,
                            interaction.InteractionAttributeNames.Aggregate((current, next) => current + ", " + next)));
                        foreach (var property in interaction.InteractionAttributeNames)
                        {
                            existingInteraction.RaisePropertyChanged(property);
                        }
                    }
                    else
                    {
                        // Add because it's missing (this shouldn't happen)
                        MainViewModel.Instance.LogMessage(
                            string.Format("[{0}] Interaction Changed (adding to list) with attributes: {1}",
                                interaction.Interaction.InteractionId.Id,
                                interaction.InteractionAttributeNames.Aggregate((current, next) => current + ", " + next)));
                        Context.Send(s => Interactions.Add(new InteractionViewModel(interaction.Interaction)), null);
                    }

                    // Update this because something might have changed (like state or capabilities), but only if we have this interaction selected
                    if (SelectedInteractions.Any(i => interaction.Interaction.InteractionId.Id == i.InteractionId.Id))
                        RaisePermissionProperties();
                }

                // Remove interactions
                foreach (var interaction in e.ItemsRemoved)
                {
                    var existingInteraction = FindInteractionById(interaction.Interaction.InteractionId.Id);

                    if (existingInteraction != null)
                    {
                        MainViewModel.Instance.LogMessage(string.Format("[{0}] Interaction Removed", interaction.Interaction.InteractionId.Id));
                        Context.Send(s => Interactions.Remove(existingInteraction), null);
                    }
                    else
                    {
                        MainViewModel.Instance.LogMessage(string.Format("[{0}] Interaction Removed (was not in list)", interaction.Interaction.InteractionId.Id));
                    }
                }

                /* Conference member description:
                 * 
                 * ConferenceId - The ID of the conference object
                 * ConferenceItem - The interaction ID of the call in the conference to which the event is referring
                 * Interaction - The interaction ID of "you" in the conference
                 */

                // Add Conference Parties
                foreach (var conference in e.ConferenceItemsAdded)
                {
                    MainViewModel.Instance.LogMessage(
                        string.Format("Conference item added:{0}ConferenceId={1}{2}ConferenceItem={3}{4}Interaction={5}",
                            Environment.NewLine,
                            conference.ConferenceId, Environment.NewLine,
                            conference.ConferenceItem.InteractionId.Id, Environment.NewLine,
                            conference.Interaction.InteractionId.Id));

                    var existingInteraction = FindInteractionById(conference.Interaction.InteractionId.Id);

                    if (existingInteraction != null)
                    {
                        // Add conference party
                        Context.Send(
                            s =>
                            {
                                try
                                {
                                    existingInteraction.ConferenceParties.Add(
                                        new InteractionViewModel(conference.ConferenceItem));
                                }
                                catch (Exception ex)
                                {
                                    MainViewModel.Instance.LogMessage(ex);
                                }
                            }, null);
                    }
                    else
                    {
                        Context.Send(s =>
                        {
                            try
                            {
                                // Create interaction VM
                                var newInteraction = new InteractionViewModel(conference.Interaction);

                                // Add conference party to interaction
                                newInteraction.ConferenceParties.Add(new InteractionViewModel(conference.ConferenceItem));

                                // Add interaction to list
                                Interactions.Add(newInteraction);
                            }
                            catch (Exception ex)
                            {
                                MainViewModel.Instance.LogMessage(ex);
                            }
                        }, null);
                    }
                }

                // Update Conference Parties
                foreach (var conference in e.ConferenceItemsChanged)
                {
                    MainViewModel.Instance.LogMessage(
                        string.Format("Conference item changed:{0}ConferenceId={1}{2}ConferenceItem={3}{4}Interaction={5}{6}Attributes={7}",
                            Environment.NewLine,
                            conference.ConferenceId, Environment.NewLine,
                            conference.ConferenceItem.InteractionId.Id, Environment.NewLine,
                            conference.Interaction.InteractionId.Id, Environment.NewLine,
                            conference.InteractionAttributeNames.Aggregate((current, next) => current + ", " + next)));

                    // Find conference host interaction
                    var existingInteraction = FindInteractionById(conference.Interaction.InteractionId.Id);

                    if (existingInteraction != null)
                    {
                        // Find party in conference members
                        var conferenceParty = FindInteractionById(conference.ConferenceItem.InteractionId.Id,
                            existingInteraction.ConferenceParties);

                        if (conferenceParty == null)
                        {
                            // Couldn't find main conference party in list. This should never happen.
                            MainViewModel.Instance.LogMessage(
                                new Exception("Unable to find conference interaction " +
                                              conference.ConferenceItem.InteractionId.Id +
                                              "in cache."));
                        }
                        else
                        {
                            // Raise property changed event for each property
                            foreach (var property in conference.InteractionAttributeNames)
                            {
                                conferenceParty.RaisePropertyChanged(property);
                            }
                        }
                    }
                    else
                    {
                        // Couldn't find main interaction in list. This should never happen.
                        MainViewModel.Instance.LogMessage(
                            new Exception("Unable to find interaction " + conference.Interaction.InteractionId.Id +
                                          "in cache."));
                    }
                }

                // Remove Conference Parties
                foreach (var conference in e.ConferenceItemsRemoved)
                {
                    MainViewModel.Instance.LogMessage(
                        string.Format("Conference item removed:{0}ConferenceId={1}{2}ConferenceItem={3}{4}Interaction={5}",
                            Environment.NewLine,
                            conference.ConferenceId, Environment.NewLine,
                            conference.ConferenceItem.InteractionId.Id, Environment.NewLine,
                            conference.Interaction.InteractionId.Id));
                    // Find conference host interaction
                    var existingInteraction = FindInteractionById(conference.Interaction.InteractionId.Id);

                    if (existingInteraction != null)
                    {
                        // Find party in conference members
                        var conferenceParty = FindInteractionById(conference.ConferenceItem.InteractionId.Id,
                            existingInteraction.ConferenceParties);

                        if (conferenceParty == null)
                        {
                            // Couldn't find main interaction in list. This should never happen.
                            MainViewModel.Instance.LogMessage("Unable to find conference party " +
                                                              conference.ConferenceItem.InteractionId.Id);
                        }
                        else
                        {
                            Context.Send(s =>
                            {
                                try
                                {
                                    existingInteraction.ConferenceParties.Remove(conferenceParty);
                                }
                                catch (Exception ex)
                                {
                                    MainViewModel.Instance.LogMessage(ex);
                                }
                            },null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MainViewModel.Instance.LogMessage(ex);
            }
        }

        #endregion
    }
}
