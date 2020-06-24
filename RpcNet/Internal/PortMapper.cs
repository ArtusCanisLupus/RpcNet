//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by RpcNetGen 1.0.0.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RpcNet.Internal
{
    using System;
    using System.Net;
    using RpcNet;

    internal static class PortMapperConstants
    {
        public const int PortMapperPort = 111;
        public const int ProtocolTcp = 6;
        public const int ProtocolUdp = 17;
        public const int PortMapperVersion = 2;
        public const int Ping_2 = 0;
        public const int Set_2 = 1;
        public const int Unset_2 = 2;
        public const int GetPort_2 = 3;
        public const int Dump_2 = 4;
        public const int Call_2 = 5;
        public const int PortMapperProgram = 100000;
    }

    internal partial class CallArguments : IXdrReadable, IXdrWritable
    {
        public uint Program { get; set; }
        public uint Version { get; set; }
        public uint Procedure { get; set; }
        public byte[] Arguments { get; set; }

        public CallArguments()
        {
        }

        public CallArguments(IXdrReader reader)
        {
            ReadFrom(reader);
        }

        public void WriteTo(IXdrWriter writer)
        {
            writer.Write(Program);
            writer.Write(Version);
            writer.Write(Procedure);
            writer.WriteVariableLengthOpaque(Arguments);
        }

        public void ReadFrom(IXdrReader reader)
        {
            Program = reader.ReadUInt();
            Version = reader.ReadUInt();
            Procedure = reader.ReadUInt();
            Arguments = reader.ReadOpaque();
        }
    }

    internal partial class CallResult : IXdrReadable, IXdrWritable
    {
        public uint Port { get; set; }
        public byte[] Result { get; set; }

        public CallResult()
        {
        }

        public CallResult(IXdrReader reader)
        {
            ReadFrom(reader);
        }

        public void WriteTo(IXdrWriter writer)
        {
            writer.Write(Port);
            writer.WriteVariableLengthOpaque(Result);
        }

        public void ReadFrom(IXdrReader reader)
        {
            Port = reader.ReadUInt();
            Result = reader.ReadOpaque();
        }
    }

    internal partial class Mapping : IXdrReadable, IXdrWritable
    {
        public uint Program { get; set; }
        public uint Version { get; set; }
        public uint Protocol { get; set; }
        public uint Port { get; set; }

        public Mapping()
        {
        }

        public Mapping(IXdrReader reader)
        {
            ReadFrom(reader);
        }

        public void WriteTo(IXdrWriter writer)
        {
            writer.Write(Program);
            writer.Write(Version);
            writer.Write(Protocol);
            writer.Write(Port);
        }

        public void ReadFrom(IXdrReader reader)
        {
            Program = reader.ReadUInt();
            Version = reader.ReadUInt();
            Protocol = reader.ReadUInt();
            Port = reader.ReadUInt();
        }
    }

    internal partial class MappingList : IXdrReadable, IXdrWritable
    {
        public Mapping Mapping { get; set; }
        public MappingList Next { get; set; }

        public MappingList()
        {
        }

        public MappingList(IXdrReader reader)
        {
            ReadFrom(reader);
        }

        public void WriteTo(IXdrWriter writer)
        {
            var current = this;
            do
            {
                current.Mapping?.WriteTo(writer);
                current = current.Next;
                writer.Write(current != null);
            } while (current != null);
        }

        public void ReadFrom(IXdrReader reader)
        {
            var current = this;
            MappingList next;
            do
            {
                current.Mapping = new Mapping(reader);
                next = reader.ReadBool() ? new MappingList() : null;
                current.Next = next;
                current = next;
            } while (current != null);
        }
    }

    internal class PortMapperClient : ClientStub
    {
        public PortMapperClient(Protocol protocol, IPAddress ipAddress, int port = 0, ILogger logger = null) :
            base(protocol, ipAddress, port, PortMapperConstants.PortMapperProgram, logger)
        {
        }

        private class Arguments_0 : IXdrWritable
        {
            public void WriteTo(IXdrWriter writer)
            {
            }
        }

        private class Result_0 : IXdrReadable
        {
            public void ReadFrom(IXdrReader reader)
            {
            }
        }

        public void Ping_2()
        {
            var args = new Arguments_0();
            var result = new Result_0();
            Call(PortMapperConstants.Ping_2, PortMapperConstants.PortMapperVersion, args, result);
        }

        private class Result_1 : IXdrReadable
        {
            public bool Value;

            public void ReadFrom(IXdrReader reader)
            {
                Value = reader.ReadBool();
            }
        }

        public bool Set_2(Mapping arg1)
        {
            var result = new Result_1();
            Call(PortMapperConstants.Set_2, PortMapperConstants.PortMapperVersion, arg1, result);
            return result.Value;
        }

        private class Result_2 : IXdrReadable
        {
            public bool Value;

            public void ReadFrom(IXdrReader reader)
            {
                Value = reader.ReadBool();
            }
        }

        public bool Unset_2(Mapping arg1)
        {
            var result = new Result_2();
            Call(PortMapperConstants.Unset_2, PortMapperConstants.PortMapperVersion, arg1, result);
            return result.Value;
        }

        private class Result_3 : IXdrReadable
        {
            public uint Value;

            public void ReadFrom(IXdrReader reader)
            {
                Value = reader.ReadUInt();
            }
        }

        public uint GetPort_2(Mapping arg1)
        {
            var result = new Result_3();
            Call(PortMapperConstants.GetPort_2, PortMapperConstants.PortMapperVersion, arg1, result);
            return result.Value;
        }

        private class Arguments_4 : IXdrWritable
        {
            public void WriteTo(IXdrWriter writer)
            {
            }
        }

        public MappingList Dump_2()
        {
            var args = new Arguments_4();
            var result = new MappingList();
            Call(PortMapperConstants.Dump_2, PortMapperConstants.PortMapperVersion, args, result);
            return result;
        }

        public CallResult Call_2(CallArguments arg1)
        {
            var result = new CallResult();
            Call(PortMapperConstants.Call_2, PortMapperConstants.PortMapperVersion, arg1, result);
            return result;
        }
    }

    internal abstract class PortMapperServerStub : ServerStub
    {
        public PortMapperServerStub(IPAddress ipAddress, int port = 0, ILogger logger = null) :
            base(ipAddress, port, PortMapperConstants.PortMapperProgram, new[] { PortMapperConstants.PortMapperVersion }, logger)
        {
        }

        private class Arguments_0 : IXdrReadable
        {
            public void ReadFrom(IXdrReader reader)
            {
            }
        }

        private class Result_0 : IXdrWritable
        {
            public void WriteTo(IXdrWriter writer)
            {
            }
        }

        private class Result_1 : IXdrWritable
        {
            public bool Value;

            public void WriteTo(IXdrWriter writer)
            {
                writer.Write(Value);
            }
        }

        private class Result_2 : IXdrWritable
        {
            public bool Value;

            public void WriteTo(IXdrWriter writer)
            {
                writer.Write(Value);
            }
        }

        private class Result_3 : IXdrWritable
        {
            public uint Value;

            public void WriteTo(IXdrWriter writer)
            {
                writer.Write(Value);
            }
        }

        private class Arguments_4 : IXdrReadable
        {
            public void ReadFrom(IXdrReader reader)
            {
            }
        }

        protected override void DispatchReceivedCall(ReceivedCall call)
        {
            if (call.Version == PortMapperConstants.PortMapperVersion)
            {
                switch (call.Procedure)
                {
                    case PortMapperConstants.Ping_2:
                    {
                        var args = new Arguments_0();
                        call.RetrieveCall(args);
                        Ping_2(call.RemoteIpEndPoint);
                        call.Reply(new Result_0());
                        break;
                    }
                    case PortMapperConstants.Set_2:
                    {
                        var args = new Mapping();
                        call.RetrieveCall(args);
                        var result = new Result_1();
                        result.Value = Set_2(call.RemoteIpEndPoint, args);
                        call.Reply(result);
                        break;
                    }
                    case PortMapperConstants.Unset_2:
                    {
                        var args = new Mapping();
                        call.RetrieveCall(args);
                        var result = new Result_2();
                        result.Value = Unset_2(call.RemoteIpEndPoint, args);
                        call.Reply(result);
                        break;
                    }
                    case PortMapperConstants.GetPort_2:
                    {
                        var args = new Mapping();
                        call.RetrieveCall(args);
                        var result = new Result_3();
                        result.Value = GetPort_2(call.RemoteIpEndPoint, args);
                        call.Reply(result);
                        break;
                    }
                    case PortMapperConstants.Dump_2:
                    {
                        var args = new Arguments_4();
                        call.RetrieveCall(args);
                        var result = Dump_2(call.RemoteIpEndPoint);
                        call.Reply(result);
                        break;
                    }
                    case PortMapperConstants.Call_2:
                    {
                        var args = new CallArguments();
                        call.RetrieveCall(args);
                        var result = Call_2(call.RemoteIpEndPoint, args);
                        call.Reply(result);
                        break;
                    }
                    default:
                        call.ProcedureUnavailable();
                        break;
                }
            }
            else
            {
                call.ProgramMismatch();
            }
        }

        public abstract void Ping_2(IPEndPoint remoteIpEndPoint);
        public abstract bool Set_2(IPEndPoint remoteIpEndPoint, Mapping arg1);
        public abstract bool Unset_2(IPEndPoint remoteIpEndPoint, Mapping arg1);
        public abstract uint GetPort_2(IPEndPoint remoteIpEndPoint, Mapping arg1);
        public abstract MappingList Dump_2(IPEndPoint remoteIpEndPoint);
        public abstract CallResult Call_2(IPEndPoint remoteIpEndPoint, CallArguments arg1);
    }
}
