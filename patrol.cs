using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class patrol : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;

    [SerializeField] LayerMask groundLayer, playerLayer;

    //patrol
    Vector3 destPoint;
    bool walkpointSet;
    [SerializeField] float range;

    //state change 
    [SerializeField] float sightRange;

    bool playerInSight;


    public float cowSpeed; 

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);

        DoPatrol();
        Chase();
    }


    void Chase()
    {

        agent.SetDestination(player.transform.position);
    }

   void DoPatrol()
    {
        cowSpeed = 30.0f;
        if (!walkpointSet) SearchforDest();
        if (walkpointSet) agent.SetDestination(destPoint);
        if (Vector3.Distance(transform.position, destPoint) < 30) walkpointSet = false;
    }

    void SearchforDest()

    {

        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z * cowSpeed); 

        if (Physics.Raycast(destPoint,Vector3.down, groundLayer))
        {
            walkpointSet = true;
        }
    }

}
