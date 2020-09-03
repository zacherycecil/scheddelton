using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonBehaviour : MonoBehaviour
{
	public MenuSystem menuSystem;

	public void ReturnToMain()
	{
		menuSystem.ReturnToMain();
	}
}
