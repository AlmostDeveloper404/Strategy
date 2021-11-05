using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform ScaleTransform;
    public Transform Target;

    Transform mainCam;

    private void Start()
    {
        mainCam = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.position = Target.position + Vector3.up * 2f;
        transform.rotation = mainCam.rotation;
    }

    public void Setup(Transform target)
    {
        Target = target;
    }

    public void SetHealth(int currentHealth, int maxHealth)
    {
        float xScale = (float)currentHealth / maxHealth;
        xScale = Mathf.Clamp01(xScale);
        ScaleTransform.localScale = new Vector3(xScale,1f,1f);
    }
}
