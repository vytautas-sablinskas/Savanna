using Savanna.Data.Abilities;
using Savanna.Data.Animals;
using Savanna.Plugins.Abilities;
using Savanna.Plugins.Behaviors;

namespace Savanna.Plugins.GameCharacters
{
    public class Zebra : Animal, IPrey
    {
        private readonly Behavior _behavior = new ZebraBehavior();

        private readonly IAbility _ability = new VisionIncrease();

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
                if (_health < 0)
                {
                    _health = 0;
                }
            }
        }

        public override IAbility Ability => _ability;

        public override char ConsoleRepresentation => 'Z';

        public float HealthRegainedOnKilling => 50;

        public override int VisionLength
        {
            get { return _visionRange; }
            set { _visionRange = value; }
        }

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