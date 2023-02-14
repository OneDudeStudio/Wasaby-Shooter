namespace Enemies
{
    public interface IBattleSequence
    {
        void InitializeSequence();
        void UpdateSequenceScenario();
        void FinishSequence();
    }
}