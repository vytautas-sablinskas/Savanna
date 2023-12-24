using Savanna.Data.Abilities;
using Savanna.Data.Animals;
using Savanna.Plugins.Behaviors;

namespace Savanna.Plugins.GameCharacters
{
    public class Cheetah : Animal, IPredator
    {
        private readonly Behavior _behavior = new CheetahBehavior();

        private readonly IAbility _ability = new SpeedBoost();

        private float _health = 200;

        private int _visionRange = 5;

        public override Behavior Behavior => _behavior;

        public override float Health
        {
            get
            {
                return _health;
            }
            protected set
            {
                _health = value;
                if (_health < 0) _health = 0;
            }
        }

        public override IAbility Ability => _ability;

        public override char ConsoleRepresentation => 'C';

        public override int VisionLength
        {
            get { return _visionRange; }
            set { _visionRange = value; }
        }

        public float Damage => 20;

        public void Attack(IPrey attackable)
        {
            attackable.OnAttack(this);
        }

        public void IncreaseHealth(float amount)
        {
            Health = Health + amount > 200 ? 200 : Health + amount;
        }
    }
}