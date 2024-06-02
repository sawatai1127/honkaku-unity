using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;


    [SerializeField] private Button itemsButton;
    [SerializeField] private Button recipeButton;

    [SerializeField] private ItemsDialog itemsDialog;

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);

        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(Resume);
        itemsButton.onClick.AddListener(ToggleItemsDialog);
        recipeButton.onClick.AddListener(ToggleRecipeDialog);
    }

    private void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }


    private void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);

    }


    private void ToggleItemsDialog()
    {
        itemsDialog.Toggle();
    }


    private void ToggleRecipeDialog()
    {
        // TODO
    }
}
