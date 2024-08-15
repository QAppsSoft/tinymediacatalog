namespace Services.Workers.Abstractions;

public enum WorkerStatus
{
    Iddle,
    Started,
    Cancelling,
    Cancelled,
    Finished,
}