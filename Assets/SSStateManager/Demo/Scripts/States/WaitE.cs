using UnityEngine;
using System.Collections;

public class WaitE : WaitState 
{
	protected override string ObjectName ()
	{
		return "Button E";
	}

	protected override void OnClick ()
	{
		NextState ();
	}
}
