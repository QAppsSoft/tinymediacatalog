﻿using Domain.Models.BaseObjects;

namespace Domain.Models;

public abstract class Person : GuidEntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Thumb { get; set; } = string.Empty;
    public string Profile { get; set; } = string.Empty;
    public ICollection<UniqueId> UniqueIds { get; set; } = new List<UniqueId>();
}