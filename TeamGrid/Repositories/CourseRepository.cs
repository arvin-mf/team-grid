using Dapper;

public interface ICourseRepository
{
    Task<int> Insert(Course c);
    Task<List<Course>> FindAll();
    Task<int> SetName(Course c);
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
        using var connection = _db.Create();

        return await connection.ExecuteScalarAsync<int>(
            CourseQueries.Insert,
            new {
                name = c.Name,
                year = c.Year
            }
        );
    }

    public async Task<List<Course>> FindAll()
    {
        using var connection = _db.Create();

        var rows = await connection.QueryAsync<Course>(
            CourseQueries.FindAll
        );

        return rows.ToList();
    }

    public async Task<int> SetName(Course c)
    {
        using var connection = _db.Create();

        return await connection.ExecuteAsync(
            CourseQueries.SetName,
            new { id = c.Id, name = c.Name }
        );
    }
}