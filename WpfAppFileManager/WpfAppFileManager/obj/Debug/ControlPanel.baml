   M S B A M L   `   `   `  ����  WpfAppFileManager$    WpfAppFileManager.ControlPanel   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5        .��5	      � .+�5
   
   �� $��4q�6   $��7��6    .+�5      c� $��3q�6   $	��Top=�6"   ���	#FFD3E0F0.@�5      �� -   5          ��Name$  buttonRefresh��5      $��24��60   $��24��6;   .��5      �� $(��"Icons Resource/circle100_64x64.png��6   $
��Left=�6H    � ToolTip 5      _� 5      .��5      �� 	Refresh61   5      5      �� 5      #��["�6   $
��Left=�6Z   �� 5      -   5        $  buttonUp��5      $��24��6+   $��24��66   $
��Left=�6B   .��5      �� $$��Icons Resource/up150_64x64.png��6   $��UniformToFill=�6E    5      _� 5      .��5      �� Up directory66   5      5      �� 5      -   5        $  
buttonRoot��5      $��24��6-   $��24��68   $
��Left=�6D   .��5      �� $&�� Icons Resource/home145_64x64.png��6   $��UniformToFill=�6F    5       _� 5!      .��5"      �� Root6.   5#      5%      �� 5&      -   5        $  
buttonCopy��5&      $��24��6-   $��24��68   $
��Left=�6D   .��5'      �� $%��Icons Resource/copy30_64x64.png��6   $��UniformToFill=�6F    5(      _� 5)      .��5*      �� Copy6.   5+      5-      �� 5.      -   5        $  
buttonMove��5.      $��24��6-   $��24��68   $
��Left=�6D   .��5/      �� $'��!Icons Resource/arrows96_64x64.png��6   $��UniformToFill=�6G    50      _� 51      .��52      �� Move6.   53      55      �� 56      -   5        $  buttonDelete��56      $��24��6/   $��24��6:   $
��Left=�6F   $��Bottom=�6a   .��57      �� $(��"Icons Resource/recycle70_64x64.png��6   $��UniformToFill=�6H    58      _� 59      .��5:      �� Delete60   5;      5=      �� 5>      #��["�6   $
��Left=�6Z   �� 5?      -   5        $  buttonSearch��5?      $��24��6/   $��24��6:   $
��Left=�6F   .��5@      �� $'��!Icons Resource/search95_64x64.png��6   $��UniformToFill=�6H    5A      _� 5B      .��5C      �� Search60   5D      5F      �� 5G      #��["�6   $
��Left=�6Z   �� 5H      -	   5        $  buttonNewFolder��5H      $��24��62   $��24��6=   $
��Left=�6I   .��5I      �� $(��"Icons Resource/folder209_64x64.png��6   $��UniformToFill=�6H    5J      _� 5K      .��5L      �� 
New folder64   5M      5O      �� 5P      -
   5        $  buttonCompare��5P      $��24��60   $��24��6;   $
��Left=�6G   .��5Q      �� $*��$Icons Resource/checkmark21_64x64.png��6   $��UniformToFill=�6J    5R      _� 5S      .��5T      �� 	Compare61   5U      5W      �� 5X      -   5         ��Name$ comboBoxDrives��5X      $��24��63   $��Right=�6?   $��Right=�6V   $��50��6r    �� 
IsReadOnly . $��Center=�6�    �� SelectedIndex$ 0��6�   $��30,0,0,0q�6�   5Z      5[      �� 5]      -   5         ��Name$ textBoxPath��5]      $��23��6+    �� TextWrapping$
 Wrap=�67   $��TextBox��6K   $��0q�6Z   &   System.Windows.Controls.DataGrid  5_      -   5          Name$ contentGrid��5_         CanUserResizeColumns . 	   
IsReadOnly	 . 
   CanUserSortColumns
 . ���	#FFD4D4D4
�������   AutoGenerateColumns .     HorizontalGridLinesBrush �	#FFD1D1D1   VerticalGridLinesBrush �	#FFD1D1D1   HeadersVisibility$ Column=�5b   :   "   VerticalScrollBarVisibility$
 Auto=�5c   G   $��Stretch=�5d      $��Stretch=�6:      Columns 5f      4  .System.Windows.Controls.DataGridCheckBoxColumn  5g      -   5        checkBox��,  &System.Windows.Controls.DataGridColumn   Header$
 Type��5g   ?      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6M      CanUserResize .  1  +System.Windows.Controls.DataGridBoundColumn   Binding 6Z   �� *isDirectory+   ElementStyle 5h      �� 5i       �� 
TargetType" ��6$   .�5j   "   ��  �� Property � �� �� Value 5k   &   �� 5l   *    �� 
TargetType" ��6;   .R�5m   .   ��  �� Orientation$ 
Horizontal=�69   $��Center=�6R   .+�5n   2   ��  ��Name$ checkboxImage��68   $��16��6O   $%��Icons Resource/file87_16x16.png��6Z   �� 5o   2   5p   /    �� Triggers 5q   .   P� 5r   2    P� Property �@d�	IsChecked P� Value$
 TrueY�6O   . �5s   6   ��  �� 
TargetName$ checkboxImage��6=    � ��$% Icons Resource/folder_16x16.png��6j   5u   3   5w   +   5y   #   5z      5|      0  *System.Windows.Controls.DataGridTextColumn  5}      $
 Name��6)   $ * 67    6A   �� *Name+  5~      $
 Size��6)   $
 Auto 67    6D   �� *Size+  5      $
 Date��6)   $
 Auto 67    6D   �� *Date+5�      5�      5�      5�      