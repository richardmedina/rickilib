
using System;
using System.Data;
using MySql.Data.MySqlClient;

using RickiLib.Data;
namespace RickiLib.Data.MySql
{
	
	public class MySqlDatabase : Database
	{
		private string name;
		private string hostname;
		private string username;
		private string password;
		
		private bool connected;
		
		private MySqlConnection connection;
		
		~MySqlDatabase ()
		{
			base.Close ();
		}
		
		public MySqlDatabase (string hostname, string username, string password, string name)
		{
			this.hostname = hostname;
			this.username = username;
			this.password = password;
			this.name = name;
		}
		
		public override bool Open ()
		{
			try {
				connection = new MySqlConnection (
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
			IDataReader reader = Query ("select last_insert_id();");
			int id = 0;
			
			if (reader.Read ()) {
				id = int.Parse (reader ["last_insert_id()"].ToString ());
			}
			
			reader.Close ();
			return id;
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
