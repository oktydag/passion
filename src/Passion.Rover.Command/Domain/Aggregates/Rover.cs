using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Passion.Rover.Command.Domain.SeedWork;

namespace Passion.Rover.Command.Domain.Aggregates
{
    [BsonIgnoreExtraElements]
    public class Rover : Entity, IAggregateRoot
    {
        protected Rover()
        {
        }

        public Rover(ObjectId id, string name, DateTime createdDate, CameraEngine cameraEngine,
            MovementEngine movementEngine, SampleCollectorEngine sampleCollectorEngine)
        {
            this.Id = id;
            Name = name;
            CreatedDate = createdDate;
            CameraEngine = cameraEngine;
            MovementEngine = movementEngine;
            SampleCollectorEngine = sampleCollectorEngine;
        }

        public string Name { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        
        public DateTime ArrivedDate { get; protected set; } = DateTime.Now.ToUniversalTime();
        public CameraEngine CameraEngine { get; protected set; }
        public MovementEngine MovementEngine { get; protected set; }
        public SampleCollectorEngine SampleCollectorEngine { get; protected set; }
    }
}