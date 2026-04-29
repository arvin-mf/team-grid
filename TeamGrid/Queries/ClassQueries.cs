public static class ClassQueries
{
    public const string Insert = @"
        INSERT INTO classes (name, course_id)
        VALUES (@name, @course_id)
        RETURNING id;
    ";
}