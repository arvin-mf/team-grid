public static class ClassQueries
{
    public const string Insert = @"
        INSERT INTO classes (name, course_id)
        VALUES (@name, @course_id)
        RETURNING id;
    ";

    public const string FindByCourseId = @"
        SELECT * FROM classes
        WHERE course_id = @course_id
        ORDER BY name;
    ";
}