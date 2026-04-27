public static class CourseQueries
{
    public const string Insert = @"
        INSERT INTO courses (name, year)
        VALUES (@name, @year)
        RETURNING id;
    ";
}