namespace Savanna.Data.Animals
{
    public interface IPrey
    {
        float HealthRegainedOnKilling { get; }

        void OnAttack(IPredator attacker);
    }
}