using App.Base;

namespace App.Features.Subjects.Models;

public class SubjectModel:Model
{
    public string Name { get; set; }
    
    public string ProfessorMail { get; set; }

    public List<Double> Grades { get; set; }

    // public SubjectModel()
    // {
    //     Grades = new List<double>();
    // }
}