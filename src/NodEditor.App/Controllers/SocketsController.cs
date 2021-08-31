﻿using NodEditor.Core;
using NodEditor.Core.Exceptions;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Controllers
{
    internal abstract class SocketsController<T> : NodeController<T> where T : ISocket
    {
        public bool HasSockets => Sockets != null;
        public ReadOnlyArray<T> Sockets { get; private set; }
        
        protected SocketsController(INode node) : base(node)
        {
        }

        public void Add(T[] sockets)
        {
            if (Sockets != null)
            {
                throw new SocketAlreadyAddedException();
            }

            if (sockets == null)
            {
                throw new SocketCanNotBeNullReferenceException();
            }

            Sockets = new ReadOnlyArray<T>(sockets);
            ConfigureSockets(sockets);
        }
        
        private void ConfigureSockets(T[] sockets)
        {
            for (var i = 0; i < sockets.Length; i++)
            {
                ConfigureSocket(sockets[i]);
            }
        }
    }
}