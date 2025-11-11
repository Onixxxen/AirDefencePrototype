namespace Domain.Drone
{
    public class DroneModel : IDroneModel
    {
        public float Speed { get; }
        public int Damage { get; }
        public int Award { get; }
        public int MaxHeight { get; }
        public int MinHeight { get; }
        public float MaxTurnAngle { get; }
        public float MaxBankAngle { get; }
        public float BankSpeed { get; }
        public float TurnSpeed { get; }
        public float HeightLerpSpeed { get; }

        public DroneModel(float speed, int damage, int award, int maxHeight, int minHeight, float maxTurnAngle,
            float maxBankAngle, float bankSpeed, float turnSpeed, float heightLerpSpeed)
        {
            Speed = speed;
            Damage = damage;
            Award = award;
            MaxHeight = maxHeight;
            MinHeight = minHeight;
            MaxTurnAngle = maxTurnAngle;
            MaxBankAngle = maxBankAngle;
            BankSpeed = bankSpeed;
            TurnSpeed = turnSpeed;
            HeightLerpSpeed = heightLerpSpeed;
        }
    }
}