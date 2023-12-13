using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public int maxAnimalCount = 25;
    public float spawnInterval = 7f;

    private void Start()
    {
        InvokeRepeating("SpawnAnimal", 0f, spawnInterval);
    }

    private void SpawnAnimal()
    {
        if (GameObject.FindGameObjectsWithTag("Animal").Length < maxAnimalCount)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-1000f, 1000f), 5f, Random.Range(-1000f, 1000f)); // Updated y to 5
            int randomIndex = Random.Range(0, animalPrefabs.Length);
            spawnPosition.y = Terrain.activeTerrain.SampleHeight(spawnPosition) + 5;

            GameObject newAnimal = Instantiate(animalPrefabs[randomIndex], spawnPosition, Quaternion.identity);
            newAnimal.tag = "Animal";

            // Optional: Set a parent object to keep the scene hierarchy organized
            newAnimal.transform.SetParent(transform);

            // Disable collision with the environment
            Collider animalCollider = newAnimal.GetComponent<Collider>();
            if (animalCollider != null)
            {
                Physics.IgnoreCollision(animalCollider, GetComponent<Collider>());
            }

            InitializeMeshCollider(newAnimal);
        }
    }

    private void InitializeMeshCollider(GameObject animal)
    {
        MeshCollider meshCollider = animal.GetComponent<MeshCollider>();
        if (meshCollider == null)
        {
            // If there is no MeshCollider, add one
            Debug.LogWarning("MeshCollider not found. Adding MeshCollider component.");
            animal.AddComponent<MeshCollider>();
        }
    }
}
