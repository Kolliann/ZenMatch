namespace CoreTiles.Scripts.ZenMatch.Finishers
{
    public interface IGameFinisher<in T>
    {
        void FinishGameActions(T finishData);
    }
}