
namespace ECommerce.Services.Order.Infrastructure.Persistance;
public class AddDbContextInitializer
{
    private readonly AppDbContext _context;
    private readonly ILogger<AddDbContextInitializer> _logger;
    public AddDbContextInitializer(AppDbContext context, ILogger<AddDbContextInitializer> logger)
    {
        this._logger = logger;
        this._context = context;

    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while initalising the database");
            throw;
        }
    }
}
