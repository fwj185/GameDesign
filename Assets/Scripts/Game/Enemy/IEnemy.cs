namespace PrjectSurvivor
{
    public interface IEnemy
    {
        public void Hurt(float hp, bool force = false, bool critical = false);
        public void SetSpeedScale(float scale);
        public void SetHPScale(float scale);
    }
}
