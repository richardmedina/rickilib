
using System;

namespace RickiLib.Types
{


	public interface IAllowed
	{
		bool GetPermission (string module, AllowType allow_type);
	}
}
