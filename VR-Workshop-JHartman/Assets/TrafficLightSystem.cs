using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightSystem : MonoBehaviour
{
    public Transform t1;
    public Transform t2;
    public Transform t3;

    public GameObject t1green;
    public GameObject t1red;
    public GameObject t2green;
    public GameObject t2red;
    public GameObject t3green;
    public GameObject t3red;

    public Collider t1rc;
    public Collider t2rc;
    public Collider t3rc;
    public Collider t1gc;
    public Collider t2gc;
    public Collider t3gc;

    public float stateTimer;
    public int state;

    // Start is called before the first frame update
    void Start()
    {
        t1 = transform.Find("TL1");
        t2 = transform.Find("TL2");
        t3 = transform.Find("TL3");

        t1green = t1.Find("Green light").gameObject;
        t1red = t1.Find("Red light").gameObject;
        t2green = t2.Find("Green light").gameObject;
        t2red = t2.Find("Red light").gameObject;
        t3green = t3.Find("Green light").gameObject;
        t3red = t3.Find("Red light").gameObject;

        GameObject t1redBox = t1.Find("Red Light Box").gameObject;
        GameObject t1greenBox = t1.Find("Green Light Box").gameObject;
        GameObject t2redBox = t2.Find("Red Light Box").gameObject;
        GameObject t2greenBox = t2.Find("Green Light Box").gameObject;
        GameObject t3redBox = t3.Find("Red Light Box").gameObject;
        GameObject t3greenBox = t3.Find("Green Light Box").gameObject;

        t1rc = t1redBox.GetComponent<Collider>();
        t2rc = t2redBox.GetComponent<Collider>();
        t3rc = t3redBox.GetComponent<Collider>();
        t1gc = t1greenBox.GetComponent<Collider>();
        t2gc = t2greenBox.GetComponent<Collider>();
        t3gc = t3greenBox.GetComponent<Collider>();

        stateTimer = 10.0f;
        SetState(1);
    }

    // Update is called once per frame
    void Update()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0.0f)
        {
            stateTimer = 10.0f;
            if(state == 1) SetState(2);
            else SetState(1);

        }
        else return;
    }

    void SetState(int c)
    {
        state = c;
        if (c == 1)
        {
            t1green.active = true;
            t1red.active = false;
            t1rc.enabled = false;
            t1gc.enabled = true;

            t2green.active = false;
            t2red.active = true;
            t2rc.enabled = true;
            t2gc.enabled = false;

            t3green.active = false;
            t3red.active = true;
            t3rc.enabled = true;
            t3gc.enabled = false;
        }
        else
        {
            t1green.active = false;
            t1red.active = true;
            t1rc.enabled = true;
            t1gc.enabled = false;

            t2green.active = true;
            t2red.active = false;
            t2rc.enabled = false;
            t2gc.enabled = true;

            t3green.active = true;
            t3red.active = false;
            t3rc.enabled = false;
            t3gc.enabled = true;
        }
    }
}
