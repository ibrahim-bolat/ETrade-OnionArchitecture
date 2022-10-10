using ETrade.Domain.Entities.Common;
using ETrade.Domain.Entities.Identity;

namespace ETrade.Domain.Entities;
public class UserImage:BaseEntity,IEntity
    {
        public string ImageTitle { get; set; }
        public string ImagePath { get; set; }
        public string ImageAltText { get; set; }
        public bool Profil { get; set; }
        public int UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
