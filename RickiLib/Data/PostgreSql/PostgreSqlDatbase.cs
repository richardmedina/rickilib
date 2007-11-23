
using System;
using System.Data;
using Npgsql;

using RickiLib.Data;
namespace RickiLib.Data.PostgreSql
{
	
	public class PostgreSqlDatabase : Database
	{
		private bool connected;
		
		private NpgsqlConnection connection;
		
		
				
		public PostgreSqlDatabase (string hostname, string username, string password, string name)
		{
			base.Hostname = hostname;
			base.Username = username;
			base.Password = password;
			base.Name = name;
		}
		
		public override bool Open ()
		{
			try {
				connection = new NpgsqlConnection (
					string.Format (
						"Server={0};" +
							"Database={1};" +
							"User ID={2};" +
							"Password={3};",				
						Hostname,
						Name,
						Username,
						Password
					)
				);
				connection.Open ();
			} catch (Exception exception) {
				Console.WriteLine (exception.Message);
				return connected = false;
			}
			
			return connected = true;
		}
		
		// TODO: Empty method
		public override int GetLastInsertId ()
		{
			return -1;
		}

		
		public override IDbConnection Connection {
			get {
				return connection;
			}
		}
		
		public bool Connected {
			get {
				return connected;
			}
		}
	}
}
