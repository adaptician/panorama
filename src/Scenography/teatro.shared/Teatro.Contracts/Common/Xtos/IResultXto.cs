namespace Teatro.Contracts.Common.Xtos;

public interface IResultXto<TResult>
{
    TResult Data { get; init; }
}