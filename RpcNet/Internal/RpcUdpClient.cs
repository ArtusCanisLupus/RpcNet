namespace RpcNet.Internal
{
    using System.Net;
    using System.Net.Sockets;

    // Public for tests
    public class RpcUdpClient : INetworkClient
    {
        private readonly RpcCall call;
        private readonly Socket client;

        public RpcUdpClient(IPAddress ipAddress, int port, int program, int version, ILogger logger)
        {
            if (port == 0)
            {
                port = PortMapperUtilities.GetPort(ProtocolKind.Udp, ipAddress, program, version);
            }

            var remoteIpEndPoint = new IPEndPoint(ipAddress, port);
            this.client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var reader = new UdpReader(this.client);
            var writer = new UdpWriter(this.client);
            this.call = new RpcCall(program, remoteIpEndPoint, reader, writer, null, logger);
        }

        public int TimeoutInMilliseconds
        {
            get => this.client.ReceiveTimeout;
            set => this.client.ReceiveTimeout = value;
        }

        public void Call(int procedure, int version, IXdrWritable argument, IXdrReadable result)
        {
            this.call.SendCall(procedure, version, argument, result);
        }

        public void Dispose()
        {
            this.client.Dispose();
        }
    }
}
