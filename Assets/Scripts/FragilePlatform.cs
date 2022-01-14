using UnityEngine;
using System.Collections;

public class FragilePlatform : MonoBehaviour
{
    public Vector3 shakeRate = new Vector3(0.05f, 0.05f, 0.05f);
    public float shakeTime = 1f;
    public float shakeDertaTime = 0.05f;
    public float returnTime = 3f;

    public void Shake()
    {
        StartCoroutine(Shake_Coroutine());
    }

    public IEnumerator Shake_Coroutine()
    {
        var oriPosition = gameObject.transform.position;
        for (float i = 0; i < shakeTime; i += shakeDertaTime)
        {
            gameObject.transform.position = oriPosition +
                Random.Range(-shakeRate.x, shakeRate.x) * Vector3.right +
                Random.Range(-shakeRate.y, shakeRate.y) * Vector3.up +
                Random.Range(-shakeRate.z, shakeRate.z) * Vector3.forward;
            yield return new WaitForSeconds(shakeDertaTime);
        }
        gameObject.transform.position = oriPosition;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.transform.position.y > gameObject.transform.position.y)
        {
            Shake();
            Invoke("Destroy", shakeTime);
        }
    }

    void Destroy()
    {
        gameObject.SetActive(false);
        Invoke("Return", returnTime);
    }

    void Return()
    {
        gameObject.SetActive(true);
    }
}
