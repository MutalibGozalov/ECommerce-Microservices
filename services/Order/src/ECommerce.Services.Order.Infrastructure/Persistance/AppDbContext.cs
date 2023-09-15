
using System.Reflection;
using ECommerce.Services.Order.Infrastructure.Persistance.Interceptors;


namespace ECommerce.Services.Order.Infrastructure.Persistance;
public class AppDbContext : DbContext, IAppDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    public AppDbContext(DbContextOptions options, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
    {
        this._auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }


    public DbSet<OrderModel> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Shipping> Shippings { get; set; }
    public DbSet<Tracking> Trackings { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}