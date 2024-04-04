namespace Models.Common;

public class RatingModel
{
    public double Value { get; set; }

    public int Votes { get; set; }

    public bool Default { get; set; }

    public int Max { get; set; }

    public string Name { get; set; }
}