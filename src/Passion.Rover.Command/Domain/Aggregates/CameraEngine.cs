using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Passion.Rover.Command.Domain.SeedWork;


namespace Passion.Rover.Command.Domain.Aggregates
{
    [BsonIgnoreExtraElements]
    public class CameraEngine : Entity
    {
        public string Name { get; protected set; }
        public List<Photo> Photos { get; protected set; }


        public CameraEngine()
        {
        }

        protected CameraEngine(string name, List<Photo> photos)
        {
            Name = name;
            Photos = photos;
        }

        protected IEnumerable<object> GetAtomicValues()
        {
            yield return this.Name;
            yield return this.Photos;
        }

        public Photo TakeNewPhoto(string objectName)
        {
            return new Photo()
            {
                Name = $"CameraEngine-Mars-RandomSurface-{GeneratePhotoName()}-{objectName}",
                Size = 30.4,
                ImageAsFormatted = GenerateFormattedImage()
            };
        }

        private string GenerateFormattedImage()
        {
            return Faker.TextFaker.Sentence();
        }

        private string GeneratePhotoName()
        {
            return Faker.NameFaker.Name();
        }
        
    }

    public class Photo : Entity
    {
        public string Name { get; set; }
        public double Size { get; set; }
        public string ImageAsFormatted { get; set; }
    }
}