using UnityEngine;

public class Barack : Building
{
    public GameObject BarackCanvas;

    public override void Select()
    {
        base.Select();
        BarackCanvas.SetActive(true);
    }

    public override void Diselect()
    {
        base.Diselect();
        BarackCanvas.SetActive(false) ;
    }
}
