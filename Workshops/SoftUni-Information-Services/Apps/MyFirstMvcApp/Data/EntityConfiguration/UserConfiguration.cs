using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFirstMvcApp.Data.EntityModels;

namespace MyFirstMvcApp.Data.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> userBuilder)
        {
            userBuilder
                .Property(p => p.Username)
                .IsUnicode();

            userBuilder
                .Property(p => p.Email)
                .IsUnicode();

            userBuilder
                .Property(x => x.Password)
                .IsUnicode();
        }
    }
}
