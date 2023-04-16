using UnityEngine;
using UnityEngine.UI;

public class DisplayName : MonoBehaviour
{
    public Text displayName;
    void Start()
    {
        displayName.text = MainMenu.input;
    }

    void Update()
    {

    }
}