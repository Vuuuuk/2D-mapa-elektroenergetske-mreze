   M S B A M L   `   `   `  ����>  :PZ1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PZ1PZ1  5   	       PZ1.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5	   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5
   	   #localclr-namespace:PZ1     5   	     � Title$  PZ1_Radunovic_Vuk��5   	   $
��1100��6#   $
��1100��61   ���#424242 � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � 	Resources 5         
btn_efekat �� 
TargetType �� Property �� Value �� 
TargetType	 �� Triggers
 P� Property P� Value��%�  &	        �� 5   
   " ��6#   .�5      ��  �@�OverridesDefaultStyle . 66   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $��	btn_okvir��6!   $��0q�62   ._�5      �� $��Center=�6/   $��Center=�6L   5      	 5      P� 5       
 �@J�IsMouseOver . . �5!   "   ��  �@J�Opacity$	 0.7Q�6<   5"      5$      5&      5'      5+      .��5-      � -   5        $��MainGrid��5-      $��0,-2,0,2q�6   .+�51   
   �� -   5         ��Name$ btn_exit��51      #��[  52      $��42��53      $��42��6   $��1038,5,20,1053q�6(   
������ .��56      �� $#��Assets\Menu\btn_menu_exit.png��6   57      �� 59   
   -   5        $ 
btn_elipse��59      #��[  5:      $��42��5;      $��42��6   $��3,5,1055,1053q�6(   
������ .��5>      �� $&�� Assets\Menu\btn_menu_ellipse.png��6   5?      �� 5A   
   -   5        $ btn_polygon��5A      #��[  5B      $��42��5C      $��42��6   $��60,3,998,1055q�6(   
������ .��5F      �� $&�� Assets\Menu\btn_menu_polygon.png��6   5G      �� 5I   
   -   5        $ btn_text��5I      #��[  5J      $��42��5K      $��42��6   $��114,4,944,1054q�6(   
������ .��5N      �� $#��Assets\Menu\btn_menu_text.png��6   5O      �� 5Q   
   -   5        $ btn_undo��5Q      #��[  5R      $��42��5S      $��45��6   $��882,4,176,1051q�6(   
������ .��5V      �� $(��"Assets\Menu\btn_menu_undo_redo.png��6   $��42��6@   5W      �� 5Y   
   -   5        $ btn_redo��5Y      #��[  5Z      $��42��5[      $��45��6   $��939,4,119,1051q�6(   
������ .��5^      �� $(��"Assets\Menu\btn_menu_undo_redo.png��6   $��42��6@    J� RenderTransformOrigin$ 0.5,0.5&�6K   z�5_      V� 5`      .~�5a      �� �� 5b      �� 5c       �� Angle$	 180Q�6*   T� 5d      5e      5g      5h      �� 5j   
   -   5        $ 	btn_clear��5j      #��[  5k      $��42��5l      $��45��6   $��991,4,67,1051q�6(   
������ .��5o      �� $$��Assets\Menu\btn_menu_clear.png��6   $��42��6<   5p      ]� 5u   
   .+�5v      �� -	   5         ��Name$ slider��5v       � Minimum$	 0.1Q�6%    � Maximum$ 10Q�63    � Value$ 1Q�6@   $��Bottom=�6J   �� 5w      $
��Auto=�6   $	��975��6@   .��5x      �� -
   5        $��
MainCanvas��5x      $	��800��6+   $
��1000��68   
������  � LayoutTransform 5z      �� 5{       �� ScaleX 6)   ��  �� ElementName$ slider�� �� Path$ Value� �� ScaleY 5|   !   �� $ slider��$ Value�5~      5      5�      5�      5�      