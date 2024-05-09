using System.Net.Sockets;
using System.Text;

namespace MyOwnRedis.Application.Commands;

public class PingCommand: Command
{
    public PingCommand(string? name, Socket? client, string? body) : base(name, client, body)
    {
    }
    
    public override Task ProcessEvent()
    {
        Client?.Send("+PONG\r\n"u8.ToArray()); // send response
        return Task.CompletedTask;
    }
}