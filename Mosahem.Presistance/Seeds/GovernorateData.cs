using mosahem.Domain.Entities.Location;

namespace mosahem.Persistence.Seeds
{
    public static class GovernorateData
    {
        public static List<Governorate> GetGovernoratesWithCities()
        {
            return new List<Governorate>
            {
                // 1. القاهرة (Cairo)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "القاهرة", NameEn = "Cairo",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "مدينة نصر", NameEn = "Nasr City" },
                        new City { Id = Guid.NewGuid(), NameAr = "مصر الجديدة", NameEn = "Heliopolis" },
                        new City { Id = Guid.NewGuid(), NameAr = "المعادي", NameEn = "Maadi" },
                        new City { Id = Guid.NewGuid(), NameAr = "التجمع الخامس", NameEn = "New Cairo" },
                        new City { Id = Guid.NewGuid(), NameAr = "الرحاب", NameEn = "Al Rehab" },
                        new City { Id = Guid.NewGuid(), NameAr = "الشروق", NameEn = "El Sherouk" },
                        new City { Id = Guid.NewGuid(), NameAr = "مدينتي", NameEn = "Madinaty" },
                        new City { Id = Guid.NewGuid(), NameAr = "شبرا", NameEn = "Shoubra" },
                        new City { Id = Guid.NewGuid(), NameAr = "الزمالك", NameEn = "Zamalek" },
                        new City { Id = Guid.NewGuid(), NameAr = "وسط البلد", NameEn = "Downtown" },
                        new City { Id = Guid.NewGuid(), NameAr = "العباسية", NameEn = "Abbaseya" },
                        new City { Id = Guid.NewGuid(), NameAr = "المنيل", NameEn = "Manial" },
                        new City { Id = Guid.NewGuid(), NameAr = "حلوان", NameEn = "Helwan" },
                        new City { Id = Guid.NewGuid(), NameAr = "المقطم", NameEn = "Mokattam" },
                        new City { Id = Guid.NewGuid(), NameAr = "عين شمس", NameEn = "Ain Shams" },
                        new City { Id = Guid.NewGuid(), NameAr = "الزيتون", NameEn = "El Zeitoun" },
                        new City { Id = Guid.NewGuid(), NameAr = "السيدة زينب", NameEn = "Sayeda Zeinab" },
                        new City { Id = Guid.NewGuid(), NameAr = "المرج", NameEn = "El Marg" },
                        new City { Id = Guid.NewGuid(), NameAr = "مصر القديمة", NameEn = "Old Cairo" },
                        new City { Id = Guid.NewGuid(), NameAr = "حدائق القبة", NameEn = "Hadayek El Kobba" }
                    }
                },

                // 2. الجيزة (Giza)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "الجيزة", NameEn = "Giza",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "السادس من أكتوبر", NameEn = "6th of October" },
                        new City { Id = Guid.NewGuid(), NameAr = "الشيخ زايد", NameEn = "Sheikh Zayed" },
                        new City { Id = Guid.NewGuid(), NameAr = "المهندسين", NameEn = "Mohandessin" },
                        new City { Id = Guid.NewGuid(), NameAr = "الدقي", NameEn = "Dokki" },
                        new City { Id = Guid.NewGuid(), NameAr = "الهرم", NameEn = "Haram" },
                        new City { Id = Guid.NewGuid(), NameAr = "فيصل", NameEn = "Faisal" },
                        new City { Id = Guid.NewGuid(), NameAr = "إمبابة", NameEn = "Imbaba" },
                        new City { Id = Guid.NewGuid(), NameAr = "العجوزة", NameEn = "Agouza" },
                        new City { Id = Guid.NewGuid(), NameAr = "الوراق", NameEn = "Warraq" },
                        new City { Id = Guid.NewGuid(), NameAr = "البدرشين", NameEn = "Badrashein" },
                        new City { Id = Guid.NewGuid(), NameAr = "العياط", NameEn = "Ayat" },
                        new City { Id = Guid.NewGuid(), NameAr = "الصف", NameEn = "Saff" },
                        new City { Id = Guid.NewGuid(), NameAr = "أطفيح", NameEn = "Atfih" },
                        new City { Id = Guid.NewGuid(), NameAr = "الحوامدية", NameEn = "Hawamdia" }
                    }
                },

                // 3. الإسكندرية (Alexandria)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "الإسكندرية", NameEn = "Alexandria",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "سموحة", NameEn = "Smouha" },
                        new City { Id = Guid.NewGuid(), NameAr = "محطة الرمل", NameEn = "Mahta Al Raml" },
                        new City { Id = Guid.NewGuid(), NameAr = "المنتزه", NameEn = "Montaza" },
                        new City { Id = Guid.NewGuid(), NameAr = "ميامي", NameEn = "Miami" },
                        new City { Id = Guid.NewGuid(), NameAr = "سيدي جابر", NameEn = "Sidi Gaber" },
                        new City { Id = Guid.NewGuid(), NameAr = "العجمي", NameEn = "Agami" },
                        new City { Id = Guid.NewGuid(), NameAr = "العصافرة", NameEn = "Asafra" },
                        new City { Id = Guid.NewGuid(), NameAr = "لوران", NameEn = "Loran" },
                        new City { Id = Guid.NewGuid(), NameAr = "سيدي بشر", NameEn = "Sidi Bishr" },
                        new City { Id = Guid.NewGuid(), NameAr = "برج العرب", NameEn = "Borg El Arab" },
                        new City { Id = Guid.NewGuid(), NameAr = "رشيد", NameEn = "Rashid" }
                    }
                },

                // 4. القليوبية (Qalyubia)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "القليوبية", NameEn = "Qalyubia",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "بنها", NameEn = "Banha" },
                        new City { Id = Guid.NewGuid(), NameAr = "شبرا الخيمة", NameEn = "Shoubra El Kheima" },
                        new City { Id = Guid.NewGuid(), NameAr = "القناطر الخيرية", NameEn = "Qanater" },
                        new City { Id = Guid.NewGuid(), NameAr = "العبور", NameEn = "Obour City" },
                        new City { Id = Guid.NewGuid(), NameAr = "طوخ", NameEn = "Toukh" },
                        new City { Id = Guid.NewGuid(), NameAr = "قليوب", NameEn = "Qalyub" },
                        new City { Id = Guid.NewGuid(), NameAr = "الخانكة", NameEn = "Khanka" },
                        new City { Id = Guid.NewGuid(), NameAr = "كفر شكر", NameEn = "Kafr Shukr" }
                    }
                },

                // 5. الدقهلية (Dakahlia)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "الدقهلية", NameEn = "Dakahlia",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "المنصورة", NameEn = "Mansoura" },
                        new City { Id = Guid.NewGuid(), NameAr = "طلخا", NameEn = "Talkha" },
                        new City { Id = Guid.NewGuid(), NameAr = "ميت غمر", NameEn = "Mit Ghamr" },
                        new City { Id = Guid.NewGuid(), NameAr = "السنبلاوين", NameEn = "Sinbillawin" },
                        new City { Id = Guid.NewGuid(), NameAr = "دكرنس", NameEn = "Dekernes" },
                        new City { Id = Guid.NewGuid(), NameAr = "بلقاس", NameEn = "Belqas" },
                        new City { Id = Guid.NewGuid(), NameAr = "المنزلة", NameEn = "Manzala" },
                        new City { Id = Guid.NewGuid(), NameAr = "شربين", NameEn = "Sherbin" },
                        new City { Id = Guid.NewGuid(), NameAr = "أجا", NameEn = "Aga" }
                    }
                },

                // 6. الشرقية (Sharkia)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "الشرقية", NameEn = "Sharkia",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "الزقازيق", NameEn = "Zagazig" },
                        new City { Id = Guid.NewGuid(), NameAr = "العاشر من رمضان", NameEn = "10th of Ramadan" },
                        new City { Id = Guid.NewGuid(), NameAr = "منيا القمح", NameEn = "Minya Al Qamh" },
                        new City { Id = Guid.NewGuid(), NameAr = "بلبيس", NameEn = "Bilbeis" },
                        new City { Id = Guid.NewGuid(), NameAr = "فاقوس", NameEn = "Faqous" },
                        new City { Id = Guid.NewGuid(), NameAr = "أبو حماد", NameEn = "Abu Hammad" },
                        new City { Id = Guid.NewGuid(), NameAr = "ههيا", NameEn = "Hihya" },
                        new City { Id = Guid.NewGuid(), NameAr = "كفر صقر", NameEn = "Kafr Saqr" },
                        new City { Id = Guid.NewGuid(), NameAr = "ديرب نجم", NameEn = "Dairb Negm" }
                    }
                },

                // 7. الغربية (Gharbia)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "الغربية", NameEn = "Gharbia",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "طنطا", NameEn = "Tanta" },
                        new City { Id = Guid.NewGuid(), NameAr = "المحلة الكبرى", NameEn = "Mahalla Al Kubra" },
                        new City { Id = Guid.NewGuid(), NameAr = "كفر الزيات", NameEn = "Kafr El Zayat" },
                        new City { Id = Guid.NewGuid(), NameAr = "زفتى", NameEn = "Zifta" },
                        new City { Id = Guid.NewGuid(), NameAr = "السنطة", NameEn = "El Santa" },
                        new City { Id = Guid.NewGuid(), NameAr = "بسيون", NameEn = "Basyoun" },
                        new City { Id = Guid.NewGuid(), NameAr = "سمنود", NameEn = "Samannoud" }
                    }
                },

                // 8. المنوفية (Monufia)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "المنوفية", NameEn = "Monufia",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "شبين الكوم", NameEn = "Shibin El Kom" },
                        new City { Id = Guid.NewGuid(), NameAr = "مدينة السادات", NameEn = "Sadat City" },
                        new City { Id = Guid.NewGuid(), NameAr = "منوف", NameEn = "Menouf" },
                        new City { Id = Guid.NewGuid(), NameAr = "أشمون", NameEn = "Ashmoun" },
                        new City { Id = Guid.NewGuid(), NameAr = "قويسنا", NameEn = "Quesna" },
                        new City { Id = Guid.NewGuid(), NameAr = "بركة السبع", NameEn = "Berkat El Saba" },
                        new City { Id = Guid.NewGuid(), NameAr = "الباجور", NameEn = "Bagour" },
                        new City { Id = Guid.NewGuid(), NameAr = "تلا", NameEn = "Tala" }
                    }
                },

                // 9. البحيرة (Beheira)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "البحيرة", NameEn = "Beheira",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "دمنهور", NameEn = "Damanhour" },
                        new City { Id = Guid.NewGuid(), NameAr = "كفر الدوار", NameEn = "Kafr El Dawar" },
                        new City { Id = Guid.NewGuid(), NameAr = "إيتاي البارود", NameEn = "Etay El Baroud" },
                        new City { Id = Guid.NewGuid(), NameAr = "أبو حمص", NameEn = "Abu Homos" },
                        new City { Id = Guid.NewGuid(), NameAr = "رشيد", NameEn = "Rosetta" },
                        new City { Id = Guid.NewGuid(), NameAr = "وادي النطرون", NameEn = "Wadi El Natrun" },
                        new City { Id = Guid.NewGuid(), NameAr = "كوم حمادة", NameEn = "Kom Hamada" }
                    }
                },

                // 10. كفر الشيخ (Kafr El Sheikh)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "كفر الشيخ", NameEn = "Kafr El Sheikh",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "كفر الشيخ", NameEn = "Kafr El Sheikh" },
                        new City { Id = Guid.NewGuid(), NameAr = "دسوق", NameEn = "Desouk" },
                        new City { Id = Guid.NewGuid(), NameAr = "فوه", NameEn = "Fouh" },
                        new City { Id = Guid.NewGuid(), NameAr = "بلطيم", NameEn = "Baltim" },
                        new City { Id = Guid.NewGuid(), NameAr = "سيدي سالم", NameEn = "Sidi Salem" },
                        new City { Id = Guid.NewGuid(), NameAr = "الحامول", NameEn = "Hamoul" }
                    }
                },

                // 11. دمياط (Damietta)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "دمياط", NameEn = "Damietta",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "دمياط", NameEn = "Damietta" },
                        new City { Id = Guid.NewGuid(), NameAr = "رأس البر", NameEn = "Ras El Bar" },
                        new City { Id = Guid.NewGuid(), NameAr = "دمياط الجديدة", NameEn = "New Damietta" },
                        new City { Id = Guid.NewGuid(), NameAr = "فارسكور", NameEn = "Faraskour" },
                        new City { Id = Guid.NewGuid(), NameAr = "الزرقا", NameEn = "Zarqa" }
                    }
                },

                // 12. بورسعيد (Port Said)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "بورسعيد", NameEn = "Port Said",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "حي الشرق", NameEn = "Sharq District" },
                        new City { Id = Guid.NewGuid(), NameAr = "حي العرب", NameEn = "Arab District" },
                        new City { Id = Guid.NewGuid(), NameAr = "بورفؤاد", NameEn = "Port Fouad" },
                        new City { Id = Guid.NewGuid(), NameAr = "حي الزهور", NameEn = "Zohour District" }
                    }
                },

                // 13. الإسماعيلية (Ismailia)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "الإسماعيلية", NameEn = "Ismailia",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "الإسماعيلية", NameEn = "Ismailia" },
                        new City { Id = Guid.NewGuid(), NameAr = "القصاصين", NameEn = "Qassasin" },
                        new City { Id = Guid.NewGuid(), NameAr = "التل الكبير", NameEn = "El Tal El Kebir" },
                        new City { Id = Guid.NewGuid(), NameAr = "فايد", NameEn = "Fayed" },
                        new City { Id = Guid.NewGuid(), NameAr = "القنطرة شرق", NameEn = "Qantara Sharq" },
                        new City { Id = Guid.NewGuid(), NameAr = "القنطرة غرب", NameEn = "Qantara Gharb" }
                    }
                },

                // 14. السويس (Suez)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "السويس", NameEn = "Suez",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "السويس", NameEn = "Suez" },
                        new City { Id = Guid.NewGuid(), NameAr = "حي الأربعين", NameEn = "Arbaeen District" },
                        new City { Id = Guid.NewGuid(), NameAr = "حي الجناين", NameEn = "Ganayen District" },
                        new City { Id = Guid.NewGuid(), NameAr = "العين السخنة", NameEn = "Ain Sokhna" }
                    }
                },

                // 15. البحر الأحمر (Red Sea)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "البحر الأحمر", NameEn = "Red Sea",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "الغردقة", NameEn = "Hurghada" },
                        new City { Id = Guid.NewGuid(), NameAr = "رأس غارب", NameEn = "Ras Gharib" },
                        new City { Id = Guid.NewGuid(), NameAr = "سفاجا", NameEn = "Safaga" },
                        new City { Id = Guid.NewGuid(), NameAr = "القصير", NameEn = "Quseir" },
                        new City { Id = Guid.NewGuid(), NameAr = "مرسى علم", NameEn = "Marsa Alam" },
                        new City { Id = Guid.NewGuid(), NameAr = "الجونة", NameEn = "El Gouna" },
                        new City { Id = Guid.NewGuid(), NameAr = "شلاتين", NameEn = "Shalatin" },
                        new City { Id = Guid.NewGuid(), NameAr = "حلايب", NameEn = "Halayeb" }
                    }
                },

                // 16. جنوب سيناء (South Sinai)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "جنوب سيناء", NameEn = "South Sinai",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "شرم الشيخ", NameEn = "Sharm El Sheikh" },
                        new City { Id = Guid.NewGuid(), NameAr = "دهب", NameEn = "Dahab" },
                        new City { Id = Guid.NewGuid(), NameAr = "نويبع", NameEn = "Nuweiba" },
                        new City { Id = Guid.NewGuid(), NameAr = "طابا", NameEn = "Taba" },
                        new City { Id = Guid.NewGuid(), NameAr = "الطور", NameEn = "El Tor" },
                        new City { Id = Guid.NewGuid(), NameAr = "سانت كاترين", NameEn = "Saint Catherine" },
                        new City { Id = Guid.NewGuid(), NameAr = "رأس سدر", NameEn = "Ras Sudr" }
                    }
                },

                // 17. شمال سيناء (North Sinai)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "شمال سيناء", NameEn = "North Sinai",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "العريش", NameEn = "Arish" },
                        new City { Id = Guid.NewGuid(), NameAr = "الشيخ زويد", NameEn = "Sheikh Zuweid" },
                        new City { Id = Guid.NewGuid(), NameAr = "رفح", NameEn = "Rafah" },
                        new City { Id = Guid.NewGuid(), NameAr = "بئر العبد", NameEn = "Bir El Abd" },
                        new City { Id = Guid.NewGuid(), NameAr = "الحسنة", NameEn = "Hasana" },
                        new City { Id = Guid.NewGuid(), NameAr = "نخل", NameEn = "Nakhl" }
                    }
                },

                // 18. مطروح (Matrouh)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "مطروح", NameEn = "Matrouh",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "مرسى مطروح", NameEn = "Marsa Matrouh" },
                        new City { Id = Guid.NewGuid(), NameAr = "العلمين", NameEn = "El Alamein" },
                        new City { Id = Guid.NewGuid(), NameAr = "الضبعة", NameEn = "Dabaa" },
                        new City { Id = Guid.NewGuid(), NameAr = "سيوة", NameEn = "Siwa" },
                        new City { Id = Guid.NewGuid(), NameAr = "السلوم", NameEn = "Salloum" },
                        new City { Id = Guid.NewGuid(), NameAr = "الحمام", NameEn = "Hamam" }
                    }
                },

                // 19. بني سويف (Beni Suef)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "بني سويف", NameEn = "Beni Suef",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "بني سويف", NameEn = "Beni Suef" },
                        new City { Id = Guid.NewGuid(), NameAr = "الواسطى", NameEn = "Al Wasta" },
                        new City { Id = Guid.NewGuid(), NameAr = "ناصر", NameEn = "Nasser" },
                        new City { Id = Guid.NewGuid(), NameAr = "إهناسيا", NameEn = "Ehnasia" },
                        new City { Id = Guid.NewGuid(), NameAr = "ببا", NameEn = "Beba" },
                        new City { Id = Guid.NewGuid(), NameAr = "الفشن", NameEn = "Fashn" },
                        new City { Id = Guid.NewGuid(), NameAr = "سمسطا", NameEn = "Samasta" }
                    }
                },

                // 20. الفيوم (Fayoum)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "الفيوم", NameEn = "Fayoum",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "الفيوم", NameEn = "Fayoum" },
                        new City { Id = Guid.NewGuid(), NameAr = "طامية", NameEn = "Tamiya" },
                        new City { Id = Guid.NewGuid(), NameAr = "سنورس", NameEn = "Snores" },
                        new City { Id = Guid.NewGuid(), NameAr = "إطسا", NameEn = "Etsa" },
                        new City { Id = Guid.NewGuid(), NameAr = "ابشواي", NameEn = "Ibsheway" },
                        new City { Id = Guid.NewGuid(), NameAr = "يوسف الصديق", NameEn = "Yousef El Seddik" }
                    }
                },

                // 21. المنيا (Minya)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "المنيا", NameEn = "Minya",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "المنيا", NameEn = "Minya" },
                        new City { Id = Guid.NewGuid(), NameAr = "المنيا الجديدة", NameEn = "New Minya" },
                        new City { Id = Guid.NewGuid(), NameAr = "مغاغة", NameEn = "Maghagha" },
                        new City { Id = Guid.NewGuid(), NameAr = "بني مزار", NameEn = "Beni Mazar" },
                        new City { Id = Guid.NewGuid(), NameAr = "مطاي", NameEn = "Matay" },
                        new City { Id = Guid.NewGuid(), NameAr = "سمالوط", NameEn = "Samalut" },
                        new City { Id = Guid.NewGuid(), NameAr = "أبو قرقاص", NameEn = "Abu Qurqas" },
                        new City { Id = Guid.NewGuid(), NameAr = "ملوي", NameEn = "Mallawi" },
                        new City { Id = Guid.NewGuid(), NameAr = "دير مواس", NameEn = "Deir Mawas" }
                    }
                },

                // 22. أسيوط (Assiut)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "أسيوط", NameEn = "Assiut",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "أسيوط", NameEn = "Assiut" },
                        new City { Id = Guid.NewGuid(), NameAr = "ديروط", NameEn = "Dayrout" },
                        new City { Id = Guid.NewGuid(), NameAr = "القوصية", NameEn = "Qusiya" },
                        new City { Id = Guid.NewGuid(), NameAr = "أبنوب", NameEn = "Abnoub" },
                        new City { Id = Guid.NewGuid(), NameAr = "منفلوط", NameEn = "Manfalut" },
                        new City { Id = Guid.NewGuid(), NameAr = "أبو تيج", NameEn = "Abu Tig" },
                        new City { Id = Guid.NewGuid(), NameAr = "الغنايم", NameEn = "El Ghanaim" },
                        new City { Id = Guid.NewGuid(), NameAr = "ساحل سليم", NameEn = "Sahel Selim" },
                        new City { Id = Guid.NewGuid(), NameAr = "البداري", NameEn = "El Badari" },
                        new City { Id = Guid.NewGuid(), NameAr = "صدفا", NameEn = "Sidfa" }
                    }
                },

                // 23. سوهاج (Sohag)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "سوهاج", NameEn = "Sohag",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "سوهاج", NameEn = "Sohag" },
                        new City { Id = Guid.NewGuid(), NameAr = "أخميم", NameEn = "Akhmim" },
                        new City { Id = Guid.NewGuid(), NameAr = "البتلينا", NameEn = "Balyana" },
                        new City { Id = Guid.NewGuid(), NameAr = "جرجا", NameEn = "Girga" },
                        new City { Id = Guid.NewGuid(), NameAr = "طهطا", NameEn = "Tahta" },
                        new City { Id = Guid.NewGuid(), NameAr = "المراغة", NameEn = "Maragha" },
                        new City { Id = Guid.NewGuid(), NameAr = "طما", NameEn = "Tima" },
                        new City { Id = Guid.NewGuid(), NameAr = "دار السلام", NameEn = "Dar El Salam" },
                        new City { Id = Guid.NewGuid(), NameAr = "المنشأة", NameEn = "Monsha'a" },
                        new City { Id = Guid.NewGuid(), NameAr = "جهينة", NameEn = "Juhayna" },
                        new City { Id = Guid.NewGuid(), NameAr = "ساقلتة", NameEn = "Saqulta" }
                    }
                },

                // 24. قنا (Qena)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "قنا", NameEn = "Qena",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "قنا", NameEn = "Qena" },
                        new City { Id = Guid.NewGuid(), NameAr = "أبو تشت", NameEn = "Abu Tesht" },
                        new City { Id = Guid.NewGuid(), NameAr = "فرشوط", NameEn = "Farshut" },
                        new City { Id = Guid.NewGuid(), NameAr = "نجع حمادي", NameEn = "Nag Hammadi" },
                        new City { Id = Guid.NewGuid(), NameAr = "دشنا", NameEn = "Dishna" },
                        new City { Id = Guid.NewGuid(), NameAr = "الوقف", NameEn = "Waqf" },
                        new City { Id = Guid.NewGuid(), NameAr = "قفط", NameEn = "Qift" },
                        new City { Id = Guid.NewGuid(), NameAr = "قوص", NameEn = "Qus" },
                        new City { Id = Guid.NewGuid(), NameAr = "نقادة", NameEn = "Naqada" }
                    }
                },

                // 25. الأقصر (Luxor)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "الأقصر", NameEn = "Luxor",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "الأقصر", NameEn = "Luxor" },
                        new City { Id = Guid.NewGuid(), NameAr = "طيبة", NameEn = "Thebes (Tiba)" },
                        new City { Id = Guid.NewGuid(), NameAr = "القرنة", NameEn = "Qurna" },
                        new City { Id = Guid.NewGuid(), NameAr = "أرمنت", NameEn = "Armant" },
                        new City { Id = Guid.NewGuid(), NameAr = "إسنا", NameEn = "Esna" }
                    }
                },

                // 26. أسوان (Aswan)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "أسوان", NameEn = "Aswan",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "أسوان", NameEn = "Aswan" },
                        new City { Id = Guid.NewGuid(), NameAr = "دراو", NameEn = "Daraw" },
                        new City { Id = Guid.NewGuid(), NameAr = "كوم أمبو", NameEn = "Kom Ombo" },
                        new City { Id = Guid.NewGuid(), NameAr = "نصر النوبة", NameEn = "Nasr Al Nuba" },
                        new City { Id = Guid.NewGuid(), NameAr = "إدفو", NameEn = "Edfu" },
                        new City { Id = Guid.NewGuid(), NameAr = "أبو سمبل", NameEn = "Abu Simbel" }
                    }
                },

                // 27. الوادي الجديد (New Valley)
                new Governorate
                {
                    Id = Guid.NewGuid(), NameAr = "الوادي الجديد", NameEn = "New Valley",
                    Cities = new List<City>
                    {
                        new City { Id = Guid.NewGuid(), NameAr = "الخارجة", NameEn = "Kharga" },
                        new City { Id = Guid.NewGuid(), NameAr = "الداخلة", NameEn = "Dakhla" },
                        new City { Id = Guid.NewGuid(), NameAr = "الفرافرة", NameEn = "Farafra" },
                        new City { Id = Guid.NewGuid(), NameAr = "باريس", NameEn = "Baris" },
                        new City { Id = Guid.NewGuid(), NameAr = "بلاط", NameEn = "Balat" }
                    }
                }
            };
        }
    }
}