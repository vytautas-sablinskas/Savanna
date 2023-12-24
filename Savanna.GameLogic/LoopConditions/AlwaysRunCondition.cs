namespace Savanna.ConsoleApp.LoopConditions
{
    public class AlwaysRunCondition : IRunCondition
    {
        public bool ShouldContinue() => true;
    }
}