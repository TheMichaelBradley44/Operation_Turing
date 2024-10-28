using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;
    
    private void Update()
    {
        if (TryHandleUnitSelection()) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection()) return;
                selectedUnit.Move(mouseWorld.GetPosition());
        }
    }
    private bool TryHandleUnitSelection ()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
        {
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    selectedUnit = unit;
                return true;
                }
        }
        return false;
    }
}
