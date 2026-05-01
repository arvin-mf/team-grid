public interface ICourseService
{
    Task<int> Create(CreateCourseRequest req);
    Task<List<CourseDto>> GetAll();
    Task SetName(int id, SetCourseNameRequest req);
    Task<ScheduleDto> GenerateSchedule(int id, int maxPerSession);
}

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepo;
    private readonly IClassRepository _classRepo;
    private readonly ITeamRepository _teamRepo;

    public CourseService(ICourseRepository courseRepo, IClassRepository classRepo, ITeamRepository teamRepo)
    {
        _courseRepo = courseRepo;
        _classRepo = classRepo;
        _teamRepo = teamRepo;
    }

    public async Task<int> Create(CreateCourseRequest req)
    {
        var course = req.ToEntity();

        return await _courseRepo.Insert(course);
    }

    public async Task<List<CourseDto>> GetAll()
    {
        var courses = await _courseRepo.FindAll();

        return courses.Select(c => c.ToDto()).ToList();
    }

    public async Task SetName(int id, SetCourseNameRequest req)
    {
        var course = req.ToEntity();

        course.Id = id;

        var rowsAffected = await _courseRepo.SetName(course);
        if (rowsAffected == 0)
        {
            throw new KeyNotFoundException($"the requested course with ID {id} does not exist");
        } 
    }

    public async Task<ScheduleDto> GenerateSchedule(int id, int maxPerSession)
    {
        var teams = await _teamRepo.FindByCourseId(id);

        var classes = await _classRepo.FindByCourseId(id);
        var classMap = classes.ToDictionary(
            c => c.Id,
            c => c.Name
        );

        var teamNames = teams.Select(t => $"{classMap[t.ClassId]}{t.Number}").ToList();
        var teamNameToIdMap = teams.ToDictionary(
            t => $"{classMap[t.ClassId]}{t.Number}",
            t => t.Id
        );

        var rg = new Random();
        var shuffledTeamNames = teamNames.OrderBy(_ => rg.Next()).ToList();

        var distributed = DistributeTeams(shuffledTeamNames, teamNameToIdMap, maxPerSession);

        var course = await _courseRepo.FindById(id);

        return new ScheduleDto {
            Course = course,
            Sessions = distributed
        };
    }

    public List<Session> DistributeTeams(List<string> teamNames, Dictionary<string, int> ids, int max)
    {
        var sessions = new List<Session>();

        int teamCount = teamNames.Count;
        if (teamCount == 0) return sessions;

        int sessionCount = (int)Math.Ceiling((double)teamCount / max);

        int leastNumber = teamCount / sessionCount;
        int extraNumber = teamCount % sessionCount;

        int currentIdx = 0;
        for (int i = 1; i <= sessionCount; i++)
        {
            var session = new Session { SessionNumber = i };

            int teamInICount = (i <= extraNumber) ? (leastNumber + 1) : leastNumber;

            for (int j = 0; j < teamInICount; j++)
            {
                if (currentIdx < teamCount)
                {
                    session.Teams.Add(
                        new TeamInSession {
                            TeamId = ids[teamNames[currentIdx]],
                            TeamName = teamNames[currentIdx]
                        }
                    );

                    currentIdx++;
                }

                sessions.Add(session);
            }
        }

        return sessions;
    }
}