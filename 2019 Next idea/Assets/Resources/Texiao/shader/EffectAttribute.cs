using UnityEngine;
using System.Collections;

public class EffectAttribute : MonoBehaviour 
{
	public bool mFollowParentOri = true;
	public float mDestroyTime = 10.0f;

	// Use this for initialization
	void Start () 
	{
		GameObject.Destroy(gameObject, mDestroyTime);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!mFollowParentOri)
		{
			gameObject.transform.rotation = Quaternion.identity;
		}
	}
}
