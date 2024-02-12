using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoomjoiner.Resources.Extended
{
    public class Days
    {
        public static Dictionary<DayOfWeek, string> Lesson1 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Біологія" },
                { DayOfWeek.Tuesday, "Історія" },
                { DayOfWeek.Wednesday, "Алгебра" },
                { DayOfWeek.Thursday, "Історія" },
                { DayOfWeek.Friday, "Фіз-ра" }
            };

        public static Dictionary<DayOfWeek, string> lesson2 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Алгебра" },
                { DayOfWeek.Tuesday, "Фіз-ра" },
                { DayOfWeek.Wednesday, "Хімія" },
                { DayOfWeek.Thursday, "Геометрія" },
                { DayOfWeek.Friday, "Укр. Мова" }
            };

        public static Dictionary<DayOfWeek, string> Lesson3 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Укр. Мова" },
                { DayOfWeek.Tuesday, "Англійська" },
                { DayOfWeek.Wednesday, "Біологія" },
                { DayOfWeek.Thursday, "Фіз-ра" },
                { DayOfWeek.Friday, "Алгебра" }
            };

        public static Dictionary<DayOfWeek, string> Lesson4 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Алгебра" },
                { DayOfWeek.Tuesday, "Укр. Література" },
                { DayOfWeek.Wednesday, "Історія" },
                { DayOfWeek.Thursday, "Геометрія" },
                { DayOfWeek.Friday, "Захист України" }
            };

        public static Dictionary<DayOfWeek, string> Lesson5 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Англійська" },
                { DayOfWeek.Tuesday, "Література" },
                { DayOfWeek.Wednesday, "Фізика" },
                { DayOfWeek.Thursday, "Географія" },
                { DayOfWeek.Friday, "Укр. Література" }
            };

        public static Dictionary<DayOfWeek, string> Lesson6 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Фізика" },
                { DayOfWeek.Tuesday, "Фізика" },
                { DayOfWeek.Wednesday, "Алгебра" },
                { DayOfWeek.Thursday, "Фізика" },
                { DayOfWeek.Friday, "Хімія" }
            };

        public static Dictionary<DayOfWeek, string> Lesson7 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Інформатика" },
                { DayOfWeek.Tuesday, "Геометрія" },
                { DayOfWeek.Wednesday, "Мистецтво" },
                { DayOfWeek.Thursday, "Інформатика" },
                { DayOfWeek.Friday, "Геометрія" }
            };

        public static TimeSpan[] LessonTime = { TimeSpan.FromMinutes(480), TimeSpan.FromMinutes(530), TimeSpan.FromMinutes(585), TimeSpan.FromMinutes(640), TimeSpan.FromMinutes(690), TimeSpan.FromMinutes(780), TimeSpan.FromMinutes(830) };

        public static Dictionary<DayOfWeek, string> Lesson2 { get => lesson2; set => lesson2 = value; }
    }
}
