namespace Teatro.Contracts.Common.Xtos;

public interface IResultXto<TResult> : IMessageXto
{
    TResult Data { get; set; }
}