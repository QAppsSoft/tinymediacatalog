﻿namespace Domain.Models.Interfaces;

public interface ITrackedEntity
{
    DateTime Created { get; set; }
    DateTime Updated { get; set; }
}