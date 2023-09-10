namespace Memorial.Lib.Cemetery
{
    public interface ITransfer : ITransaction
    {
        bool AllowPlotDeceasePairing(Core.Domain.Plot plot, int applicantId);
        bool Add(Core.Domain.CemeteryTransaction cemeteryTransaction);
        bool Change(string AF, Core.Domain.CemeteryTransaction cemeteryTransaction);
        bool Remove(string AF);
    }
}