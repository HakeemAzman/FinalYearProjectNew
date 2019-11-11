using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CompanionScript : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 playerAI;
    private Animator anim;
    [Space]
    [Header("Scripts")]
    public Companion_Commands cc;
    public CompanionHealth ch;
    public AoeAttack aAttack;
    [Space]
    public float speedFloat;
    public float charges;
    [Space]
    [Header("Bool")]
    public bool canSlam;
    public bool isPlayer;
    public bool haveEnemy;
    public bool isWandering;
    public bool playerAiFound = true;
    [Space]
    [Header("Gameobjects")]
    public GameObject Player;
    public GameObject Overcharge;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        cc.GetComponent<Companion_Commands>();
        ch.GetComponent<CompanionHealth>();
        aAttack.GetComponent<AoeAttack>();
    }

    void Update()
    {
        //if (!isPlayer && !haveEnemy)
        //{
        //    speedFloat = 0;
        //    anim.SetFloat("wSpeed", 0);
        //}

        if (charges >= 1)
        {
            aAttack.damage = 100;
            //speedFloat = 10;
        }
        if (charges >= 3)
        {
            charges = 3;
        }
        if(charges <= 0)
        {
            Overcharge.SetActive(false);
            charges = 0;
        }

        if(charges <= 0)
        {
            aAttack.damage = 30;
        }
       
        playerAI = FindClosestPlayer().transform.position;
        agent.destination = playerAI;
        gameObject.GetComponent<NavMeshAgent>().speed = speedFloat;

        //float dist = Vector3.Distance(gameObject.transform.position, GameObject.FindWithTag("Enemy").transform.position);


        //if (dist >= 25)
        //{
        //    canSlam = true;
        //    speedFloat = 10;
        //}

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
            speedFloat = 0;
            print("This is happening");
            anim.SetFloat("wSpeed", 0);
            isPlayer = true;
            
        }

        else if (other.gameObject.tag == "Enemy")
        {
            isPlayer = false;
        }

        

        if (!haveEnemy)
        {
            Player.gameObject.GetComponent<BoxCollider>().enabled = true;
        }

        else if(haveEnemy)
        {
            Player.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        

        if (other.gameObject.tag == "Point1")
        {
            StartCoroutine("interacting");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wrench")
        {
            Overcharge.SetActive(true);
            charges += 1;
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
