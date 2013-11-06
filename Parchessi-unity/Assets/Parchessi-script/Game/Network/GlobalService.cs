using System;
using MessageObject;

namespace Parchessi
{
	public class GlobalService
	{
		public GlobalService ()
		{
		}
		
		
		public static void login()
		{
			Login o = new Login();
			o.id = "Truong";
			o.pass = "123456";
			Message m = new Message("LOGIN", o);
			CoreService.Instance.sendMessage(m);
		}
	}
}

