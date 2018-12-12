using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.paxialabs.mabe.serviplus.data
{
    internal abstract class BaseFactory<C, E, D> where C : BaseFactory<C, E, D>, new()
    {
        private static C NewObject;

        private static object locker = new object();

        private static void Init()
        {
            lock (locker)
            {
                if (NewObject == null)
                    NewObject = new C();
            }
        }

        public static E Get(D entidad)
        {
            try
            {
                E entity;

                if (entidad == null)
                    return default(E);

                entity = GetNotClose(entidad);

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
        }

        public static List<E> GetList(List<D> entidad)
        {
            List<E> list = new List<E>();

            foreach (var item in entidad)
            {
                list.Add(GetNotClose(item));
            }

            return list;
        }

        protected static E GetNotClose(D entidad)
        {
            Init();

            E entity = NewObject._GetEntity(entidad);

            return entity;
        }

        public abstract E _GetEntity(D entidad);
    }
}
