using System.Data.Entity;

namespace AzureWebAppComponent.Models
{
	public class MySqlInitializer : IDatabaseInitializer<ApplicationDbContext>
	{
		public void InitializeDatabase(ApplicationDbContext context)
		{
			if (!context.Database.Exists())
			{
				context.Database.Create();
			}
			else
			{
				//var migrationHistoryTableExists = ((IObjectContextAdapter)context).ObjectContext.ExecuteStoreQuery<int>(
				//	string.Format("SELECT COUNT(*) FROM information_schemas.tables WHERE table_schema = '{0}' AND table_name = '__MigrationHistory'", ""));

				//if (migrationHistoryTableExists.FirstOrDefault() == 0)
				//{
				//	context.Database.Delete();
				//	context.Database.Create();
				//}
			}
		}
	}
}