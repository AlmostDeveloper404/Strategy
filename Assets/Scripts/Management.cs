using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public enum SelectionState
{
    UnitsSelected,
    Frame,
    Other
}

public class Management : MonoBehaviour
{
    public Camera Camera;
    public SelectableObject CurrentSelected;
    public List<SelectableObject> ListOfSelected;

    public Image FrameImage;

    Vector2 startPoint;
    Vector2 endPoint;

    public SelectionState CurrentSelectionState;

    private void Update()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.white);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.GetComponent<SelectableCollider>())
            {
                SelectableObject currentSelectableObject = hitInfo.collider.GetComponent<SelectableCollider>().SelectableObject;
                if (CurrentSelected)
                {
                    if (CurrentSelected != currentSelectableObject)
                    {
                        CurrentSelected.OnUnhover();
                        CurrentSelected = currentSelectableObject;
                        CurrentSelected.OnHover();
                    }
                }
                else
                {
                    CurrentSelected = currentSelectableObject;
                    CurrentSelected.OnHover();
                }
            }
            else
            {
                UnhoverSelectableObject();
            }
        }
        else
        {
            UnhoverSelectableObject();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (CurrentSelected)
            {
                CurrentSelectionState = SelectionState.UnitsSelected;
                if (!Input.GetKey(KeyCode.LeftControl))
                {
                    DiselectAll();
                }
                Select(CurrentSelected);
            }
        }

        if (CurrentSelectionState==SelectionState.UnitsSelected)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (hitInfo.collider.tag == "Ground")
                {
                    for (int i = 0; i < ListOfSelected.Count; i++)
                    {
                        ListOfSelected[i].ClickOnGround(hitInfo.point);
                    }
                }
            }
        }
        

        
        if (Input.GetMouseButtonDown(1))
        {
            DiselectAll();
        }
        if (Input.GetMouseButtonDown(0))
        {
            FrameImage.enabled = true;
            startPoint = Input.mousePosition;

        }
        if (Input.GetMouseButton(0))
        {
            endPoint = Input.mousePosition;

            Vector2 min = Vector2.Min(startPoint, endPoint);
            Vector2 max = Vector2.Max(startPoint, endPoint);

            FrameImage.rectTransform.anchoredPosition = min;

            Vector2 size = max - min;

            FrameImage.rectTransform.sizeDelta = size;

            if (size.magnitude > 10)
            {

                DiselectAll();
                Rect rect = new Rect(min, size);

                Unit[] units = FindObjectsOfType<Unit>();
                for (int i = 0; i < units.Length; i++)
                {
                    Vector2 unitPosition = Camera.WorldToScreenPoint(units[i].transform.position);
                    if (rect.Contains(unitPosition))
                    {
                        Select(units[i]);
                    }
                }
                CurrentSelectionState = SelectionState.Frame;
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            FrameImage.enabled = false;
            if (ListOfSelected.Count>0)
            {
                CurrentSelectionState = SelectionState.UnitsSelected;
            }
            else
            {
                CurrentSelectionState = SelectionState.Other;
            }
        }

    }

    void DiselectAll()
    {
        for (int i = 0; i < ListOfSelected.Count; i++)
        {
            ListOfSelected[i].Diselect();
        }
        CurrentSelectionState = SelectionState.Other;
        ListOfSelected.Clear();
    }

    void Select(SelectableObject currentSelected)
    {
        if (!ListOfSelected.Contains(currentSelected))
        {
            ListOfSelected.Add(currentSelected);
            currentSelected.Select();
        }
    }

    void UnhoverSelectableObject()
    {
        if (CurrentSelected)
        {
            CurrentSelected.OnUnhover();
            CurrentSelected = null;
        }
    }
}
