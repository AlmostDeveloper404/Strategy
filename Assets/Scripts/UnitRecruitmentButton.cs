using UnityEngine;

public class UnitRecruitmentButton : MonoBehaviour
{
    public GameObject Pref;
    public Transform SpawnPoint;
    public Sprite WariorSprite;

    public Barack BarackBuilding;
    public void TryRecruit()
    {
        BarackBuilding.TryRecruit(Pref,WariorSprite);
    }
}
