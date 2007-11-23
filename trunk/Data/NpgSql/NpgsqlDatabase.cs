// NpgSqlDatabase.cs created with MonoDevelop
// User: ricki at 05:16 p 18/10/2007
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Data;
using Npgsql;

namespace RickiLib.Data
{
	
	
	public class NpgsqlDatabase : RickiLib.Data.Database
	{
		private string name;
		private string hostname;
		private string username;
		private string password;
		
		private bool connected;
		
		private NpgsqlConnection connection;
		
		public NpgsqlDatabase (string hostname, string username, string password, string name)
		{
			this.hostname = hostname;
			this.username = username;
			this.password = password;
			this.name = name;
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
						hostname,
						name,
						username,
						password
					)
				);
				connection.Open ();
			} catch (Exception) {
				return connected = false;
			}
			
			return connected = true;
		}
		
		public override int GetLastInsertId ()
		{
			return 0;
		}

		
		public override System.Data.IDbConnection Connection {
			get { return connection; }
		}
		
		public bool Connected {
			get { return connected; }
		}
	}
}
