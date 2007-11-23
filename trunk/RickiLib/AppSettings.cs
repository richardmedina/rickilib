// AppSettings.cs created with MonoDevelop
// User: ricki at 05:23 p 19/10/2007
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace RickiLib
{
	
	
	public class AppSettings
	{
		private string name;
		
		public AppSettings ()
		{
			name = string.Empty;
		}
		
		public string GetTitle (string title)
		{
			return string.Format ("{0} - {1}", title, name);
		}
		
		public string Name {
			get { return name; }
			protected set { name = value; }
		}
	}
}
