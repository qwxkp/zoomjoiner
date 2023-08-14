using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vladnigger.Resources.Extended
{
    public class Days
    {
        public static Dictionary<DayOfWeek, string> Lesson1 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Захист України" },
                { DayOfWeek.Tuesday, "Зар. Література" },
                { DayOfWeek.Wednesday, "Зар. Література" },
                { DayOfWeek.Thursday, "Правознавство" },
                { DayOfWeek.Friday, "Укр. Мова" }
            };

        public static Dictionary<DayOfWeek, string> Lesson2 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Алгебра" },
                { DayOfWeek.Tuesday, "Геометрія" },
                { DayOfWeek.Wednesday, "Алгебра" },
                { DayOfWeek.Thursday, "Алгебра" },
                { DayOfWeek.Friday, "Фіз-ра" }
            };

        public static Dictionary<DayOfWeek, string> Lesson3 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Алгебра" },
                { DayOfWeek.Tuesday, "Англійська" },
                { DayOfWeek.Wednesday, "Історія" },
                { DayOfWeek.Thursday, "Алгебра" },
                { DayOfWeek.Friday, "Укр. Література" }
            };

        public static Dictionary<DayOfWeek, string> Lesson4 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Фіз-ра" },
                { DayOfWeek.Tuesday, "Фіз-ра" },
                { DayOfWeek.Wednesday, "Англійська" },
                { DayOfWeek.Thursday, "Укр. Література" },
                { DayOfWeek.Friday, "Географія" }
            };

        public static Dictionary<DayOfWeek, string> Lesson5 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Мистецтво" },
                { DayOfWeek.Tuesday, "Історія" },
                { DayOfWeek.Wednesday, "Географія" },
                { DayOfWeek.Thursday, "Захист України" },
                { DayOfWeek.Friday, "Англійська" }
            };

        public static Dictionary<DayOfWeek, string> Lesson6 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Правознавство" },
                { DayOfWeek.Tuesday, "Укр. Мова" },
                { DayOfWeek.Wednesday, "Геометрия" },
                { DayOfWeek.Thursday, "Фізика" },
                { DayOfWeek.Friday, "Біологія" }
            };

        public static Dictionary<DayOfWeek, string> Lesson7 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Физика" },
                { DayOfWeek.Tuesday, "Біологія" },
                { DayOfWeek.Wednesday, "Фізика" },
                { DayOfWeek.Thursday, "Хімія" },
                { DayOfWeek.Friday, "Геометрія" }
            };

        public static Dictionary<DayOfWeek, string> Lesson8 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Информатика" },
                { DayOfWeek.Tuesday, "Нема" },
                { DayOfWeek.Wednesday, "Нема" },
                { DayOfWeek.Thursday, "Нема" },
                { DayOfWeek.Friday, "Нема" }
            };

        public static TimeSpan[] LessonTime = { TimeSpan.FromMinutes(510), TimeSpan.FromMinutes(565), TimeSpan.FromMinutes(620), TimeSpan.FromMinutes(675), TimeSpan.FromMinutes(730), TimeSpan.FromMinutes(785), TimeSpan.FromMinutes(840), TimeSpan.FromMinutes(895) };
    }
}
