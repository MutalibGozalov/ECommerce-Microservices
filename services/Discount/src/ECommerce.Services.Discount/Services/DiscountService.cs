using System.Data;
using Dapper;
using ECommerce.Services.Discount.Dtos;
using ECommerce.Shared.Dtos;
using Npgsql;


namespace ECommerce.Services.Discount.Services;
public class DiscountService : IDiscountService
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _dbConnection;

    public DiscountService(IConfiguration configuration)
    {
        _configuration = configuration;
        _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
    }


    public async Task<Response<List<DiscountDto>>> GetAll()
    {
        var discounts = await _dbConnection.QueryAsync<DiscountDto>("SELECT * FROM discount");
        return Response<List<DiscountDto>>.Success(discounts.ToList(), 200);
    }

    public async Task<Response<DiscountDto>> GetById(int id)
    {
        var discount = (await _dbConnection.QueryAsync<DiscountDto>($"SELECT * FROM discount WHERE id = @Id", new { Id = id })).SingleOrDefault();

        if (discount is null)
        {
            return Response<DiscountDto>.Failure("Discount not fount", 404);
        }

        return Response<DiscountDto>.Success(discount, 200);
    }

    public async Task<Response<DiscountDto>> GetByCodeAndUserId(string code, int userId)
    {
        var discount = (await _dbConnection.QueryAsync<DiscountDto>($"SELECT * FROM discount WHERE code = @Code AND userid = @Id", new { Code = code, Id = userId })).SingleOrDefault();

        if (discount is null)
        {
            return Response<DiscountDto>.Failure("Discount not fount", 404);
        }

        return Response<DiscountDto>.Success(discount, 200);
    }

    public async Task<Response<NoContent>> Create(DiscountDto discountDto)
    {
        var saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount (userId, rate, code) VALUES(@UserId, @Rate, @Code)", discountDto);

        if (saveStatus > 0)
        {
            return Response<NoContent>.Success(204);
        }

        return Response<NoContent>.Failure("An error occurred while adding discount", 500);

    }

    public async Task<Response<NoContent>> Update(DiscountDto discountDto)
    {
        var updateStatus = await _dbConnection.ExecuteAsync("UPDATE discount SET userid = @UserId, code = @Code, rate = @Rate WHERE id = @Id", discountDto);

        if (updateStatus > 0)
        {
            return Response<NoContent>.Success(204);
        }

        return Response<NoContent>.Failure("Discount not found", 404);
    }
    public async Task<Response<NoContent>> Delete(int id)
    {
        var deleteStatus = await _dbConnection.ExecuteAsync("DELETE FROM discount WHERE id = @Id", new { Id = id });

        if (deleteStatus > 0)
        {
            return Response<NoContent>.Success(204);
        }

        return Response<NoContent>.Failure("Discount not found", 404);

    }
}

//docker run --name postgresql-discount -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=Alaska2017 -p 5432:5432 -d postgres
// CREATE TABLE leads (id serial PRIMARY KEY, userid varchar(200) unique not null, rate smallint not null, code varchar(50) not null, createddate timestamp not null default CURRENT_TIMESTAMP)