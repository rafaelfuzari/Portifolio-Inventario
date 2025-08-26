using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject darkBackground;
    public GameObject inventoryPanel;
    public GameObject debugButtons;
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            darkBackground.SetActive(!darkBackground.activeInHierarchy);
            //inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
            //debugButtons.SetActive(!debugButtons.activeInHierarchy);
            
        }

        Time.timeScale = inventoryPanel.activeInHierarchy ? 0f : 1f;
    }

  
}
