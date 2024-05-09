using System.Net;
using System.Net.Sockets;
using System.Text;
using codecrafters_redis.src;

async Task Main()
{
    var eventLoop = new EventLoop();
    eventLoop.Start();
    TcpListener server = new TcpListener(IPAddress.Any, 6379);
    server.Start();
    var client = server.AcceptSocket(); // wait for client
    try
    {
        while(true)
        {
            byte[] buffer = new byte[1024];
            int bytesReceived = client.Receive(buffer); // receive data
            string data = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
            data = data.Trim();
            if (data.Contains("exit"))
            {
                eventLoop.Stop();
            }
            Console.WriteLine(data);
            client.Send("+PONG\r\n"u8.ToArray()); // send response
        }
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    finally
    {
        client.Close();
        server.Stop();
    }
}


void HandleClient(Socket client)
{
    byte[] buffer = new byte[1024];
    int bytesReceived = client.Receive(buffer); // receive data
    string data = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
    data = data.Trim();
    Console.WriteLine(data);
    client.Send("+PONG\r\n"u8.ToArray()); // send response
}

await Main();