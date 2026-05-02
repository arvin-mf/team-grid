using Dapper;

public interface ITeamRepository
{
    Task<int> Insert(Team t);
    Task<List<Team>> FindByClassId(int classId);
    Task<List<Team>> FindByCourseId(int courseId);
}

public class TeamRepository : ITeamRepository
{
    private readonly IDbConnectionFactory _db;

    public TeamRepository(IDbConnectionFactory factory)
    {
        _db = factory;
    }

    public async Task<int> Insert(Team t)
    {
        using var connection = _db.Create();

        return await connection.ExecuteScalarAsync<int>(
            TeamQueries.Insert,
            new {
                number = t.Number,
                class_id = t.ClassId
            }
        );
    }

    public async Task<List<Team>> FindByClassId(int classId)
    {
        using var connection = _db.Create();

        var rows = await connection.QueryAsync<Team>(
            TeamQueries.FindByClassId,
            new {
                class_id = classId
            }
        );

        return rows.ToList();
    }

    public async Task<List<Team>> FindByCourseId(int courseId)
    {
        using var connection = _db.Create();

        var rows = await connection.QueryAsync<Team>(
            TeamQueries.FindByCourseId,
            new {
                course_id = courseId
            }
        );

        return rows.ToList();
    }
}