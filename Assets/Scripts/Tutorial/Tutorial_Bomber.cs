using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Bomber : MonoBehaviour
{
    public float Speed;
    public GameObject Target;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Go_To_Target(Target);
    }

    void Go_To_Target(GameObject target)
    {
        transform.LookAt(target.transform);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
    }

}
