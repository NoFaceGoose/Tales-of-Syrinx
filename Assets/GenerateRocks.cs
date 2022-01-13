using UnityEngine;

public class GenerateRocks : MonoBehaviour
{
    public GameObject RockPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("Generate", 5f, 0.1f);
    }

    void Generate()
    {
        Instantiate(RockPrefab, transform.position, transform.rotation);
        Rock.RockTrigger.Falling = true;
    }
}
