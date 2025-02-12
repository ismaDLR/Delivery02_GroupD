using UnityEngine;
using UnityEngine.SceneManagement;

public class InputAction : MonoBehaviour
{
    private Scene scene;
    private SoundManager soundManager;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        soundManager = FindAnyObjectByType<SoundManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnPressEnter()
    {
        if (scene.name != "Gameplay")
        {
            soundManager.SeleccionAudio(0, 0.9f);
            SceneManager.LoadScene("Gameplay");
        }
    }

    private void OnPressEscape()
    {
        Application.Quit();
    }
}
