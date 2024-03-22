namespace ADHD_Web.Server.Utils;

public abstract class Error;

public abstract class Result<TD, TE> where TE : Error
{
    public class Success : Result<TD, TE>
    {
        public TD Data { get; }

        public Success(TD data)
        {
            Data = data;
        }
    }

    public class Failure : Result<TD, TE>
    {
        public TE Error { get; }

        public Failure(TE error)
        {
            Error = error;
        }
    }
}