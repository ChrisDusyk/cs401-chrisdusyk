using Microsoft.Azure;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure.ServiceRuntime;
using OrderServiceBus.Models;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace OrderServiceBus
{
	public class WorkerRole : RoleEntryPoint
	{
		// The name of your queue
		private const string QueueName = "cs401queue";

		// QueueClient is thread-safe. Recommended that you cache
		// rather than recreating it on every request
		private QueueClient Client;

		private ManualResetEvent CompletedEvent = new ManualResetEvent(false);

		public override void Run()
		{
			Trace.WriteLine("Starting processing of messages");

			// Initiates the message pump and callback is invoked for each message that is received, calling close on the client will stop the pump.
			Client.OnMessage((receivedMessage) =>
				{
					try
					{
						// Process the message
						Trace.WriteLine("Processing Service Bus message: " + receivedMessage.SequenceNumber.ToString());

						// Parse the object from the message
						PackagedOrder order = receivedMessage.GetBody<PackagedOrder>();
						receivedMessage.Complete();
					}
					catch
					{
						// Handle any message processing specific exceptions here
					}
				});

			CompletedEvent.WaitOne();
		}

		public override bool OnStart()
		{
			// Set the maximum number of concurrent connections
			ServicePointManager.DefaultConnectionLimit = 12;

			// Create the queue if it does not exist already
			string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
			var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
			if (!namespaceManager.QueueExists(QueueName))
			{
				namespaceManager.CreateQueue(QueueName);
			}

			// Initialize the connection to Service Bus Queue
			Client = QueueClient.CreateFromConnectionString(connectionString, QueueName);
			return base.OnStart();
		}

		public override void OnStop()
		{
			// Close the connection to Service Bus Queue
			Client.Close();
			CompletedEvent.Set();
			base.OnStop();
		}
	}
}