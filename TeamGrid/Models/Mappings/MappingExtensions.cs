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

    public static Class ToEntity(this CreateClassRequest req) => new()
    {
        Name = req.Name
    };

    public static ClassDto ToDto(this Class c) => new()
    {
        Id = c.Id,
        Name = c.Name,
        CourseId = c.CourseId
    };

    public static Team ToEntity(this CreateTeamRequest req) => new()
    {
        Number = req.Number
    };

    public static TeamDto ToDto(this Team t) => new()
    {
        Id = t.Id,
        Number = t.Number,
        ClassId = t.ClassId
    };
}