using System;
using System.Collections.Generic;
using EnsureThat;
using MongoDB.Bson.Serialization.Attributes;
using Passion.Rover.Command.Domain.SeedWork;

namespace Passion.Rover.Command.Domain.Aggregates
{
    [BsonIgnoreExtraElements]
    public class SampleCollectorEngine : Entity
    {
        
        public List<Sample> Samples { get; protected set; }
        public SampleCollectorEngine(List<Sample> samples = null)
        {
            Samples = samples;
        }
        
        public Sample CollectSample(string objectName, double objectAmountAsGram)
        {
            return Sample.Create(objectName, objectAmountAsGram);
        }

        protected IEnumerable<object> GetAtomicValues()
        {
            yield return this.Samples;
        }
    }

    public class Sample : Entity
    {
        public string ObjectName { get; protected set; }
        public double ObjectAmountAsGram { get; protected set; }
        
        public Sample(string objectName, double objectAmountAsGram)
        {
            ObjectName = objectName;
            ObjectAmountAsGram = objectAmountAsGram;
        }
        public static Sample Create(string objectName, double objectAmountAsGram)
        {
            Ensure.That(objectName).IsNotEmpty();
            Ensure.That(objectAmountAsGram).IsGt(0);

            return new Sample($"{objectName}.From.Mars", objectAmountAsGram);
        }
    }
}