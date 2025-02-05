using UnityEngine;

public class MovableObstacle : MonoBehaviour
{
    [SerializeField] private float speedForward;
    //[SerializeField] private float speedBackward;

    private bool isSwitchDir;
    
    void Update()
    {
        if (!isSwitchDir) 
        {
            transform.Translate(Vector3.forward * speedForward * Time.deltaTime);
        }

        else 
        {
            transform.Translate(Vector3.back * speedForward * Time.deltaTime);
        }

        if (transform.position.z >= 8) 
        {
            isSwitchDir = true;
        }
        else if (transform.position.z <= -8) 
        {
            isSwitchDir= false;
        }
    }
}
