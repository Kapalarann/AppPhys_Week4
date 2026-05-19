using UnityEngine;

public class SamplePhysicsOverlap : MonoBehaviour
{
    public float radius = 5f;
    public GameObject player;

    private void Start()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Debug.Log("Damaged enemy: " + hit.name);
                MeshRenderer mesh = hit.GetComponent<MeshRenderer>();
                mesh.material.color = Color.blueViolet;

                float dis = Vector3.Distance(hit.transform.position, player.transform.position);
                Debug.Log($"Distance of enemy {hit.name} to {player.name} is {dis}");
            }
        }
        Destroy(gameObject, 0.1f);
    }

    private void OnDrawGizmosSeleted()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
