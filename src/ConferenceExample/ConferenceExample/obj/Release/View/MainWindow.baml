   M S B A M L   `   `   `  ���� L  HConferenceExample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullyAclr-namespace:ININ.Alliances.Examples.ConferenceExample.ViewModel3ININ.Alliances.Examples.ConferenceExample.ViewModel  �Gclr-namespace:ININ.Alliances.Examples.ConferenceExample.View.Supporting9ININ.Alliances.Examples.ConferenceExample.View.Supporting  ?    9ININ.Alliances.Examples.ConferenceExample.View.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     W	viewModelAclr-namespace:ININ.Alliances.Examples.ConferenceExample.ViewModel     ^
supportingGclr-namespace:ININ.Alliances.Examples.ConferenceExample.View.Supporting     -     � Title$  Conference Example��$	��600��$	��940�� � Icon$. (/ConferenceExample;component/General.ico�� � 	Resources  �� 
TargetType �� Property �� Value   
AuthLabels �� BasedOn  AuthBoxStyle �� Triggers �� Binding	 �� Value  AuthPwBoxStyle  SectionHeaderBorderStyle  SectionHeaderTextStyle  MediaTypeOptionStyle  FileFormatOptionStyle  ConnectButtonStyle  ExportOperationStyle
 �� RelativeSource �� AncestorType �� Mode �� Path 	 ExportButtonStyle 
 LogStyle  NoChromeButton �� 
TargetType �Name J� SnapsToDevicePixels �� Triggers P� Property P� Value �� 
TargetName  CallControlButtonStyle  PickupButtonStyle �� ImageSource j� TileMode j� Stretch j� 
AlignmentX j� 
AlignmentY ^� 
Conditions �� Binding �� Value  DisconnectButtonStyle  MuteButtonStyle  HoldButtonStyle  ConferenceButtonStyle  DialButtonStyle6  0System.Windows.Controls.GridViewColumnCollection  QueueColumnCollection �� Width �� CellTemplate d� 	IsChecked  �� DisplayMemberBinding  ExpandCollapseToggleStyle! �� CornerRadius" �� 
StartPoint# �� EndPoint$ � Color% � Offset& F�NameV   PININ.Alliances.Examples.ConferenceExample.View.Supporting.LevelToIndentConverter  LevelToIndentConverter  CellTemplate_Name' d�Name( �� 	ClickMode) �� 	ConverterP   JININ.Alliances.Examples.ConferenceExample.View.Supporting.TreeListViewItem* �� Triggers+ �� Padding, ��Name- �� Content. �� ColumnsL   FININ.Alliances.Examples.ConferenceExample.View.Supporting.TreeListView/ ��Name0 ]� 
Conditions1 �� Propertya   [ININ.Alliances.Examples.ConferenceExample.View.Supporting.CommandBindingCollectionConverter $  RegisterCommandBindingsConverter��%(  '��       '�� R     '�� �     &	  �     7��&	 �     7��&	 �    &	 g    &	 �    &	 �    &	 8    &	 t    &	 �    &		 �    &	
 �    '�� l    &	 �    &	 '    7  &	 z    7  &	 "    7  &	 �    7  &	 x    7  &	     7  &	 �    7  &	 �    &	      &	 B"    &	 G"    7  7  '  H#    '  &    &	 �&    �� " ��.���  � ��$ 2q���  � ��$ Center=���  � ��$ 22���� " ��.���  � ��$ 2q���  � ��$ Center=��� " ��.���  � ��$ 2q��� " ��8   .���  � ��$ 60����  � ��$
 Left=��� " ��8    ��  �� *IsConnectionInProgress+	 True.a���  � {� .  ��  �� *IsConnected+	 True.a���  � {� .  �� " G�.���  � ��$ 2q���  � ��$ Center=���  � ��$ 22�� ��  �� *IsConnectionInProgress+	 True.a���  � {� .  ��  �� *IsConnected+	 True.a���  � {� .  �� " ��.���  � ��$	 0,3q���  � ��
 �������  � ��$ 0,0,0,1q��� " ��.���  � ��$
 Bold"���  � ��$	 0,1q��� " ��.���  � ��$	 2,1q���  � ��$ Center=��� " ��.���  � ��
 �������  � ��$	 3,1q��� " ��.���  � ��$ 22����  � ��$ 4q� ��  �� *IsConnectionInProgress+	 True.a���  � {� .  �� " ]�.���  � {� .   ��  �� 
 �� $ WindowL�$ FindAncestor=�$ DataContext.IsConnected�	 True.a���  � {� . �� " ��.���  � ��$ 22����  � ��$ Right=���  � ��$	 8,2q���  � ��$	 2,6q��� " ��.���  �@��TextWrapping$
 Wrap=���  �@��
IsReadOnly . ��  � �� �	#09000000�� " ��.���  � ��$	 7,2q��� " ��.���  � ��
 ���� ��  � ��$ 1q���  � ��# � ����  � ��$ Center=���  � ��$ Center=���  � ��$ 0q���  �@�Cursor$
 Hand����  � ��$ 0q���  � �� �� " ��.R�� $ Chrome��#��z�� . .+��� #��z��#��z����. # z #��z�� P�  � {� .  . ���  � �� �#ADADAD�� $ Chrome�� �@J�Opacity$	 0.5Q��� " ��8   .���  � ��$ 4q���  � ��$ 70����  � ��$ 40���� " ��8   .���  � ��$( "viewModel:UiCommands.PickUpCommand����  � �� � $��Stretch=�$��Stretch=����� 
 �� $ ButtonL�$ FindAncestor=�$ Height����� 
 �� $ ButtonL�$ FindAncestor=�$ Width�.+��� $��Bottom=�$��Center=����� 
 �� $ FindAncestor=�" ��$ Command.Text���  � �� �� $* $/img/callcontrols/Pick Up 24 d m.png��$
 None=�$
 None=�$ Center=�$	 Top=���  � ��
 �������  �@�Cursor$ No�� ^�  ��  �� 
 �� *Self+$ IsMouseOver� true��  �� $ 	CanPickup� true.0���  � �� �� $* $/img/callcontrols/Pick Up 24 h m.png��$
 None=�$
 None=�$ Center=�$	 Top=���  �� $ 	CanPickup�	 true.a���  � �� �� $* $/img/callcontrols/Pick Up 24 n m.png��$
 None=�$
 None=�$ Center=�$	 Top=���  � ��
 �   ���  �@�Cursor$
 Hand���� " ��8   .���  � ��$, &viewModel:UiCommands.DisconnectCommand����  � �� � $��Stretch=�$��Stretch=����� 
 �� $ ButtonL�$ FindAncestor=�$ Height����� 
 �� $ ButtonL�$ FindAncestor=�$ Width�.+��� $��Bottom=�$��Center=����� 
 �� $ FindAncestor=�" ��$ Command.Text���  � �� �� $- '/img/callcontrols/Disconnect 24 d m.png��$
 None=�$
 None=�$ Center=�$	 Top=���  � ��
 �������  �@�Cursor$ No�� ^�  ��  �� 
 �� *Self+$ IsMouseOver� true��  �� $ CanDisconnect� true.0���  � �� �� $- '/img/callcontrols/Disconnect 24 h m.png��$
 None=�$
 None=�$ Center=�$	 Top=���  �� $ CanDisconnect�	 true.a���  � �� �� $- '/img/callcontrols/Disconnect 24 n m.png��$
 None=�$
 None=�$ Center=�$	 Top=���  � ��
 �   ���  �@�Cursor$
 Hand���� " ��8   .���  � ��$&  viewModel:UiCommands.MuteCommand����  � �� � $��Stretch=�$��Stretch=����� 
 �� $ ButtonL�$ FindAncestor=�$ Height����� 
 �� $ ButtonL�$ FindAncestor=�$ Width�.+��� $��Bottom=�$��Center=����� 
 �� $ FindAncestor=�" ��$ Command.Text���  � �� �� $' !/img/callcontrols/Mute 24 d m.png��$
 None=�$
 None=�$ Center=�$	 Top=���  � ��
 �������  �@�Cursor$ No�� ^�  ��  �� 
 �� *Self+$ IsMouseOver� true��  �� $ CanMute� true.0���  � �� �� $' !/img/callcontrols/Mute 24 h m.png��$
 None=�$
 None=�$ Center=�$	 Top=���  �� $ CanMute�	 true.a���  � �� �� $' !/img/callcontrols/Mute 24 n m.png��$
 None=�$
 None=�$ Center=�$	 Top=���  � ��
 �   ���  �@�Cursor$
 Hand���� " ��8   .���  � ��$&  viewModel:UiCommands.HoldCommand����  � �� � $��Stretch=�$��Stretch=����� 
 �� $ ButtonL�$ FindAncestor=�$ Height����� 
 �� $ ButtonL�$ FindAncestor=�$ Width�.+��� $��Bottom=�$��Center=����� 
 �� $ FindAncestor=�" ��$ Command.Text���  � �� �� $' !/img/callcontrols/Hold 24 d m.png��$
 None=�$
 None=�$ Center=�$	 Top=���  � ��
 �������  �@�Cursor$ No�� ^�  ��  �� 
 �� *Self+$ IsMouseOver� true��  �� $ CanHold� true.0���  � �� �� $' !/img/callcontrols/Hold 24 h m.png��$
 None=�$
 None=�$ Center=�$	 Top=���  �� $ CanHold�	 true.a���  � �� �� $' !/img/callcontrols/Hold 24 n m.png��$
 None=�$
 None=�$ Center=�$	 Top=���  � ��
 �   ���  �@�Cursor$
 Hand���� " ��8   .���  � ��$, &viewModel:UiCommands.ConferenceCommand����  � �� � $��Stretch=�$��Stretch=����� 
 �� $ ButtonL�$ FindAncestor=�$ Height����� 
 �� $ ButtonL�$ FindAncestor=�$ Width�.+��� $��Bottom=�$��Center=����� 
 �� $ FindAncestor=�" ��$ Command.Text���  � �� �� $6 0/img/callcontrols/Make Conference 24 d m_add.png��$
 None=�$
 None=�$ Center=�$	 Top=���  � ��
 �������  �@�Cursor$ No�� ^�  ��  �� 
 �� *Self+$ IsMouseOver� true��  �� $ CanConference� true.0���  � �� �� $6 0/img/callcontrols/Make Conference 24 h m_add.png��$
 None=�$
 None=�$ Center=�$	 Top=���  �� $ CanConference�	 true.a���  � �� �� $6 0/img/callcontrols/Make Conference 24 n m_add.png��$
 None=�$
 None=�$ Center=�$	 Top=���  � ��
 �   ���  �@�Cursor$
 Hand���� " ��8   .���  � ��$ 16����  � ��$ 16����  � ��$&  viewModel:UiCommands.DialCommand����  � �� �� $. (/img/callcontrols/Make a Call 16 d m.png��$
 None=�$
 None=�$ Center=�$	 Top=���  � ��
 �������  �@�Cursor$ No�� ^�  ��  �� 
 �� *Self+$ IsMouseOver� true��  �� $ QueueViewModel.CanDial� true.0���  � �� �� $. (/img/callcontrols/Make a Call 16 h m.png��$
 None=�$
 None=�$ Center=�$	 Top=���  �� $ QueueViewModel.CanDial�	 true.a���  � �� �� $. (/img/callcontrols/Make a Call 16 n m.png��$
 None=�$
 None=�$ Center=�$	 Top=���  � ��
 �   ���  �@�Cursor$
 Hand��  �� $��
IsSelected��$
 auto�� �� .R��� $��Center=�$��Center=� �� *
IsSelected+�� $��InteractionId��$
 auto��  �� *InteractionId+�� $��InteractionType��$
 auto��  �� *InteractionType+�� $��State��$
 auto��  �� *State+�� $��StateDescription��$
 auto��  �� *StateDescription+�� $��UserQueueNames��$
 auto��  �� *UserQueueNames+�� $��RemoteAddress��$
 auto��  �� *RemoteAddress+�� $��RemoteId��$
 auto��  �� *
RemoteId+�� $��
RemoteName��$
 auto��  �� *
RemoteName+�� $��LocalAddress��$
 auto��  �� *LocalAddress+�� $��LocalId��$
 auto��  �� *	LocalId+�� $��	LocalName��$
 auto��  �� *	LocalName+�� $��ConferenceId��$
 auto��  �� *ConferenceId+�� " d�.���  � |� .  ��  � ��$ 19����  � ��$ 13����  � �� �� " d�.R��� $��19��$��13��
������ ._��� $��9��$��9��$��1q����	#FF7898B5$! 1�� . ���� $	" 0,0&�$	# 1,1&���� $$ White��$% .2Q�� $$ 	#FFC0B7A6��$% 1Q�._�F� $& 
ExpandPath��$��1,1,1,1q�
���   �s����    �� �    ��- р� ��- р� @KL ���- @KL ���- ��- �@KL ��- �@KL �� ���- �� ���-     р�     р� �� 	 P�  �@d�	IsChecked$
 TrueY�. ��� $ 
ExpandPath�� � ��+ ��    �� �    ��- �@KL ��- �@KL �� 	  �� .R�]� .+�d� $' Expander��8��  $( Press=����� *Level+8)  
 �� "   �� $ 
IsExpanded�
 �� "  �� ���� *Name+* �� $	 False�� �� $ HasItems�
 �� "  .a��� $ Expander�� � y�$ Hidden=��� "  .���  � �� �� "  .R��� .+��� $��Bd��#��z��#��z��#��z��#+ z��._��� $, PART_Header��#- z��. �� $ Columns�
 �� "  �� $/ 	ItemsHost�� P�  �@Q�
IsExpanded .  . ��� $ 	ItemsHost�� � y�$ 	Collapsed=�]� 0 �� 1 � �� .  �� 1 � ��$
 Auto��./��� $ PART_Header�� � ��$ 75��]� 0 �� 1 � �� .  �� 1 � ��$
 Auto��./��� $ PART_Header�� � ��$ 19��P�  �@Q�
IsSelected . . ��� $ Bd��# � �� � ���� # � �� � ��]� 0 �� 1 �@Q�
IsSelected . �� 1 �@Q�IsSelectionActive .  ./��� $ Bd��# � �� � ���� # � �� � ��P�  � {� .  . ��� # � �� � ���� "  .���  � �� �� "  .R��� #��z��#��z��._�]� .+��� $	��Top=�. �� $ Columns�
 �� *TemplatedParent+��   .��� U   OININ.Alliances.Examples.ConferenceExample.View.Supporting.CommandBindingHelpers2   RegisterCommandBindings2 `� 3 `� 	Converter#3 [ .1��� $! DataContext.CommandBindings�
 �� $ FindAncestor=�$ WindowL��� $0 *DataContext.QueueViewModel.CommandBindings�
 �� $ FindAncestor=�$ WindowL�.+�]� .+��� 4 �� Orientation$4 Vertical=�$
��Left=�$	��240��.+��� ���� $4 
Horizontal=�.+��� $��16��$��16������ *CicConnectionStateImage+5 � ToolTip5 �� $	��200��$
��Wrap=����� *ConnectionStateString+�� CIC Authentication.���� $4 Vertical=�.+�]� .+��� #��[  
Username�� #��[ ���� $ CicUsername�6 �� UpdateSourceTrigger$6 PropertyChanged=�]� .+��� #��[  
PasswordG� -   $��CicPasswordBox��#��[ ]� .+��� #��[  	Station�� #��[ ���� $ 
CicStation�$6 PropertyChanged=�]� .+��� #��[  Server�� #��[ ���� $ 	CicServer�$6 PropertyChanged=��� $'��!viewModel:UiCommands.LogInCommand��#��[ ���� *ConnectButtonText+�� $��Interaction Queue��.��� 7 � RowDefinitions7 �� $��30 ��� $	��180 ��� $��* �.+�]� $��0��.+��� $
��Left=�$��Center=�.��Dial:�� #��[ $��Right=�$��Center=�$	��4,0q��� $��Center=�$	��2,0q����� $ QueueViewModel.DialString�$6 PropertyChanged=��� $��1��
�������$��1q����#F9F9F98 � DataContext8 �� $ QueueViewModel�._�]� .+��� $4 
Horizontal=�$��Bottom=�.+��� #��[ �� #��[ �� #��[ �� #��[ �� #��[ �� $
��Auto=�$
��Auto=�$��0q�.��]� .+�  L   FININ.Alliances.Examples.ConferenceExample.View.Supporting.TreeListView9   SelectedItemChanged$$9 TreeView_OnSelectedItemChanged��$��Stretch=�$��0q�:   Columns#: [ ���� *Interactions+���� ; �� ItemsSource; �� *ConferenceParties+.R��� #. [ - �� �� $��2��$��Stretch=�$	��Top=�$��6��$	��0,2q�
��������� -   < ��Name$	< Log��$
��Auto=�$��Disabled=�$��0,10,0,0q�$��2��.���� 
�������._��� #��[
 $��0q����� 
 �� $ WindowL�$ FindAncestor=�$ DataContext.Log�