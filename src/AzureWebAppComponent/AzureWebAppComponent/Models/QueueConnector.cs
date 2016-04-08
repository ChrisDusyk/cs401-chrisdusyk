using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AzureWebAppComponent.Models
{
	public static class QueueConnector
	{
		private static string _connectionString = ConfigurationManager.AppSettings["ServiceBusConnectionString"];

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