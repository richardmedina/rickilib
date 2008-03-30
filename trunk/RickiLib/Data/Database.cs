
using System;
using System.Data;

namespace RickiLib.Data
{
	
	
	public abstract class Database
	{
	
		private string _hostname;
		private string _username;
		private string _password;
		private string _name;
		
		public Database () :
			this (string.Empty, 
				string.Empty, 
				string.Empty, 
				string.Empty)
		{
		}
				
		public Database (string hostname, string username, string password, string name)
		{
			_hostname = string.Empty;
			_username = string.Empty;
			_password = string.Empty;
			_name = name;
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
			get { return _name; }
			set {	_name = value; }
		}
		public virtual string Username { 
			get { return _username; }
			set { _username = value; }
		}
		public virtual string Password { 
			get { return _password; }
			set { _password = value; }
		}
		public virtual string Hostname {
			get { return _hostname; }
			set { _hostname = value; }
		}
		
		//string ConnectionString { get; }
		
		
		public abstract bool Open ();
		public abstract int GetLastInsertId ();		
		public abstract IDbConnection Connection { get; }
	}
}
