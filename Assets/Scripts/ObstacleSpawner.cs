using UnityEngine;
using Unity.AI.Navigation;
public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private NavMeshSurface meshSurface;
    [SerializeField] private GameObject obstaclePrefab;

    [SerializeField] private int numberOfSpawn;
    [SerializeField] private Vector2 spawnPosition;
    [SerializeField] private Vector2 spawnSize;
    [SerializeField] private LayerMask floorMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < numberOfSpawn;) 
        {
            float xPos = Random.Range(spawnPosition.x, spawnPosition.y);
            float zPos = Random.Range(spawnPosition.x, spawnPosition.y);

            Vector3 newPos = new Vector3(xPos, transform.position.y, zPos);

            float xSize = Random.Range(spawnSize.x, spawnSize.y);
            float zSize = Random.Range(spawnSize.x, spawnSize.y);

            Vector3 newSize = new Vector3(xSize, 1, zSize);

            Ray ray = new Ray(newPos, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, floorMask)) 
            {
                Collider[] colliders = Physics.OverlapSphere(hit.point, SphereRadius(xSize, zSize));
                if(colliders.Length == 1) 
                {
                    Vector3 newObsPos = new Vector3(hit.point.x, 0.5f, hit.point.z);
                    GameObject newObstacle = Instantiate(obstaclePrefab, newObsPos, Quaternion.identity);
                    newObstacle.transform.localScale = new Vector3(xSize, 1, zSize);
                    i++;
                }
            }
        }
        meshSurface.BuildNavMesh();
    }

    private float SphereRadius(float xSize, float zSize) 
    {
        if(xSize > zSize)
            return xSize;

            return zSize;
    }
}
