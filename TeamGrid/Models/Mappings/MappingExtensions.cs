public static class MappingExtensions
{
    public static Course ToEntity(this CreateCourseRequest req) => new()
    {
        Name = req.Name,
        Year = req.Year
    };

    public static Course ToEntity(this SetCourseNameRequest req) => new()
    {
        Name = req.Name
    };

    public static CourseDto ToDto(this Course c) => new()
    {
        Id = c.Id,
        Name = c.Name,
        Year = c.Year
    };
}