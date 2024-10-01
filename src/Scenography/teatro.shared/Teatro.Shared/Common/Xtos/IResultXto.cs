namespace Teatro.Shared.Common.Xtos;

public interface IResultXto<TResult>
{
    TResult Data { get; init; }
}