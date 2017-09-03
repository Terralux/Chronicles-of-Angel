using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleClicker : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;

    public GameObject plingPrefab;
    private bool canClickParticles = true;

    private ParticleSystemRenderer psr;

	private List<Vector3> positions = new List<Vector3>();

	public float clickOffsetRange = 20f;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        Toolbox.FindRequiredComponent<GameMaster>().InitiateStory += BeginFade;
    }

    // Update is called once per frame
	void Update () {
	    if (canClickParticles)
	    {
	        if (Input.GetMouseButtonDown(0))
	        {
				positions.Clear ();

	            particles = new ParticleSystem.Particle[ps.main.maxParticles];
	            ps.GetParticles(particles);

	            int target = 0;
	            float minDistance = 10000f;

	            for (int i = 0; i < ps.particleCount; i++)
	            {
					Vector3 particlePos = Camera.main.WorldToScreenPoint(particles[i].position);

					particlePos = new Vector3 (particlePos.x, particlePos.y, Input.mousePosition.z);

					positions.Add (particlePos);

	                float distance = Vector3.Distance(particlePos, Input.mousePosition);

	                if (distance < minDistance)
	                {
	                    target = i;
	                    minDistance = distance;
	                }
	            }

				if (minDistance < clickOffsetRange) {
					particles [target].remainingLifetime = 0f;
					ps.SetParticles (particles, particles.Length);
					Destroy (Instantiate (plingPrefab, Camera.main.transform.position, Quaternion.identity), 5f);
				}
	        }
	    }
	    else
	    {
	        if (psr.material.GetColor("_TintColor").a > 0)
	        {
	            psr.material.SetColor("_TintColor", psr.material.GetColor("_TintColor") - new Color(0, 0, 0, 0.01f));
	        }
	        else
	        {
	            Destroy(gameObject);
	        }
	    }
	}

    public void BeginFade()
    {
        psr = GetComponent<ParticleSystemRenderer>();
        canClickParticles = false;
    }

	void OnDrawGizmos(){
		foreach (Vector3 v in positions) {
			Gizmos.DrawWireSphere (v, 0.1f);
		}
	}
}