using UnityEngine;
using UnityEngine.AI;

public class Footsteps : MonoBehaviour
{
    //jebote isus
    public AudioSource footstepSound;
    public bool playerIsMoving = false;
    private NavMeshAgent agent;

    private void Start()
    {
        footstepSound = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (agent.velocity.magnitude > 0.1f)
        {
            playerIsMoving = true;
            PlayFootstepSound();
        }
        else
        {
            playerIsMoving = false;
            footstepSound.Stop();
        }
    }

    private void PlayFootstepSound()
    {
        if (!footstepSound.isPlaying)
        {
            footstepSound.Play();
        }
    }
}