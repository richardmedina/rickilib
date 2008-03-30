
using System;
using System.Data;
using MySql.Data.MySqlClient;

using RickiLib.Data;
namespace RickiLib.Data.MySql
{
	
	public class MySqlDatabase : Database
	{		
		private bool connected;
		
		private MySqlConnection connection;
				
		public MySqlDatabase ()
		{
		
		}
		public MySqlDatabase (string hostname, 
			string username, 
			string password, 
			string name) : base (hostname, username, password, name)
		{
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
						Hostname,
						Name,
						Username,
						Password));
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
