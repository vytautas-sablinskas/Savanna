using Savanna.Data.Animals;

namespace GameCharacterTests
{
    public class AntelopeTests
    {
        private readonly Antelope _antelope;
        private readonly Lion _lion;

        public AntelopeTests()
        {
            _antelope = new Antelope();
            _lion = new Lion();
        }

        [Fact]
        public void Antelope_HealthZero_IsDead()
        {
            while (_antelope.Health > 0)
            {
                _lion.Attack(_antelope);
            }

            Assert.True(_antelope.IsDead());
        }
    }
}