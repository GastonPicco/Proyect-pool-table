using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poolStick : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        GameManager.data.Stick = this.gameObject;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(GameManager.data.WhiteBall.transform.position.x + GameManager.data._RealStickPos.x, GameManager.data.WhiteBall.transform.position.y, GameManager.data.WhiteBall.transform.position.z + GameManager.data._RealStickPos.z);
        gameObject.transform.LookAt(GameManager.data.WhiteBall.transform.position);
    }
}
