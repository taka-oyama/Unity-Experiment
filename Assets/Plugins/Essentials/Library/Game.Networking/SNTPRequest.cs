using UnityEngine;
using System;

namespace Game.Networking
{
	/// <summary>
	/// Loosely based on the code below
	/// http://ftvoid.com/blog/post/847
	/// 
	/// Also checkout
	/// http://juni24.blog.fc2.com/blog-entry-24.html
	/// </summary>
	public class SNTPRequest
	{
		Uri uri;
		int sendTimeout;
		int receiveTimeout;

		DateTime _time;
		Exception _exception;

		public DateTime time {
			get { lock(this) return _time; }
			internal set { lock(this) _time = value; }
		}

		public Exception exception {
			get { lock(this) return _exception; }
			internal set { lock(this) _exception = value; }
		}

		public SNTPRequest(Uri uri = null, int sendTimeout = 1000, int receiveTimeout = 1000)
		{
			this.uri = uri ?? new Uri("udp://ntp.nict.jp:123");
			this.sendTimeout = sendTimeout;
			this.receiveTimeout = receiveTimeout;
		}

		public SNTPRequestAsyncOperation GetAsync()
		{
			return new SNTPRequestAsyncOperation(this, uri, sendTimeout, receiveTimeout);
		}
	}
}