using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Passion.Outbox.Publisher.Documents
{
    [BsonIgnoreExtraElements]
    public class OutboxMessage
    {
        public  ObjectId Id { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
        public string Status { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}