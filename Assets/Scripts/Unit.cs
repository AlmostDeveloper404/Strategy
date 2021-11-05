using UnityEngine;
using UnityEngine.AI;

public class Unit : SelectableObject
{
    public NavMeshAgent NavMeshAgent;
    public int Price;
    public int Health;

    private int maxHealth;

    UnitManagement unitManagement;

    public GameObject HealthBar;
    HealthBar healthBar;
    public override void Start()
    {
        maxHealth = Health;
        base.Start();
        GameObject _healthBar= Instantiate(HealthBar);
        healthBar= _healthBar.GetComponent<HealthBar>();
        healthBar.Setup(transform);
        unitManagement = UnitManagement.instance;
    }
     
    public override void ClickOnGround(Vector3 point)
    {
        base.ClickOnGround(point);

        NavMeshAgent.SetDestination(point);
    }

    public void TakeDamage(int damageValue)
    {
        Health -= damageValue;
        healthBar.SetHealth(Health,maxHealth);
        if (Health<=0)
        {
            Die();
        }
    }

    void Die()
    {
        FindObjectOfType<Management>().Unselect(this);
        Destroy(healthBar.gameObject);
        unitManagement.RemoveUnitFromList(this);
        Destroy(gameObject);
    }
}
