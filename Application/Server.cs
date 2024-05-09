using System.Net;
using System.Net.Sockets;
using System.Text;
using MyOwnRedis.Application;
using MyOwnRedis.Application.Commands;

List<Socket> clients = new();


async Task Main()
{
    TcpListener server = new TcpListener(IPAddress.Any, 6379);
    server.Start();
    EventLoop.Start();

    try
    {
        do
        {
            
            var client = server.AcceptSocket(); // wait for client
            clients.Add(client);
            Task.Run(() => HandleClient(client)).ConfigureAwait(false);
        } while(clients.Any());
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    finally
    {
        server.Stop();
        EventLoop.Stop();
    }
}


void HandleClient(Socket client)
{
    try
    {
        while (client.Connected)
        {
            byte[] buffer = new byte[1024];
            int bytesReceived = client.Receive(buffer); // receive data
            string data = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
            data = data.Trim();
            if (data.Contains("exit"))
            {
                break;
            }

            Console.WriteLine(data);
            EventLoop.AddEvent(new PingCommand("PING", client, data));
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
    finally
    {
        client.Close();
        clients.Remove(client);
    }
}

await Main();