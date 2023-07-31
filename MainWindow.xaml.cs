using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using vladnigger.Resources.Extended;

namespace vladnigger
{
    public partial class MainWindow : Window
    {
        private TimeSpan currentTime;
        private Dictionary<string, bool> lessonLate = new Dictionary<string, bool>();
        private Dictionary<string, string> currentLesson = new Dictionary<string, string>();
        private Dictionary<Button, bool> isAnim = new Dictionary<Button, bool>();
        private bool TimeAnim = false;
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
                    if (lessonDays[index][day] == "Нету")
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
                    _ = ButtonTextAnim(btn, "Нету", 20, wind: this, winOpacityLimit: 0.8);
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
                    if (lessonDays[index][day] == "Нету")
                        status.Text = "";
                    else if ((Days.LessonTime[index] - currentTime).TotalMilliseconds > 0)
                    {
                        lessonLate[lessonButtons[index].Name] = false;
                        status.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#81E474"));
                        _ = TextBlockTextAnim(status, "Через " + Math.Round((Days.LessonTime[index] - currentTime).TotalMinutes).ToString() + " мин.", 20, wind: this, winOpacityLimit: 0.8);
                    }
                    else if ((currentTime - Days.LessonTime[index]).TotalMinutes <= 45) // lesson is going
                    {
                        lessonLate[lessonButtons[index].Name] = false;
                        status.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF7538"));
                        _ = TextBlockTextAnim(status, "Идет уже " + Math.Round((currentTime - Days.LessonTime[index]).TotalMinutes).ToString() + " мин.", 20, wind: this, winOpacityLimit: 0.8);
                    }
                    else if ((currentTime - Days.LessonTime[index]).TotalMilliseconds >= 0) // negative
                    {
                        lessonLate[lessonButtons[index].Name] = true;
                        status.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F33E3E"));
                        _ = TextBlockTextAnim(status, Math.Round((currentTime - Days.LessonTime[index]).TotalMinutes).ToString() + $" мин. назад ({Math.Round((currentTime - Days.LessonTime[index]).TotalHours, 1)} ч.)", 20, wind: this, winOpacityLimit: 0.8);
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
                    await ButtonTextAnim((sender as Button), "Нет смысла братик", 20);
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
                button.IsEnabled = oldText[index] == "Нету" ? false : true;
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
            await ButtonTextAnim(CheckTime, "Расписание", 20);
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
    }
}
