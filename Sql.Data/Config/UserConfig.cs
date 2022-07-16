using Identity.Sql.Data.Model;

namespace Identity.Sql.Data.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("T_User");
        }
    }
}
