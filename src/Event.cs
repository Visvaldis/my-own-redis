using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace codecrafters_redis.src
{
	internal class Event
	{
		public int Id { get; set; }

		public string? Name { get; set; }

		public Socket? Client { get; set; }

		public string? Body { get; set; }

		//should be abstract
		public void ProcessEvent()
		{
			//do stuff
		}
	}
}
