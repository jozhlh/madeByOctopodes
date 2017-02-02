using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTrigger : AkTriggerBase
{
	public void ActivateTrigger()
	{
		if (triggerDelegate != null)
		{
			triggerDelegate(null);
		}
	}
}
