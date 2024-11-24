using System.Collections;
using System.Collections.Generic;
using UnityEngine;

private float totalSpinAmount;
public class SpinAction : MonoBehaviour
{
	public bool isActive;

	private void Update()
	{
		if (!isActive)
			{
				return;
			}
			
			float spinAddAmount = 360f * Time.deltaTime;
			transform.eulerAngles += new Vector3(0,spinAddAmount,0);

			totalSpinAmount += spinAddAmount;
			if (totalSpinAmount >= 360f)
			{
				isActive = false;
			}
	}

	public void Spin()
	{
		isActive = true;
		totalSpinAmount = 0f;
		Debug.Log("Spin");
	}

}
