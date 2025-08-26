using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject inventoryCanvas;
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
       //Codigo de pausa
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryCanvas.SetActive(!inventoryCanvas.activeInHierarchy);
          
            
        }

        Time.timeScale = inventoryCanvas.activeInHierarchy ? 0f : 1f;
    }

  
}
