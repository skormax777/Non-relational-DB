   M S B A M L   `   `   `  ����>  :WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WPFWPF  5   	       WPF.Windows.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:WPF     5   	   $	��600��5   	   $
��1000��6     � WindowStartupLocation$  CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	   $��Georgia)�5   	   .��5      � -   5        
������� � ColumnDefinitions 5   
   �� 5      $	��800 �6   �� 5      $	��200 �6   .+�5   
   � ���	#FF243378.+�5      � -   5         �Name$ MainGrid��5      $��Center=�6%   $��Center=�6B   $��0��6]   5      � 5   
   ���	#FF243378$��1��6&   .+�5      �� -   5         ��Name$ ListViewMenu��5      $��0 40 0 0q�6-   
������ $��2q�6X   
���   �$��18(�6    �� SelectedIndex$ 0��6�   
�������.@�5      �� $��70��6   
�������.��5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� $��Timeline��6$   $��17(�64   $��Center=�6B   $
��20 0q�6]   
�������6k   5      5      �� 5       $��70��6   
�������
�������.��5!      �� $ 
Horizontal=�6!   .+�5"      �� $��About Me��6$   $��17(�64   $��Center=�6B   $
��20 0q�6]   
�������6k   5#      5$      �� 5%      $��70��6   
�������
�������.��5&      �� $ 
Horizontal=�6!   .+�5'      �� $��Settings��6$   $��17(�64   $��Center=�6B   $
��20 0q�6]   
�������6k   5(      5)      5*      �� 5+      -   5         ��Name$ txtLogin��5+      $��20,310,30,230q�6(   $��Name of friend��6?   $��15(�6U   
�������
�������6u   �� 5,      -   5         ��Name$ 	btnSearch��5,      $��Search��6(   ���	#FF243378$��20,350,30,190q�6h   
�������
�������$��13(�6�   �� 5-      -   5        $ 	btnLogout��5-      $��Log Out��6(   ���	#FF243378$��20,520,30,20q�6i   
�������
�������$��13(�6�   5.      5/      50      