
namespace net.paxialabs.mabe.serviplus.data
{
    internal interface IRepositorySET<T>
    {
        T Insert(T data);
        T Update(T data);
    }
}
