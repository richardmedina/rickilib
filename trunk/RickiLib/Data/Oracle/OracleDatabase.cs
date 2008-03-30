
using System;
using System.Data;
using System.Data.OracleClient;
using RickiLib.Data;

namespace RickiLib.Data.Oracle
{
	 
	public class OracleDatabase : Database
	{
		private OracleConnection _connection;
		
		public OracleDatabase ()
		{
		}
		
		public OracleDatabase (
			string hostname,
			string username,
			string password,
			string name) : base (hostname, username, password, name)
		{
		}
		
		public override bool Open ()
		{
			try {
				_connection = new OracleConnection (
					string.Format (
						"Server={0};" +
							//"Data Source={1};" +
							"User Id={2};" +
							"Password={3};",				
						Hostname,
						Name,
						Username,
						Password));
				_connection.Open ();
			} catch (Exception exception) {
				Console.WriteLine ("Exception connecting to database: {0}",
					exception.ToString ());
				return false;
			}
			
			return true;
		}
		
		public override int GetLastInsertId ()
		{
			throw new NotImplementedException ();
		}
		
		public override IDbConnection Connection {
			get { return _connection; }
		}
	}
}
