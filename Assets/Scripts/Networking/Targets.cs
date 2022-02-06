using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targets : MonoBehaviour
{
    [Tooltip("The explosion asset")]
    public GameObject Explosion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// The collide detector
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Ouch");
        if (collision.gameObject.tag == "Bullets")
        {
            Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
