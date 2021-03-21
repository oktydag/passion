using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Passion.Rover.Process.Consumer.Entities
{
    [BsonIgnoreExtraElements]
    public class Rover
    {
        public ObjectId Id { get; protected set; }
        public string Name { get; protected set; }
        public DateTime CreatedDate { get; protected set; }

        public DateTime ArrivedDate { get; protected set; }
        public CameraEngine CameraEngine { get; protected set; }
        public MovementEngine MovementEngine { get; protected set; }
        public SampleCollectorEngine SampleCollectorEngine { get; protected set; }
    }

    public class SampleCollectorEngine
    {
        public List<Sample> Samples { get; protected set; }
    }

    public class Sample
    {
        public ObjectId Id { get; set; }
        public string ObjectName { get; set; }
        public double ObjectAmountAsGram { get; set; }
    }

    public class MovementEngine
    {
        public ObjectId Id { get; protected set; }
        public Location Location { get; protected set; }
        public DateTime UpdatedDate { get; protected set; }
    }

    public class Location
    {
        public ObjectId Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Direction { get; set; }
    }

    public class CameraEngine
    {
        public ObjectId Id { get; protected set; }
        public string Name { get; protected set; }
        public List<Photo> Photos { get; protected set; }
    }

    public class Photo
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public double Size { get; set; }
        public string ImageAsFormatted { get; set; }
    }
}