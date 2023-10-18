using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    public float chaseRange = 2.5f;
    public float attackRange = 0.75f;
    private Health healthCmp;
    public CharacterStatsSO stats;
    [NonSerialized] public Combat combatCmp;
    [NonSerialized] public GameObject player;
    [NonSerialized] public float distanceFromPlayer;
    [NonSerialized] public Vector3 originalPosition;
    [NonSerialized] public Movement movementCmp;
    [NonSerialized] public Patrol patrolCmp;
    [NonSerialized] public bool hasUIOpened = false;
    private AIBaseState currentState;
    public AIReturnState retreatState = new AIReturnState();
    public AIChaseState chaseState = new AIChaseState();
    public AIAttackState attackState = new AIAttackState();
    public AIPatrolState patrolState = new AIPatrolState();
    public AIDefeatedState defeatState = new AIDefeatedState();

    private void Awake()
    {
        if (stats == null)
        {
            Debug.LogWarning($"{name} does not have a CharacterStatsSO component attached to it");
        }
        currentState = retreatState;
        player = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG);
        movementCmp = GetComponent<Movement>();
        patrolCmp = GetComponent<Patrol>();
        healthCmp = GetComponent<Health>();
        combatCmp = GetComponent<Combat>();
        originalPosition = transform.position;
    }

    private void Start()
    {
        currentState.EnterState(this);
        healthCmp.healthPoints = stats.health;
        combatCmp.damage = stats.damage;

        if (healthCmp.sliderCmp != null)
        {
            healthCmp.sliderCmp.maxValue = stats.health;
            healthCmp.sliderCmp.value = stats.health;
        }
    }

    private void OnEnable()
    {
        healthCmp.OnStartDefeated += HandleStartDefeated;
        EventManager.OnToggleUI += HandleToggleUI;
    }

    private void OnDisable()
    {
        healthCmp.OnStartDefeated -= HandleStartDefeated;
        EventManager.OnToggleUI -= HandleToggleUI;
    }

    private void Update()
    {
        CalculateDistanceFromPlayer();
        currentState.UpdateState(this);
    }

    public void SwitchState(AIBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    private void CalculateDistanceFromPlayer()
    {
        if (player == null) return;
        Vector3 enemyPosition = transform.position;
        Vector3 playerPosition = player.transform.position;
        distanceFromPlayer = Vector3.Distance(enemyPosition, playerPosition);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    public void Attack()
    {
        //Debug.Log("Attacking");
    }

    private void HandleStartDefeated()
    {
        SwitchState(defeatState);
        currentState.EnterState(this);
    }

    private void HandleToggleUI(bool isOpened)
    {
        hasUIOpened = isOpened;
    }
}
