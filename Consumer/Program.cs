using System;
using System.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tools.RuntimeClient;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter process id");
            var processId = Console.ReadLine();

            var configuration = new SessionConfiguration(
                1000,
                EventPipeSerializationFormat.NetTrace,
                new[]
                {
                    new Provider(
                        name: "Microsoft-Windows-DotNETRuntime",
                        keywords: (uint) ClrTraceEventParser.Keywords.GC,
                        eventLevel: EventLevel.Informational)
                }
            );

            var binaryReader =
                EventPipeClient.CollectTracing(Int32.Parse(processId), configuration, out var _sessionId);
            EventPipeEventSource source = new EventPipeEventSource(binaryReader);
            source.Process();
            Console.WriteLine("Hello World!");
        }
    }
}