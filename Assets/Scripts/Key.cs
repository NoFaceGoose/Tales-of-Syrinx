using UnityEngine;

public class Key : MonoBehaviour
{
    public float TumbleSpeed;

    private bool _keyB = false;
    private int _keyIndex = 0;
    private Vector3 _offset;
    private Transform _playerTrans;
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.angularVelocity = Random.insideUnitSphere * TumbleSpeed;
        _playerTrans = GameObject.FindWithTag("Player").transform;
        _offset = new Vector3(-0.6f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_keyB == false)
        {
            if (_rigidbody.angularVelocity.z < 0.5)
            {
                _rigidbody.angularVelocity = Random.insideUnitSphere * TumbleSpeed;
            }
        }
        else
        {
            if (_playerTrans != null)
            {
                switch (_keyIndex)
                {
                    case 1:
                        transform.position = _playerTrans.position + _offset; break;
                    case 2:
                        transform.position = _playerTrans.position + _offset * 2; break;
                    case 3:
                        transform.position = _playerTrans.position + _offset * 3; break;
                    default: break;
                }

            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _keyB = true;
            _keyIndex = PlayerController.PlayerInstance.SetKeys();
        }
    }
}
