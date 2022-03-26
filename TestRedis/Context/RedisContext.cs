using StackExchange.Redis;
using System;

namespace TestRedis.Context
{
    public class RedisContext
    {
        /// <summary>
        /// Lazy<T> https://docs.microsoft.com/en-us/dotnet/api/system.lazy-1?view=netframework-4.8
        /// 
        /// Provides a thread safe method of creating objects if they are required
        /// </summary>

        private static Lazy<ConnectionMultiplexer> lazyConnection;

        /// <summary>
        /// Returns the Redis Connection(s) for communicating with the server
        /// </summary>
        /// <value></value>
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        /// <summary>
        /// Constructor... Creates the singleton connection object when required
        /// </summary>
        static RedisContext()
        {
            RedisContext.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                string connection = Environment.GetEnvironmentVariable("RedisConnection", EnvironmentVariableTarget.Process);
                Console.WriteLine($"Connection String: {connection}");

                return ConnectionMultiplexer.Connect(connection);
            });
        }
    }
}
