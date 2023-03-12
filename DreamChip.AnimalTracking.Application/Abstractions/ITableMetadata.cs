namespace DreamChip.AnimalTracking.Application.Abstractions;

public interface ITableMetadata
{
    static abstract string TableName { get; } 

    static abstract string[] Columns { get; }
}
