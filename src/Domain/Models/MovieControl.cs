using System.ComponentModel.DataAnnotations;
using Domain.Models.BaseObjects;
using Domain.Models.Interfaces;

namespace Domain.Models;

public class MovieControl : GuidEntityBase, ITrackedEntity
{
    [StringLength(250)]
    public string Title { get; set; } = null!;

    [StringLength(500)]
    public string NfoPath { get; set; } = null!;

    public Status Status { get; set; } = Status.Created;
    
    [StringLength(250)]
    public string MessageId { get; set; } = null!;
    
    public DateTime Created { get; set; }
    
    public DateTime Updated { get; set; }
}