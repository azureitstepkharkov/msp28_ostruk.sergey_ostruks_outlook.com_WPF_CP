   M S B A M L   `   `   `  ����@  <Admin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:AdminAdmin  5   	       	Admin.Add   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Admin     5   	     � Title$  Add User��5   	   $	��300��6   $	��300��6'    � WindowStartupLocation$ CenterScreen=�5	   	   .��5
      �  � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��2* �6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��0��6   $��0��6$   Name67   �� 5   
   $��0��6   $��1��6$   Email68   �� 5   
   $��0��6   $��2��6$   
Password6;   �� 5   
   $��0��6   $��3��6$   Confirm Password6C   �� 5   
   $��0��6   $��4��6$   Role67   �� 5   
   -   5         ��Name$ txtName��5   /   $��1��6   $��0��6"   6/   �� 5   
   -   5        $ txtEmail��5   /   $��1��6   $��1��6"   6/   G� 5   
   -   5         G�Name$ txtPassword��5   3   $��1��6   $��2��6&   63   G� 5    
   -   5        $ txtCPassword��5    3   $��1��6   $��3��6&   63   �� 5!   
   -   5         ��Name$ cmbRoles��5!   0   $��1��6   $��4��6#   60   �� 5"   
   -   5         ��Name$ btnStart��5#   .   $��0��5"      $��5��6"   $��2��6/   $	��100��5#      $��30��6"   $	��Add��5$   )   5&      5'      