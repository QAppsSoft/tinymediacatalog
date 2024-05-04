namespace Services.Abstractions.Domains;

public record RatingDto(string Name,bool Default, int Max, double Value, int Votes);