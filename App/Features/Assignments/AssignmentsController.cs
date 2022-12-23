using App.Features.Assignments.Models;
using App.Features.Assignments.Views;
using Microsoft.AspNetCore.Mvc;

namespace App.Features.Assignments;

[ApiController]
[Route("assignments")] //zona de enpoint/ adresa controlerului/un fel de url
public class AssignmentsController
{
    private static List<AssignmentModel> _mockDb = new List<AssignmentModel>();

    public AssignmentsController() { }
    
    //endpoint
    //Add information to database
    [HttpPost]
    public AssignmentResponse Add(AssignmentRequest request)
    {
        var assignment = new AssignmentModel //mapping un request intr-un model
        {
            Id = Guid.NewGuid().ToString(), //unique identifier
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Subject = request.Subject,
            Description = request.Description,
            Deadline = request.Deadline
        };
        
        _mockDb.Add(assignment);

        return new AssignmentResponse() //mapping
        {
            Id = assignment.Id,
            Subject = assignment.Subject,
            Deadline = assignment.Deadline,
            Description = assignment.Description
        };
    }
    //in terminalul din rider: dotnet watch run

    //Get list of assignments
    [HttpGet]
    public IEnumerable<AssignmentResponse> Get()
    {
        return _mockDb.Select(
            assignment => new AssignmentResponse
            {
                Id = assignment.Id,
                Subject = assignment.Subject,
                Description = assignment.Description,
                Deadline = assignment.Deadline
            }
        ).ToList();
    }

    //Get assignment by id
    [HttpGet("{id}")] //("{id}")route nou pt ca exista deja un httpget si se da un parametru routerului
    public AssignmentResponse Get([FromRoute] string id)
    {
        var assignment = _mockDb.FirstOrDefault(x => x.Id == id);
        if (assignment is null)
        {
            return null;
        }
        
        //Return mapped response
        return new AssignmentResponse
        {
            Id = assignment.Id,
            Subject = assignment.Subject,
            Deadline = assignment.Deadline,
            Description = assignment.Description
        };
    }

    //Delete assignment
    [HttpDelete("{id}")]
    public AssignmentResponse Delete([FromRoute] string id)
    {
        var assignment = _mockDb.FirstOrDefault(x => x.Id == id);

        if (assignment == null)
        {
            return null;
        }
        
        _mockDb.Remove(assignment);

        return new AssignmentResponse
        {
            Id = assignment.Id,
            Subject = assignment.Subject,
            Description = assignment.Description,
            Deadline = assignment.Deadline
        };
    }

    //Update assignment
    [HttpPatch("{id}")]
    public AssignmentResponse Update(string id, AssignmentRequest request)
    {
        var assignment = _mockDb.FirstOrDefault(x => x.Id == id);

        if (assignment == null)
        {
            return null;
        }

        assignment.Subject = request.Subject;
        assignment.Description = request.Description;
        assignment.Deadline = request.Deadline;
        assignment.Updated = DateTime.UtcNow;
        
        return new AssignmentResponse
        {
            Id = assignment.Id,
            Subject = assignment.Subject,
            Description = assignment.Description,
            Deadline = assignment.Deadline
        };
    }
}