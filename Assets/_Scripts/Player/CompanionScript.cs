using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CompanionScript : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 playerAI;
    private Animator anim;
    public bool playerAiFound = true;
    public Companion_Commands cc;
    public CompanionHealth ch;
    public float speedFloat;
    public bool canSlam;
    public bool isPlayer;
    public bool haveEnemy;
    public bool isWandering;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        cc.GetComponent<Companion_Commands>();
        ch.GetComponent<CompanionHealth>();
    }

    void Update()
    {

        //if (!isPlayer && !haveEnemy)
        //{
        //    speedFloat = 0;
        //    anim.SetFloat("wSpeed",0);
        //}

        playerAI = FindClosestPlayer().transform.position;
        agent.destination = playerAI;
        gameObject.GetComponent<NavMeshAgent>().speed = speedFloat;

        float dist = Vector3.Distance(gameObject.transform.position, GameObject.FindWithTag("Enemy").transform.position);


        if (dist >= 25)
        {
            canSlam = true;
            speedFloat = 10;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ch.companionHealth -= 1;
        }
    }

    GameObject FindClosestPlayer()
    {

        GameObject[] targets;

        targets = GameObject.FindGameObjectsWithTag("Enemy");
        if (targets.Length >= 1)
        {
            haveEnemy = true;
        }
        if (targets.Length == 0 && isWandering)
        {
            targets = GameObject.FindGameObjectsWithTag("Interest");
        }

        if (targets.Length == 0 && !isWandering)
        {
            haveEnemy = false;
            targets = GameObject.FindGameObjectsWithTag("Player");
        }

        GameObject closestPlayer = null;
        var distance = Mathf.Infinity;
        Vector3 position = transform.position;

        // Iterate through them and find the closest one
        foreach (GameObject target in targets)
        {
            Vector3 difference = (target.transform.position - position);
            float curDistance = difference.sqrMagnitude;
            if (curDistance < distance)
            {
                closestPlayer = target;
                distance = curDistance;
            }
        }

        if (speedFloat >= 5)
        {
            anim.SetFloat("wSpeed", 5);
        }

        else if (speedFloat <= 0 && isPlayer)
        {
            anim.SetFloat("wSpeed", 0);
        }
        return closestPlayer;

    }
    private void OnTriggerStay(Collider other) //Stops before reaching the player so it's not directly behind the player.
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetFloat("wSpeed", 0);
            speedFloat = 0;
            isPlayer = true;
            print("ccb");
            if (other.gameObject.tag == "Enemy")
            {
                isPlayer = false;
            }
        }

        

        if (other.gameObject.tag == "Point1")
        {
            StartCoroutine("interacting");
        }
    }

    private void OnTriggerExit(Collider other) //Starts following when the player is too far again.
    {
        if (other.gameObject.tag == "Player")
        {
            speedFloat = 5;
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;

        }
    }

    public void ShieldBash()
    {
        float dist = Vector3.Distance(gameObject.transform.position, GameObject.FindWithTag("Enemy").transform.position);
    }

    IEnumerator interacting()
    {
        yield return new WaitForSeconds(5);
        //Play animation here to show interactivity
        isWandering = false;
    }
}
