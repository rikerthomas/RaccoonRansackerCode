using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RetryGame : MonoBehaviour
{
    public Button button;
    public Button button2;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        button.onClick.AddListener(QuitGame);
        button2.onClick.AddListener(StartGame);
    }
    void Update()
    {
        button = GetComponent<Button>();
        button2 = GetComponent<Button>();
    }

    void QuitGame()
    {
        Debug.Log("asiodfh");
        Application.Quit();
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}