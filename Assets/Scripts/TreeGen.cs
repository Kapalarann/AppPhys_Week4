using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TreeGen : MonoBehaviour
{
    public int max = 15;
    public float raycastLength = 10f;
    public float waterRadius = 3f;
    public float otherTreeRadius = 3f;
    public GameObject treePrefab;
    public LayerMask islandLayer;
    public LayerMask waterLayer;
    public LayerMask treeLayer;
    private int count = 0;
    private int maxAttempts = 10000;
    private int attempts = 0;

    public Vector3 range = new Vector3(10f, 0f, 10f);

    private void Start()
    {
        while (count < max && attempts<maxAttempts)
        {
            generateTree();
        }
    }

    void generateTree()
    {
        Vector3 pos = new Vector3(Random.Range(range.x, -range.x), 
            transform.position.y, 
            Random.Range(range.z, -range.z));

        RaycastHit hit;
        Collider[] cols = new Collider[1];
        if(Physics.Raycast(pos, Vector3.down, out hit, raycastLength, islandLayer))
        {
            if (Physics.OverlapSphereNonAlloc(hit.point, otherTreeRadius, cols, treeLayer) == 0 &&
                Physics.OverlapSphereNonAlloc(hit.point, waterRadius, cols, waterLayer) == 0)
            {
                Instantiate(treePrefab, hit.point, Quaternion.identity);
                count++;
            }
        }
        attempts++;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, range);
    }
}
