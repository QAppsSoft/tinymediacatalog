using System.ComponentModel;

namespace Services.Workers.Abstractions;

public interface IWorker : INotifyPropertyChanged
{
    int TotalWork { get; }
    int RemainingWork { get; }
    int PercentCompleted { get; }
    WorkerStatus Status { get; }
    string CurrentStep { get; }
    string CurrentStepDetails { get; }
    Task StartWorkerAsync();
    Task CancelWorkerAsync();
}