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

        public static List<Skill> GetSkills(IReadOnlyList<Field> fields)
        {
            Guid ResolveFieldId(string fieldNameEn)
            {
                var field = fields.FirstOrDefault(f => f.NameEn == fieldNameEn);
                if (field is null)
                    throw new InvalidOperationException($"Field with english name '{fieldNameEn}' was not found in seed data.");

                return field.Id;
            }
            var communicationFieldId = ResolveFieldId("Media & Public Relations");
            var educationFieldId = ResolveFieldId("Education & Literacy");
            var healthFieldId = ResolveFieldId("Medical & Health Services");
            var digitalFieldId = ResolveFieldId("Technology & Digital Transformation");
            var contentFieldId = ResolveFieldId("Digital Content Creation");
            var eventFieldId = ResolveFieldId("Event Planning & Management");
            var womenSupportFieldId = ResolveFieldId("Women & Family Support");
            var economicFieldId = ResolveFieldId("Economic Empowerment");
            return new List<Skill>
            {
            new Skill { Id = Guid.NewGuid(), NameAr = "التواصل الفعال", NameEn = "Effective Communication", FieldId = communicationFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "العمل الجماعي", NameEn = "Teamwork", FieldId = communicationFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "القيادة وإدارة الفرق", NameEn = "Leadership", FieldId = eventFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "حل المشكلات", NameEn = "Problem Solving", FieldId = economicFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "إدارة الوقت", NameEn = "Time Management", FieldId = eventFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "التفاوض والإقناع", NameEn = "Negotiation", FieldId = communicationFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "التفكير النقدي", NameEn = "Critical Thinking", FieldId = educationFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "المرونة والتكيف", NameEn = "Adaptability", FieldId = womenSupportFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "الخطابة والعرض", NameEn = "Public Speaking", FieldId = communicationFieldId },

                new Skill { Id = Guid.NewGuid(), NameAr = "الإسعافات الأولية", NameEn = "First Aid", FieldId = healthFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "التمريض", NameEn = "Nursing", FieldId = healthFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "الطب العام", NameEn = "General Medicine", FieldId = healthFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "العلاج الطبيعي", NameEn = "Physical Therapy", FieldId = healthFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "التغذية العلاجية", NameEn = "Clinical Nutrition", FieldId = healthFieldId },

                new Skill { Id = Guid.NewGuid(), NameAr = "البرمجة والتطوير", NameEn = "Programming & Development", FieldId = digitalFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "تصميم الجرافيك", NameEn = "Graphic Design", FieldId = contentFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "تصميم واجهة المستخدم (UI/UX)", NameEn = "UI/UX Design", FieldId = contentFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "التصوير الفوتوغرافي", NameEn = "Photography", FieldId = contentFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "المونتاج وتحرير الفيديو", NameEn = "Video Editing", FieldId = contentFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "إدارة حسابات التواصل الاجتماعي", NameEn = "Social Media Management", FieldId = communicationFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "التسويق الرقمي", NameEn = "Digital Marketing", FieldId = digitalFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "تحليل البيانات", NameEn = "Data Analysis", FieldId = digitalFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "صيانة الحاسب الآلي", NameEn = "IT Support", FieldId = digitalFieldId },

                new Skill { Id = Guid.NewGuid(), NameAr = "كتابة المحتوى", NameEn = "Content Writing", FieldId = contentFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "الترجمة", NameEn = "Translation", FieldId = educationFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "التدقيق اللغوي", NameEn = "Proofreading", FieldId = educationFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "اللغة الإنجليزية", NameEn = "English Language", FieldId = educationFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "لغة الإشارة", NameEn = "Sign Language", FieldId = womenSupportFieldId },

                new Skill { Id = Guid.NewGuid(), NameAr = "التدريس والتدريب", NameEn = "Teaching & Training", FieldId = educationFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "إعداد الحقائب التدريبية", NameEn = "Curriculum Development", FieldId = educationFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "التعامل مع الأطفال", NameEn = "Child Care", FieldId = educationFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "التعامل مع ذوي الاحتياجات الخاصة", NameEn = "Special Needs Care", FieldId = womenSupportFieldId },

                new Skill { Id = Guid.NewGuid(), NameAr = "إدخال البيانات", NameEn = "Data Entry", FieldId = digitalFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "الأرشفة وإدارة الملفات", NameEn = "Archiving", FieldId = eventFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "الرسم والأعمال اليدوية", NameEn = "Painting & Crafts", FieldId = contentFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "الطبخ وإعداد الوجبات", NameEn = "Cooking & Catering", FieldId = economicFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "قيادة السيارات", NameEn = "Driving", FieldId = economicFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "النجارة", NameEn = "Carpentry", FieldId = economicFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "السباكة", NameEn = "Plumbing", FieldId = economicFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "الكهرباء", NameEn = "Electrical Work", FieldId = economicFieldId },
                new Skill { Id = Guid.NewGuid(), NameAr = "الخياطة والتطريز", NameEn = "Sewing & Embroidery", FieldId = economicFieldId }

            };
        }
    }
}