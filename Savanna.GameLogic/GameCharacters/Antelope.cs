using Savanna.Data.Abilities;
using Savanna.Data.Behaviors;
using Savanna.Data.Constants;
using Savanna.GameLogic.Abilities;

namespace Savanna.Data.Animals
{
    public class Antelope : Animal, IPrey
    {
        private readonly Behavior _behavior = new AntelopeBehavior();

        private readonly IAbility _ability = new Invisibility();

        private float _health = CharacterHealth.ANTELOPE_INITIAL_HEALTH;

        private int _visionRange = 4;

        public override float Health
        {
            get
            {
                return _health;
            }
            protected set
            {
                _health = value;
                if (_health < 0)
                {
                    _health = 0;
                }
            }
        }

        public override char ConsoleRepresentation => 'A';

        public override int VisionLength
        {
            get { return _visionRange; }
            set { _visionRange = value; }
        }

        public override IAbility Ability => _ability;

        public override Behavior Behavior => _behavior;

        public float HealthRegainedOnKilling => 20;

        public void OnAttack(IPredator attacker)
        {
            DecreaseHealth(attacker.Damage);

            var attackerKilledAntelope = Health > -attacker.Damage;
            if (Health <= 0 && attackerKilledAntelope)
            {
                attacker.IncreaseHealth(HealthRegainedOnKilling);
            }
        }
    }
}