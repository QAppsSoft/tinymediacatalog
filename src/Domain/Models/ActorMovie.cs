﻿using Domain.Models.Movie;

namespace Domain.Models;

public sealed class ActorMovie
{
    public string Role { get; set; } = string.Empty;
    public Person Person { get; set; } = null!;
    public MovieContainer Movie { get; set; } = null!;
}