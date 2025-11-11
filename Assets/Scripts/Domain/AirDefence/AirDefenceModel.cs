namespace Domain.AirDefence
{
    public class AirDefenceModel : IAirDefenceModel
    {
        private float _missileSpeed;
        private float _reloadSpeed;

        public float MissileSpeed => _missileSpeed;
        public float ReloadSpeed => _reloadSpeed;

        public AirDefenceModel(float missileSpeed, float reloadSpeed)
        {
            _missileSpeed = missileSpeed;
            _reloadSpeed = reloadSpeed;
        } 
    }
}