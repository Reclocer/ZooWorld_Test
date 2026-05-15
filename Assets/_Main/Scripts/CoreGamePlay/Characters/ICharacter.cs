namespace ZooWorld.CoreGamePlay
{
    public interface ICharacter
    {
        string Id { get; }
        ICharacterView View { get; }
    }
}