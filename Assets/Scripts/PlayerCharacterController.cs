using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerCharacterController : MonoBehaviour
{
    public const string ANIMATOR_VELOCITY_PARAM = "Velocity";
    public const string ANIMATOR_FALL_PARAM = "Fall";
    
    [Header("Navigation")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform[] pathPoints;
    private int _currentPoint;
    private bool _isMoving = true;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Events")]
    [SerializeField] public UnityEvent OnPlayerFall;
    [SerializeField] public UnityEvent OnPlayerReachedPoint;
    [SerializeField] public UnityEvent OnPlayerTookDamage;

    [Header("Player Data")] 
    [SerializeField] private int hp = 100;
    [SerializeField] private int fallDamage = 10;

    
    [ContextMenu("Fall")]
    public void Fall()
    {
       _isMoving = false;
        agent.SetDestination(transform.position);
        
        OnPlayerFall?.Invoke();
        
        ApplyDamage(fallDamage);
    }

    [ContextMenu("Continue Route")]
    public void ContinueRoute()
    {
        _isMoving = true;
        agent.SetDestination(pathPoints[_currentPoint].position);
    }

    private void OnEnable() => OnPlayerFall.AddListener(ActivateFallAnimationTrigger);
    private void OnDisable() => OnPlayerFall.RemoveListener(ActivateFallAnimationTrigger);
    
        
    

    private void ApplyDamage(int dmg)
    {
        if(dmg <= 0)
            return;
        
        hp -= dmg;
        if (hp < 0)
            hp = 0;
        
        OnPlayerTookDamage?.Invoke();
  
    }
    
    
    private void Start()
    {
        agent.SetDestination(pathPoints[_currentPoint].transform.position);
    }

    private void Update()
    {
        animator.SetFloat(ANIMATOR_VELOCITY_PARAM,agent.velocity.magnitude);
        if (agent.remainingDistance <= 0.1  && _isMoving )
        {
            _currentPoint++;
            if (_currentPoint > pathPoints.Length-1)
                _currentPoint = 0;

            agent.SetDestination(pathPoints[_currentPoint].position);

            OnPlayerReachedPoint?.Invoke();
        }
    }

    private void ActivateFallAnimationTrigger() => animator.SetTrigger(ANIMATOR_FALL_PARAM);
    

   
}
