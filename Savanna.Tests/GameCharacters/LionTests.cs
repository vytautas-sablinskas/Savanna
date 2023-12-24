using Savanna.Data.Animals;

namespace GameCharacterTests
{
    public class LionTests
    {
        private readonly Antelope _antelope;
        private readonly Lion _lion;

        public LionTests()
        {
            _antelope = new Antelope();
            _lion = new Lion();
        }

        [Fact]
        public void Attack_PreyHealthReduces()
        {
            _lion.Attack(_antelope);

            Assert.True(_antelope.Health < 100);
        }
    }
}