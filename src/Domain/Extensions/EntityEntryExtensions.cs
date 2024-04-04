using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain.Extensions;

public static class EntityEntryExtensions
{
    public static bool IsModified(this EntityEntry entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        return entry.State == EntityState.Modified ||
               entry.References.Any(
                   r => r.TargetEntry?.Metadata.IsOwned() == true &&
                        (IsModified(r.TargetEntry) ||
                         r.TargetEntry.State == EntityState.Deleted ||
                         r.TargetEntry.State == EntityState.Added));
    }
}