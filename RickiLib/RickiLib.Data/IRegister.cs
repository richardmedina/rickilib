
using System;

namespace RickiLib.Data
{
		
	public interface IRegister
	{
		bool Save ();
		bool Remove ();
		bool Update ();
		bool Exists { get; }
		
//		string TableName { get; }
	}
}
