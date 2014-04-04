using UnityEngine;
using System.Collections;

public class WaitC : WaitState 
{
	protected override string ObjectName ()
	{
		return "Button C";
	}

	protected override void OnClick ()
	{
		NextState ();
	}
}
