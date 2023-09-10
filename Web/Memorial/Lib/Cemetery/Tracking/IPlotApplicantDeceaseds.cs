namespace Memorial.Lib.Cemetery
{
    public interface IPlotApplicantDeceaseds
    {
        bool RollbackPlotApplicantDeceaseds(string cemeteryTransactionAF, int plotId);
    }
}