
using System;

namespace RickiLib.Data
{
		
	public interface IRegister
	{
		void Save ();
		bool Remove ();
		bool Restore ();
		bool Exists { get; }
		
//		string TableName { get; }
	}
}
