using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collider collision)
    {
        Tutorial_Health h = GetComponent<Tutorial_Health>();
        Debug.Log(h.HP);
    }
}
