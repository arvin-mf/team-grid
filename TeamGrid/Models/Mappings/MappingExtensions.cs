public static class MappingExtensions
{
    public static Course ToEntity(this CreateCourseRequest req) => new()
    {
        Name = req.Name,
        Year = req.Year
    };
}