   M S B A M L   `   `   `  ����@  <Admin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Admin.Windows.ProjectsAdmin.Windows.Projects  5   	        Admin.Windows.Projects.Add   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:Admin.Windows.Projects     5   	     � Title$  Add Project��5   	   $	��300��6   $	��300��6*    � WindowStartupLocation$ CenterScreen=�5	   	   .��5
      �  � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��2* �6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��0��6   $��0��6$   Name67   �� 5   
   $��0��6   $��1��6$   Description6>   �� 5   
   $��0��6   $��2��6$   Status69   �� 5   
   $��0��6   $��3��6$   	Manager6:   �� 5   
   -   5         ��Name$ txtName��5   /   $��1��6   $��0��6"   6/   �� 5   
   -   5        $ txtDescription��5   /   $��1��6   $��1��6"   6/   �� 5   
   -   5        $ 	txtStatus��5   /   $��1��6   $��2��6"   6/   �� 5   
   -   5         ��Name$ cmbManagers��5   0   $��1��6   $��3��6#   60   �� 5   
   -   5         ��Name$ btnStart��5    .   $��0��5      $��5��6"   $��2��6/   $	��100��5       $��30��6"   $	��Add��5!   )   5#      5$      