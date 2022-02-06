using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bomber_Guns : NetworkBehaviour
{
    [Tooltip("The position where the bullets are spawned")]
    public GameObject[] Bullet_Spawn;

    [Tooltip("The bullet")]
    public GameObject BulletPrefab;

    public GameObject Bullet_Explosion;

    float Shooting_Time;

    [Tooltip("The max rotation on X")]
    public float Gun_Rotation_X_Max;

    [Tooltip("The max rotation on Y")]
    public float Gun_Rotation_Y_Max;

    [Tooltip("The min rotation on X")]
    public float Gun_Rotation_X_Min;

    [Tooltip("The min rotation on Y")]
    public float Gun_Rotation_Y_Min;


    [Tooltip("The range of the guns")]
    public float Range=200f;

    public string Enemies_Aim;


    /// <summary>
    /// The enemy airplane
    /// </summary>
    private Transform Enemy;

    // Start is called before the first frame update
    void Start()
    {
        // Calls the UpdateTarget functions twice per second
        InvokeRepeating("UpdateTarget", 0f, 0.5f); 
    }

    // Update is called once per frame
    void Update()
    {

        if (Enemy==null)
        {
            return;
        }

        //Gets the position of the enemy
        Vector3 dir = Enemy.position - transform.position;
        Quaternion LookRotation = Quaternion.LookRotation(dir);
        Vector3 Rotation = LookRotation.eulerAngles;
        Aim(Rotation);

    }

    /// <summary>
    /// Is selecting the target
    /// </summary>
    private void UpdateTarget()
    {
        //if (GameObject.FindGameObjectsWithTag("Axis").Length == 0)
        //{
        //    return;
        //}
        //if (GameObject.FindGameObjectsWithTag("Allies").Length == 0)
        //{
        //    return;
        //}
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag(Enemies_Aim);

        float ShortestDistance = Mathf.Infinity;

        GameObject NearestEnemy = null;

        //Checks which enemy is closer
        foreach (GameObject enemy in Enemies)
        {
            float DistanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if(DistanceToEnemy<ShortestDistance)
            {
                ShortestDistance = DistanceToEnemy;
                NearestEnemy = enemy;
            }
        }

        if(NearestEnemy!=null && ShortestDistance<=Range)
        {
            Enemy = NearestEnemy.transform;
        }
        else
        {
            Enemy = null;
        }
    }

    /// <summary>
    /// Is aiming at the enemy
    /// </summary>
    /// <param name="Rotation"></param>
    public void Aim(Vector3 Rotation)
    {
        if (gameObject.name == "Gun_Front")
        {
            if (((Rotation.x < Gun_Rotation_X_Min && Rotation.x > 0f) || (Rotation.x > Gun_Rotation_X_Max)) && ((Rotation.y < Gun_Rotation_Y_Min && Rotation.y > 0f) || (Rotation.y > Gun_Rotation_Y_Max)))
            {
                transform.rotation = Quaternion.Euler(Rotation.x, Rotation.y, 0f);
                Shoot_Enemy();
            }
        }
        else
        {
            if (((Rotation.x < Gun_Rotation_X_Min && Rotation.x > 0f) || (Rotation.x > Gun_Rotation_X_Max)) && (Rotation.y < Gun_Rotation_Y_Max && Rotation.y > Gun_Rotation_Y_Min))
            {
                transform.rotation = Quaternion.Euler(Rotation.x, Rotation.y, 0f);
                Shoot_Enemy();
            }
        }
    }

    public void Shoot_Enemy()
    {
        Shooting_Time += Time.deltaTime;
        if (Shooting_Time >= 1f)
        {
            foreach (GameObject spawn in Bullet_Spawn)
            {
                Instantiate(BulletPrefab, spawn.transform.position, spawn.transform.rotation);
                GameObject go = Instantiate(Bullet_Explosion, spawn.transform.position, spawn.transform.rotation);
                NetworkServer.Spawn(go);
            }
            Shooting_Time = 0;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
