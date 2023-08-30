using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using vladnigger.Resources.Extended;
using Zoomjoiner;

namespace vladnigger
{
    public partial class MainWindow : Window
    {
        private TimeSpan currentTime;
        private Dictionary<string, bool> lessonLate = new Dictionary<string, bool>();
        private Dictionary<string, string> currentLesson = new Dictionary<string, string>();
        private Dictionary<Button, bool> isAnim = new Dictionary<Button, bool>();
        private bool TimeAnim = false;
        private bool TimeAnim1 = false;
        public MainWindow()
        {
            InitializeComponent();

            CheckingLoop();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }

        private TimeSpan GetCurrentTime()
        {
            return DateTime.Now.TimeOfDay;
        }

        private DayOfWeek CheckDay() { return DateTime.Now.DayOfWeek; }

        private void WriteDays(DayOfWeek day)
        {
            Button[] lessonButtons = { Lesson1, Lesson2, Lesson3, Lesson4, Lesson5, Lesson6, Lesson7, Lesson8 };
            Dictionary<DayOfWeek, string>[] lessonDays = { Days.Lesson1, Days.Lesson2, Days.Lesson3, Days.Lesson4, Days.Lesson5, Days.Lesson6, Days.Lesson7, Days.Lesson8 };
            int index = 0;
            try
            {
                foreach (Button btn in lessonButtons)
                {
                    isAnim[btn] = false;
                    currentLesson[btn.Name] = lessonDays[index][day];
                    _ = ButtonTextAnim(btn, lessonDays[index][day], 20, wind: this, winOpacityLimit: 0.8);
                    if (lessonDays[index][day] == "Нема")
                    {
                        btn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8E8E8E"));
                        btn.IsEnabled = false;
                        btn.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8E8E8E"));
                    }
                    index++;
                }
            }
            catch
            {
                foreach (Button btn in lessonButtons)
                {
                    btn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8E8E8E"));
                    btn.IsEnabled = false;
                    btn.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8E8E8E"));
                    _ = ButtonTextAnim(btn, "Нема", 20, wind: this, winOpacityLimit: 0.8);
                }
            }
        }

        private void WriteTime(DayOfWeek day)
        {
            TextBlock[] lessonStatus = { LessonStatus1, LessonStatus2, LessonStatus3, LessonStatus4, LessonStatus5, LessonStatus6, LessonStatus7, LessonStatus8 };
            Button[] lessonButtons = { Lesson1, Lesson2, Lesson3, Lesson4, Lesson5, Lesson6, Lesson7, Lesson8 };
            Dictionary<DayOfWeek, string>[] lessonDays = { Days.Lesson1, Days.Lesson2, Days.Lesson3, Days.Lesson4, Days.Lesson5, Days.Lesson6, Days.Lesson7, Days.Lesson8 };
            int index = 0;
            try
            {
                foreach (var status in lessonStatus)
                {
                    if (lessonDays[index][day] == "Нема")
                        status.Text = "";
                    else if ((Days.LessonTime[index] - currentTime).TotalMilliseconds > 0)
                    {
                        lessonLate[lessonButtons[index].Name] = false;
                        status.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#81E474"));
                        _ = TextBlockTextAnim(status, "Через " + Math.Round((Days.LessonTime[index] - currentTime).TotalMinutes).ToString() + " хв.", 20, wind: this, winOpacityLimit: 0.8);
                    }
                    else if ((currentTime - Days.LessonTime[index]).TotalMinutes <= 45) // lesson is going
                    {
                        lessonLate[lessonButtons[index].Name] = false;
                        status.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF7538"));
                        _ = TextBlockTextAnim(status, "Йде вже " + Math.Round((currentTime - Days.LessonTime[index]).TotalMinutes).ToString() + " хв.", 20, wind: this, winOpacityLimit: 0.8);
                    }
                    else if ((currentTime - Days.LessonTime[index]).TotalMilliseconds >= 0) // negative
                    {
                        lessonLate[lessonButtons[index].Name] = true;
                        status.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F33E3E"));
                        _ = TextBlockTextAnim(status, Math.Round((currentTime - Days.LessonTime[index]).TotalMinutes).ToString() + $" хв. тому ({Math.Round((currentTime - Days.LessonTime[index]).TotalHours, 1)} год.)", 20, wind: this, winOpacityLimit: 0.8);
                    }
                    index++;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async void CheckingLoop()
        {
            WriteDays(CheckDay());
            while (true)
            {
                currentTime = GetCurrentTime();
                WriteTime(CheckDay());
                await Task.Delay(30000);
            }
        }

        private async void JoinLesson(object sender, RoutedEventArgs e)
        {
            if (lessonLate[(sender as Button).Name] == true && !TimeAnim)
            {
                if (!isAnim[(Button)sender] || TimeAnim)
                {
                    CheckTime.IsEnabled = false;
                    isAnim[(Button)sender] = true;
                    string oldText = (string)(sender as Button).Content;
                    await ButtonTextAnim((sender as Button), "Немає сенсу", 20);
                    await Task.Delay(800);
                    await ButtonTextAnim((sender as Button), oldText, 20);
                    isAnim[(Button)sender] = false;
                    CheckTime.IsEnabled = true;
                }
            }
            else if (!TimeAnim)
            {
                Process.Start(Links.Link[currentLesson[(sender as Button).Name]]);
            }
        }

        private async void CheckTime_Click(object sender, RoutedEventArgs e)
        {
            Button[] lessonButtons = { Lesson1, Lesson2, Lesson3, Lesson4, Lesson5, Lesson6, Lesson7, Lesson8 };
            if (TimeAnim)
                return;

            TimeAnim = true;

            List<string> oldText = new List<string>();
            string[] time = { "8:30", "9:25", "10:20", "11:15", "12:10", "13:05", "14:00", "14:55" };
            int index = 0;

            StartCountDown();

            foreach (Button button in lessonButtons)
            {
                button.IsEnabled = false;
                oldText.Add(button.Content.ToString());

                _ = ButtonTextAnim(button, time[index], 20);

                index++;
            }

            await Task.Delay(5400);

            index = 0;

            foreach (Button button in lessonButtons)
            {
                button.IsEnabled = oldText[index] == "Нема" ? false : true;
                _ = ButtonTextAnim(button, oldText[index], 20);

                index++;
            }

            await Task.Delay(1000);

            TimeAnim = false;
        }

        private async void StartCountDown()
        {
            await ButtonTextAnim(CheckTime, "5", 20);
            for (int i = 0; i < 5; i++)
            {
                CheckTime.Content = (5 - i).ToString();
                await Task.Delay(1000);
            }
            await ButtonTextAnim(CheckTime, "Розклад", 20);
        }

        private async void StartCountDownAuto()
        {
            await ButtonTextAnim(LessonAuto2, "5", 20);
            for (int i = 0; i < 5; i++)
            {
                LessonAuto2.Content = (5 - i).ToString();
                await Task.Delay(1000);
               
            }
             await ButtonTextAnim(LessonAuto2, "Працює..", 20);
        }



        void AutoUrok1(string link)
        {
            var t = Task.Run(async delegate
            {
                await Task.Delay(10);
                return 42;
            });
            t.Wait();
            Process.Start($"{link}");

        }
        void AutoUrok2(string link, int time)
        {
            var t = Task.Run(async delegate
            {
                await Task.Delay(time);
                return 42;
            });
            t.Wait();
            Process.Start($"{link}");

        }
        void AutoUrok3(string link, int time)
        {
            var t = Task.Run(async delegate
            {
                await Task.Delay(time);
                return 42;
            });
            t.Wait();
            Process.Start($"{link}");

        }
        void AutoUrok4(string link, int time)
        {
            var t = Task.Run(async delegate
            {
                await Task.Delay(time);
                return 42;
            });
            t.Wait();
            Process.Start($"{link}");

        }
        void AutoUrok5(string link, int time)
        {
            var t = Task.Run(async delegate
            {
                await Task.Delay(time);
                return 42;
            });
            t.Wait();
            Process.Start($"{link}");

        }
        void AutoUrok6(string link, int time)
        {
            var t = Task.Run(async delegate
            {
                await Task.Delay(time);
                return 42;
            });
            t.Wait();
            Process.Start($"{link}");

        }
        void AutoUrok7(string link, int time)
        {
            var t = Task.Run(async delegate
            {
                await Task.Delay(time);
                return 42;
            });
            t.Wait();
            Process.Start($"{link}");

        }







        public async Task ButtonTextAnim(Button button, string animationText, int animationSpeed = 5, int delayBeforeChanging = 0, Window wind = null, double winOpacityLimit = 0)
        {

            if (wind != null)
            {
                while (wind.Opacity < winOpacityLimit)
                    await Task.Delay(1);
            }

            if (button.Content != null)
            {
                while (button.Content.ToString().Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        button.Content = button.Content.ToString().Substring(0, button.Content.ToString().Length - 1);
                    });

                    await Task.Delay(animationSpeed);
                }
                button.Content = "";
                await Task.Delay(delayBeforeChanging);
            }

            for (int i = 0; i != animationText.Length; i++)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    button.Content += animationText[i].ToString() == " " && button.Content.ToString().Length > 0 ? " " + animationText[++i].ToString() : animationText[i].ToString();
                });
                await Task.Delay(animationSpeed);
            }
        }
        public async Task TextBlockTextAnim(TextBlock textBlock, string animationText, int animationSpeed = 5, int delayBeforeChanging = 0, Window wind = null, double winOpacityLimit = 0)
        {

            if (wind != null)
            {
                while (wind.Opacity < winOpacityLimit)
                    await Task.Delay(1);
            }

            if (textBlock.Text != null)
            {
                while (textBlock.Text.ToString().Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        textBlock.Text = textBlock.Text.ToString().Substring(0, textBlock.Text.ToString().Length - 1);
                    });

                    await Task.Delay(animationSpeed);
                }

                await Task.Delay(delayBeforeChanging);
            }

            for (int i = 0; i != animationText.Length; i++)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    textBlock.Text += animationText[i].ToString() == " " && textBlock.Text.ToString().Length > 0 ? " " + animationText[++i].ToString() : animationText[i].ToString();
                });
                await Task.Delay(animationSpeed);
            }
        }

        public Brush ToBrush(string color)
        {
            return (Brush)new System.Windows.Media.BrushConverter().ConvertFromString(color);
        }

        private void LessonManual(object sender, RoutedEventArgs e)
        {
            Zoomjoiner.helpwindow taskWindow = new Zoomjoiner.helpwindow();
            taskWindow.Show();

        }

        async void LessonAutoJoin(object sender, RoutedEventArgs e)
        {
            StartCountDownAuto();
            await Task.Delay(6000);
            Hide();
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                if (DateTime.Now.Hour == 8 & DateTime.Now.Minute >= 25 & DateTime.Now.Minute <= 45)
                {


                    AutoUrok1("http://algebra.com/");
                    AutoUrok2("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 3240000);
                    AutoUrok3("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 3240000);
                    AutoUrok4("https://www.youtube.com/", 3255000);
                    AutoUrok5("https://us04web.zoom.us/j/77560815613?pwd=DnW2ZGGGL0h89VSjZaFaPWbZMIkjXX.1", 3240000);
                    AutoUrok6("https://us04web.zoom.us/j/3626675144?pwd=YTBVMEJWcXZrVERqbHFNcEc4UE1qZz09", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/74535513574?pwd=ox6Yr29SKYjnnfrVf0Zy0MlAudaUf8.1", 3240000);



                }
                else if (DateTime.Now.Hour == 9 & DateTime.Now.Minute >= 20 & DateTime.Now.Minute <= 35)
                {


                    await Task.Delay(6000);
                    Process.Start("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1");
                    await Task.Delay(3240000);
                    Process.Start("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1");
                    await Task.Delay(3240000);
                    Process.Start("https://web.telegram.org/z/");
                    await Task.Delay(3240000);
                    Process.Start("https://us04web.zoom.us/j/77560815613?pwd=DnW2ZGGGL0h89VSjZaFaPWbZMIkjXX.1");
                    await Task.Delay(3240000);
                    Process.Start("https://us04web.zoom.us/j/3626675144?pwd=YTBVMEJWcXZrVERqbHFNcEc4UE1qZz09");
                    await Task.Delay(3240000);
                    Process.Start("https://us04web.zoom.us/j/74535513574?pwd=ox6Yr29SKYjnnfrVf0Zy0MlAudaUf8.1");
                    await Task.Delay(6000);



                }
                else if (DateTime.Now.Hour == 10 & DateTime.Now.Minute >= 15 & DateTime.Now.Minute <= 25)
                {


                    AutoUrok3("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 100);
                    AutoUrok4("https://web.telegram.org/z/", 3255000);
                    AutoUrok5("https://us04web.zoom.us/j/77560815613?pwd=DnW2ZGGGL0h89VSjZaFaPWbZMIkjXX.1", 3240000);
                    AutoUrok6("https://us04web.zoom.us/j/3626675144?pwd=YTBVMEJWcXZrVERqbHFNcEc4UE1qZz09", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/74535513574?pwd=ox6Yr29SKYjnnfrVf0Zy0MlAudaUf8.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();



                }
                else if (DateTime.Now.Hour == 11 & DateTime.Now.Minute >= 10 & DateTime.Now.Minute <= 25)
                {


                    AutoUrok4("https://web.telegram.org/z/", 100);
                    AutoUrok5("https://us04web.zoom.us/j/77560815613?pwd=DnW2ZGGGL0h89VSjZaFaPWbZMIkjXX.1", 3240000);
                    AutoUrok6("https://us04web.zoom.us/j/3626675144?pwd=YTBVMEJWcXZrVERqbHFNcEc4UE1qZz09", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/74535513574?pwd=ox6Yr29SKYjnnfrVf0Zy0MlAudaUf8.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();



                }
                else if (DateTime.Now.Hour == 12 & DateTime.Now.Minute >= 5 & DateTime.Now.Minute <= 15)
                {


                    AutoUrok5("https://us04web.zoom.us/j/77560815613?pwd=DnW2ZGGGL0h89VSjZaFaPWbZMIkjXX.1", 100);
                    AutoUrok6("https://us04web.zoom.us/j/3626675144?pwd=YTBVMEJWcXZrVERqbHFNcEc4UE1qZz09", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/74535513574?pwd=ox6Yr29SKYjnnfrVf0Zy0MlAudaUf8.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();



                }
                else if (DateTime.Now.Hour == 13 & DateTime.Now.Minute >= 0 & DateTime.Now.Minute <= 10)
                {


                    AutoUrok6("https://us04web.zoom.us/j/3626675144?pwd=YTBVMEJWcXZrVERqbHFNcEc4UE1qZz09", 100);
                    AutoUrok7("https://us04web.zoom.us/j/74535513574?pwd=ox6Yr29SKYjnnfrVf0Zy0MlAudaUf8.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();



                }
                else if (DateTime.Now.Hour == 14 & DateTime.Now.Minute >= 0 & DateTime.Now.Minute <= 10)
                {


                    AutoUrok7("https://us04web.zoom.us/j/74535513574?pwd=ox6Yr29SKYjnnfrVf0Zy0MlAudaUf8.1", 100);
                    var time = Task.Delay(10000);
                    time.Wait();



                }
                else
                {
                    string messageBoxText = "Помилка, зпробуйте пізніше!";
                    string caption = "Помилка";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.None;
                    MessageBoxResult result;

                    result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
                    LessonAuto2.Content = "Авто-Підключення";
                    
                }


            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                if (DateTime.Now.Hour == 8 & DateTime.Now.Minute >= 25 & DateTime.Now.Minute <= 35)
                {


                    AutoUrok1("https://us04web.zoom.us/j/73177828356?pwd=MHVKSjZ0cFNSMkt5a05qOXJyclRLUT09");
                    AutoUrok2("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 3240000);
                    AutoUrok3("https://us04web.zoom.us/j/75913045961?pwd=qbtU4bFPc1ELoy842dk6hGXPZfI3OX.1", 3240000);
                    AutoUrok4("https://web.telegram.org/z/", 3255000);
                    AutoUrok5("https://us04web.zoom.us/j/77560815613?pwd=DnW2ZGGGL0h89VSjZaFaPWbZMIkjXX.1", 3240000);
                    AutoUrok6("https://us04web.zoom.us/j/72161361348?pwd=azbctGwbaIjGEgcL5OiusFFfvLEH1r.1", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/7733527729?pwd=f780aruX8l4AhGSaWpBRj2sPs58jnx.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();



                }
                else if (DateTime.Now.Hour == 9 & DateTime.Now.Minute >= 20 & DateTime.Now.Minute <= 30)
                {


                    AutoUrok2("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 100);
                    AutoUrok3("https://us04web.zoom.us/j/75913045961?pwd=qbtU4bFPc1ELoy842dk6hGXPZfI3OX.1", 3240000);
                    AutoUrok4("https://web.telegram.org/z/", 3240000);
                    AutoUrok5("https://us04web.zoom.us/j/72252888239?pwd=93bbPv3FHfqaSQWMAUbYeug8oaa1qs.1", 3240000);
                    AutoUrok6("https://us04web.zoom.us/j/72161361348?pwd=azbctGwbaIjGEgcL5OiusFFfvLEH1r.1", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/7733527729?pwd=f780aruX8l4AhGSaWpBRj2sPs58jnx.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();



                }
                else if (DateTime.Now.Hour == 10 & DateTime.Now.Minute >= 15 & DateTime.Now.Minute <= 25)
                {


                    AutoUrok3("https://us04web.zoom.us/j/75913045961?pwd=qbtU4bFPc1ELoy842dk6hGXPZfI3OX.1", 100);
                    AutoUrok4("https://web.telegram.org/z/", 3255000);
                    AutoUrok5("https://us04web.zoom.us/j/72252888239?pwd=93bbPv3FHfqaSQWMAUbYeug8oaa1qs.1", 3240000);
                    AutoUrok6("https://us04web.zoom.us/j/72161361348?pwd=azbctGwbaIjGEgcL5OiusFFfvLEH1r.1", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/7733527729?pwd=f780aruX8l4AhGSaWpBRj2sPs58jnx.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();



                }
                else if (DateTime.Now.Hour == 11 & DateTime.Now.Minute >= 10 & DateTime.Now.Minute <= 25)
                {


                    AutoUrok4("https://web.telegram.org/z/", 100);
                    AutoUrok5("https://us04web.zoom.us/j/72252888239?pwd=93bbPv3FHfqaSQWMAUbYeug8oaa1qs.1", 3240000);
                    AutoUrok6("https://us04web.zoom.us/j/72161361348?pwd=azbctGwbaIjGEgcL5OiusFFfvLEH1r.1", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/7733527729?pwd=f780aruX8l4AhGSaWpBRj2sPs58jnx.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();



                }
                else if (DateTime.Now.Hour == 12 & DateTime.Now.Minute >= 5 & DateTime.Now.Minute <= 15)
                {


                    AutoUrok5("https://us04web.zoom.us/j/72252888239?pwd=93bbPv3FHfqaSQWMAUbYeug8oaa1qs.1", 100);
                    AutoUrok6("https://us04web.zoom.us/j/72161361348?pwd=azbctGwbaIjGEgcL5OiusFFfvLEH1r.1", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/7733527729?pwd=f780aruX8l4AhGSaWpBRj2sPs58jnx.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();



                }
                else if (DateTime.Now.Hour == 13 & DateTime.Now.Minute >= 0 & DateTime.Now.Minute <= 10)
                {


                    AutoUrok6("https://us04web.zoom.us/j/72161361348?pwd=azbctGwbaIjGEgcL5OiusFFfvLEH1r.1", 100);
                    AutoUrok7("https://us04web.zoom.us/j/7733527729?pwd=f780aruX8l4AhGSaWpBRj2sPs58jnx.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();



                }
                else if (DateTime.Now.Hour == 14 & DateTime.Now.Minute >= 0 & DateTime.Now.Minute <= 10)
                {


                    AutoUrok7("https://us04web.zoom.us/j/7733527729?pwd=f780aruX8l4AhGSaWpBRj2sPs58jnx.1", 100);
                    var time = Task.Delay(10000);
                    time.Wait();



                }
                else
                {
                    string messageBoxText = "Помилка, зпробуйте пізніше!";
                    string caption = "Помилка";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.None;
                    MessageBoxResult result;

                    result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
                    LessonAuto2.Content = "Авто-Підключення";
                    
                }



            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                if (DateTime.Now.Hour == 8 & DateTime.Now.Minute >= 25 & DateTime.Now.Minute <= 35)
                {


                    AutoUrok1("https://us04web.zoom.us/j/73177828356?pwd=MHVKSjZ0cFNSMkt5a05qOXJyclRLUT09");
                    AutoUrok2("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 3240000);
                    AutoUrok3("https://us04web.zoom.us/j/72252888239?pwd=93bbPv3FHfqaSQWMAUbYeug8oaa1qs.1", 3240000);
                    AutoUrok4("https://us04web.zoom.us/j/75913045961?pwd=qbtU4bFPc1ELoy842dk6hGXPZfI3OX.1", 3255000);
                    AutoUrok5("https://us05web.zoom.us/j/6764523122?pwd=YWtiWGdpRzZiWjE0WTlDb2lqTUpWQT09", 3240000);
                    AutoUrok6("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/74535513574?pwd=ox6Yr29SKYjnnfrVf0Zy0MlAudaUf8.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";


                }
                else if (DateTime.Now.Hour == 9 & DateTime.Now.Minute >= 20 & DateTime.Now.Minute <= 30)
                {


                    AutoUrok2("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 100);
                    AutoUrok3("https://us04web.zoom.us/j/72252888239?pwd=93bbPv3FHfqaSQWMAUbYeug8oaa1qs.1", 3240000);
                    AutoUrok4("https://us04web.zoom.us/j/75913045961?pwd=qbtU4bFPc1ELoy842dk6hGXPZfI3OX.1", 3240000);
                    AutoUrok5("https://us05web.zoom.us/j/6764523122?pwd=YWtiWGdpRzZiWjE0WTlDb2lqTUpWQT09", 3240000);
                    AutoUrok6("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/74535513574?pwd=ox6Yr29SKYjnnfrVf0Zy0MlAudaUf8.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";


                }
                else if (DateTime.Now.Hour == 10 & DateTime.Now.Minute >= 15 & DateTime.Now.Minute <= 25)
                {


                    AutoUrok3("https://us04web.zoom.us/j/72252888239?pwd=93bbPv3FHfqaSQWMAUbYeug8oaa1qs.1", 100);
                    AutoUrok4("https://us04web.zoom.us/j/75913045961?pwd=qbtU4bFPc1ELoy842dk6hGXPZfI3OX.1", 3240000);
                    AutoUrok5("https://us05web.zoom.us/j/6764523122?pwd=YWtiWGdpRzZiWjE0WTlDb2lqTUpWQT09", 3240000);
                    AutoUrok6("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/74535513574?pwd=ox6Yr29SKYjnnfrVf0Zy0MlAudaUf8.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";


                }
                else if (DateTime.Now.Hour == 11 & DateTime.Now.Minute >= 10 & DateTime.Now.Minute <= 25)
                {


                    AutoUrok4("https://us04web.zoom.us/j/75913045961?pwd=qbtU4bFPc1ELoy842dk6hGXPZfI3OX.1", 100);
                    AutoUrok5("https://us05web.zoom.us/j/6764523122?pwd=YWtiWGdpRzZiWjE0WTlDb2lqTUpWQT09", 3240000);
                    AutoUrok6("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/74535513574?pwd=ox6Yr29SKYjnnfrVf0Zy0MlAudaUf8.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";


                }
                else if (DateTime.Now.Hour == 12 & DateTime.Now.Minute >= 5 & DateTime.Now.Minute <= 15)
                {


                    AutoUrok5("https://us05web.zoom.us/j/6764523122?pwd=YWtiWGdpRzZiWjE0WTlDb2lqTUpWQT09", 100);
                    AutoUrok6("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/74535513574?pwd=ox6Yr29SKYjnnfrVf0Zy0MlAudaUf8.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";


                }
                else if (DateTime.Now.Hour == 13 & DateTime.Now.Minute >= 0 & DateTime.Now.Minute <= 10)
                {


                    AutoUrok6("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 100);
                    AutoUrok7("https://us04web.zoom.us/j/74535513574?pwd=ox6Yr29SKYjnnfrVf0Zy0MlAudaUf8.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";


                }
                else if (DateTime.Now.Hour == 14 & DateTime.Now.Minute >= 0 & DateTime.Now.Minute <= 10)
                {


                    AutoUrok7("https://us04web.zoom.us/j/74535513574?pwd=ox6Yr29SKYjnnfrVf0Zy0MlAudaUf8.1", 100);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";


                }
                else
                {
                    string messageBoxText = "Помилка, зпробуйте пізніше!";
                    string caption = "Помилка";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.None;
                    MessageBoxResult result;

                    result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
                    LessonAuto2.Content = "Авто-Підключення";
                    
                }

            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {

                if (DateTime.Now.Hour == 8 & DateTime.Now.Minute >= 25 & DateTime.Now.Minute <= 35)
                {


                    AutoUrok1("https://us04web.zoom.us/j/3626675144?pwd=YTBVMEJWcXZrVERqbHFNcEc4UE1qZz09");
                    AutoUrok2("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 3240000);
                    AutoUrok3("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 3240000);
                    AutoUrok4("https://us04web.zoom.us/j/72161361348?pwd=azbctGwbaIjGEgcL5OiusFFfvLEH1r.1", 3255000);
                    AutoUrok5("https://us05web.zoom.us/j/81793137250?pwd=dFEvM21GSHlmb05KYVlWeURJaTR2QT09", 3240000);
                    AutoUrok6("https://us04web.zoom.us/j/74535513574?pwd=ox6Yr29SKYjnnfrVf0Zy0MlAudaUf8.1", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/77139151153?pwd=t1NaCuhRdQhA6YglMRoql6dqF6d7cT.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";


                }
                else if (DateTime.Now.Hour == 9 & DateTime.Now.Minute >= 20 & DateTime.Now.Minute <= 30)
                {


                    AutoUrok2("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 100);
                    AutoUrok3("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 3240000);
                    AutoUrok4("https://us04web.zoom.us/j/72161361348?pwd=azbctGwbaIjGEgcL5OiusFFfvLEH1r.1", 3240000);
                    AutoUrok5("https://us05web.zoom.us/j/81793137250?pwd=dFEvM21GSHlmb05KYVlWeURJaTR2QT09", 3240000);
                    AutoUrok6("https://us04web.zoom.us/j/74535513574?pwd=ox6Yr29SKYjnnfrVf0Zy0MlAudaUf8.1", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/77139151153?pwd=t1NaCuhRdQhA6YglMRoql6dqF6d7cT.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";


                }
                else if (DateTime.Now.Hour == 10 & DateTime.Now.Minute >= 15 & DateTime.Now.Minute <= 25)
                {


                    AutoUrok3("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 100);
                    AutoUrok4("https://us04web.zoom.us/j/72161361348?pwd=azbctGwbaIjGEgcL5OiusFFfvLEH1r.1", 3255000);
                    AutoUrok5("https://us05web.zoom.us/j/81793137250?pwd=dFEvM21GSHlmb05KYVlWeURJaTR2QT09", 3240000);
                    AutoUrok6("https://us04web.zoom.us/j/74535513574?pwd=ox6Yr29SKYjnnfrVf0Zy0MlAudaUf8.1", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/77139151153?pwd=t1NaCuhRdQhA6YglMRoql6dqF6d7cT.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";


                }
                else if (DateTime.Now.Hour == 11 & DateTime.Now.Minute >= 10 & DateTime.Now.Minute <= 25)
                {


                    AutoUrok4("https://us04web.zoom.us/j/72161361348?pwd=azbctGwbaIjGEgcL5OiusFFfvLEH1r.1", 100);
                    AutoUrok5("https://us05web.zoom.us/j/81793137250?pwd=dFEvM21GSHlmb05KYVlWeURJaTR2QT09", 3240000);
                    AutoUrok6("https://us04web.zoom.us/j/74535513574?pwd=ox6Yr29SKYjnnfrVf0Zy0MlAudaUf8.1", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/77139151153?pwd=t1NaCuhRdQhA6YglMRoql6dqF6d7cT.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";


                }
                else if (DateTime.Now.Hour == 12 & DateTime.Now.Minute >= 5 & DateTime.Now.Minute <= 15)
                {


                    AutoUrok5("https://us05web.zoom.us/j/81793137250?pwd=dFEvM21GSHlmb05KYVlWeURJaTR2QT09", 100);
                    AutoUrok6("https://us04web.zoom.us/j/74535513574?pwd=ox6Yr29SKYjnnfrVf0Zy0MlAudaUf8.1", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/77139151153?pwd=t1NaCuhRdQhA6YglMRoql6dqF6d7cT.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";


                }
                else if (DateTime.Now.Hour == 13 & DateTime.Now.Minute >= 0 & DateTime.Now.Minute <= 10)
                {


                    AutoUrok6("https://us04web.zoom.us/j/74535513574?pwd=ox6Yr29SKYjnnfrVf0Zy0MlAudaUf8.1", 100);
                    AutoUrok7("https://us04web.zoom.us/j/77139151153?pwd=t1NaCuhRdQhA6YglMRoql6dqF6d7cT.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";


                }
                else if (DateTime.Now.Hour == 14 & DateTime.Now.Minute >= 0 & DateTime.Now.Minute <= 10)
                {


                    AutoUrok7("https://us04web.zoom.us/j/77139151153?pwd=t1NaCuhRdQhA6YglMRoql6dqF6d7cT.1", 100);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";



                }
                else
                {
                    string messageBoxText = "Помилка, зпробуйте пізніше!";
                    string caption = "Помилка";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.None;
                    MessageBoxResult result;

                    result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
                    LessonAuto2.Content = "Авто-Підключення";
                    
                }

            }

            else if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                if (DateTime.Now.Hour == 8 & DateTime.Now.Minute >= 25 & DateTime.Now.Minute <= 35)
                {


                    AutoUrok1("https://us04web.zoom.us/j/72161361348?pwd=azbctGwbaIjGEgcL5OiusFFfvLEH1r.1");
                    AutoUrok2("https://youtube.com", 3240000);
                    AutoUrok3("https://us04web.zoom.us/j/72161361348?pwd=azbctGwbaIjGEgcL5OiusFFfvLEH1r.1", 3240000);
                    AutoUrok4("https://us05web.zoom.us/j/6764523122?pwd=YWtiWGdpRzZiWjE0WTlDb2lqTUpWQT09", 3255000);
                    AutoUrok5("https://us04web.zoom.us/j/75913045961?pwd=qbtU4bFPc1ELoy842dk6hGXPZfI3OX.1", 3240000);
                    AutoUrok6("https://us04web.zoom.us/j/7733527729?pwd=f780aruX8l4AhGSaWpBRj2sPs58jnx.1", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";


                }
                else if (DateTime.Now.Hour == 9 & DateTime.Now.Minute >= 20 & DateTime.Now.Minute <= 30)
                {


                    AutoUrok2("https://youtube.com", 100);
                    AutoUrok3("https://us04web.zoom.us/j/72161361348?pwd=azbctGwbaIjGEgcL5OiusFFfvLEH1r.1", 3240000);
                    AutoUrok4("https://us05web.zoom.us/j/6764523122?pwd=YWtiWGdpRzZiWjE0WTlDb2lqTUpWQT09", 3255000);
                    AutoUrok5("https://us04web.zoom.us/j/75913045961?pwd=qbtU4bFPc1ELoy842dk6hGXPZfI3OX.1", 3240000);
                    AutoUrok6("https://us04web.zoom.us/j/7733527729?pwd=f780aruX8l4AhGSaWpBRj2sPs58jnx.1", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";


                }
                else if (DateTime.Now.Hour == 10 & DateTime.Now.Minute >= 15 & DateTime.Now.Minute <= 25)
                {


                    AutoUrok3("https://us04web.zoom.us/j/72161361348?pwd=azbctGwbaIjGEgcL5OiusFFfvLEH1r.1", 100);
                    AutoUrok4("https://us05web.zoom.us/j/6764523122?pwd=YWtiWGdpRzZiWjE0WTlDb2lqTUpWQT09", 3255000);
                    AutoUrok5("https://us04web.zoom.us/j/75913045961?pwd=qbtU4bFPc1ELoy842dk6hGXPZfI3OX.1", 3240000);
                    AutoUrok6("https://us04web.zoom.us/j/7733527729?pwd=f780aruX8l4AhGSaWpBRj2sPs58jnx.1", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";


                }
                else if (DateTime.Now.Hour == 11 & DateTime.Now.Minute >= 10 & DateTime.Now.Minute <= 25)
                {


                    AutoUrok4("https://us05web.zoom.us/j/6764523122?pwd=YWtiWGdpRzZiWjE0WTlDb2lqTUpWQT09", 100);
                    AutoUrok5("https://us04web.zoom.us/j/75913045961?pwd=qbtU4bFPc1ELoy842dk6hGXPZfI3OX.1", 3240000);
                    AutoUrok6("https://us04web.zoom.us/j/7733527729?pwd=f780aruX8l4AhGSaWpBRj2sPs58jnx.1", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";


                }
                else if (DateTime.Now.Hour == 12 & DateTime.Now.Minute >= 5 & DateTime.Now.Minute <= 15)
                {


                    AutoUrok5("https://us04web.zoom.us/j/75913045961?pwd=qbtU4bFPc1ELoy842dk6hGXPZfI3OX.1", 100);
                    AutoUrok6("https://us04web.zoom.us/j/7733527729?pwd=f780aruX8l4AhGSaWpBRj2sPs58jnx.1", 3240000);
                    AutoUrok7("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";


                }
                else if (DateTime.Now.Hour == 13 & DateTime.Now.Minute >= 0 & DateTime.Now.Minute <= 10)
                {


                    AutoUrok6("https://us04web.zoom.us/j/7733527729?pwd=f780aruX8l4AhGSaWpBRj2sPs58jnx.1", 100);
                    AutoUrok7("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 3240000);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";


                }
                else if (DateTime.Now.Hour == 14 & DateTime.Now.Minute >= 0 & DateTime.Now.Minute <= 10)
                {


                    AutoUrok7("https://us04web.zoom.us/j/79248404114?pwd=AyLWCMjZnzxtI6ZTRRbpDQyYdXXSqy.1", 100);
                    var time = Task.Delay(10000);
                    time.Wait();
                    LessonAuto2.Content = "Авто-Підключення";


                }
                else
                {
                    string messageBoxText = "Помилка, зпробуйте пізніше!";
                    string caption = "Помилка";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.None;
                    MessageBoxResult result;

                    result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
                    LessonAuto2.Content = "Авто-Підключення";
                    
                }

            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                
            }
            else
            {
                string messageBoxText = "Нащо ти сюди зайшов? Відпочивай";
                string caption = "Немає сенсу, друже";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.None;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
                LessonAuto2.Content = "Авто-Підключення";
            }

            Show();
            LessonAuto2.Content = "Авто-Підключення";


        }
    }


}

