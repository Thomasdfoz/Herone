using UnityEngine;

public class Direction : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem pSystem;

    private void Start()
    {
      
    }
    // Update is called once per frame
    void Update()
    {
        if (pSystem.isStopped)
        {
            Destroy(gameObject);
        }

    }
}
