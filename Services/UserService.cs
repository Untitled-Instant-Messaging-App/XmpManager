﻿using XmpManager.Clients;
using XmpManager.Contracts;
using XmpManager.RabbitMQ;

namespace XmpManager.Services
{
    public class UserService
    {
        private readonly EjabberdClient client;

        public UserService(EjabberdClient client, IRabbitMQListener<User> unregistrations, IRabbitMQListener<User> registrations) 
        {
            this.client = client;
            unregistrations.OnReceive += (_, user) => UnregisterUser(user.Username);
            registrations.OnReceive += (_, user) => RegisterUser(user);
        }

        public async Task RegisterUser(User user)
        {
            if (user != null)
            {
                await client.RegisterUser(user.Username, user.EphemeralPassword);
            }
        }

        public async Task UnregisterUser(string username)
        {
            if (username != null)
            {
                await client.UnregisterUser(username);
            }
        }
    }
}
