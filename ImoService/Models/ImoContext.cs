using ImoService.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Imo.Models;

public class ImoContext : DbContext
{
    public ImoContext(DbContextOptions<ImoContext> options)
        : base(options)
    {
    }

    public DbSet<Imovel> Imovels { get; set; }
    public DbSet<Imovel_Foto> Imovel_Fotos { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
       // builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}

//public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ImoUser>
//{
//    public void Configure(EntityTypeBuilder<ImoUser> builder)
//    {
//        builder.Property(x => x.FirstName).HasMaxLength(100);
//        builder.Property(x => x.LastName).HasMaxLength(100);
//    }
//}
