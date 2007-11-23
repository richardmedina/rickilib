
using System;
using System.Data;

namespace RickiLib.Data
{
	
	
	public abstract class Database
	{
	
		private string name;
		private string username;
		private string password;
		private string hostname;
				
		public Database ()
		{
			name = string.Empty;
			username = string.Empty;
			password = string.Empty;
			hostname = string.Empty;
		}
		
		public void Close ()
		{
			Connection.Close ();
		}
		
		public IDataReader Query (
			string query, params object [] args)
		{
			IDbCommand command = Connection.CreateCommand ();
			command.CommandText = string.Format (query, args);
			System.Data.IDataReader reader = command.ExecuteReader ();
			command.Dispose ();
			command = null;
			
			return reader;
		}
		
		public int NonQuery (string query, params object [] args)
		{
			IDbCommand command = Connection.CreateCommand ();
			command.CommandText = string.Format (query, args);
			int affected_rows = command.ExecuteNonQuery ();
			command.Dispose ();
			command = null;
			
			return affected_rows;
		}
		
		public virtual string Name { 
			get {
				return name;
			}
			
			protected set {
				name = value;
			}
		}
		public virtual string Username { 
			get {
				return username;
			}
			protected set {
				username = value;
			}
		}
		public virtual string Password { 
			get {
				return password;
			}
			protected set {
				password = value;
			}
		}
		public virtual string Hostname {
			get {
				return hostname;
			}
			protected set {
				hostname = value;
			}
		}
		
		//string ConnectionString { get; }
		
		
		public abstract bool Open ();
		public abstract int GetLastInsertId ();		
		public abstract IDbConnection Connection { get; }
	}
}
