using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseScreen : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Win()
    {
        animator.SetTrigger("Win");
    }

    public void Lose()
    {
        print("Lose");
        animator.SetTrigger("Lose");
    }

    public void OnLoseEnd()
    {
        print("On Lose end");
        SceneManager.LoadScene(0); //main menu
    }

    public void OnWinEnd()
    {
        GameManager.LoadNextSceneTrigger();
        this.gameObject.SetActive(false);
    }
}
