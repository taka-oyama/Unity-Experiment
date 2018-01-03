using UnityEngine;
using Game.Messaging;
using System.Collections.Generic;
using System;
using Game;

public class NotificationCenter : MonoBehaviour
{
	protected Dictionary<Type, List<Notifier>> notifiers;

	void Awake()
	{
		this.notifiers = new Dictionary<Type, List<Notifier>>();
	}

	public void AddObserver<T>(object context, Action<T> action) where T : Notification
	{
		NotifiersForType(typeof(T)).Add(new Notifier<T>(context, action));
	}

	public bool RemoveObserver<T>(object context, Action<T> action) where T : Notification
	{
		return NotifiersForType(typeof(T)).Remove(new Notifier<T>(context, action));
	}

	public bool RemoveObservers<T>() where T : Notification
	{
		Type type = typeof(T);
		if(notifiers.ContainsKey(type)) {
			return notifiers.Remove(type);
		} else {
			return false;
		}
	}

	public void RemoveAllObservers()
	{
		notifiers.Clear();
	}

	public void PostNotification<T>(T notification) where T : Notification
	{
		Type type = typeof(T);
		if(notifiers.ContainsKey(type)) {
			List<Notifier> stales = new List<Notifier>();
			foreach(Notifier notifier in notifiers[type]) {
				if(!notifier.Send(notification)) {
					stales.Add(notifier);
				}			
			}
			foreach(Notifier stale in stales) {
				notifiers[type].Remove(stale);
				stale.Dispose();
			}
			if(notifiers[type].IsEmpty()) {
				notifiers[type] = null;
			}
		}
	}

	List<Notifier> NotifiersForType(Type type)
	{
		if(!notifiers.ContainsKey(type)) {
			notifiers.Add(type, new List<Notifier>());
		}
		return notifiers[type];
	}

	void OnDestroy()
	{
		RemoveAllObservers();
	}

	public abstract class Notifier : IDisposable
	{
		public abstract bool Send(object notification);

		public abstract void Dispose();
	}

	public class Notifier<T> : Notifier, IDisposable where T : Notification
	{
		object context;
		Action<T> action;

		public Notifier(object context, Action<T> action)
		{
			this.context = context;
			this.action = action;
		}

		public override bool Send(object notification)
		{
			if(context == null || action == null) {
				return false;
			}
			action.Invoke((T) notification);
			return true;
		}

		public override bool Equals(object obj)
		{
			return action.Equals(((Notifier<T>) obj).action);
		}

		public override int GetHashCode()
		{
			return action.GetHashCode();
		}

		public override void Dispose()
		{
			this.context = null;
			this.action = null;
		}
	}
}
