using App.Base;

namespace App.Features.Tests.Models;

public class TestModel:Model
{
    public string Subject { get; set; }
    
    public DateTime TestDate { get; set; }
}