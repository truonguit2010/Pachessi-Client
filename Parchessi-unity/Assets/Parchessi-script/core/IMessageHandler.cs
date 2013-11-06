using System;

namespace Parchessi
{
	public interface IMessageHandler
	{
		/// <summary>
		/// Ons the connected.
		/// </summary>
		void onConnected ();
	
		/// <summary>
		/// Ons the connect fail.
		/// </summary>
		void onConnectFail ();
	
		/// <summary>
		/// On the disconnect.
		/// </summary>
		void onDisconnect ();
	
		/// <summary>
		/// On the  message.
		/// </summary>
		/// <param name='m'>
		/// M.
		/// </param>
		void onMessage (string m);
	}
}

