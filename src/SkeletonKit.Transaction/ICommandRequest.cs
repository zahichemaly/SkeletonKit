namespace CME.Transaction
{
    /// <summary>
    /// Marker interface to represent a request with a void response
    /// </summary>
    public interface ICommandRequest : IBaseRequest
    {
    }

    /// <summary>
    /// Marker interface to represent a request with a response
    /// </summary>
    /// <typeparam name="TResponse">Response type</typeparam>
    public interface ICommandRequest<out TResponse> : IBaseRequest
    {
    }

    /// <summary>
    /// Allows for generic type constraints of objects implementing ICommandRequest or ICommandRequest{ICommandResponse}
    /// </summary>
    public interface IBaseRequest { }
}
