namespace Memorial.Lib.Space
{
    public interface IElectric : ITransaction
    {
        bool Add(Core.Domain.SpaceTransaction spaceTransaction);
        bool Change(string AF, Core.Domain.SpaceTransaction spaceTransaction);
        bool Remove(string AF);
    }
}