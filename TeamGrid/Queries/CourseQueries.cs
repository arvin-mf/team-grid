public static class CourseQueries
{
    public const string Insert = @"
        INSERT INTO courses (name, year)
        VALUES (@name, @year)
        RETURNING id;
    ";

    public const string FindAll = @"
        SELECT * FROM courses
        ORDER BY year DESC, name;
    ";

    public const string SetName = @"
        UPDATE courses SET name = @name
        WHERE id = @id;
    ";
}