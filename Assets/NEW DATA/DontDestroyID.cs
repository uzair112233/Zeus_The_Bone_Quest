using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyID : MonoBehaviour
{
		void Awake()
		{
			GameObject[] objs = GameObject.FindGameObjectsWithTag("CustomID");

			if (objs.Length > 1)
			{
				Destroy(this.gameObject);
			}

			DontDestroyOnLoad(this.gameObject);
		}
}
