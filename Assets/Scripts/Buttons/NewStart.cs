using UnityEngine;
using UnityEngine.SceneManagement;

public class NewStart : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene(1);
    }
}
