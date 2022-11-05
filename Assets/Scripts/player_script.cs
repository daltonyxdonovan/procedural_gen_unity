using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script : MonoBehaviour
{
    public float speed = 5f;
    public float vertical_input;
    public float horizontal_input;
    public float vertical_mouse_input;
    public float horizontal_mouse_input;
    public float running_speed = 3f;
    public float walking_speed = 1.5f;
    public bool is_running = false;
    public Camera camera;
    public game_manager_script game_manager_script;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        game_manager_script = GameObject.Find("game_manager").GetComponent<game_manager_script>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!game_manager_script.is_paused && !game_manager_script.start_panel.activeSelf)
        {
            //find all trees
            GameObject[] trees = GameObject.FindGameObjectsWithTag("tree");

            //get trees behind camera
            List<GameObject> trees_behind_camera = new List<GameObject>();
            foreach (GameObject tree in trees)
            {
                Vector3 screen_point = camera.WorldToViewportPoint(tree.transform.position);
                if (screen_point.z < 0)
                {
                    trees_behind_camera.Add(tree);
                }
            }

            //set all trees in trees_behind_camera to inactive
            foreach (GameObject tree in trees_behind_camera)
            {
                tree.SetActive(false);
            }

            
            
            //lock cursor
            Cursor.lockState = CursorLockMode.Locked;
            vertical_input = Input.GetAxis("Vertical");
            horizontal_input = Input.GetAxis("Horizontal");
            vertical_mouse_input = Input.GetAxis("Mouse Y");
            horizontal_mouse_input = Input.GetAxis("Mouse X");
            

            //move the player
            transform.Translate(Vector3.back * speed * Time.deltaTime * vertical_input);
            transform.Translate(Vector3.left * speed * Time.deltaTime * horizontal_input);

            //rotate the player's y axis based on the mouse's x axis
            transform.Rotate(Vector3.up * horizontal_mouse_input * 2);
            //rotate the camera's x axis based on the mouse's y axis
            camera.transform.Rotate(Vector3.right * vertical_mouse_input * -2);

            //if the player is holding shift, they are running
            if (Input.GetKey(KeyCode.LeftShift))
            {
                is_running = true;
            }
            else
            {
                is_running = false;
            }

            

            //if the player is moving, set is_running to true
            if (vertical_input != 0 || horizontal_input != 0)
            {
                is_running = true;
            }
            else
            {
                is_running = false;
            }
            if (is_running)
            {
                speed = running_speed;

            }
            else
            {
                speed = walking_speed;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            
            
        }
        //if the player presses escape, pause = !pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            game_manager_script.is_paused = !game_manager_script.is_paused;
        }
    }
}
