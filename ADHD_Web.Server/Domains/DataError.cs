using ADHD_Web.Server.Utils;

namespace ADHD_Web.Server.Domains;

public class DataError(DataErrorType dataErrorType, string message = null!) : Error
{
    public DataErrorType DataErrorType { get; } = dataErrorType;
    public string Message { get; } = message;
}

public enum DataErrorType
{
    NotFound,
    UnknownError,
    DatabaseError,
    DuplicateData
}