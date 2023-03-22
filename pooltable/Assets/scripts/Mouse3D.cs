using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse3D : MonoBehaviour
{
    Ray ScreenRay;
    [SerializeField] Camera mainCamera;
    RaycastHit Hit;
    [SerializeField] private LayerMask TableMask;
    Vector3 HitPoint;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _Mouse3D();
    }
    public void _Mouse3D()
    {
        if (GameManager.data.GameOn)
        {
            ScreenRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ScreenRay, out Hit, 400, TableMask))
            {
                HitPoint = Hit.point;
            }
            else
            {
                HitPoint = new Vector3(0, 0, 0);
            }
            GameManager.data.stickPosition = HitPoint;
            Debug.DrawRay(HitPoint, Vector3.up, Color.black);
        }
        
    }
    
    

}
