using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public InputField playerInput;
    public static string input;
    public Text playerName;

    public void PlayerName()
    {
        input = playerInput.text;
        playerName.text = input;
    }

    public void DisplayName()
    {
        playerName.text = input;
    }

    public void StartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
