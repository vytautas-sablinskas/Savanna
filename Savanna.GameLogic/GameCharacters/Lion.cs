using Savanna.Data.Abilities;
using Savanna.Data.Behaviors;
using Savanna.Data.Constants;

namespace Savanna.Data.Animals
{
    public class Lion : Animal, IPredator
    {
        private readonly LionBehavior _behavior = new LionBehavior();

        private readonly IAbility _ability = new SpeedBoost();

        private float _health = CharacterHealth.LION_INITIAL_HEALTH;

        private int _visionRange = 5;

        public float Damage => 15;

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

        public override char ConsoleRepresentation => 'L';

        public override int VisionLength
        {
            get { return _visionRange; }
            set { _visionRange = value; }
        }

        public override IAbility Ability => _ability;

        public override Behavior Behavior => _behavior;

        public void Attack(IPrey attackable)
        {
            attackable.OnAttack(this);
        }

        public void IncreaseHealth(float amount)
        {
            Health = Health + amount > CharacterHealth.LION_INITIAL_HEALTH ? CharacterHealth.LION_INITIAL_HEALTH : Health + amount;
        }
    }
}