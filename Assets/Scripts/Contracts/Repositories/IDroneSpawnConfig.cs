public interface IDroneSpawnConfig
{
    int DronesCount { get; }
    float SpawnTimeInterval { get; }
    float MinSpawnRadius { get; }
    float MaxSpawnRadius { get; }
    bool IsReplenished { get; }
}