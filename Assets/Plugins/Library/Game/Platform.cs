using UnityEngine;

namespace Game
{
	public class Platform
	{
		public static bool isDebug
		{
			get {
				#if DEBUG
				return true;
				#else
				return false;
				#endif
			}
		}

		public static bool isProduction
		{
			get { return !isDebug; }
		}

		public static bool isAndroid
		{
			get { return Application.platform == RuntimePlatform.Android; }
		}

		public static bool isIOS
		{
			get { return Application.platform == RuntimePlatform.IPhonePlayer; }
		}

		public static bool isEditor
		{
			get { return Application.isEditor; }
		}
	}
}