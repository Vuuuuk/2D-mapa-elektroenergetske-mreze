   M S B A M L   `   `   `  ����>  :PZ1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PZ1PZ1  5   	       PZ1.Ecllipse   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:PZ1     5   	   -   5          � Title$  Ecllipse��5   	   $	��500��6   $	��900��6'   ���#212121 � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � 	Resources 5         
btn_efekat �� 
TargetType �� Property �� Value �� 
TargetType	 �� Triggers
 P� Property P� Value��%�  &	        �� 5   
   " ��6#   .�5      ��  �@�OverridesDefaultStyle . 66   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $��	btn_okvir��6!   $��0q�62   ._�5      �� $��Center=�6/   $��Center=�6L   5      	 5      P� 5      
 �@J�IsMouseOver . . �5   "   ��  �@J�Opacity$	 0.7Q�6<   5      5       5"      5#      5'      .��5)      � .+�5-   
   �� -   5         ��Name$ btn_exit��5-      #��[  5.      $��42��5/      $��42��6   $��856,1,2,457q�6(   
������ .��52      �� $#��Assets\Menu\btn_menu_exit.png��6   53      �� 55   
   -   5        $ btn_draw��55      #��[  56      $��42��57      $��42��6   $��0,1,858,457q�6(   
������ .��5:      �� $#��Assets\Menu\btn_menu_draw.png��6   5;      �� 5A   
   -   5         ��Name$ lbl_ellipse_width��5B   	   $��18(�5C   	   $��219,73,611,391q�5D   	   ���#E0E0E0$��Width:��5F   	   �� 5H   
   -   5         ��Name$ txt_ellipse_width��5I   	   $��Center=�5J   	   $��Center=�6-   $��13(�5K   	   ���#E0E0E0���#212121$��404,65,209,403q�5N   	    �� TextWrapping$
 Wrap=�5O   	   �� 5Q   
   -   5        $ lbl_ellipse_height��5R   	   $��18(�5S   	   $��219,125,611,339q�5T   	   ���#E0E0E0$��Height:��5V   	   �� 5X   
   -   5        $ txt_ellipse_height��5Y   	   $��Center=�5Z   	   $��Center=�6-   $��13(�5[   	   ���#E0E0E0���#212121$��404,117,208,351q�5^   	   $
 Wrap=�5_   	   �� 5a   
   -   5        $ lbl_border_thickness��5b   	   $��18(�5c   	   $��223,176,531,288q�5d   	   ���#E0E0E0$��Border thickness:��5f   	   �� 5h   
   -	   5        $ txt_border_thickness��5i   	   $��Center=�5j   	   $��Center=�6-   $��13(�5k   	   ���#E0E0E0���#212121$��404,173,209,295q�5n   	   $
 Wrap=�5o   	   �� 5q   
   -
   5        $ lbl_border_color��5r   	   $��18(�5s   	   $��223,231,531,233q�5t   	   ���#E0E0E0$��Border color:��5v   	   �� 5x   
   -   5         ��Name$ cmb_border_color��5y   	   $��Center=�5z   	   $��Center=�6-   $��13(�5{   	   ���#E0E0E0���#212121$��404,228,209,240q�5~   	   $	��287��5   	   $��32��6   ��5�      �� 5�      .R�5�      ��  �� Orientation$ 
Horizontal=�6!   ���#E0E0E0.+�5�      �� $��50��6$   $��50��6/   ��6;   �� *Name+�� 5�      ��6$   �� *Name+5�      5�      5�      �� 5�   
   -   5        $ lbl_ellipse_color��5�   	   $��18(�5�   	   $��223,285,531,179q�5�   	   ���#E0E0E0$��Ellipse color:��5�   	   �� 5�   
   -   5        $ cmb_ellipse_color��5�   	   $��Center=�5�   	   $��Center=�6-   $��13(�5�   	   ���#E0E0E0���#212121$��405,282,208,186q�5�   	   $	��287��5�   	   $��32��6   ��5�      �� 5�      .R�5�      �� $ 
Horizontal=�6!   ���#E0E0E0.+�5�      �� $��50��6$   $��50��6/   ��6;   �� *Name+�� 5�      ��6$   �� *Name+5�      5�      5�      �� 5�   
   -   5        $ lbl_text��5�   	   $��18(�5�   	   $��223,332,531,132q�5�   	   ���#E0E0E0$��Ellipse text:��5�   	   �� 5�   
   -   5        $ txt_ellipse_text��5�   	   $��Center=�5�   	   $��Center=�6-   $��13(�5�   	   ���#E0E0E0���#212121$��405,334,208,134q�5�   	   $
 Wrap=�5�   	   �� 5�   
   -   5        $ lbl_text_color��5�   	   $��18(�5�   	   $��223,383,531,81q�5�   	   ���#E0E0E0$��Text color:��5�   	   �� 5�   
   -   5        $ cmb_text_color��5�   	   $��Center=�5�   	   $��Center=�6-   $��13(�5�   	   ���#E0E0E0���#212121$��405,385,208,83q�5�   	   $	��287��5�   	   $��32��6   ��5�      �� 5�      .R�5�      �� $ 
Horizontal=�6!   ���#E0E0E0.+�5�      �� $��50��6$   $��50��6/   ��6;   �� *Name+�� 5�      ��6$   �� *Name+5�      5�      5�      �� 5�   
   -   5        $ lbl_text_size��5�   	   $��18(�5�   	   $��223,435,531,29q�5�   	   ���#E0E0E0$��
Text size:��5�   	   �� 5�   
   -   5        $ txt_ellipse_text_size��5�   	   $��Center=�5�   	   $��Center=�6-   $��13(�5�   	   ���#E0E0E0���#212121$��405,437,208,31q�5�   	   $
 Wrap=�5�   	   5�      5�      