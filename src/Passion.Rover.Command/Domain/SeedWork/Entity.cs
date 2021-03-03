using MongoDB.Bson;

namespace Passion.Rover.Command.Domain.SeedWork
{
    public class Entity
    {
        public ObjectId Id { get; protected set; }

        protected Entity()
        {
            this.Id = ObjectId.GenerateNewId();
        }
    }
}