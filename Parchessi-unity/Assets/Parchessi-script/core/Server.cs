using System;

/// <summary>
/// Server.
/// </summary>
public class Server
{
	/// <summary>
	/// The ip.
	/// </summary>
	public string ip;
	
	/// <summary>
	/// The port.
	/// </summary>
	public int port;
		
	/// <summary>
	/// Initializes a new instance of the <see cref="Server"/> class.
	/// </summary>
	/// <param name='ip'>
	/// Ip.
	/// </param>
	/// <param name='port'>
	/// Port.
	/// </param>
	public Server (string ip, int port)
	{
		this.ip = ip;
		this.port = port;
	}
}

