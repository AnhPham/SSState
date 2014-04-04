using UnityEngine;
using System.Collections;

public class WaitB : WaitState 
{
	protected override string ObjectName ()
	{
		return "Button B";
	}

	protected override void OnClick ()
	{
		NextState ();
	}
}
