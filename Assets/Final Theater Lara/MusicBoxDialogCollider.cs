using UnityEngine;
using TMPro;

public class MusicBoxDialogCollider : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public GameObject dialogBox;
    public string textToShow = "PLAYER :  WHERE IS THIS MUSIC COMING FROM ?  IT FEELS AS IF IT'S ECHOING THROUGH THESE WALLS.  I SHOULD KEEP MY EYES OPEN AND FIND THE SOURCE .  IT MIGHT BE A CLUE TO UNRAVELING THE MYSTERIES OF THIS STRANGE PLACE.";

    private bool isPlayerInside = false;
    private float dialogDuration = 7f;
    private float dialogTimer = 0f;

    private void Update()
{
    if (isPlayerInside || dialogTimer > 0f)
    {
        if (dialogBox != null) 
        {
            dialogBox.SetActive(true);
            dialogText.text = textToShow;

            if (dialogTimer > 0f)
            {
                dialogTimer -= Time.deltaTime;
                if (dialogTimer <= 0f)
                {
                    if (dialogBox != null) 
                    {
                        Destroy(dialogBox); 
                    }
                }
            }
        }
    }
    else
    {
        if (dialogBox != null) 
        {
            dialogBox.SetActive(false);
        }
    }
}


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            dialogTimer = dialogDuration;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
}
