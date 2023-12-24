using Savanna.Data.Abilities;
using Savanna.Data.Board;

namespace Savanna.Data.Animals
{
    public abstract class Animal : IPrintable
    {
        public Position Position { get; set; }
        public abstract Behavior Behavior { get; }

        public abstract float Health { get; protected set; }

        public abstract IAbility Ability { get; }

        public abstract char ConsoleRepresentation { get; }

        public abstract int VisionLength { get; set; }

        public bool IsDead() => Health <= 0;

        public void DecreaseHealthAfterMove() => DecreaseHealth(0.5f);

        protected void DecreaseHealth(float amount)
        {
            Health = Health - amount >= 0 ? Health - amount : 0;
        }
    }
}