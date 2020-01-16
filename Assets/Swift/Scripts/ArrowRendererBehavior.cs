using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRendererBehavior : MonoBehaviour
{

    public GameObject ObjectFrom;
    public GameObject ObjectTo;
    public float Distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<LineRenderer>().SetPosition(0, ObjectFrom.transform.position);
        gameObject.GetComponent<LineRenderer>().SetPosition(1, ObjectTo.transform.position);
        Distance = Vector3.Distance(ObjectFrom.transform.position, ObjectTo.transform.position);
    }
}
