﻿namespace RpcNet.Internal
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    // Public for tests
    // This service writer for UDP is asynchronous, because there is
    // no synchronous method to call SendTo without a SocketException.
    // SocketExceptions on server side are ugly!
    // Making SendToAsync synchronous using a reset event would block two threads instead of one,
    // therefore the implementation is fully asynchronous (not async/await).
    public class UdpWriter : INetworkWriter, IDisposable
    {
        private readonly Socket socket;
        private readonly byte[] buffer;
        private readonly SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();
        private int writeIndex;

        public UdpWriter(Socket socket) : this(socket, 65536)
        {
        }

        public UdpWriter(Socket socket, int bufferSize)
        {
            if (bufferSize < sizeof(int) || bufferSize % 4 != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bufferSize));
            }

            this.socket = socket;
            this.buffer = new byte[bufferSize];
            this.socketAsyncEventArgs.Completed += this.OnCompleted;
        }

        public Action<NetworkResult> Completed { get; set; }

        private void OnCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (this.socketAsyncEventArgs.SocketError != SocketError.Success)
            {
                this.Completed?.Invoke(new NetworkResult { SocketError = this.socketAsyncEventArgs.SocketError });
            }

            this.Completed?.Invoke(new NetworkResult
            {
                IpEndPoint = (IPEndPoint)this.socketAsyncEventArgs.RemoteEndPoint
            });
        }

        public void BeginWriting() => this.writeIndex = 0;

        public void EndWritingAsync(IPEndPoint remoteEndPoint)
        {
            this.socketAsyncEventArgs.RemoteEndPoint = remoteEndPoint;
            this.socketAsyncEventArgs.SetBuffer(this.buffer, 0, this.writeIndex);

            bool willRaiseEvent = this.socket.SendToAsync(this.socketAsyncEventArgs);
            if (!willRaiseEvent)
            {
                this.OnCompleted(this, this.socketAsyncEventArgs);
            }
        }

        public NetworkResult EndWritingSync(IPEndPoint remoteEndPoint)
        {
            try
            {
                int sendSize = this.socket.SendTo(this.buffer, this.writeIndex, SocketFlags.None, remoteEndPoint);
                return new NetworkResult
                {
                    SocketError = SocketError.Success,
                    IpEndPoint = remoteEndPoint
                };
            }
            catch (SocketException exception)
            {
                return new NetworkResult
                {
                    SocketError = exception.SocketErrorCode
                };
            }
        }

        public Span<byte> Reserve(int length)
        {
            if (this.writeIndex + length > this.buffer.Length)
            {
                throw new RpcException("Buffer overflow.");
            }

            Span<byte> span = this.buffer.AsSpan(this.writeIndex, length);
            this.writeIndex += length;
            return span;
        }

        public void Dispose() => this.socketAsyncEventArgs.Dispose();
    }
}