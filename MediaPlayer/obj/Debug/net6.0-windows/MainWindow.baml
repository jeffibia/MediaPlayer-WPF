   M S B A M L   `   `   `  ����F  BMediaPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MediaPlayerMediaPlayer  5   	       MediaPlayer.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:MediaPlayer     5   	     � Title$  Media Player��5   	   $	��450��6   $	��800��6+   ���#000000.��5	      �  � ColumnDefinitions 5
   
   �� 5      $��3* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   .+�5   
   � $��0��6   $��0��5      $��2��5      ���#1D1D1D.+�5      �� $��16(�6   $��Center=�6,   $��Roboto Black)�6I   $��20(�6c   ���#F2E7FE��6�   �� *
AppTitle+6�   5      � 5   
   $��1��6   $��0��5      ���#1D1D1D$��5 5 0 0q�5       .+�5"      r� -   5        $��MediaPlayerElement��5"       r� Source$ assets\video.mp4?�62    r� LoadedBehavior$ Manual=�6L   5%      �� 5'   
   -   5        $��lstPlaylist��5'      $��2��5(      $��Roboto Black)�5)      ���#BB86FC$��14(�5+      ���#1D1D1D$��5 5 5 0q�5-      $��0q�5.      $��1��51       �� SelectionMode$ Single=�52      $
��Auto=�54      ��5/      �� *
Playlist+ �� SelectedItem 50      �� *SelectedMedia+ �� SelectedIndex 53      �� *SelectedIndex+��55      �� 56      .R�57      �� ���#BB86FC$��14(�6Q   ��65   ��  �� Path$ Title�6Q   58      5:      �� 5=   
   	 �� Orientation$	 
Horizontal=�5>      $��2��5?      $��2��5@      ���#1D1D1D$��5 5 5 5q�5B      .+�5D      �� 
������ 
������ $��0q�6�   $|�False��6�   ��6   �� ��62   �� *	AddFolder+
 � FocusVisualStyle
 6�   Z� .��5E      (� $��5q�6   .��5F      �� $��48��6   $��48��6'      
add-folder#��[  63   6g   5G      5H      �� 5I      
������ 
������ $��0q�6�   $|�False��6�   ��6   �� ��62   �� *	AddFile+
 6�   Z� .��5J      (� $��5q�6   .��5K      �� $��48��6   $��48��6'     add-file#��[ 63   6e   5L      5M      �� 5N      
������ 
������ $��0q�6�   $|�False��6�    K� ZIndex$ 90��6�   ��6   �� ��62   �� *RemoveMedia+.��5O      (� $��5q�6   .��5P      �� $��48��6   $��48��6'    
 remove#��[ 63   6c   5Q      5R      5S      �� 5U   
   $��3��6   $	 
Horizontal=�5V      ���#1D1D1D$��5 5 0 5 q�5W   *   .+�5X      �� 
������ 
������ $��0q�6�   ��6   �� ��62   �� *
Previous+
 6�   Z� .��5Y      (� $��5q�6   .��5Z      �� $��42��6   $��42��6'     previous#��[ 63   6e   5[      5\      �� 5]      
������ 
������ $��0q�6~   ��6   �� ��62   �� *Play+
 6�   Z� .��5^      (� $��5q�6   .��5_      �� $��42��6   $��42��6'     play#��[ 63   6a   5`      5a      �� 5b      
������ 
������ $��0q�6   ��6   �� ��62   �� *Pause+
 6�   Z� .��5c      (� $��5q�6   .��5d      �� $��42��6   $��42��6'    	 pause#��[ 63   6b   5e      5f      �� 5g      
������ 
������ $��0q�6~   ��6   �� ��62   �� *Stop+
 6�   Z� .��5h      (� $��5q�6   .��5i      �� $��42��6   $��42��6'     stop#��[ 64   6b   5j      5k      �� 5l      
������ 
������ $��0q�6~   ��6   �� ��62   �� *Next+
 6�   Z� .��5m      (� $��5q�6   .��5n      �� $��42��6   $��42��6'     next#��[ 63   6a   5o      5p      �� 5q      $��Right=�6   $	 
Horizontal=�65   $��Center=�6N   $	��300��6i   .+�5r      �� $��Roboto Black)�6   $��16(�62   ���#F2E7FE��6V   �� *SelectedMedia.Duration+5s      5t      5v      5w      