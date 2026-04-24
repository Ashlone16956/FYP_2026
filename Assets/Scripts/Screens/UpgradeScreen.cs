using UnityEngine;

public class UpgradeScreen : MonoBehaviour
{

    
    private bool isOpen = false;
    
    public void ToggleMenu()
    {

        isOpen = !isOpen;
        gameObject.SetActive(isOpen);

        GameManager.Instance.IsPaused = isOpen;
        Time.timeScale = isOpen ? 0f : 1f; //Pauses the gameplay when the menu is open.

    }

}
