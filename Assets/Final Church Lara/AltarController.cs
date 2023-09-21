using System.Collections;
using UnityEngine;
using TMPro;

public class AltarController : MonoBehaviour
{
    private Vector3 initialPosition;
    private float maxMoveDistanceY = 1.5f; 
    private float maxMoveDistanceZ = 1.5f; 
    private float moveSpeed = 0.8f;
    private bool isMoving = false;
    public MonsterAnimationAltar monsterAnimationAltar;
    private AudioSource audioSource;

    public TextMeshProUGUI dialogText;
    public GameObject dialogBox;

    private void Start()
    {
        initialPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();

        dialogBox.SetActive(false);
        dialogText.text = "";
    }

    private void Update()
    {
        if (isMoving)
        {
            Vector3 targetPosition = new Vector3(
                initialPosition.x,
                Mathf.Min(initialPosition.y + maxMoveDistanceY, transform.position.y + moveSpeed * Time.deltaTime),
                Mathf.Min(initialPosition.z + maxMoveDistanceZ, transform.position.z + moveSpeed * Time.deltaTime)
            );

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Mathf.Approximately(transform.position.y, initialPosition.y + maxMoveDistanceY) &&
                Mathf.Approximately(transform.position.z, initialPosition.z + maxMoveDistanceZ))
            {
                isMoving = false;
            }

            if (!isMoving && monsterAnimationAltar != null)
            {
                monsterAnimationAltar.StartMonsterAnimation();
                audioSource.Stop();

                StartCoroutine(DisplayJumpscareTextWithDelay(3f));
            }
        }
    }

    private void OnMouseDown()
    {
        if (!isMoving)
        {
            isMoving = true;
            audioSource.Play();
        }
    }

    private IEnumerator DisplayJumpscareTextWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        dialogBox.SetActive(true);
        dialogText.text = "WHAT WAS THAT?!?! THIS PLACE IS CREEPY. LET'S TAKE THAT KEY AND RUN FROM HERE !";
    }
}
