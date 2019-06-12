using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camera_Control : MonoBehaviour
{

    public GameObject Target;
    public Transform leftBounds;
    //public Transform rightBounds;

    //public float smoothDampTime = 0.15f;
    //private float smoothDampVelocity = Vector3.zero;

    //private float camWidth, camHeight, levelMinx, levelMaxX;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        transform.position = new Vector3(Target.transform.position.x, transform.position.y,transform.position.z);
       
    }
}
