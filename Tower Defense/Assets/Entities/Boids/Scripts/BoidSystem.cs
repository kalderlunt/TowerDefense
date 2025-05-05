using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Random = UnityEngine.Random;

public class BoidSystem : MonoBehaviour
{
	public Transform attractor;
	public Transform boidPrefab;
	public int numberOf;

	[SerializeField]
	BoidSettings settings;

	Boid[] boids;
	BoidRegions regions = new BoidRegions();

	static int[] nearCoords = new int[] { 0, -1, 1 };

	void Start()
	{
		boids = new Boid[numberOf];
		regions.Add(Vector3Int.zero, new List<Boid>());

		for (int i = 0; i < numberOf; i++)
		{
			boids[i] = new Boid { transform = Instantiate(boidPrefab, transform), velocity = Random.onUnitSphere };
			regions[Vector3Int.zero].Add(boids[i]);
		}
	}

	void Update()
	{
		ComputeNextVelocities();
		ApplyNextVelocities();
	}

	void ComputeNextVelocities()
	{
		Vector3 attractorPosition = attractor? attractor.position : Vector3.zero;

		Parallel.For(0, boids.Length, i =>
		{
			boids[i].ComputeNextVelocity(settings, regions, attractorPosition);
		});
	}

	void ApplyNextVelocities()
	{
		for (int i = 0; i < boids.Length; i++)
		{
			boids[i].ApplyNextVelocity(settings, regions);
		}
	}

	struct Boid
	{
		public Transform transform;
		public Vector3 position;
		public Vector3 velocity;
		public Vector3 nextVelocity;

		Vector3Int region;

		public void ComputeNextVelocity(BoidSettings settings, BoidRegions regions, Vector3 attractorPosition)
		{
			Vector3 alignement = Vector3.zero;
			Vector3 avoidance = Vector3.zero;
			Vector3 cohesion = Vector3.zero;

			int counter = 0;

			for (int x = 0; x < 3; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					for (int z = 0; z < 3; z++)
					{
						Vector3Int testedRegion = region + new Vector3Int(nearCoords[x], nearCoords[y], nearCoords[z]);

						if (!regions.ContainsKey(testedRegion))
							continue;

						foreach (Boid b in regions[testedRegion])
						{
							if (b.transform == transform)
								continue;

							//alignement
							Vector3 direction = b.velocity;
							float distance = Vector3.Distance(position, b.position);
							alignement += Vector3.ClampMagnitude(direction / Mathf.Max(distance, 0.01f), 1);

							//avoidance
							direction = position - b.position;
							distance = direction.magnitude / settings.farThreshold;
							avoidance += direction.normalized * (1 - distance);

							//cohesion
							direction *= -1;
							if (distance > settings.farThreshold)
								cohesion += Vector3.ClampMagnitude(direction.normalized * (distance - 1), 1);

							if (counter++ > settings.maxIterations)
								break;
						}
					}
				}
			}

			nextVelocity = alignement * settings.alignement +
				avoidance * settings.avoidance +
				cohesion * settings.cohesion;

			nextVelocity.Normalize();

			nextVelocity += (attractorPosition - position).normalized * settings.attraction;
			nextVelocity.Normalize();
		}

		public void ApplyNextVelocity(BoidSettings settings, BoidRegions regions)
		{
			velocity = Vector3.Slerp(velocity, nextVelocity, settings.turnRate);
			position = transform.position += settings.speed * Time.deltaTime * velocity;
			transform.forward = velocity;

			Vector3Int newRegion = new Vector3Int
			{
				x = Mathf.FloorToInt(transform.position.x / settings.farThreshold),
				y = Mathf.FloorToInt(transform.position.y / settings.farThreshold),
				z = Mathf.FloorToInt(transform.position.z / settings.farThreshold),
			};

			if (newRegion == region)
				return;

			regions[region].Remove(this);

			if (!regions.ContainsKey(newRegion))
				regions[newRegion] = new List<Boid>();

			regions[newRegion].Add(this);

			region = newRegion;
		}
	}

	[Serializable]
	class BoidSettings
	{
		public float alignement;
		public float avoidance;
		public float cohesion;
		public float attraction;
		public float farThreshold;
		public float speed;
		public float turnRate;
		public int maxIterations;
	}

	class BoidRegions : Dictionary<Vector3Int, List<Boid>> { }
}
