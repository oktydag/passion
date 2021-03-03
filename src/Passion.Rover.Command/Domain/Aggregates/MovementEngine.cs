using System;
using System.Collections.Generic;
using EnsureThat;
using MongoDB.Bson.Serialization.Attributes;
using Passion.Rover.Command.Domain.SeedWork;

namespace Passion.Rover.Command.Domain.Aggregates
{
    [BsonIgnoreExtraElements]
    public class MovementEngine : Entity
    {
        public Location Location { get; protected set; }
        public DateTime UpdatedDate { get; protected set; }

        public MovementEngine(Location location, DateTime updatedDate)
        {
            this.Location = location;
            this.UpdatedDate = updatedDate;
        } 
        

        public MovementEngine Go(Location newLocation)
        {
            Ensure.That(newLocation).IsNotNull();
            
            this.Location = newLocation;
            this.UpdatedDate = DateTime.Now.ToUniversalTime();
            
            return this;
        }
        
        protected IEnumerable<object> GetAtomicValues()
        {
            yield return this.Location;
            yield return this.UpdatedDate;
        }
    }


    public class Location : Entity
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public Direction Direction { get; protected set; }

        public const int MIN_LOCATION_VALUE = -1;

        public Location(int x, int y, Direction direction)
        {
            Ensure.That(x).IsGt(MIN_LOCATION_VALUE);
            Ensure.That(y).IsGt(MIN_LOCATION_VALUE);

            X = x;
            Y = y;
            Direction = direction;
        }
    }

    public enum Direction
    {
        Undefined = '0',
        North = 'N',
        East = 'E',
        South = 'S',
        West = 'W'
    }
}