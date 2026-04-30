public interface IClassService
{
    Task<int> Create(int courseId, CreateClassRequest req);
    Task<List<ClassDto>> GetByCourseId(int courseId);
}

public class ClassService : IClassService
{
    private readonly IClassRepository _repo;

    public ClassService(IClassRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> Create(int courseId, CreateClassRequest req)
    {
        var cls = req.ToEntity();

        cls.CourseId = courseId;

        return await _repo.Insert(cls); 
    }

    public async Task<List<ClassDto>> GetByCourseId(int courseId)
    {
        var classes = await _repo.FindByCourseId(courseId);

        return classes.Select(c => c.ToDto()).ToList();
    }
}