public interface ITeamService
{
    Task<int> Create(int classId, CreateTeamRequest req);
    Task<List<TeamDto>> GetByClassId(int classId);
}

public class TeamService : ITeamService
{
    private readonly ITeamRepository _repo;

    public TeamService(ITeamRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> Create(int classId, CreateTeamRequest req)
    {
        var team = req.ToEntity();

        team.ClassId = classId;

        return await _repo.Insert(team); 
    }

    public async Task<List<TeamDto>> GetByClassId(int classId)
    {
        var teams = await _repo.FindByClassId(classId);

        return teams.Select(t => t.ToDto()).ToList();
    }
}