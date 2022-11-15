using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRoute : MonoBehaviour
{
    public List<Transform> wps;
    public List<Transform> route;
    public Rigidbody rb;
    public int routeNumber = 0;
    public int targetWP = 0;
    public bool stop = false;
    public bool go = false;
    public bool started = false;
    public float initialDelay;
    public int collisions = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        wps = new List<Transform>();
        GameObject wp;

        wp = GameObject.Find("CWP1");
        wps.Add(wp.transform);
        wp = GameObject.Find("CWP2");
        wps.Add(wp.transform);
        wp = GameObject.Find("CWP3");
        wps.Add(wp.transform);
        wp = GameObject.Find("CWP4");
        wps.Add(wp.transform);

        initialDelay = Random.Range(2.0f, 12.0f);
        transform.position = new Vector3(0.0f, -5.0f, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!go)
        {
            initialDelay -= Time.deltaTime;
            if (initialDelay <= 0.0f)
            {
                go = true;
                SetRoute();
            }
            else return;
        }

        Vector3 displacement = route[targetWP].position - transform.position;
        displacement.y = 0;
        float dist = displacement.magnitude;

        if (dist < 0.6f)
        {
            go = false;
            initialDelay = Random.Range(2.0f, 12.0f);
            transform.position = new Vector3(0.0f, -5.0f, 0.0f);
            return;
        }

        if(collisions == 0 && !stop)
        {
            //calculate velocity for this frame
            Vector3 velocity = displacement;
            velocity.Normalize();
            velocity *= 30;

            //apply velocity
            Vector3 newPosition = transform.position;
            newPosition += velocity * Time.deltaTime;
            rb.MovePosition(newPosition);

            //align to velocity
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity, 180.0f * Time.deltaTime, 0f);
            Quaternion rotation = Quaternion.LookRotation(desiredForward);
            rb.MoveRotation(rotation);
        }
    }

    void SetRoute()
    {
        //randomise the next route
        routeNumber = Random.Range(0, 2);
        //set the route waypoints
        if (routeNumber == 0) route = new List<Transform> { wps[0], wps[1] };
        else if (routeNumber == 1) route = new List<Transform> { wps[3], wps[2] };

        //initialise position and waypoint counter
        if (!Physics.CheckSphere(route[0].position, 4))
        {
            transform.position = new Vector3(route[0].position.x, 0.55f, route[0].position.z);
            targetWP = 1;
        }
        else SetRoute();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pedestrian") || other.gameObject.CompareTag("Car"))
        {
            collisions++;
        }
        else if (other.gameObject.CompareTag("RedLight"))
        {
            stop = true;
        }
        else if(other.gameObject.CompareTag("GreenLight"))
        {
            stop = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Pedestrian") || other.gameObject.CompareTag("Car"))
        {
            collisions--;
        }
    }
}
