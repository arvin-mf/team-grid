using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClassesController : ControllerBase
{
    private readonly ITeamService _teamServ;

    public ClassesController(ITeamService teamServ)
    {
        _teamServ = teamServ;
    }

    [HttpPost("{id}/teams")]
    public async Task<IActionResult> CreateTeam(int id, [FromBody] CreateTeamRequest req)
    {
        var teamId = await _teamServ.Create(id, req);

        return Created("", new ApiSuccessResponse<int>(teamId, "Team successfully created"));
    }

    [HttpGet("{id}/teams")]
    public async Task<IActionResult> GetTeamsByClass(int id)
    {
        var teams = await _teamServ.GetByClassId(id);

        return Ok(new ApiSuccessResponse<List<TeamDto>>(teams, "Teams successfully retrieved"));
    }
}