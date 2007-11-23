
using System;
using System.Collections;
using Gtk;

namespace RickiLib.Widgets
{
	
	
	public class ManagedNotebook : Gtk.VBox, System.Collections.ICollection 
	{
		private System.Collections.ArrayList list;
		private Gtk.Notebook notebook;
		
		public ManagedNotebook ()
		{
			list = new ArrayList ();
			notebook = new Notebook ();
			notebook.ShowBorder = false;
			notebook.Scrollable = true;
			notebook.EnablePopup = true;
			
			base.PackStart (notebook);
			//notebook.AppendPage (new Button (Stock.Ok), new Label ("Hello"));
		}
		
		public void LoadPage (string title)
		{
			foreach (INotebookPage page in this) {
				if (page.Title == title) {
					if (page.Position > 0) {
						notebook.Page = page.Position;
					} else {
						page.Position = notebook.AppendPage (
							page.Widget,
							page.TitleWidget
						);
					}
						
				}
			}
		}
		
		public void LoadPage (INotebookPage page)
		{
			
		}
		
		public void LoadAllPages ()
		{
			foreach (INotebookPage page in list) {
				notebook.AppendPage (page.Widget,  page.TitleWidget);
			}
		}
		
		public int Add (INotebookPage page)
		{
			return list.Add (page);
		}
		
		public void Remove (INotebookPage page)
		{
			list.Remove (page);
		}
		
		public void RemoveAt (int position)
		{
			list.RemoveAt (position);
		}
		
		public INotebookPage IndexOf (int position)
		{
			return (INotebookPage) list [position];
		}
		
		public Gtk.Notebook RawNotebook {
			get { return notebook; }
		}
				

#region ICollection members
		public new IEnumerator GetEnumerator ()
		{
			return list.GetEnumerator ();
			//foreach (INotebookPage page in list)
			//	yield return page;
		}
		
		public void CopyTo (System.Array array, int n)
		{
			list.CopyTo (array, n);
		}
		
		public int Count {
			get {
				return list.Count;
			}
		}
		
		public bool IsSynchronized {
			get {
				return list.IsSynchronized;
			}
		}
		
		public object SyncRoot {
			get {
				return list.SyncRoot;
			}
		}
	}
#endregion
}
