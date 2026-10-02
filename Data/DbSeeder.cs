using Rasttad.Models;

namespace Rasttad.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // اگر قبلاً داده‌ها اضافه شده‌اند، دوباره اضافه نکن
            if (context.Industries.Any())
            {
                return;
            }

            // ==================== ۱. فناوری اطلاعات ====================
            var it = new Industry
            {
                Slug = "it",
                Name = "فناوری اطلاعات و استارتاپ",
                Description = "شرکت‌های نرم‌افزاری، استارتاپ‌ها و کسب‌وکارهای دیجیتال با چالش‌های منحصر به فرد منابع انسانی روبرو هستند.",
                ColorTheme = "#004a99",
                Challenges = new List<Challenge>
                {
                    new Challenge { Title = "کمبود نیروی متخصص", Description = "رقابت شدید برای جذب برنامه‌نویس و متخصصان فناوری" },
                    new Challenge { Title = "ترک خدمت بالا", Description = "نرخ جابجایی نیروها در این صنعت بسیار بالاست" },
                    new Challenge { Title = "دورکاری و مدیریت از راه دور", Description = "چالش‌های مدیریت تیم‌های پراکنده جغرافیایی" }
                },
                Services = new List<Service>
                {
                    new Service { Title = "جذب و استخدام تخصصی", Description = "شناسایی و جذب نیروهای فنی متخصص", Icon = "search" },
                    new Service { Title = "طراحی نظام جبران خدمات", Description = "ساختار حقوق و مزایای رقابتی برای حفظ نیرو", Icon = "money" },
                    new Service { Title = "آموزش مهارت‌های نرم", Description = "تقویت مهارت‌های ارتباطی و کار تیمی", Icon = "book" }
                },
                CaseStudies = new List<CaseStudy>
                {
                    new CaseStudy { Title = "کاهش ۳۰٪ ترک خدمت در یک استارتاپ", Description = "با طراحی نظام جبران خدمات و مسیر شغلی", Result = "کاهش ۳۰٪ ترک خدمت در ۶ ماه" }
                }
            };

            // ==================== ۲. تولید و صنایع ====================
            var manufacturing = new Industry
            {
                Slug = "manufacturing",
                Name = "تولید و صنایع",
                Description = "کارخانه‌ها و واحدهای تولیدی با چالش‌های بهره‌وری، ایمنی و روابط کار روبرو هستند.",
                ColorTheme = "#d97706",
                Challenges = new List<Challenge>
                {
                    new Challenge { Title = "بهره‌وری پایین نیروی کار", Description = "افت بهره‌وری به دلیل خستگی و انگیزه پایین" },
                    new Challenge { Title = "ایمنی و سلامت کارکنان", Description = "رعایت استانداردهای ایمنی در محیط کار" },
                    new Challenge { Title = "روابط کار و مذاکرات جمعی", Description = "مدیریت روابط با تشکل‌های کارگری" }
                },
                Services = new List<Service>
                {
                    new Service { Title = "نظام ارزیابی عملکرد", Description = "طراحی شاخص‌های کلیدی عملکرد (KPI)", Icon = "chart" },
                    new Service { Title = "آموزش ایمنی و بهداشت", Description = "دوره‌های تخصصی HSE", Icon = "shield" },
                    new Service { Title = "مدیریت روابط کار", Description = "مشاوره در مذاکرات و قراردادهای کارگری", Icon = "handshake" }
                },
                CaseStudies = new List<CaseStudy>
                {
                    new CaseStudy { Title = "افزایش ۲۵٪ بهره‌وری در یک کارخانه", Description = "با بازطراحی نظام ارزیابی عملکرد", Result = "افزایش ۲۵٪ بهره‌وری" }
                }
            };

            // ==================== ۳. خرده‌فروشی ====================
            var retail = new Industry
            {
                Slug = "retail",
                Name = "خرده‌فروشی و زنجیره‌ای",
                Description = "فروشگاه‌های زنجیره‌ای و خرده‌فروشی با چالش‌های استخدام سریع و آموزش پرسنل خط مقدم روبرو هستند.",
                ColorTheme = "#059669",
                Challenges = new List<Challenge>
                {
                    new Challenge { Title = "ترک خدمت بالای پرسنل فروش", Description = "نرخ بالای خروج پرسنل فروشگاه‌ها" },
                    new Challenge { Title = "آموزش سریع پرسنل جدید", Description = "نیاز به آموزش سریع و کارآمد پرسنل تازه‌وارد" },
                    new Challenge { Title = "مدیریت شیفت‌ها", Description = "برنامه‌ریزی پیچیده شیفت‌های کاری" }
                },
                Services = new List<Service>
                {
                    new Service { Title = "استخدام انبوه", Description = "فرآیند سریع جذب و استخدام پرسنل فروش", Icon = "users" },
                    new Service { Title = "آموزش پرسنل خط مقدم", Description = "دوره‌های کوتاه و کاربردی فروشندگی", Icon = "book" },
                    new Service { Title = "نرم‌افزار مدیریت شیفت", Description = "ابزار برنامه‌ریزی و مدیریت شیفت‌ها", Icon = "calendar" }
                },
                CaseStudies = new List<CaseStudy>
                {
                    new CaseStudy { Title = "کاهش ۴۰٪ زمان آموزش پرسنل جدید", Description = "با طراحی دوره‌های ویدیویی کوتاه", Result = "کاهش ۴۰٪ زمان آموزش" }
                }
            };

            // ==================== ۴. خدمات مالی و بانکی ====================
            var finance = new Industry
            {
                Slug = "finance",
                Name = "خدمات مالی و بانکی",
                Description = "بانک‌ها و مؤسسات مالی با چالش‌های آموزش تخصصی و جانشین‌پروری روبرو هستند.",
                ColorTheme = "#1e40af",
                Challenges = new List<Challenge>
                {
                    new Challenge { Title = "نیاز به آموزش‌های تخصصی مستمر", Description = "تغییرات سریع قوانین و مقررات مالی" },
                    new Challenge { Title = "جانشین‌پروری برای پست‌های کلیدی", Description = "کمبود نیروهای آماده برای تصدی پست‌های مدیریتی" },
                    new Challenge { Title = "حفظ محرمانگی اطلاعات", Description = "آموزش کارکنان در مورد امنیت اطلاعات" }
                },
                Services = new List<Service>
                {
                    new Service { Title = "طراحی مسیر شغلی", Description = "برنامه‌ریزی توسعه حرفه‌ای کارکنان", Icon = "path" },
                    new Service { Title = "آموزش‌های تخصصی بانکی", Description = "دوره‌های تخصصی مطابق با قوانین بانک مرکزی", Icon = "book" },
                    new Service { Title = "برنامه جانشین‌پروری", Description = "شناسایی و توسعه استعدادهای داخلی", Icon = "star" }
                },
                CaseStudies = new List<CaseStudy>
                {
                    new CaseStudy { Title = "آماده‌سازی ۱۵ مدیر جانشین در یک بانک", Description = "با اجرای برنامه توسعه رهبری", Result = "۱۵ مدیر آماده جانشینی" }
                }
            };

            // ==================== ۵. سلامت و درمان ====================
            var health = new Industry
            {
                Slug = "health",
                Name = "سلامت و درمان",
                Description = "بیمارستان‌ها و مراکز درمانی با چالش‌های شیفت‌کاری و تعهد پرسنل روبرو هستند.",
                ColorTheme = "#0891b2",
                Challenges = new List<Challenge>
                {
                    new Challenge { Title = "شیفت‌کاری سنگین", Description = "مدیریت شیفت‌های شبانه و تعادل کار و زندگی" },
                    new Challenge { Title = "فرسودگی شغلی", Description = "فرسودگی ناشی از فشار کاری بالا" },
                    new Challenge { Title = "حفظ پرسنل متخصص", Description = "جلوگیری از مهاجرت پرستاران و پزشکان" }
                },
                Services = new List<Service>
                {
                    new Service { Title = "برنامه‌های سلامت روان", Description = "حمایت از سلامت روان کارکنان درمان", Icon = "heart" },
                    new Service { Title = "مدیریت شیفت هوشمند", Description = "نرم‌افزار برنامه‌ریزی شیفت", Icon = "calendar" },
                    new Service { Title = "نظام نگهداشت پرسنل", Description = "طراحی بسته‌های انگیزشی برای حفظ نیرو", Icon = "gift" }
                },
                CaseStudies = new List<CaseStudy>
                {
                    new CaseStudy { Title = "کاهش ۲۰٪ فرسودگی شغلی در یک بیمارستان", Description = "با اجرای برنامه‌های سلامت روان", Result = "کاهش ۲۰٪ فرسودگی" }
                }
            };

            // ==================== ۶. عمران و ساختمان ====================
            var construction = new Industry
            {
                Slug = "construction",
                Name = "عمران و ساختمان",
                Description = "شرکت‌های عمرانی با چالش‌های مدیریت پروژه‌محور نیروی انسانی روبرو هستند.",
                ColorTheme = "#ca8a04",
                Challenges = new List<Challenge>
                {
                    new Challenge { Title = "مدیریت نیروی انسانی پروژه‌ای", Description = "جابجایی نیروها بین پروژه‌های مختلف" },
                    new Challenge { Title = "ایمنی در کارگاه", Description = "بالا بودن ریسک حوادث کاری" },
                    new Challenge { Title = "جذب نیروی فنی متخصص", Description = "کمبود مهندسان و تکنسین‌های ماهر" }
                },
                Services = new List<Service>
                {
                    new Service { Title = "آموزش ایمنی کارگاهی", Description = "دوره‌های تخصصی HSE برای کارگاه‌ها", Icon = "shield" },
                    new Service { Title = "مدیریت نیروی پروژه‌ای", Description = "سیستم تخصیص نیرو به پروژه‌ها", Icon = "users" },
                    new Service { Title = "جذب مهندسان", Description = "استخدام تخصصی مهندسان عمران", Icon = "search" }
                },
                CaseStudies = new List<CaseStudy>
                {
                    new CaseStudy { Title = "صفر حادثه در یک پروژه بزرگ", Description = "با اجرای برنامه آموزش ایمنی", Result = "صفر حادثه در ۱۲ ماه" }
                }
            };

            // ==================== ۷. خدمات و مهمان‌نوازی ====================
            var hospitality = new Industry
            {
                Slug = "hospitality",
                Name = "خدمات و مهمان‌نوازی",
                Description = "هتل‌ها و رستوران‌ها با چالش‌های استخدام سریع و کیفیت خدمات روبرو هستند.",
                ColorTheme = "#db2777",
                Challenges = new List<Challenge>
                {
                    new Challenge { Title = "استخدام سریع و فصلی", Description = "نیاز به جذب سریع نیرو در فصول اوج" },
                    new Challenge { Title = "کیفیت خدمات", Description = "حفظ استانداردهای بالای خدمات به مشتری" },
                    new Challenge { Title = "آموزش مهارت‌های ارتباطی", Description = "تقویت مهارت‌های برخورد با مشتری" }
                },
                Services = new List<Service>
                {
                    new Service { Title = "استخدام فصلی", Description = "بانک نیروی آماده برای فصول اوج", Icon = "users" },
                    new Service { Title = "آموزش مهارت‌های مشتری‌مداری", Description = "دوره‌های تخصصی خدمات به مشتری", Icon = "smile" },
                    new Service { Title = "ارزیابی کیفیت خدمات", Description = "سیستم نظرسنجی و بهبود مستمر", Icon = "chart" }
                },
                CaseStudies = new List<CaseStudy>
                {
                    new CaseStudy { Title = "افزایش ۳۵٪ رضایت مشتریان یک هتل", Description = "با آموزش مهارت‌های مشتری‌مداری", Result = "افزایش ۳۵٪ رضایت" }
                }
            };

            // ==================== ۸. بازرگانی و لجستیک ====================
            var logistics = new Industry
            {
                Slug = "logistics",
                Name = "بازرگانی و لجستیک",
                Description = "شرکت‌های بازرگانی و لجستیکی با چالش‌های مدیریت نیروی کار پراکنده روبرو هستند.",
                ColorTheme = "#7c3aed",
                Challenges = new List<Challenge>
                {
                    new Challenge { Title = "مدیریت نیروی کار پراکنده", Description = "نظارت بر نیروهای مستقر در نقاط مختلف" },
                    new Challenge { Title = "بهره‌وری رانندگان و انبارداران", Description = "افزایش بهره‌وری نیروهای عملیاتی" },
                    new Challenge { Title = "کاهش خطاهای عملیاتی", Description = "کاهش خطا در فرآیندهای انبار و حمل" }
                },
                Services = new List<Service>
                {
                    new Service { Title = "نظام ارزیابی عملکرد عملیاتی", Description = "KPI برای رانندگان و انبارداران", Icon = "chart" },
                    new Service { Title = "آموزش مدیریت انبار", Description = "دوره‌های تخصصی انبارداری مدرن", Icon = "book" },
                    new Service { Title = "سیستم پایش نیرو", Description = "ابزار ردیابی و مدیریت نیروهای میدانی", Icon = "location" }
                },
                CaseStudies = new List<CaseStudy>
                {
                    new CaseStudy { Title = "کاهش ۵۰٪ خطاهای انبارداری", Description = "با آموزش و نظام ارزیابی", Result = "کاهش ۵۰٪ خطا" }
                }
            };

            // ==================== ذخیره در دیتابیس ====================
            context.Industries.AddRange(
                it, manufacturing, retail, finance,
                health, construction, hospitality, logistics
            );

            context.SaveChanges();
        }
    }
}