   M S B A M L   `   `   `  ���� E  AZoomjoiner, Version=1.6.2.0, Culture=neutral, PublicKeyToken=null'clr-namespace:zoomjoiner
zoomjoiner      zoomjoiner.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:zoomjoiner     X TFontAwesome.Sharp, Version=6.1.1.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp    � AllowsTransparency  . 
������  � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � Title$ 
MainWindow��$	��450��$	��800��   fadeWin#��[  .��� .+��� -    �� CornerRadius$ 40�����#242424._��� -    ��Name$ MyTab��
������ $��0q�.@��� $y�Hidden=� �� 
IsSelected . .���  � RowDefinitions �� �� �� �� �� �� .+��� $��3��$��0 30 0 0q�.+���  �� Orientation$ 
Horizontal=�$��Center=�$��1��$��0 1 0 0q�.+��� $��Center=�$��20 0 0 0q�.+��� -   	 ��Name$	 LessonStatus1��$
��Wrap=� 	 bText#��[ $��Center=�$��0 0 0 5q��� -   
 ��Name$
 Lesson1��$	��155��$��50��  Button1#��[ 
������ �� $��Center=�$��20 0 0 0q�.+��� -   $	 LessonStatus2��$
��Wrap=�#��[ $��Center=�$��0 0 0 5q��� -   $
 Lesson2��$	��155��$��50��#��[ 
������ �� $��Center=�$��20 0 0 0q�.+��� -   $	 LessonStatus3��$
��Wrap=�#��[ $��Center=�$��0 0 0 5q��� -   $
 Lesson3��$	��155��$��50��#��[ 
������ �� $��Center=�$��	20 0 20 0q�.+��� -	   $	 LessonStatus4��$
��Wrap=�#��[ $��Center=�$��0 0 0 5q��� -
   $
 Lesson4��$	��155��$��50��#��[ 
������ �� $ 
Horizontal=�$��Center=�$��2��$��0 15 0 0q�$��2��.+��� $��Center=�$��20 0 0 0q�.+��� -   $	 LessonStatus5��$
��Wrap=�#��[ $��Center=�$��0 0 0 5q��� -   $
 Lesson5��$	��155��$��50��#��[ 
������ �� $��Center=�$��20 0 0 0q�.+��� -   $	 LessonStatus6��$
��Wrap=�#��[ $��Center=�$��0 0 0 5q��� -   $
 Lesson6��$	��155��$��50��#��[ 
������ �� $��Center=�$��20 0 0 0q�.+��� -   $	 LessonStatus7��$
��Wrap=�#��[ $��Center=�$��0 0 0 5q��� -   $
 Lesson7��$	��155��$��50��#��[ 
������ �� $��Center=�$��5��$��20 0 0 0q�$��Stretch=�.+��� -   $
 	CheckTime��$	��140��$��50��#��[ 
������ $��Right=�$��0 0 15 0q�$��Розклад���� $��Bottom=�$��Stretch=�$��20 0 0 -354q�.+��� -   $��LessonManuall��$
��Wrap=�#��[ $��Stretch=�$��0 0 0 5q��� -   $��LessonManual1��$	��155��$��50��#��[ 
������ $��Інструкція���� $��Bottom=�$
��Left=�$��20 0 0 -354q�.+��� -   $��
LessonAuto��$
��Wrap=�#��[ $��Center=�$��Stretch=�$��0 0 0 5q��� -   $��LessonAuto2��$	��155��$��50��#��[ 
������ $%��Авто-Підключення��