namespace Petroineos.Intraday.Lib
{
    public interface IPowerIntraDayReportFileNameBuilder
    {
        string GetFilename(string prefix);
    }
}