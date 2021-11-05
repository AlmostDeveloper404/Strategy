using UnityEngine;
using UnityEditor;


public enum UnitState
{
    Idle,
    MoveToPoint,
    MoveToEnemy,
    Attack
}
public class Knight : Unit
{
    public UnitState CurrentUnitState;

    [SerializeField] float DistanceToAttack;
    [SerializeField] float DistanceToFollow;

    public Vector3 TargetPoint;
    public Enemy TargetEnemy;

    EnemyManager enemyManager;

    [SerializeField] float attackRate;
    float _timer;

    public override void Start()
    {
        base.Start();
        enemyManager = EnemyManager.instance;
        SetUnitState(UnitState.MoveToPoint);
    }

    private void Update()
    {
        if (CurrentUnitState == UnitState.Idle)
        {
            FindNearestEnemy();
        }
        if (CurrentUnitState == UnitState.MoveToPoint)
        {
            FindNearestEnemy();
        }
        if (CurrentUnitState == UnitState.MoveToEnemy)
        {
            FindNearestEnemy();
            if (TargetEnemy)
            {
                float distance = Vector3.Distance(transform.position, TargetEnemy.transform.position);
                if (distance > DistanceToFollow)
                {
                    SetUnitState(UnitState.MoveToPoint);
                }
                if (distance < DistanceToAttack)
                {
                    SetUnitState(UnitState.Attack);
                }
            }
            else
            {
                SetUnitState(UnitState.MoveToPoint);
            }
        }
        if (CurrentUnitState == UnitState.Attack)
        {
            if (TargetEnemy)
            {
                NavMeshAgent.SetDestination(TargetEnemy.transform.position);
                float distance = Vector3.Distance(transform.position, TargetEnemy.transform.position);
                if (distance > DistanceToAttack)
                {
                    SetUnitState(UnitState.MoveToEnemy);
                }
                _timer += Time.deltaTime;
                if (_timer > attackRate)
                {
                    _timer = 0;
                    TargetEnemy.TakeDamage(1);
                }

            }
            else
            {
                SetUnitState(UnitState.MoveToPoint);
            }
        }
    }

    void SetUnitState(UnitState state)
    {
        CurrentUnitState = state;
        if (CurrentUnitState == UnitState.Idle)
        {

        }
        if (CurrentUnitState == UnitState.MoveToPoint)
        {

        }
        if (CurrentUnitState == UnitState.MoveToEnemy)
        {
            NavMeshAgent.SetDestination(TargetEnemy.transform.position);
        }
        if (CurrentUnitState == UnitState.Attack)
        {
            _timer = 0;
        }

    }

    public void FindNearestEnemy()
    {
        float minDistance = Mathf.Infinity;


        for (int i = 0; i < enemyManager.allEnemies.Count; i++)
        {
            Enemy unitToFollow = enemyManager.allEnemies[i];

            float distance = Vector3.Distance(transform.position, unitToFollow.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                TargetEnemy = unitToFollow;

            }
            if (DistanceToFollow > minDistance)
            {
                SetUnitState(UnitState.MoveToEnemy);
            }
        }



    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, DistanceToAttack);

        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.up, DistanceToFollow);
    }
}

