using UnityEngine;

public class ExitButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Click()
    {
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
