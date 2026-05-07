using Bogus;
using Moq;

public class CourseServiceTests
{
    [Fact]
    public async Task SetName_Success()
    {
        var courseRepo = new Mock<ICourseRepository>();
        var service = new CourseService(courseRepo.Object, null, null);

        var f = new Faker();
        int courseId = f.Random.Number(1, 100);
        var req = new SetCourseNameRequest { Name = f.Random.String() };

        courseRepo.Setup(r => r.SetName(It.IsAny<Course>())).ReturnsAsync(1);

        await service.SetName(courseId, req);

        courseRepo.Verify(r => r.SetName(It.Is<Course>(c => c.Id == courseId)), Times.Once);
    }

    [Fact]
    public async Task SetName_NotFound()
    {
        var courseRepo = new Mock<ICourseRepository>();
        var service = new CourseService(courseRepo.Object, null, null);

        var f = new Faker();
        int courseId = f.Random.Number(1, 100);
        var req = new SetCourseNameRequest { Name = f.Random.String() };

        courseRepo.Setup(r => r.SetName(It.IsAny<Course>())).ReturnsAsync(0);

        var act = () => service.SetName(courseId, req);
        
        await Assert.ThrowsAsync<KeyNotFoundException>(act);

        courseRepo.Verify(r => r.SetName(It.Is<Course>(c => c.Id == courseId)), Times.Once);
    }

    [Fact]
    public void DistributeTeams()
    {
        var service = new CourseService(null, null, null);

        var classMap = new Dictionary<int, string> {
            {11, "A"}, {12, "B"}, {13, "C"}
        };

        // setup faker
        var teamFaker = new Faker<Team>()
            .RuleFor(t => t.Id, f => f.IndexFaker + 1)
            .RuleFor(t => t.Number, f => f.IndexFaker + 1)
            .RuleFor(t => t.ClassId, f => f.PickRandom(classMap.Keys.ToList()));

        var f = new Faker();
        int teamCount = f.Random.Number(15, 35);
        int maxPerSession = f.Random.Number(4, 7);

        // data preparation
        var dummyTeams = teamFaker.Generate(teamCount);

        var teamNametoIdMap = dummyTeams.ToDictionary(
            t => $"{classMap[t.ClassId]}{t.Number}",
            t => t.Id
        );
        var names = teamNametoIdMap.Keys.ToList();

        // execution
        var result = service.DistributeTeams(names, teamNametoIdMap, maxPerSession);

        int expectedSessionCount = (int)Math.Ceiling((double)teamCount / maxPerSession);
        var resultTeams = result.SelectMany(s => s.Teams).ToList();

        // assertion
        Assert.Equal(expectedSessionCount, result.Count);
        Assert.Equal(teamCount, resultTeams.Count);
        Assert.Distinct(resultTeams.Select(t => t.TeamId));
        Assert.All(result, s => {
            Assert.True(s.Teams.Count <= maxPerSession);
        });
        for (int i = 0; i < result.Count; i++)
        {
            Assert.Equal(i + 1, result[i].SessionNumber);
        }
        foreach (var s in result)
        {
            foreach (var t in s.Teams)
            {
                Assert.Equal(teamNametoIdMap[t.TeamName], t.TeamId);
            }
        }
    }

    [Fact]
    public void MapTeamNamesToIds_Success()
    {
        var service = new CourseService(null, null, null);

        // setup faker and data preparation
        var f = new Faker();
        int classCount = f.Random.Number(1, 10);
        int teamCount = f.Random.Number(1, 35);
        int classId = 1;

        var classMap = Enumerable.Range(1, classCount).ToDictionary(
            id => classId++, id => ((char)('A' + id - 1)).ToString()
        );

        var counterPerClass = classMap.Keys.ToDictionary(
            id => id, _ => 1
        );
        var teamFaker = new Faker<Team>()
            .RuleFor(t => t.Id, f => f.IndexFaker + 1)
            .RuleFor(t => t.ClassId, f => f.PickRandom(classMap.Keys.ToList()))
            .RuleFor(t => t.Number, (f, t) => counterPerClass[t.ClassId]++);

        var teams = teamFaker.Generate(teamCount);

        // execution
        var result = service.MapTeamNamesToIds(teams, classMap);

        // assertion
        Assert.NotNull(result);
        Assert.Equal(teamCount, result.Count);
        foreach (var t in teams)
        {
            var key = $"{classMap[t.ClassId]}{t.Number}";
            Assert.Contains(key, result.Keys);
            Assert.Equal(t.Id, result[key]);
        }
        Assert.Equivalent(teams.Select(t => t.Id), result.Values);
    }
}