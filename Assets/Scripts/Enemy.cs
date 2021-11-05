using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public enum EnemyState 
{ 
    Idle,
    MoveToBuilding,
    MoveToUnit,
    Attack
}

 
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public EnemyState CurrentEnemyState;


    [SerializeField] float DistanceToAttack;
    [SerializeField] float DistanceToFollow;

    public Building TargetBuilding;
    public Unit TargetUnit;

    BuildingPlacer buildingPlacer;
    UnitManagement unitManagement;
    EnemyManager enemyManager;

    public int Health;
    public GameObject HealthBar;
    HealthBar healthBar;
    private int maxHealth;

    NavMeshAgent navMeshAgent;

    [SerializeField] float attackRate;
    float _timer;

    


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()

    {
        GameObject _healthBar = Instantiate(HealthBar);
        maxHealth = Health;
        healthBar = _healthBar.GetComponent<HealthBar>();
        healthBar.Setup(transform);
        unitManagement = UnitManagement.instance;
        buildingPlacer = BuildingPlacer.instance;
        enemyManager = EnemyManager.instance;
        SetEnemyState(EnemyState.MoveToBuilding);
    }

    private void Update()
    {
        if (CurrentEnemyState==EnemyState.Idle)
        {
            FindNearestBulding();
            if (TargetBuilding)
            {
                CurrentEnemyState = EnemyState.MoveToBuilding;
            }
            FindNearestUnit();
        }
        if (CurrentEnemyState == EnemyState.MoveToBuilding)
        {
            FindNearestUnit();
            if (!TargetBuilding)
            {
                SetEnemyState(EnemyState.Idle);
            }
        }
        if (CurrentEnemyState == EnemyState.MoveToUnit)
        {
            FindNearestUnit();
            if (TargetUnit)
            {
                navMeshAgent.SetDestination(TargetUnit.transform.position);
                float distance = Vector3.Distance(transform.position,TargetUnit.transform.position);
                if (distance>DistanceToFollow)
                {
                    SetEnemyState(EnemyState.MoveToBuilding);
                }
                if (distance < DistanceToAttack)
                {
                    SetEnemyState(EnemyState.Attack);
                }
            }
            else
            {
                SetEnemyState(EnemyState.MoveToBuilding);
            }
        }
        if (CurrentEnemyState == EnemyState.Attack)
        {
            if (TargetUnit)
            {
                navMeshAgent.SetDestination(TargetUnit.transform.position);
                float distance = Vector3.Distance(transform.position, TargetUnit.transform.position);
                if (distance > DistanceToAttack)
                {
                    SetEnemyState(EnemyState.MoveToUnit);
                }
                _timer += Time.deltaTime;
                if (_timer>attackRate)
                {
                    _timer = 0;
                    TargetUnit.TakeDamage(1);
                }

            }
            else
            {
                SetEnemyState(EnemyState.MoveToBuilding);
            }
        }
    }

    #region SettingEnemyState
    void SetEnemyState(EnemyState state)
    {
        CurrentEnemyState = state;
        if (CurrentEnemyState == EnemyState.Idle)
        {

        }
        if (CurrentEnemyState == EnemyState.MoveToBuilding)
        {
            FindNearestBulding();
            if (TargetBuilding)
            {
                navMeshAgent.SetDestination(TargetBuilding.transform.position);
            }
            else
            {
                CurrentEnemyState = EnemyState.Idle;
            }
        }
        if (CurrentEnemyState == EnemyState.MoveToUnit)
        {
            navMeshAgent.SetDestination(TargetUnit.transform.position);
        }
        if (CurrentEnemyState == EnemyState.Attack)
        {
            _timer = 0;
        }

    }
    #endregion

    #region FindNearestBuilding
    public void FindNearestBulding()
    {
        float minDistance = Mathf.Infinity;

        for (int i = 0; i < buildingPlacer.amountOfBuildings.Count; i++)
        {
            Building currentBuildingToCheck= buildingPlacer.amountOfBuildings[i];

            float distance = Vector3.Distance(transform.position,currentBuildingToCheck.transform.position);
            if (distance<minDistance)
            {
                minDistance = distance;
                TargetBuilding = currentBuildingToCheck;
            }
        }

        return;
    }
    #endregion

    #region FindNearestUnit
    public void FindNearestUnit()
    {
        float minDistance = Mathf.Infinity;


        for (int i = 0; i < unitManagement.currentUnitsInScene.Count; i++)
        {
            Unit unitToFollow = unitManagement.currentUnitsInScene[i];

            float distance = Vector3.Distance(transform.position, unitToFollow.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                TargetUnit = unitToFollow;
                
            }
            if (DistanceToFollow > minDistance)
            {
                SetEnemyState(EnemyState.MoveToUnit);
            }
        }  
    }
    #endregion

    public void TakeDamage(int damageValue)
    {
        Health -= damageValue;
        healthBar.SetHealth(Health, maxHealth);
        if (Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(healthBar.gameObject);
        enemyManager.RemoveEnemy(this);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position,Vector3.up,DistanceToAttack);

        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.up, DistanceToFollow);
    }
}
