using UnityEngine;

public class GenerateRocks : MonoBehaviour
{
    public GameObject RockPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Generate", 0f, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Generate()
    {
        Instantiate(RockPrefab, transform.position, transform.rotation);
        Rock.RockTrigger.Falling = true;
    }
}
