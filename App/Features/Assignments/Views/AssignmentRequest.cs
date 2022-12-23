using System.ComponentModel.DataAnnotations;

namespace App.Features.Assignments.Views;
//informatii esentiale introduse de user
public class AssignmentRequest
{
    [Required] public string Subject { get; set; }
    
    [Required] public string Description { get; set; }
    
    [Required] public DateTime Deadline { get; set; }
}