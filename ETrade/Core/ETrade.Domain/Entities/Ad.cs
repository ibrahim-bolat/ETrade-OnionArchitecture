
using ETrade.Domain.Entities.Common;
using ETrade.Domain.Enums;

namespace ETrade.Domain.Entities;

    public class Ad : BaseEntity,IEntity
    {
        public string AdNo { get; set; }  //123458752 gibi
        public string AdTitle { get; set; }  //SAHİBİNDEN 2016 SERVİS BAKIMLI İYİ NİYET GARANTİLİ GOLF gibi
        public DateTime AdDate { get; set; } // 25 Eylül 2022 gibi
        public AdVehicleStatus AdVehicleStatus { get; set; } //2.El ,Sıfır gibi
        public AdFromWhoType AdFromWho { get; set; } // Sahibinden gibi
        public AdSwapStatus AdSwapStatus { get; set; } // Evet Hayır gibi
        public DamageStatus DamageStatus { get; set; } // Ağır Hasarlı gibi
        public decimal AdVehiclePrice { get; set; } // 500.500 TL gibi
        public string AdDetail { get; set; } // Araç Hakkında Uzun Açıklamalar 

        public Model Model { get; set; }
        public VehicleAddress VehicleAddress { get; set; }
        public List<VehicleImage> VehicleImages { get; set; }
        
    }
