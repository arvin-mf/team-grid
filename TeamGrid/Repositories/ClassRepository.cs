using Dapper;

public interface IClassRepository
{
    Task<int> Insert(Class c);
    Task<List<Class>> FindByCourseId(int courseId);
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

    public async Task<List<Class>> FindByCourseId(int courseId)
    {
        using var connection = _db.Create();

        var rows = await connection.QueryAsync<Class>(
            ClassQueries.FindByCourseId,
            new {
                course_id = courseId
            }
        );

        return rows.ToList();
    }
}