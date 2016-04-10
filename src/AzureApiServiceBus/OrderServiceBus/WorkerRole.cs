using Microsoft.Azure;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure.ServiceRuntime;
using OrderServiceBus.Models;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Net.Mail;
using CS401DataContract;

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

						try
						{
							// Parse the object from the message
							PackagedOrder order = receivedMessage.GetBody<PackagedOrder>();
							receivedMessage.Complete();

							// Call the method to insert the data into the database
							InsertPackagedOrder(order);
						}
						catch (Exception ex)
						{
							SendErrorMessage(ex);
						}
					}
					catch
					{
						// Handle any message processing specific exceptions here
						Trace.WriteLine("Error retrieving message: " + receivedMessage.SequenceNumber);
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

		private void InsertPackagedOrder(PackagedOrder packagedOrder)
		{
			try
			{
				SendTestMessage(packagedOrder);

				// Create the database connection and context
				// Since it's in a using-block, it will automatically dispose of the connection when it exits the block
				using (CS401_DBEntities1 db = new CS401_DBEntities1())
				{
					var order = packagedOrder.Order;

					// Add the order to the db context
					db.Orders.Add(order);

					// Insert order to get an OrderID
					db.SaveChanges();

					foreach (var orderProduct in packagedOrder.OrderProducts)
					{
						orderProduct.OrderId = order.OrderId;

						db.OrderProducts.Add(orderProduct);
					}

					// Insert OrderProducts into database now
					db.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine("Error inserting data into db: " + ex.Message);
				SendErrorMessage(ex);
			}
		}

		private void SendErrorMessage(Exception ex)
		{
			var message = new SendGrid.SendGridMessage();
			message.AddTo("chris.dusyk@gmail.com");
			message.From = new MailAddress("chris.dusyk@gmail.com", "Service Bus Error");
			message.Subject = "Service Bus Error";
			message.Text = ex.Message;

			var transport = new SendGrid.Web("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");
			transport.DeliverAsync(message).Wait();
		}

		private void SendTestMessage(PackagedOrder order)
		{
			var message = new SendGrid.SendGridMessage();
			message.AddTo("chris.dusyk@gmail.com");
			message.From = new MailAddress("chris.dusyk@gmail.com", "Service Bus Error");
			message.Subject = "Service Bus Message";
			message.Html =
				"<table> "
				+ " <tr>"
				+ "    <th>OrderID</th>"
				+ "    <th>CustomerID</th>"
				+ "    <th>SoldByID</th>"
				+ "    <th>OrderProducts</th>"
				+ " </tr>"
				+ " <tr>"
				+ "    <td>" + order.Order.OrderId + "</td>"
				+ "    <td>" + order.Order.CustomerId + "</td>"
				+ "    <td>" + order.Order.SoldById + "</td>"
				+ "    <td>" + order.OrderProducts.Count + "</td>"
				+ " </tr></table>";

			var transport = new SendGrid.Web("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");
			transport.DeliverAsync(message).Wait();
		}
	}
}