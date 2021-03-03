using System;
using MongoDB.Bson;
using Passion.Rover.Command.Domain.SeedWork;

namespace Passion.Rover.Command.Domain.Aggregates
{
    public class Rover : Entity, IAggregateRoot
    {
        protected Rover()
        {
        }

        public Rover(ObjectId id, string name, DateTime createdDate, CameraEngine cameraEngine,
            MovementEngine movementEngine, SampleCollectorEngine sampleCollectorEngine, SynthesisEngine synthesisEngine,
            CommunicationWithWorldEngine communicationWithWorldEngine)
        {
            this.Id = id;
            Name = name;
            CreatedDate = createdDate;
            CameraEngine = cameraEngine;
            MovementEngine = movementEngine;
            SampleCollectorEngine = sampleCollectorEngine;
            SynthesisEngine = synthesisEngine;
            CommunicationWithWorldEngine = communicationWithWorldEngine;
        }

        public string Name { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        
        public DateTime ArrivedDate { get; protected set; } = DateTime.Now.ToUniversalTime();
        public CameraEngine CameraEngine { get; protected set; }
        public MovementEngine MovementEngine { get; protected set; }
        public SampleCollectorEngine SampleCollectorEngine { get; protected set; }
        public SynthesisEngine SynthesisEngine { get; protected set; }
        public CommunicationWithWorldEngine CommunicationWithWorldEngine { get; protected set; }
        

        // public void Synthesis()
        // {
        //     
        // }
        //
        // public void SendTokenPhotos()
        // {
        //     
        // }
        //
        // public void SendRecentLocation()
        // {
        //     
        // }
        //
        // public void SendCollectedSamples()
        // {
        //     
        // }
        //
        // public void SendSynthesisResults()
        // {
        //     
        // }
    }
}