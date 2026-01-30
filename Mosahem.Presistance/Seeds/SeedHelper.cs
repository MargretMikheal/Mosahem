using mosahem.Domain.Entities.Identity;
using mosahem.Domain.Entities.MasterData;
using mosahem.Domain.Enums;
using Mosahem.Application.Interfaces.Security;

namespace mosahem.Persistence.Seeds
{
    public static class SeedHelper
    {
        public static MosahmUser GetAdminUser(IPasswordHasher passwordHasher)
        {
            return new MosahmUser
            {
                Id = Guid.NewGuid(),
                FullName = "Mosahm Admin",
                Email = "admin@mosahem.com",
                UserName = "admin@mosahem.com",
                PhoneNumber = "01000000000",
                Role = UserRole.Admin,
                AuthProvider = AuthProvider.Local,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                PasswordHash = passwordHasher.HashPassword("Mosahem@2026")
            };
        }

        public static List<Field> GetFields()
        {
            return new List<Field>
            {
                // الصحة والرعاية
                new Field { Id = Guid.NewGuid(), NameAr = "الخدمات الطبية والصحية", NameEn = "Medical & Health Services" },
                new Field { Id = Guid.NewGuid(), NameAr = "الصحة النفسية والدعم النفسي", NameEn = "Mental Health & Psychosocial Support" },
                new Field { Id = Guid.NewGuid(), NameAr = "التوعية الصحية والوقائية", NameEn = "Health Awareness & Prevention" },
                
                // التعليم والتنمية
                new Field { Id = Guid.NewGuid(), NameAr = "التعليم ومحو الأمية", NameEn = "Education & Literacy" },
                new Field { Id = Guid.NewGuid(), NameAr = "التدريب المهني والحرفي", NameEn = "Vocational & Craft Training" },
                new Field { Id = Guid.NewGuid(), NameAr = "تنمية مهارات الأطفال", NameEn = "Child Skills Development" },

                // البيئة
                new Field { Id = Guid.NewGuid(), NameAr = "حماية البيئة والتشجير", NameEn = "Environment & Greening" },
                new Field { Id = Guid.NewGuid(), NameAr = "إعادة التدوير والنظافة", NameEn = "Recycling & Cleaning" },
                new Field { Id = Guid.NewGuid(), NameAr = "الطاقة المتجددة", NameEn = "Renewable Energy" },

                // المجتمع والإغاثة
                new Field { Id = Guid.NewGuid(), NameAr = "التكافل الاجتماعي والمساعدات", NameEn = "Social Solidarity & Aid" },
                new Field { Id = Guid.NewGuid(), NameAr = "الإغاثة والكوارث", NameEn = "Relief & Disaster Management" },
                new Field { Id = Guid.NewGuid(), NameAr = "توزيع الوجبات والمواد الغذائية", NameEn = "Food Distribution" },
                new Field { Id = Guid.NewGuid(), NameAr = "ترميم المنازل والمرافق", NameEn = "Housing Renovation" },

                // الفئات الخاصة
                new Field { Id = Guid.NewGuid(), NameAr = "رعاية الأيتام والمسنين", NameEn = "Orphans & Elderly Care" },
                new Field { Id = Guid.NewGuid(), NameAr = "ذوي الهمم والاحتياجات الخاصة", NameEn = "People with Disabilities" },
                new Field { Id = Guid.NewGuid(), NameAr = "دعم المرأة والأسرة", NameEn = "Women & Family Support" },

                // التكنولوجيا والإعلام
                new Field { Id = Guid.NewGuid(), NameAr = "التكنولوجيا والتحول الرقمي", NameEn = "Technology & Digital Transformation" },
                new Field { Id = Guid.NewGuid(), NameAr = "الإعلام والعلاقات العامة", NameEn = "Media & Public Relations" },
                new Field { Id = Guid.NewGuid(), NameAr = "صناعة المحتوى الرقمي", NameEn = "Digital Content Creation" },

                // الثقافة والترفيه
                new Field { Id = Guid.NewGuid(), NameAr = "الفنون والثقافة", NameEn = "Arts & Culture" },
                new Field { Id = Guid.NewGuid(), NameAr = "التنظيم وإدارة الفعاليات", NameEn = "Event Planning & Management" },
                new Field { Id = Guid.NewGuid(), NameAr = "الرياضة والشباب", NameEn = "Sports & Youth" },
                new Field { Id = Guid.NewGuid(), NameAr = "السياحة والآثار", NameEn = "Tourism & Antiquities" },

                // حقوق وحيوان
                new Field { Id = Guid.NewGuid(), NameAr = "حقوق الإنسان والتوعية القانونية", NameEn = "Human Rights & Legal Awareness" },
                new Field { Id = Guid.NewGuid(), NameAr = "الرفق بالحيوان", NameEn = "Animal Welfare" },
                new Field { Id = Guid.NewGuid(), NameAr = "التمكين الاقتصادي والمشاريع الصغيرة", NameEn = "Economic Empowerment" }
            };
        }

        public static List<Skill> GetSkills()
        {
            return new List<Skill>
            {
                // Soft Skills (المهارات الناعمة)
                new Skill { Id = Guid.NewGuid(), NameAr = "التواصل الفعال", NameEn = "Effective Communication", Category = "Soft Skills" },
                new Skill { Id = Guid.NewGuid(), NameAr = "العمل الجماعي", NameEn = "Teamwork", Category = "Soft Skills" },
                new Skill { Id = Guid.NewGuid(), NameAr = "القيادة وإدارة الفرق", NameEn = "Leadership", Category = "Soft Skills" },
                new Skill { Id = Guid.NewGuid(), NameAr = "حل المشكلات", NameEn = "Problem Solving", Category = "Soft Skills" },
                new Skill { Id = Guid.NewGuid(), NameAr = "إدارة الوقت", NameEn = "Time Management", Category = "Soft Skills" },
                new Skill { Id = Guid.NewGuid(), NameAr = "التفاوض والإقناع", NameEn = "Negotiation", Category = "Soft Skills" },
                new Skill { Id = Guid.NewGuid(), NameAr = "التفكير النقدي", NameEn = "Critical Thinking", Category = "Soft Skills" },
                new Skill { Id = Guid.NewGuid(), NameAr = "المرونة والتكيف", NameEn = "Adaptability", Category = "Soft Skills" },
                new Skill { Id = Guid.NewGuid(), NameAr = "الخطابة والعرض", NameEn = "Public Speaking", Category = "Soft Skills" },

                // Medical & Health (طبية)
                new Skill { Id = Guid.NewGuid(), NameAr = "الإسعافات الأولية", NameEn = "First Aid", Category = "Medical" },
                new Skill { Id = Guid.NewGuid(), NameAr = "التمريض", NameEn = "Nursing", Category = "Medical" },
                new Skill { Id = Guid.NewGuid(), NameAr = "الطب العام", NameEn = "General Medicine", Category = "Medical" },
                new Skill { Id = Guid.NewGuid(), NameAr = "العلاج الطبيعي", NameEn = "Physical Therapy", Category = "Medical" },
                new Skill { Id = Guid.NewGuid(), NameAr = "التغذية العلاجية", NameEn = "Clinical Nutrition", Category = "Medical" },

                // Tech & Design (تقنية وتصميم)
                new Skill { Id = Guid.NewGuid(), NameAr = "البرمجة والتطوير", NameEn = "Programming & Development", Category = "Technology" },
                new Skill { Id = Guid.NewGuid(), NameAr = "تصميم الجرافيك", NameEn = "Graphic Design", Category = "Design" },
                new Skill { Id = Guid.NewGuid(), NameAr = "تصميم واجهة المستخدم (UI/UX)", NameEn = "UI/UX Design", Category = "Design" },
                new Skill { Id = Guid.NewGuid(), NameAr = "التصوير الفوتوغرافي", NameEn = "Photography", Category = "Media" },
                new Skill { Id = Guid.NewGuid(), NameAr = "المونتاج وتحرير الفيديو", NameEn = "Video Editing", Category = "Media" },
                new Skill { Id = Guid.NewGuid(), NameAr = "إدارة حسابات التواصل الاجتماعي", NameEn = "Social Media Management", Category = "Marketing" },
                new Skill { Id = Guid.NewGuid(), NameAr = "التسويق الرقمي", NameEn = "Digital Marketing", Category = "Marketing" },
                new Skill { Id = Guid.NewGuid(), NameAr = "تحليل البيانات", NameEn = "Data Analysis", Category = "Technology" },
                new Skill { Id = Guid.NewGuid(), NameAr = "صيانة الحاسب الآلي", NameEn = "IT Support", Category = "Technology" },

                // Content & Languages (محتوى ولغات)
                new Skill { Id = Guid.NewGuid(), NameAr = "كتابة المحتوى", NameEn = "Content Writing", Category = "Content" },
                new Skill { Id = Guid.NewGuid(), NameAr = "الترجمة", NameEn = "Translation", Category = "Languages" },
                new Skill { Id = Guid.NewGuid(), NameAr = "التدقيق اللغوي", NameEn = "Proofreading", Category = "Languages" },
                new Skill { Id = Guid.NewGuid(), NameAr = "اللغة الإنجليزية", NameEn = "English Language", Category = "Languages" },
                new Skill { Id = Guid.NewGuid(), NameAr = "لغة الإشارة", NameEn = "Sign Language", Category = "Languages" },

                // Teaching & Training (تعليم)
                new Skill { Id = Guid.NewGuid(), NameAr = "التدريس والتدريب", NameEn = "Teaching & Training", Category = "Education" },
                new Skill { Id = Guid.NewGuid(), NameAr = "إعداد الحقائب التدريبية", NameEn = "Curriculum Development", Category = "Education" },
                new Skill { Id = Guid.NewGuid(), NameAr = "التعامل مع الأطفال", NameEn = "Child Care", Category = "Education" },
                new Skill { Id = Guid.NewGuid(), NameAr = "التعامل مع ذوي الاحتياجات الخاصة", NameEn = "Special Needs Care", Category = "Education" },

                // Administrative & Manual (إداري ويدوي)
                new Skill { Id = Guid.NewGuid(), NameAr = "إدخال البيانات", NameEn = "Data Entry", Category = "Admin" },
                new Skill { Id = Guid.NewGuid(), NameAr = "الأرشفة وإدارة الملفات", NameEn = "Archiving", Category = "Admin" },
                new Skill { Id = Guid.NewGuid(), NameAr = "الرسم والأعمال اليدوية", NameEn = "Painting & Crafts", Category = "Arts" },
                new Skill { Id = Guid.NewGuid(), NameAr = "الطبخ وإعداد الوجبات", NameEn = "Cooking & Catering", Category = "Services" },
                new Skill { Id = Guid.NewGuid(), NameAr = "قيادة السيارات", NameEn = "Driving", Category = "Services" },
                new Skill { Id = Guid.NewGuid(), NameAr = "النجارة", NameEn = "Carpentry", Category = "Crafts" },
                new Skill { Id = Guid.NewGuid(), NameAr = "السباكة", NameEn = "Plumbing", Category = "Crafts" },
                new Skill { Id = Guid.NewGuid(), NameAr = "الكهرباء", NameEn = "Electrical Work", Category = "Crafts" },
                new Skill { Id = Guid.NewGuid(), NameAr = "الخياطة والتطريز", NameEn = "Sewing & Embroidery", Category = "Crafts" }
            };
        }
    }
}