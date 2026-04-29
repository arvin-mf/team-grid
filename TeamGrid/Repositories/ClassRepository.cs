using Dapper;

public interface IClassRepository
{
    Task<int> Insert(Class c);
}

public class ClassRepository : IClassRepository
{
    private readonly IDbConnectionFactory _db;

    public ClassRepository(IDbConnectionFactory factory)
    {
        _db = factory;
    }

    public async Task<int> Insert(Class c)
    {
        using var connection = _db.Create();

        return await connection.ExecuteScalarAsync<int>(
            ClassQueries.Insert,
            new {
                name = c.Name,
                course_id = c.CourseId
            }
        );
    }
}