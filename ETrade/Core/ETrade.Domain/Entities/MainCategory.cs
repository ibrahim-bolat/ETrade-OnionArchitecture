using ETrade.Domain.Entities.Common;

namespace ETrade.Domain.Entities;

    public class MainCategory : BaseEntity
    {
        public string Name { get; set; }  //Araçlar ,, Yedek Parça Gibi
        public List<SubCategory> SubCategories { get; set; }
    }
