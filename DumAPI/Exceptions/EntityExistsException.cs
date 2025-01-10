using System.Runtime.Serialization;

namespace DumAPI.Exceptions
{
    [Serializable()]
    public class EntityExistsException<T>(T entity) : Exception("Username already exists : {0}"), ISerializable
    {
        private readonly T? _entity = entity;

        public override string ToString()
        {
            return string.Format(Message, _entity?.ToString());
        }
    }
}
