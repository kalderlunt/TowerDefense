using UnityEngine;

public class BoidSystem : MonoBehaviour
{
    public Transform boidPrefab;
    public int numberOf;

    [SerializeField] BoidSettings settings;

    Boid[] boids;

    private void Start()
    {
        boids = new Boid[numberOf];
        for (int i = 0; i < numberOf; i++)
        {
            boids[i] = new Boid { transform = Instantiate(boidPrefab, transform), velocity = Random.onUnitSphere };
        }
    }

    private void Update()
    {
        ComputeNextVelocities();
        ApplyNextVelocities();
    }

    private void ComputeNextVelocities()
    {
        for (int i = 0; i < boids.Length; i++)
        {
            boids[i].ComputeNextVelocity(boids, settings);
        }
    }

    private void ApplyNextVelocities()
    {
        for (int i = 0; i < boids.Length; i++)
        {
            boids[i].ApplyNextVelocity(boids, settings);
        }
    }

    struct Boid
    {
        public Transform transform;
        public Vector3 velocity;
        public Vector3 nextVelocity;

        public void ComputeNextVelocity(Boid[] boids, BoidSettings settings)
        {

            Vector3 alignement = Vector3.zero;
            Vector3 avoidance = Vector3.zero;
            Vector3 cohesion = Vector3.zero;

            for (int i = 0; i < boids.Length; i++)
            {
                if (boids[i].transform == transform) 
                    continue;

                //alignement
                alignement += boids[i].velocity;

                //avoidance
                Vector3 direction = transform.position - boids[i].transform.position;
                float distance = direction.magnitude / settings.farThreshold;
                
                avoidance += direction.normalized * (1 - distance);

                //cohesion
                direction *= -1;
                
                if (distance > settings.farThreshold)
                {
                    cohesion += Vector3.ClampMagnitude( direction.normalized * (distance - 1), 1);
                }
            }

            nextVelocity = alignement * settings.alignment + avoidance * settings.avoidance + cohesion * settings.cohesion;
            nextVelocity.Normalize();
        }

        public void ApplyNextVelocity(Boid[] boids, BoidSettings settings)
        {
            velocity = Vector3.Slerp(velocity, nextVelocity, settings.turnRate * Time.deltaTime);
            transform.position += velocity * settings.speed * Time.deltaTime;
        }

    }

    [System.Serializable]
    class BoidSettings
    {
        public float alignment;
        public float avoidance;
        public float cohesion;
        public float attraction;
        public float farThreshold;
        public float speed;
        public float turnRate;
    }
}