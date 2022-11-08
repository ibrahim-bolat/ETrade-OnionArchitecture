using ETrade.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Action = ETrade.Domain.Entities.Action;

namespace ETrade.Persistence.Mappings;

    public class ActionMap:IEntityTypeConfiguration<Action>
    {
        public void Configure(EntityTypeBuilder<Action> builder)
        {
            builder.HasKey(subCategory => subCategory.Id);
            builder.Property(subCategory => subCategory.Id).ValueGeneratedOnAdd();
            builder.Property(subCategory => subCategory.Note).HasMaxLength(500);
            builder.HasOne(action => action.Menu).WithMany(menu => menu.Actions)
                .HasForeignKey(action => action.MenuId).OnDelete(DeleteBehavior.Cascade);
            /*
            builder.HasData(new Action()
            {
                Id = 1,
                MenuId = 1,
                ActionType = "Updating",
                HttpType = "HttpPost",
                Definition = "Edit Password",
                Code = "123",
                IsActive = true
            },new Action()
                {
                    Id = 2,
                    MenuId = 1,
                    ActionType = "Updating",
                    HttpType = "HttpPost",
                    Definition = "Edit Profile",
                    Code = "1234",
                    IsActive = true
                },new Action()
                {
                    Id = 3,
                    MenuId = 2,
                    ActionType = "Writing",
                    HttpType = "HttpPost",
                    Definition = "Create Address",
                    Code = "12345",
                    IsActive = true
                },new Action()
                {
                    Id = 4,
                    MenuId = 2,
                    ActionType = "Reading",
                    HttpType = "HttpGet",
                    Definition = "Get By Id",
                    Code = "123456",
                    IsActive = true
                });
                */
        }
    }
