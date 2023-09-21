using System.Collections;
using UnityEngine;

public class PawPrintsManager : MonoBehaviour
{
    public GameObject pawPrintPrefab;
    public Transform[] pawPrintWaypoints;
    public float spawnDelay = 1.0f;
    private bool spawningStarted = false;

    public void StartSpawning()
    {
        if (!spawningStarted)
        {
            StartCoroutine(SpawnPawPrints());
            spawningStarted = true;
        }
    }

    IEnumerator SpawnPawPrints()
    {
        foreach (Transform waypoint in pawPrintWaypoints)
        {
            Instantiate(pawPrintPrefab, waypoint.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }

        // When all paw prints are spawned, you can open the forest entrance, etc.
    }
}
