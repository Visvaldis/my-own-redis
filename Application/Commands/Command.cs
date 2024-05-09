using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnRedis.Application.Commands
{
	public abstract class Command
	{
		protected Command(string? name, Socket? client, string? body)
		{
			Name = name;
			Client = client;
			Body = body;
		}

		public int Id { get; init; }

		public string? Name { get; init; }

		public Socket? Client { get; init; }

		public string? Body { get; init; }

		//should be abstract
		public abstract Task ProcessEvent();
	}
}
