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
    Console.WriteLine("Server started");
    try
    {
        do
        {
            
            var client = server.AcceptSocket(); // wait for client
            clients.Add(client);
            Console.WriteLine("Client connected");
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
        Console.WriteLine("Server stopped");
    }
}


Task HandleClient(Socket client)
{
    try
    {
        while (true)
        {
            byte[] buffer = new byte[1024];
            int bytesReceived = client.Receive(buffer); // receive data
            if (bytesReceived == 0)
            {
                break;
            }
            
            string data = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
            data = data.Trim();

            Console.WriteLine(data);
            if (data.Length > 0)
            {
                EventLoop.AddEvent(new PingCommand("PING", client, data));
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
    finally
    {
        client.Close();
        clients.Remove(client);
        Console.WriteLine("Client disconnected");
    }
}

await Main();