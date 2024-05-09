using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codecrafters_redis.src
{
	public static class EventLoop
	{
		private static Queue<Event> queue;
		private static CancellationTokenSource cancellationTokenSource;
		public static void AddEvent(Event @event)
		{
			queue.Enqueue(@event);
		}

		public async void Start()
		{
			queue = new Queue<Event>();
			cancellationTokenSource = new CancellationTokenSource();
			while (!cancellationTokenSource.IsCancellationRequested)
			{
				await Process();
			}
		}

		public void Stop()
		{
			cancellationTokenSource.Cancel();
			//thread stop
			// queue write down or clear
		}

		private async Task Process()
		{

			if (queue.Count == 0)
			{
				await Task.Delay(250);
			}

			Console.WriteLine("--- Processing event");
			//var current = queue.Dequeue();
			//current.ProcessEvent();
		}


	}
}
