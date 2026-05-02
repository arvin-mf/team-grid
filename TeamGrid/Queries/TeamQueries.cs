public static class TeamQueries
{
    public const string Insert = @"
        INSERT INTO teams (team_number, class_id)
        VALUES (@number, @class_id)
        RETURNING id;
    ";

    public const string FindByClassId = @"
        SELECT
            id,
            team_number AS number,
            class_id
        FROM teams
        WHERE class_id = @class_id
        ORDER BY team_number;
    ";

    public const string FindByCourseId = @"
        SELECT
            t.id,
            t.team_number AS number,
            t.class_id
        FROM teams t
        INNER JOIN classes c ON c.id = t.class_id
        WHERE c.course_id = @course_id;
    ";
}