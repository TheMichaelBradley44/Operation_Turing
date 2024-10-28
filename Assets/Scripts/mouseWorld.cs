using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseWorld : MonoBehaviour
{
    private static mouseWorld instance;
    [SerializeField] private LayerMask mousePlaneLayerMask;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        transform.position = mouseWorld.GetPosition();
    }

    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayerMask);
        return raycastHit.point;
    }
}