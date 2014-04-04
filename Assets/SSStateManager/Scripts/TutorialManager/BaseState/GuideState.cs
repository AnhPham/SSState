using UnityEngine;
using System.Collections;

public class GuideStateData : SSStateData
{
	public GuideStateData(params string[] talks) : base (typeof(GuideState), talks)
	{
	}
}

public class GuideState : SSTutorialState 
{

}
