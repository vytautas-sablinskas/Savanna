namespace Savanna.Data.Animals
{
    public interface IPredator
    {
        float Damage { get; }

        void Attack(IPrey prey);

        void IncreaseHealth(float amount);
    }
}