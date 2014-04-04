using UnityEngine;
using System.Collections;

public class WaitA : WaitState 
{
	protected override string ObjectName ()
	{
		return "Button A";
	}

	protected override void OnClick ()
	{
		NextState ();
	}
}
