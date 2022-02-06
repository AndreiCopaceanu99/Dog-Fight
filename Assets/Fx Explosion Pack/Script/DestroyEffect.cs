using UnityEngine;
using System.Collections;

public class DestroyEffect : MonoBehaviour {

    float time;

	void Update ()
	{
        time += Time.deltaTime;
		if(time>5.0f)
        {
            Destroy(gameObject);
        }
	
	}
}
