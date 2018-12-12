using System.Collections.Generic;

namespace net.paxialabs.mabe.serviplus.data
{
    internal interface IRepositoryGET<T>
    {
        T Get(int Id);
        List<T> GetActives();
        List<T> GetAll();
    }
}
