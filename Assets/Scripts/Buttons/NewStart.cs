using UnityEngine;
using UnityEngine.SceneManagement;

public class NewStart : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(1);
    }
}
