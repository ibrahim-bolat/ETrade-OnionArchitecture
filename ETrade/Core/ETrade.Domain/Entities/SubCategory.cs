using ETrade.Domain.Entities.Common;

namespace ETrade.Domain.Entities;

    public class SubCategory : BaseEntity,IEntity
    {
        public string Name { get; set; }    // Ototmobil , Minivan & Panelvan ,Elektrikli Araçlar gibi
        public int MainCategoryId { get; set; }
        public MainCategory MainCategory { get; set; }
        public List<Brand> Brands { get; set; }
    }
