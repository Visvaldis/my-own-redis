using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyOwnRedis.Application.Commands;

namespace MyOwnRedis.Application
{
	public static class EventLoop
	{
		private static Queue<Command> queue;
		private static CancellationTokenSource cancellationTokenSource;
		

		public static async void Start()
		{
			queue = new Queue<Command>();
			cancellationTokenSource = new CancellationTokenSource();
			while (!cancellationTokenSource.IsCancellationRequested)
			{
				await Process();
			}
		}

		public static void Stop()
		{
			cancellationTokenSource.Cancel();
			//thread stop
			// queue write down or clear
		}

		public static void AddEvent(Command command)
		{
			queue.Enqueue(command);
		}
		
		private static async Task Process()
		{
			if (queue.Count == 0)
			{
				await Task.Delay(250);
				return;
			}
			
			var current = queue.Dequeue();
			Console.WriteLine($"--- Processing command -- {current.Name}");
			try
			{
				await current.ProcessEvent();
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
