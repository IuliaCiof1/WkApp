using System.Reflection.PortableExecutable;
using App.Features.Tests.Models;
using App.Features.Tests.Views;
using Microsoft.AspNetCore.Mvc;

namespace App.Features.Tests;

[ApiController]
[Route("tests")]
public class TestsController
{
     private static List<TestModel> _mockDB = new List<TestModel>();
    
    public TestsController(){}

    [HttpPost]
    public TestResponse Add(TestRequest request)
    {
        var test = new TestModel
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Subject = request.Subject,
            TestDate = request.TestDate
        };
        
        _mockDB.Add(test);

        return new TestResponse()
        {
            Id = test.Id,
            Subject = test.Subject,
            TestDate = test.TestDate
        };
    }

    [HttpGet]
    public IEnumerable<TestResponse> Get()
    {
        return _mockDB.Select(test => new TestResponse()
        {
            Id = test.Id,
            Subject = test.Subject,
            TestDate = test.TestDate
        }).ToList();
    }

    [HttpGet("{id}")]
    public TestResponse Get([FromRoute] string id)
    {
        var test = _mockDB.FirstOrDefault(x => x.Id == id);

        if (test == null)
            return null;

        return new TestResponse
        {
            Id = test.Id,
            Subject = test.Subject,
            TestDate = test.TestDate
        };
    }

    [HttpDelete]
    public TestResponse Delete(string id)
    {
        var test = _mockDB.FirstOrDefault(x => x.Id == id);

        if (test == null)
            return null;

        _mockDB.Remove(test);
        
        return new TestResponse
        {
            Id = test.Id,
            Subject = test.Subject,
            TestDate = test.TestDate
        };
    }

    [HttpPatch]
    public TestResponse Update(string id, TestRequest request)
    {
        var test = _mockDB.FirstOrDefault(x => x.Id == id);
        
        if (test == null)
            return null;

        test.Updated = DateTime.UtcNow;
        test.Subject = request.Subject;
        test.TestDate = request.TestDate;

        return new TestResponse
        {
            Id = test.Id,
            Subject = test.Subject,
            TestDate = test.TestDate
        };
    }
}