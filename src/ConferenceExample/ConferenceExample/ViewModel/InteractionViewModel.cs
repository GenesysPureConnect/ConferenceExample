using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ININ.IceLib.Interactions;

namespace ININ.Alliances.Examples.ConferenceExample.ViewModel
{
    public class InteractionViewModel : ViewModelBase
    {
        private readonly Interaction _interaction;
        private bool _isSelected;
        private ObservableCollection<InteractionViewModel> _conferenceParties = new ObservableCollection<InteractionViewModel>();

        internal Interaction Interaction { get { return _interaction; } }



        #region Public Properties

        public ObservableCollection<InteractionViewModel> ConferenceParties
        {
            get { return _conferenceParties; }
            set
            {
                _conferenceParties = value; 
                OnPropertyChanged();
            }
        }

        public string this[string attributeName]
        {
            get { return _interaction != null ? _interaction.GetStringAttribute(attributeName) : ""; }
        }

        public long InteractionId
        {
            get { return _interaction.InteractionId.Id; }
        }

        public string InteractionType
        {
            get { return _interaction.InteractionType.ToString(); }
        }

        public string State
        {
            get { return _interaction.State.ToString(); }
        }

        public string StateDescription
        {
            get { return _interaction.StateDescription; }
        }

        public string UserQueueNames
        {
            get { return _interaction.UserQueueNames.Count > 0 ? _interaction.UserQueueNames[0] : ""; }
        }

        public string RemoteAddress
        {
            get { return _interaction.RemoteAddress; }
        }

        public string RemoteId
        {
            get { return _interaction.RemoteId; }
        }

        public string RemoteName
        {
            get { return _interaction.RemoteName; }
        }

        public string LocalAddress
        {
            get { return _interaction.LocalAddress; }
        }

        public string LocalId
        {
            get { return _interaction.LocalId; }
        }

        public string LocalName
        {
            get { return _interaction.LocalName; }
        }

        public long ConferenceId
        {
            get { return _interaction.ConferenceId.Id; }
        }

        public bool IsInConference { get { return _interaction.ConferenceId.Id > 0; } }

        public bool CanConferenceThisInteraction
        {
            get
            {
                // Determine if the interaction has the capability to be conferenced
                var conferenceCapabilitiy = (_interaction.Capabilities & InteractionCapabilities.Conference) > 0;

                // If it doesn't, make sure this interaction isn't selected for conferencing since the checkbox would be hidden
                if (!conferenceCapabilitiy) IsSelected = false;

                // Return true only if the interaction has the capability and the interaction isn't already in a conference
                return conferenceCapabilitiy && !IsInConference;
            }
        }

        public bool IsOtherConferenceParty { get { return IsInConference && !_interaction.IsOnMyInteractionsQueue; } }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged();

                // This will trigger changes to the CanXXX properties. This is necessary to call here because we don't get any events for multi-select
                MainViewModel.Instance.QueueViewModel.RaisePermissionProperties();
            }
        }

        public bool IsConference { get; set; }

        public bool IsDisconnected { get { return _interaction.IsDisconnected; } }

        public bool IsConnected { get { return _interaction.IsConnected; } }

        #endregion



        public InteractionViewModel(Interaction interaction)
        {
            _interaction = interaction;
        }



        #region Public Methods

        public void RaisePropertyChanged(string name)
        {
            //MainViewModel.Instance.LogMessage(string.Format("[{0}] Interaction property changed: {1}", InteractionId, name));

            // Map call attribute name to property name
            if (name.Equals(InteractionAttributeName.InteractionId)) OnPropertyChanged("InteractionId");
            else if (name.Equals(InteractionAttributeName.InteractionType)) OnPropertyChanged("InteractionType");
            else if (name.Equals(InteractionAttributeName.State))
            {
                OnPropertyChanged("State");
                // Raise meta properties (value created from state)
                OnPropertyChanged("IsDisconnected");
                OnPropertyChanged("IsConnected");
                OnPropertyChanged("CanConferenceThisInteraction");
            }
            else if (name.Equals(InteractionAttributeName.StateDescription)) OnPropertyChanged("StateDescription");
            else if (name.Equals(InteractionAttributeName.UserQueueNames)) OnPropertyChanged("UserQueueNames");
            else if (name.Equals(InteractionAttributeName.RemoteAddress)) OnPropertyChanged("RemoteAddress");
            else if (name.Equals(InteractionAttributeName.RemoteId)) OnPropertyChanged("RemoteId");
            else if (name.Equals(InteractionAttributeName.RemoteName)) OnPropertyChanged("RemoteName");
            else if (name.Equals(InteractionAttributeName.LocalAddress)) OnPropertyChanged("LocalAddress");
            else if (name.Equals(InteractionAttributeName.LocalId)) OnPropertyChanged("LocalId");
            else if (name.Equals(InteractionAttributeName.LocalName)) OnPropertyChanged("LocalName");
            else if (name.Equals(InteractionAttributeName.ConferenceId)) OnPropertyChanged("ConferenceId");
            else if (name.Equals(InteractionAttributeName.Capabilities)) OnPropertyChanged("CanConferenceThisInteraction");
            else OnPropertyChanged(name);
        }

        public void Pickup()
        {
            if (_interaction != null && !_interaction.IsDisconnected)
                _interaction.Pickup();
        }

        public void Disconnect()
        {
            if (_interaction != null && !_interaction.IsDisconnected)
                _interaction.Disconnect();
        }

        public void Hold()
        {
            if (_interaction != null && !_interaction.IsDisconnected)
                _interaction.Hold(!_interaction.IsHeld);
        }

        public void Mute()
        {
            if (_interaction != null && !_interaction.IsDisconnected)
                _interaction.Mute(!_interaction.IsMuted);
        }

        #endregion
    }
}
