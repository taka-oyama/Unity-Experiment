using UnityEngine;
using System.IO;
using System;
using System.Threading;

namespace Game.IO
{
	public sealed class WriteFileAsync : CustomYieldInstruction
	{
		static object locker = new object();
		string destinationPath;
		int bufferSize;
		Stream stream;
		long fileSize;

		float _progress = 0f;
		bool _isDone = false;
		Exception _exception;
		bool _disposed = false;

		public float progress {
			get { lock(this) return _progress; }
			private set { lock(this) _progress = value; }
		}

		public bool isDone {
			get { lock(this) return _isDone; }
			private set { lock(this) _isDone = value; }
		}

		public Exception exception {
			get { lock(this) return _exception; }
			private set { lock(this) _exception = value; }
		}

		bool disposed {
			get { lock(this) return _disposed; }
			set { lock(this) _disposed = value; }
		}
			
		public sealed override bool keepWaiting { get { return !isDone; } }

		public WriteFileAsync(string destinationPath, Stream stream, int bufferSize = 1024 * 1024)
		{
			this.destinationPath = destinationPath;
			this.stream = stream;
			this.bufferSize = bufferSize; // 1MB buffer as default
			this.fileSize = stream.Length;
			StartWrite();
		}

		public WriteFileAsync(string destinationPath, Byte[] data, int bufferSize = 1024 * 1024) : 
		this(destinationPath, new MemoryStream(data), bufferSize) {}

		public void StartWrite()
		{
			ThreadPool.QueueUserWorkItem(_ => {
				try {
					lock(locker) {
						WriteInThread();
					}
				}
				catch(Exception exception) {
					this.exception = exception;
				}
				finally {
					stream.Dispose();
					this.isDone = true;
				}
			});
		}

		void WriteInThread()
		{
			Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));

			using(Stream dest = File.OpenWrite(destinationPath)) {
				byte[] buffer = new byte[bufferSize];
				long totalBytes = 0;
				int readBytes = 0;

				while((readBytes = stream.Read(buffer, 0, buffer.Length)) > 0) {
					if(disposed) {
						throw new Exception("Reading file cancelled by Dispose()");
					}
					dest.Write(buffer, 0, readBytes);
					totalBytes += readBytes;
					this.progress = (float)totalBytes / fileSize;
				}
			}
		}

		public void Dispose()
		{
			if(!disposed) {
				lock(this) {
					this.disposed = true;
				}
			}
		}
	}
}