using Dapper;

public interface ICourseRepository
{
    Task<int> Insert(Course c);
}

public class CourseRepository : ICourseRepository
{
    private readonly IDbConnectionFactory _db;

    public CourseRepository(IDbConnectionFactory factory)
    {
        _db = factory;
    }

    public async Task<int> Insert(Course c)
    {
        var connection = _db.Create();

        return await connection.ExecuteScalarAsync<int>(
            CourseQueries.Insert,
            new {
                name = c.Name,
                year = c.Year
            }
        );
    }
}