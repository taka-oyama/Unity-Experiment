using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Game.Networking
{
	/// <summary>
	/// Loosely based on the code below
	/// http://ftvoid.com/blog/post/847
	/// 
	/// Also checkout
	/// http://juni24.blog.fc2.com/blog-entry-24.html
	/// </summary>
	public class SNTPRequestAsyncOperation : CustomYieldInstruction
	{
		bool _isDone = false;

		public bool isDone {
			get { lock(this) return _isDone; }
			private set { lock(this) _isDone = value; }
		}

		public override bool keepWaiting {
			get { return !isDone; }
		}

		public SNTPRequestAsyncOperation(SNTPRequest request, Uri uri, int sendTimeout, int receiveTimeout)
		{
			ThreadPool.QueueUserWorkItem(o => {
				try {
					request.time = Send(uri, sendTimeout, receiveTimeout);
				}
				catch(Exception exception) {
					request.exception = exception;
				}
				finally {
					this.isDone = true;
				}
			});
		}

		DateTime Send(Uri uri, int sendTimeout, int receiveTimeout)
		{
			DateTime then = DateTime.Now;
			IPEndPoint remoteEP = null;
			byte[] sndData = new byte[48];
			sndData[0] = 0xB;

			UdpClient socket = new UdpClient(uri.Host, uri.Port);
			socket.Client.SendTimeout = sendTimeout;
			socket.Client.ReceiveTimeout = receiveTimeout;
			socket.Send(sndData, sndData.Length);
			byte[] rcvData = socket.Receive(ref remoteEP);
			double roundTripTime = (DateTime.Now - then).Milliseconds / 1000.0;
			socket.Close();

			double intPart = (double)BitConverter.ToUInt32(new byte[] { rcvData[43], rcvData[42], rcvData[41], rcvData[40] }, 0);
			double fracPart = (double)BitConverter.ToUInt32(new byte[] { rcvData[47], rcvData[46], rcvData[45], rcvData[44] }, 0);
			double serverTime = (intPart + fracPart / UInt32.MaxValue);
			DateTime serverDate = new DateTime(1900, 1, 1).AddSeconds(serverTime).ToLocalTime();
			return serverDate.AddSeconds(roundTripTime / 2);
		}
	}
}