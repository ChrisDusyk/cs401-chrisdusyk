using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System.Configuration;

namespace CS401WebApp
{
	public static class QueueConnector
	{
		public static QueueClient Client;

		public static string QueueAddress = ConfigurationManager.AppSettings["ServiceBusAddress"];

		public static string QueueName = ConfigurationManager.AppSettings["ServiceBusQueueName"];

		public static void Initialize()
		{
			ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Http;

			var namespaceManager = NamespaceManager.CreateFromConnectionString(QueueAddress);

			if (!namespaceManager.QueueExists(QueueName))
			{
				namespaceManager.CreateQueue(QueueName);
			}

			Client = QueueClient.CreateFromConnectionString(QueueAddress, QueueName);
		}
	}
}