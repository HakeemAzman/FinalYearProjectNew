using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterCombat : MonoBehaviour
{
    [SerializeField] float timeBetweenAttacks = 1f;
    [SerializeField] float damageOutput = 5f;
    [SerializeField] float health;

    float timeSinceLastAttack = Mathf.Infinity;

    #region Player, PlayerHealth
    Transform targetPlayer;
    PlayerHealth playerHealth;
    #endregion

    #region Companion
    Transform targetCompanion;
    #endregion

    public Rigidbody projectile;
    public Transform fireTransform;
    public float launchForce;

    // Start is called before the first frame update
    void Start()
    {

        targetPlayer = PlayerManager.instance.player.transform;
        targetCompanion = PlayerManager.instance.companion.transform;

        playerHealth = targetPlayer.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        AttackPlayer();
        AttackCompanion();
    }

    public void AttackPlayer()
    {
        //the distance between the player and the enemy.
        float distanceFromPlayer = Vector3.Distance(targetPlayer.position, transform.position);
        
        //if the desired distance between the enemy and player is met
        //or when the player is in enemy's attack range, enemy will rotate to face the player.
        if (distanceFromPlayer <= 10)
        {
            FaceTarget(targetPlayer);
            Fire(targetPlayer);
        }
    }

    public void AttackCompanion()
    {
        //the distance between the bot and the enemy.
        float distanceFromCompanion = Vector3.Distance(targetCompanion.position, transform.position);
        
        //if the desired distance between the enemy and bot is met
        //or when the bot is in enemy's attack range, enemy will rotate to face the bot.
        if (distanceFromCompanion <= 10)
        {
            FaceTarget(targetCompanion);
            Fire(targetCompanion);
        }
    }


    void Fire(Transform target)
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float angle = Vector3.Angle(directionToTarget, transform.forward);

        GetComponent<Animator>().SetTrigger("attackbow");

        if(Mathf.Abs(angle) < 40 && timeSinceLastAttack > timeBetweenAttacks)
        {
            timeSinceLastAttack = 0;

            Rigidbody shellInstance = Instantiate(projectile, fireTransform.position, Quaternion.LookRotation(directionToTarget)) as Rigidbody;

            shellInstance.velocity = shellInstance.transform.forward * launchForce;

            //shellInstance.velocity = directionToTarget * launchForce;
        }

    }


    void FaceTarget(Transform target)
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0, directionToTarget.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == targetPlayer)
        {

        }
    }
}
