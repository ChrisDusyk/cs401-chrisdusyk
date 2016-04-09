using Microsoft.Azure;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System.Configuration;

namespace AzureWebAppComponent.Models
{
	public static class QueueConnector
	{
		private static string _connectionString = CloudConfigurationManager.GetSetting("ServiceBusConnectionString");

		public static QueueClient Client;

		public static string QueueName = ConfigurationManager.AppSettings["ServiceBusQueueName"];

		public static void Initialize()
		{
			ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Http;

			var namespaceManager = NamespaceManager.CreateFromConnectionString(_connectionString);

			if (!namespaceManager.QueueExists(QueueName))
			{
				namespaceManager.CreateQueue(QueueName);
			}

			Client = QueueClient.CreateFromConnectionString(_connectionString, QueueName);
		}
	}
}