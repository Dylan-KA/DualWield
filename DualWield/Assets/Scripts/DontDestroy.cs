using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
	private static DontDestroy DontDestory;
	void Awake()
	{
		DontDestroyOnLoad(this);

		if (DontDestory == null)
		{
			DontDestory = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
