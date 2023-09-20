using System.Collections;
using UnityEngine;

public class MonsterAnimationAltar : MonoBehaviour
{
    public GameObject monsterFace;
    public float animationSpeed = 0.5f;
    public float maxScale = 5.0f;
    public float fullViewDuration = 0.5f; 
    public AudioClip screamSound;

    private bool isTriggered = false;
    private Collider triggerCollider; 
    private AudioSource audioSource;

    public void StartMonsterAnimation()
    {
        if (!isTriggered)
        {
            isTriggered = true;
            monsterFace.SetActive(true);

            if (screamSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(screamSound);
            }

            StartCoroutine(AnimateMonsterFace());

            triggerCollider.enabled = false;
            Destroy(triggerCollider, 2.0f);
        }
    }

    private void Start()
    {
        triggerCollider = GetComponent<Collider>(); 
        audioSource = monsterFace.GetComponent<AudioSource>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.CompareTag("Player"))
        {
            isTriggered = true;
            monsterFace.SetActive(true);

            if (screamSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(screamSound);
            }

            StartCoroutine(AnimateMonsterFace());

            triggerCollider.enabled = false;
            Destroy(triggerCollider, 2.0f); 
        }
    }

    private IEnumerator AnimateMonsterFace()
    {
        Vector3 initialScale = monsterFace.transform.localScale;

        monsterFace.transform.localScale = Vector3.one * 0.1f;

        while (monsterFace.transform.localScale.x < maxScale)
        {
            monsterFace.transform.localScale += Vector3.one * Time.deltaTime * animationSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(fullViewDuration);

        monsterFace.SetActive(false);
        monsterFace.transform.localScale = initialScale;

        isTriggered = false;
    }
}
