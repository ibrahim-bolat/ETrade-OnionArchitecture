namespace ETrade.Application.Wrappers.Abstract;

public interface IDataResult<out T> : IResult
    {
        public T Data { get; }
    }
