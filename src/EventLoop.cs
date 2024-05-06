using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codecrafters_redis.src
{
	internal class EventLoop
	{
		Queue<Event> queue;
		Thread thread;
		
		public void AddEvent(Event @event)
		{
			queue.Enqueue(@event);
		}

		public void Start()
		{
			thread = new Thread(Process);
			thread.Start();
		}

		public void Stop()
		{
			//thread stop
			// queue write down or clear
		}

		private void Process()
		{

			if (queue.Count == 0)
			{
				thread.Delay();
			}

			var current = queue.Dequeue();
			current.ProcessEvent();
		}


	}
}
