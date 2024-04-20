namespace Services.Abstractions.Settings;

public interface ISetting<T> where T : notnull
{
    IObservable<T> Value { get; }

    void Write(T item);
}