namespace CodeBuilder.Operations
{
    public interface IExecutable<TResult>
    {
        TResult Execute();
    }
}
