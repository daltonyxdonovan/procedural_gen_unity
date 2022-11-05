using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using scenemanagement
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class game_manager_script : MonoBehaviour
{
    public GameObject room;
    public GameObject wall;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject tree;
    public GameObject player;
    public GameObject rock;
    public int[,] rooms_array = new int[10,10];
    public GameObject[] tree_spawn_points;
    public int cycles = 5;
    public int tree_spawn_number = 50;
    public bool is_generating = true;
    public int[] spawns_used;
    public bool is_paused = false;
    public GameObject start_panel;


   void Start()
   {
        // Set the target frame rate to 60
        Application.targetFrameRate = 60;
        
        
        rooms_array[5, 5] = 1;

        for (int i = 0; i < cycles; i++)
        {
            //choose a random room with a value of 0 that is bordering a room with a value of 1
            int x = Random.Range(0, 10);
            int y = Random.Range(0, 10);
            if (rooms_array[x, y] == 0)
            {
                if (x > 0 && rooms_array[x - 1, y] == 1)
                {
                    rooms_array[x, y] = 1;
                }
                else if (x < 9 && rooms_array[x + 1, y] == 1)
                {
                    rooms_array[x, y] = 1;
                }
                else if (y > 0 && rooms_array[x, y - 1] == 1)
                {
                    rooms_array[x, y] = 1;
                }
                else if (y < 9 && rooms_array[x, y + 1] == 1)
                {
                    rooms_array[x, y] = 1;
                }
                else
                {
                    i--;
                }
            }
            else
            {
                i--;
            }
        }
   }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //set start panel to !activew
        start_panel.SetActive(false);
    }

    public void HidePanel()
    {
        start_panel.SetActive(false);
        Debug.Log("hide panel");
    }

    // Update is called once per frame
    void Update()
    {
        if (is_generating)
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (rooms_array[x, y] == 1)
                    {
                        Instantiate(room, new Vector3(x * 10, 0, y * 10), Quaternion.identity);
                    }
                    else
                    {
                        int random = Random.Range(0, 3);
                        if (random == 0)
                        {
                            Instantiate(wall, new Vector3(x * 10, 0, y * 10), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));
                        }
                        else if (random == 1)
                        {
                            Instantiate(wall2, new Vector3(x * 10, 0, y * 10), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));
                        }
                        else if (random == 2)
                        {
                            Instantiate(wall3, new Vector3(x * 10, 0, y * 10), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));
                        }
                        
                        
                    }
                }
            }
            //surround the map with walls
            for (int x = 0; x < 10; x++)
            {
                int random = Random.Range(0, 3);
                if (random == 0)
                {
                    Instantiate(wall, new Vector3(x * 10, 0, -10), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));
                    Instantiate(wall, new Vector3(x * 10, 0, 100), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));
                }
                else if (random == 1)
                {
                    Instantiate(wall2, new Vector3(x * 10, 0, -10), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));
                    Instantiate(wall2, new Vector3(x * 10, 0, 100), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));
                }
                else if (random == 2)
                {
                    Instantiate(wall3, new Vector3(x * 10, 0, -10), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));
                    Instantiate(wall3, new Vector3(x * 10, 0, 100), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));
                }
                
                
                
            }
            //now the z axis
            for (int y = -1; y < 11; y++)
            {
                int random = Random.Range(0, 3);
                if (random == 0)
                {
                    //random 90 degree rotation
                    Instantiate(wall, new Vector3(-10, 0, y * 10), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));
                    Instantiate(wall, new Vector3(100, 0, y * 10), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));
                }
                else if (random == 1)
                {
                    //random 90 degree rotation
                    Instantiate(wall2, new Vector3(-10, 0, y * 10), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));
                    Instantiate(wall2, new Vector3(100, 0, y * 10), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));
                }
                else if (random == 2)
                {
                    //random 90 degree rotation
                    Instantiate(wall3, new Vector3(-10, 0, y * 10), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));
                    Instantiate(wall3, new Vector3(100, 0, y * 10), Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));
                }
                
            }
            
            tree_spawn_points = GameObject.FindGameObjectsWithTag("spawnpoint_tree");
            //inflate spawns_used array
            spawns_used = new int[tree_spawn_points.Length];
            for (int i = 0; i < tree_spawn_number; i++)
            {
                int spawn = Random.Range(0, tree_spawn_points.Length);
                //if spawns_used does not contain the spawn number, spawn a tree
                if (!spawns_used.Contains(spawn))
                {
                    //at a random rotation
                    Instantiate(tree, tree_spawn_points[spawn].transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0));
                    //random scale between .7 and 1.3
                    tree.transform.localScale = new Vector3(Random.Range(.3f, .7f), Random.Range(.3f, .7f), Random.Range(.3f, .7f));
                    spawns_used[i] = spawn;
                }
                else
                {
                    i--;
                }
                
            }
            //spawn rocks in spawnpoints that haven't been used
            for (int i = 0; i < tree_spawn_number/20; i++)
            {
                int spawn = Random.Range(0, tree_spawn_points.Length);
                //if spawn is not in spawns_used
                if (!spawns_used.Contains(spawn))
                {
                    Instantiate(rock, tree_spawn_points[spawn].transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0));
                    //random scale between .7 and 1.3
                    rock.transform.localScale = new Vector3(Random.Range(.3f, .7f), Random.Range(.3f, .7f), Random.Range(.3f, .7f));
                    spawns_used[i+tree_spawn_number] = spawn;
                }
                else
                {
                    i--;
                }
                
            }

            //spawn player
            Instantiate(player, new Vector3(50, 0f, 50), Quaternion.identity);
            is_generating = false;
        }
        
        
    }
}
