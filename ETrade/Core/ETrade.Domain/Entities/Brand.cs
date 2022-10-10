using ETrade.Domain.Entities.Common;

namespace ETrade.Domain.Entities;

    public class Brand : BaseEntity,IEntity
    {
        public string Name { get; set; }  //Volkswagen gibi
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public List<Model> Models { get; set; }
    }
