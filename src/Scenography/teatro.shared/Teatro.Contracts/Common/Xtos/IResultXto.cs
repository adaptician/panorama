namespace Teatro.Contracts.Common.Xtos;

public interface IResultXto<out TResult> : IMessageXto
{
    TResult? Data { get; }
}

public interface IResultXto : IMessageXto
{
}