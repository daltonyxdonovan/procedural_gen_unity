using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block_script : MonoBehaviour
{
    public GameObject block;
    public GameObject cube; 
    public string block_name;
    public int block_id;
    public int block_health;
    public Color block_color;
    // Start is called before the first frame update
    void Start()
    {
        block_name = "block";
        block_id = 1;
        block_health = 100;
        //make block color a random color
        block_color = new Color(Random.value, Random.value, Random.value, 1.0f);

    }

    // Update is called once per frame
    void Update()
    {
        //set block color
        cube.GetComponent<Renderer>().material.color = block_color;
    }
}
