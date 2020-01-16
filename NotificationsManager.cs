using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotificationsManager : MonoBehaviour {
	private Dictionary<string, List<Component>> Listeners = new Dictionary<string, List<Component>>();

	public void AddListener(Component Sender, string NotificationName)
	{
		if(!Listeners.ContainsKey (NotificationName))
			Listeners.Add (NotificationName, new List<Component>());

		Listeners[NotificationName].Add(Sender);
	}

	public void RemoveListener(Component Sender, string NotificationName) 
	{
		if (!Listeners.ContainsKey (NotificationName))
						return;

		for (int i = Listeners[NotificationName].Count-1; i>=0; i--) 
		{
			if(Listeners[NotificationName][i].GetInstanceID() == Sender.GetInstanceID())
				Listeners[NotificationName].RemoveAt(i);
		}
	}

	public void PostNotification(Component Sender, string NotificationName) 
	{
		if (!Listeners.ContainsKey (NotificationName))
		    return;

		foreach(Component Listener in Listeners[NotificationName]) {
			if (Listener != null) {
			    Listener.SendMessage(NotificationName, Sender, SendMessageOptions.DontRequireReceiver);
			}
		}
    }

	public void ClearListeners() 
	{
		Listeners.Clear ();
	}

	public void RemoveRedundancies() 
	{
		Dictionary<string, List<Component>> TmpListeners = new Dictionary<string, List<Component>>();

		foreach(KeyValuePair<string, List<Component>> Item in Listeners) 
		{
			for(int i = Item.Value.Count-1; i>=0; i--) 
			{
				if (Item.Value[i] == null)
					Item.Value.RemoveAt(i);
			}

			if(Item.Value.Count > 0)
				TmpListeners.Add(Item.Key, Item.Value);
		}

		Listeners = TmpListeners;
	}

	void OnLevelWasLoaded()
	{
		RemoveRedundancies ();
	}
}
