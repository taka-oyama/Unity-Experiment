using UnityEngine;
using System.IO;
using System;
using System.Threading;

namespace Game.IO
{
	public sealed class ReadFileAsync : CustomYieldInstruction, IDisposable
	{
		static object locker = new object();

		string sourcePath;
		int bufferSize;

		float _progress = 0f;
		bool _isDone = false;
		byte[] _bytes;
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

		public byte[] bytes {
			get { lock(this) return _bytes; }
			private set { lock(this) _bytes = value; }
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

		public ReadFileAsync(string sourcePath, int bufferSize = 1024 * 1024)
		{
			this.sourcePath = sourcePath;
			this.bufferSize = bufferSize; // 1MB buffer as default
			StartRead();
		}

		public void StartRead()
		{
			ThreadPool.QueueUserWorkItem(_ => {
				try {
					lock(locker) {
						this.bytes = ReadInThread();
					}
				}
				catch(Exception exception) {
					this.exception = exception;
				}
				finally {
					this.isDone = true;
				}
			});
		}

		byte[] ReadInThread()
		{
			using(Stream stream = File.OpenRead(sourcePath))
			{
				byte[] data = new byte[stream.Length];
				int fileSize = (int)stream.Length;
				int readBlock = 0;
				int readBytes = 0;
				int nextBlockSize = bufferSize;

				while((readBlock = stream.Read(data, readBytes, nextBlockSize)) > 0) {
					if(disposed) {
						throw new Exception("Reading file cancelled by Dispose()");
					}
					readBytes += readBlock;
					this.progress = (float)readBytes / fileSize;
					nextBlockSize = (int)Math.Min(bufferSize, fileSize - readBytes);
				}
				return data;
			}
		}

		public void Dispose()
		{
			if(!disposed) {
				lock(this) {
					this.bytes = null;
					this.disposed = true;
				}
			}
		}
	}
}
