using System.Reflection.PortableExecutable;
using App.Features.Subjects.Models;
using App.Features.Subjects.Views;
using Microsoft.AspNetCore.Mvc;

namespace App.Features.Subjects;

[ApiController]
[Route("subjects")]
public class SubjectsController
{
    private static List<SubjectModel> _mockDB = new List<SubjectModel>();
    
    public SubjectsController(){}

    [HttpPost]
    public SubjectResponse Add(SubjectRequest request)
    {
        var subject = new SubjectModel
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Name = request.Name,
            ProfessorMail = request.ProfessorMail,
            Grades = request.Grades
        };
        
        _mockDB.Add(subject);

        return new SubjectResponse
        {
            Id = subject.Id,
            Name = subject.Name,
            ProfessorMail = subject.ProfessorMail,
            Grades = subject.Grades
        };
    }

    [HttpGet]
    public IEnumerable<SubjectResponse> Get()
    {
        return _mockDB.Select(subject => new SubjectResponse
        {
            Id = subject.Id,
            Name = subject.Name,
            ProfessorMail = subject.ProfessorMail,
            Grades = subject.Grades
        }).ToList();
    }

    [HttpGet("{id}")]
    public SubjectResponse Get([FromRoute] string id)
    {
        var subject = _mockDB.FirstOrDefault(x => x.Id == id);

        if (subject == null)
            return null;

        return new SubjectResponse
        {
            Id = subject.Id,
            Name = subject.Name,
            ProfessorMail = subject.ProfessorMail,
            Grades = subject.Grades
        };
    }

    [HttpDelete]
    public SubjectResponse Delete(string id)
    {
        var subject = _mockDB.FirstOrDefault(x => x.Id == id);

        if (subject == null)
            return null;

        _mockDB.Remove(subject);
        
        return new SubjectResponse
        {
            Id = subject.Id,
            Name = subject.Name,
            ProfessorMail = subject.ProfessorMail,
            Grades = subject.Grades
        };
    }

    [HttpPatch]
    public SubjectResponse Update(string id, SubjectRequest request)
    {
        var subject = _mockDB.FirstOrDefault(x => x.Id == id);
        
        if (subject == null)
            return null;

        subject.Name = request.Name;
        subject.ProfessorMail = request.ProfessorMail;
        subject.Grades = request.Grades;
        subject.Updated = DateTime.UtcNow;

        return new SubjectResponse
        {
            Id = subject.Id,
            Name = subject.Name,
            ProfessorMail = subject.ProfessorMail,
            Grades = subject.Grades
        };
    }
}