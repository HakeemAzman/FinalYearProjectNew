﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Control
{
    public class EnemyGuardMover : MonoBehaviour
    {
        [SerializeField] float chasePlayerDistance = 5f;
        [SerializeField] float chaseCompanionDistance = 5f;
        [SerializeField] float suspicionTime = 5f;

        public float timeSinceLastSawPlayer = Mathf.Infinity;

        NavMeshAgent agent;

        #region Player, PlayerHealth
        Transform targetPlayer;
        PlayerHealth playerHealth;
        #endregion

        #region Enemy Combat System, Enemy Idle Behaviour
        EnemyAgroCombat Combat;

        Vector3 guardPosition;

        #endregion

        #region Companion
        Transform targetCompanion;
        #endregion

        private void Start()
        {
            targetPlayer = PlayerManager.instance.player.transform;
            targetCompanion = PlayerManager.instance.companion.transform;

            Combat = GetComponent<EnemyAgroCombat>();
            agent = GetComponent<NavMeshAgent>();

            playerHealth = targetPlayer.GetComponent<PlayerHealth>();
            guardPosition = transform.position;
        }

        private void Update()
        {
            if (playerHealth.player_Health == 0) return;

            if (targetCompanion != null && InAttackingRangeCompanion())
            {
                timeSinceLastSawPlayer = 0;
                ChaseCompanion();
            }
            else if (InAttackingRangePlayer() && InLineOfSightPlayer())
            {
                timeSinceLastSawPlayer = 0;
                ChasePlayer();
            }
            else if (InDistractionRange())
            {
                bool isdropped = distractionItem.GetComponent<DistractionItem>().isdropped;
                if(isdropped)
                {
                    timeSinceLastSawPlayer = 0;
                    InspectItem();
                }
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                agent.destination = transform.position;
            }
            else
            {
                agent.destination = guardPosition;
            }
            

            timeSinceLastSawPlayer += Time.deltaTime;

            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        public GameObject distractionItem;
        private bool InDistractionRange()
        {
            distractionItem = GameObject.FindGameObjectWithTag("DistractionItem");
            float distanceFromDistraction = Vector3.Distance(distractionItem.transform.position, transform.position);
            return distanceFromDistraction < chasePlayerDistance;
        }

        //When player is in front of the enemy then the enemy will start chasing.
        private bool InLineOfSightPlayer()
        {
            Vector3 directionToTarget = targetPlayer.position - transform.position;
            //the angle between the enemy's face and the player.
            float angle = Vector3.Angle(transform.forward, directionToTarget);
            return Mathf.Abs(angle) < 90;
        }

        private bool InAttackingRangeCompanion()
        {
            float distanceFromCompanion = Vector3.Distance(targetCompanion.position, transform.position);
            return distanceFromCompanion < chaseCompanionDistance;
        }

        private bool InAttackingRangePlayer()
        {
            float distanceFromPlayer = Vector3.Distance(targetPlayer.position, transform.position);
            return distanceFromPlayer < chasePlayerDistance;
        }

        private void ChasePlayer()
        {
            //the distance between the player and the enemy.
            float distanceFromPlayer = Vector3.Distance(targetPlayer.position, transform.position);

            agent.SetDestination(targetPlayer.position);

            //if the desired distance between the enemy and player is met
            //or when the player is in enemy's attack range, enemy will rotate to face the player.
            if (distanceFromPlayer <= agent.stoppingDistance)
            {
                FaceTarget(targetPlayer);

                //Calling the enemy's attack function
                Combat.AttackPlayer();
            }
        }

        private void ChaseCompanion()
        {
            //the distance between the bot and the enemy.
            float distanceFromCompanion = Vector3.Distance(targetCompanion.position, transform.position);

            agent.SetDestination(targetCompanion.position);

            //if the desired distance between the enemy and bot is met
            //or when the bot is in enemy's attack range, enemy will rotate to face the bot.
            if (distanceFromCompanion <= agent.stoppingDistance)
            {
                FaceTarget(targetCompanion);
                Combat.AttackCompanion();
            }
        }

        private void InspectItem()
        {
            float distanceFromDistraction = Vector3.Distance(distractionItem.transform.position, transform.position);

            agent.SetDestination(distractionItem.transform.position);

            //if the desired distance between the enemy and bot is met
            //or when the bot is in enemy's attack range, enemy will rotate to face the bot.
            if (distanceFromDistraction <= agent.stoppingDistance)
            {
                FaceTarget(distractionItem.transform);
            }
        }

        void FaceTarget(Transform target)
        {
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0, directionToTarget.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chasePlayerDistance);
        }
    }
}