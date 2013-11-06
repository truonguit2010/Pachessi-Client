using System;

namespace Parchessi
{
	public class Message
	{
		public String c;
		public Object d;
		public Message (String command, Object data)
		{
			this.c = command;
			this.d = data;
		}
	
		public Message (string jsonMessageString)
		{
		}
	
		/// <summary>
		/// Tos the json.
		/// </summary>
		/// <returns>
		/// The json String.
		/// </returns>
		public string toJson ()
		{
			return fastJSON.JSON.Instance.ToJSON (this);
		}
	}
}

