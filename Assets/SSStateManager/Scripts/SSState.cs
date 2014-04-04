using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SSStateData
{
	public System.Type Type { get; protected set; }
	public object Data { get; protected set; }

	public SSStateData()
	{
	}

	public SSStateData(System.Type type, object data = null)
	{
		this.Type = type;
		this.Data = data;
	}
}

public class SSState : MonoBehaviour 
{
	public delegate void OnNextStateDelegate ();

	public OnNextStateDelegate onNextState;

	public virtual void Set(GameObject host, object data = null)
	{
	}

	public void Run()
	{
		StartCoroutine (IERun ());
	}

	protected virtual void NextState()
	{
		if (onNextState != null) 
		{
			onNextState ();
		}
	}
				

	protected virtual IEnumerator IERun()
	{
		yield return 0;
	}
}
