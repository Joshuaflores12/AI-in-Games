using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) 
            {
                Vector3 location = new Vector3(hit.point.x, 0, hit.point.z);
                agent.SetDestination(location);
            }
        }
    }
}
