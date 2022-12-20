using App.Features.Assignments.Models;
using App.Features.Assignments.Views;
using Microsoft.AspNetCore.Mvc;

namespace App.Features.Assignments;

[ApiController]
[Route("assignments")] //zona de enpoint/ adresa controlerului/un fel de url
public class AssignmentsController
{
    private static List<AssignmentModel> _mockDb = new List<AssignmentModel>();

    public AssignmentsController()
    {
        //_mockDb = new List<AssignmentModel>();
    }
    
    //endpoint
    [HttpPost] //adauga informatii in baza de date
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

    [HttpGet("{id}")] //("{id}")route nou pt ca exista deja un httpget si se da un parametru routerului
    public AssignmentResponse Get([FromRoute] string id)
    {
        var assignment = _mockDb.FirstOrDefault(x => x.Id == id); //returneaza x care verifica daca e egal cu id din parametru
        if (assignment is null) //daca nu a returnat nimic returneaza null
        {
            return null;
        }
        
        //returneaza un raspuns mapat daca nu e null
        return new AssignmentResponse()
        {
            Id = assignment.Id,
            Subject = assignment.Subject,
            Deadline = assignment.Deadline,
            Description = assignment.Description
        };
    }
    
    //TEMA
    //creati o functie de delete si una de update [httpdelete] si [httppatch]
    //functia de delete e asemanatoare cu cea de dinainte
    //cea de update primeste un string si un request nou pt noul update

    [HttpDelete]
    public void Delete([FromRoute] string id)
    {
        var assignment = _mockDb.FirstOrDefault(x => x.Id == id);

        if (assignment != null)
        {
            _mockDb.Remove(assignment);
        }
        
    }
}