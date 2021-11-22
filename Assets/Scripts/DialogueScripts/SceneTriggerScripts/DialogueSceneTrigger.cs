using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSceneTrigger : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    [SerializeField] private bool shouldShowUpdatables;
    [SerializeField] private GameObject[] updatables;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] objectsToTeleport;
    [SerializeField] private Text objectiveText;
    [TextArea(3,1)]
    public string objectiveString;
    private Vector3 savedPosition = Vector3.zero;

    void OnEnable()
    {
        ShowUpdatables();
    }

    void Start()
    {
        ShowUpdatables();
        if(enemies.Length > 0)
            enemies[0].SetActive(true);
    }
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            int i = 0;
            foreach(GameObject objectToTeleport in objectsToTeleport)
            {
                objectToTeleport.GetComponentInChildren<CharacterController>().enabled = false;
                objectToTeleport.transform.position = transform.position + new Vector3(0.0f, 0.0f, i * 5.0f);
                foreach(Transform child in objectToTeleport.transform)
                {
                    child.localPosition = Vector3.zero;
                }
                objectToTeleport.GetComponentInChildren<CharacterController>().enabled = true;
                i++;
            }
            DialogueDirector.IsShowingDialogue = true;
            if(sceneIndex != -1)
            {
                objectiveText.text = objectiveString;
                FindObjectOfType<DialogueManager>().LoadDialogue(sceneIndex);
            }
            Destroy(this.gameObject);
        }
    }
    private void ShowUpdatables()
    {
        foreach(GameObject updatable in updatables)
        {
            updatable.SetActive(shouldShowUpdatables);
        }
    }
}
