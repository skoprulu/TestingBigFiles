using UnityEngine;

public class UIScript : MonoBehaviour
{
    public GameObject keysPanel;


    // Start is called before the first frame update
    void Start()
    {
        keysPanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        // Control Keys Will Show Up With Tab Key
        if (Input.GetKey(KeyCode.Tab))
        {
            keysPanel.SetActive(true);
        }
        else
        {
            keysPanel.SetActive(false);
        }
        
    }
}
