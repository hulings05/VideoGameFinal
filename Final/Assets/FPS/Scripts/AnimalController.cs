using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float changeDestinationInterval = 5f;

    private float elapsedTimeSinceLastDestinationChange = 0f;

    private void Start()
    {
        InitializeRigidbody();
        SetRandomDestination();
    }

    private void Update()
    {
        elapsedTimeSinceLastDestinationChange += Time.deltaTime;

        // Change destination every specified interval
        if (elapsedTimeSinceLastDestinationChange >= changeDestinationInterval)
        {
            SetRandomDestination();
            elapsedTimeSinceLastDestinationChange = 0f;
        }

        // Move towards the destination
        MoveTowardsDestination();
    }

    private void InitializeRigidbody()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.useGravity = true; // Ensure gravity is enabled
            rigidbody.drag = 0.5f; // Adjust drag as needed
            rigidbody.angularDrag = 0.5f; // Adjust angular drag as needed
        }
        else
        {
            // If there is no Rigidbody attached, you might want to add one
            Debug.LogWarning("Rigidbody not found. Adding Rigidbody component.");
            gameObject.AddComponent<Rigidbody>();
        }
    }

    private void SetRandomDestination()
    {
        // Set the spawn position at a random (x, z) within a radius and y = 3
        Vector3 spawnPosition = new Vector3(Random.Range(-1000f, 1000f), 3f, Random.Range(-1000f, 1000f));
        transform.LookAt(spawnPosition); // Optional: Look at the destination
    }

    private void MoveTowardsDestination()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
