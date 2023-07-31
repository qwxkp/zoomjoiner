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
                { DayOfWeek.Monday, "Захист Украины" },
                { DayOfWeek.Tuesday, "Зарубежная Литература" },
                { DayOfWeek.Wednesday, "Зарубежная Литература" },
                { DayOfWeek.Thursday, "Право" },
                { DayOfWeek.Friday, "Укр. Мова" }
            };

        public static Dictionary<DayOfWeek, string> Lesson2 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Алгебра" },
                { DayOfWeek.Tuesday, "Геометрия" },
                { DayOfWeek.Wednesday, "Алгебра" },
                { DayOfWeek.Thursday, "Алгебра" },
                { DayOfWeek.Friday, "Физ-ра" }
            };

        public static Dictionary<DayOfWeek, string> Lesson3 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Алгебра" },
                { DayOfWeek.Tuesday, "Английский" },
                { DayOfWeek.Wednesday, "История" },
                { DayOfWeek.Thursday, "Алгебра" },
                { DayOfWeek.Friday, "Укр. Литература" }
            };

        public static Dictionary<DayOfWeek, string> Lesson4 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Физ-ра" },
                { DayOfWeek.Tuesday, "Физ-ра" },
                { DayOfWeek.Wednesday, "Английский" },
                { DayOfWeek.Thursday, "Укр. Литература" },
                { DayOfWeek.Friday, "География" }
            };

        public static Dictionary<DayOfWeek, string> Lesson5 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Мистецтво" },
                { DayOfWeek.Tuesday, "История" },
                { DayOfWeek.Wednesday, "География" },
                { DayOfWeek.Thursday, "Захист Украины" },
                { DayOfWeek.Friday, "Английский" }
            };

        public static Dictionary<DayOfWeek, string> Lesson6 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Право" },
                { DayOfWeek.Tuesday, "Укр. Мова" },
                { DayOfWeek.Wednesday, "Геометрия" },
                { DayOfWeek.Thursday, "Физика" },
                { DayOfWeek.Friday, "Биология" }
            };

        public static Dictionary<DayOfWeek, string> Lesson7 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Физика" },
                { DayOfWeek.Tuesday, "Биология" },
                { DayOfWeek.Wednesday, "Физика" },
                { DayOfWeek.Thursday, "Химия" },
                { DayOfWeek.Friday, "Геометрия" }
            };

        public static Dictionary<DayOfWeek, string> Lesson8 = new Dictionary<DayOfWeek, string>()
            {
                { DayOfWeek.Monday, "Информатика" },
                { DayOfWeek.Tuesday, "Нету" },
                { DayOfWeek.Wednesday, "Нету" },
                { DayOfWeek.Thursday, "Нету" },
                { DayOfWeek.Friday, "Нету" }
            };

        public static TimeSpan[] LessonTime = { TimeSpan.FromMinutes(510), TimeSpan.FromMinutes(565), TimeSpan.FromMinutes(620), TimeSpan.FromMinutes(675), TimeSpan.FromMinutes(730), TimeSpan.FromMinutes(785), TimeSpan.FromMinutes(840), TimeSpan.FromMinutes(895) };
    }
}
