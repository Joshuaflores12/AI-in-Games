using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    [SerializeField] Camera cam;
    [SerializeField] private float velocityZ, velocityX;
    [SerializeField] private float jumpDuration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        jumpDuration = 1f;
        agent.autoTraverseOffMeshLink = false;
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 location = new Vector3(hit.point.x, 0, hit.point.z);
                agent.SetDestination(location);
            }
        }

        if (agent.isOnOffMeshLink)
        {
            StartCoroutine(toJump(agent.currentOffMeshLinkData));
        }


        Vector3 velocity = agent.velocity;


        Vector3 agentVelocity = transform.InverseTransformDirection(velocity);


        velocityX = agentVelocity.x;
        velocityZ = agentVelocity.z;


        animator.SetFloat("Velocity Z", velocityZ);
        animator.SetFloat("Velocity X", velocityX);


        IEnumerator toJump(OffMeshLinkData linkData)
        {
            {
                agent.isStopped = true;
                animator.SetTrigger("Jump");

                Vector3 startPos = transform.position;
                Vector3 endPos = linkData.endPos;
                float elapsedTime = 0f;


                while (elapsedTime < jumpDuration)
                {
                    float t = elapsedTime / jumpDuration;
                    Vector3 horizontalPos = Vector3.Lerp(startPos, endPos, t);
                    float height = Mathf.Sin(t * Mathf.PI) * 1.0f; // tweak 1.0f as needed
                    transform.position = new Vector3(horizontalPos.x, horizontalPos.y + height, horizontalPos.z);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                transform.position = endPos;
                agent.CompleteOffMeshLink();
                agent.isStopped = false;
                animator.SetTrigger("DoNotJump");
            }
        }
    }
}
