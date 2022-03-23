using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartScript : MonoBehaviour
{
	public void OnClick(){
		Time.timeScale = 1;
		Application.LoadLevel(0);
	}
}
