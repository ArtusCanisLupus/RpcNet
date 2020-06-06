﻿namespace RpcNet.Internal
{
    using System.Net;
    using System.Net.Sockets;
    public struct UdpResult
    {
        public SocketError SocketError;
        public int BytesLength;
        public IPEndPoint IpEndPoint;
    }
}