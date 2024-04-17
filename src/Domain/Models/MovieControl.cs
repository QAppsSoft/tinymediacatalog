using System.ComponentModel.DataAnnotations;
using Domain.Models.BaseObjects;
using Domain.Models.Interfaces;

namespace Domain.Models;

public class MovieControl : GuidEntityBase, ITrackedEntity
{
    [StringLength(250)]
    public string Title { get; set; }  = string.Empty;

    [StringLength(500)]
    public string NfoPath { get; set; }  = string.Empty;

    public Status Status { get; set; } = Status.Created;

    [StringLength(250)]
    public string MessageId { get; set; } = string.Empty;
    
    public DateTime Created { get; set; }
    
    public DateTime Updated { get; set; }
}