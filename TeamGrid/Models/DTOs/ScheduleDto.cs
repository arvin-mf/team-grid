public class ScheduleDto
{
    public required Course Course { get; set; }

    public List<Session> Sessions { get; set; } = new();
}

public class Session
{
    public int SessionNumber { get; set; }

    public List<TeamInSession> Teams { get; set; } = new();
}

public class TeamInSession
{
    public int TeamId { get; set; }

    public string TeamName { get; set; } = string.Empty;
}