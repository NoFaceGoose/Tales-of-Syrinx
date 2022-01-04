using UnityEngine;

public class Key : MonoBehaviour
{
    public float radian = 0;
    public float perRadian = 0.1f;
    public float radius = 0.1f;
    private Vector3 _originalPosition;

    void Start()
    {
        _originalPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        radian += perRadian;
        float dy = Mathf.Cos(radian) * radius;
        transform.position = _originalPosition + new Vector3(0, dy, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.PlayerInstance.SetKeys();
            Destroy(gameObject);
        }
    }
}
