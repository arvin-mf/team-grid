public interface IClassService
{
    Task<int> Create(int courseId, CreateClassRequest req);
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
}