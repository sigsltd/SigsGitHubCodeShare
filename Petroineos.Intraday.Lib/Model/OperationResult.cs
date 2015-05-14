namespace Petroineos.Intraday.Lib.Model
{
    public class OperationResult<TResult>
    {
        public OperationResult(TResult result, bool success)
        {
            Result = result;
            Success = success;
        }

        public TResult Result { get; private set; }
        public bool Success { get; private set; }
    }
}