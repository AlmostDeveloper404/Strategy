using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Building
{
    float _timer;
    public float TimeToAddMoney;

    Resources resources;

    public override void Start()
    {
        base.Start();
        resources = Resources.instance;
        resources.AddMoney();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer>TimeToAddMoney)
        {
            _timer = 0;
            AddMoney();
        }
    }

    void AddMoney()
    {
        resources.AddMoney();
    }
}
