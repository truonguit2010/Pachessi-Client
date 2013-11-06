using System;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using System.Threading;
using System.Collections;
#if UNITY_EDITOR || UNITY_STANDALONE
using System.Net.Sockets;
#endif
using Parchessi;

/// <summary>
/// Mobile client.
/// Handle communication with server.
/// </summary>
public class MobileClient
{
	const string TAG = "MobileClient";
	
#if UNITY_EDITOR || UNITY_STANDALONE
	/// <summary>
	/// The socket.
	/// </summary>
	private Socket socket;

	
	/// <summary>
	/// The reader thread.
	/// </summary>
	private Thread readerThread;
	
	/// <summary>
	/// The message handler.
	/// </summary>
	private IMessageHandler messageHandler;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="MobileClient"/> class.
	/// </summary>
	public MobileClient ()
	{
		
	}
	
	/// <summary>
	/// Connect the specified server.
	/// </summary>
	/// <param name='server'>
	/// Server.
	/// </param>
	public void connect (Server server, IMessageHandler messageHandler)
	{
		Log.log (TAG, "Connecting to " + server.ip + ", " + server.port);
		this.messageHandler = messageHandler;
		socket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		try {
			socket.Connect (server.ip, server.port);
			Log.log (TAG, "Connected to " + server.ip + ", " + server.port);
			if (socket.Connected && messageHandler != null) {
				startReaderthread();
				messageHandler.onConnected ();
			}
		} catch (Exception e) {
			Log.log (TAG, "Connect fail to " + server.ip + ", " + server.port);
			Log.log (TAG, e);
			if (messageHandler != null) {
				messageHandler.onConnectFail ();
			}
		}
	}
	
	/// <summary>
	/// Sends the message.
	/// </summary>
	/// <param name='m'>
	/// M.
	/// </param>
	public void sendMessage(Message m) {
		if (socket != null && socket.Connected) {
			
			byte[] message = System.Text.Encoding.UTF8.GetBytes (m.toJson());
			
			byte[] length = BitConverter.GetBytes (message.Length);
			Array.Reverse (length);
			
			byte[] output = appendTwoByteArrays(length, message);
			
			int byteSent = socket.Send(output);
			Log.log(TAG, "Total byte sent: " + byteSent);
		}
	}
	
	
	/// <summary>
	/// Close this instance.
	/// </summary>
	public void close ()
	{
		Log.log(TAG, "close socket");
		if (socket != null) {
			socket.Close();
		}	
	}
	
	/// <summary>
	/// Start the readerthread.
	/// </summary>
	private void startReaderthread ()
	{
		if (readerThread == null) {
			readerThread = new Thread (() =>
			{
				while (socket != null && socket.Connected) {
					//Log.log(TAG, "Loop");
					int messageLenght = 0;
					if (socket.Available > 4) {
						
						Log.log(TAG, "Data Available: " + socket.Available);
						
						byte[] temp = new byte[4];
						socket.Receive (temp, 0, 4, SocketFlags.None);
						Array.Reverse (temp);
						messageLenght = BitConverter.ToInt32 (temp, 0);
						
						Log.log(TAG, "Received message lenght: " + messageLenght);
						
						while (socket.Available < messageLenght) {
							// Loop to Wait incoming message.
						}
						
						temp = new byte[messageLenght];
						socket.Receive (temp, 0, messageLenght, SocketFlags.None);
						string msg = System.Text.Encoding.UTF8.GetString (temp);
						
						Log.log(TAG, "Received message String: " + msg);
						
						if (messageHandler != null) {
							Message m = new Message(msg);
							//messageHandler.onMessage(m);
						}
					}
				}
				if (socket != null && messageHandler != null) {
					Log.log(TAG, "Reader thread, on disconnect");
					messageHandler.onDisconnect();
					socket = null;
				}
			});
		}
		readerThread.Start ();
	}
	
	private byte[] appendTwoByteArrays (byte[] arrayA, byte[] arrayB)
	{
		byte[] outputBytes = new byte[arrayA.Length + arrayB.Length];
		Buffer.BlockCopy (arrayA, 0, outputBytes, 0, arrayA.Length);
		Buffer.BlockCopy (arrayB, 0, outputBytes, arrayA.Length, arrayB.Length);
		return outputBytes;
	}
#endif
}