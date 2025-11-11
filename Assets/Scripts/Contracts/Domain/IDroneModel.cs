public interface IDroneModel
{
    float Speed { get; }
    int Damage { get; }
    int Award { get; }
    int MaxHeight { get; }
    int MinHeight { get; }
    float MaxTurnAngle { get; }
    float MaxBankAngle { get; }
    float BankSpeed { get; }
    float TurnSpeed { get; }
    float HeightLerpSpeed { get; }
}