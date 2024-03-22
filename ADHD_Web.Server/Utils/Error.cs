namespace ADHD_Web.Server.Utils;

public abstract class Error;

public abstract class Result<TD, TE> where TE : Error
{
    public class Success(TD data) : Result<TD, TE>
    {
        public TD Data { get; } = data;
    }

    public class Failure(TE error) : Result<TD, TE>
    {
        public TE Error { get; } = error;
    }
}