using System;
using System.Data;

using RickiLib.Data.Postgres;
using RickiLib.Data;

public class DataTesting
{

	public static void Main ()
	{
		PostgresDatabase database = new PostgresDatabase (
			"localhost", "ricki", "medina", "testing");
		//database.Name = "Testing";
		//database.Hostname = "localhost";
		//database.Username = "ricki";
		//database.Password = "medina";
		
		database.Open ();
		
		IDataReader reader = database.Query ("select * from users");
		
		while (reader.Read ()) {
			Console.WriteLine ("{0}:{1}", reader [0], reader [1]);
		}
		
		database.Close ();
	}

}

