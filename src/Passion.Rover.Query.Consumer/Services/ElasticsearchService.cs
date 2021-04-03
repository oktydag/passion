using System;
using System.Threading.Tasks;
using Passion.Rover.Query.Consumer.Services.Contracts;

namespace Passion.Rover.Query.Consumer.Services
{
    public class ElasticsearchService : IElasticsearchService
    {
        public async Task Write<TEvent>(TEvent @event)
        {
            Console.WriteLine("Write to ELK");
        }
    }
}