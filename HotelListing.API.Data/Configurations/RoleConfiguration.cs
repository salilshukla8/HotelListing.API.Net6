using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(

                new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMISNISTRATOR"
                },

                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
                );
        }
    }
}
