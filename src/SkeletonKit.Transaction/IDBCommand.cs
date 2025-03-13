namespace SkeletonKit.Transaction
{
    public interface IDBCommand<in TRequest, TResponse>
        where TRequest : ICommandRequest<TResponse>
    {
        Task<TResponse> Execute(TRequest request);
    }
}
