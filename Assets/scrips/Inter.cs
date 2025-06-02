using UnityEngine;
using UnityEngine.UI;

public class Inter : MonoBehaviour
{
    public Button butt;
    void Start()
    {
        butt = GetComponent<Button>();
        butt.onClick.AddListener(autodestruir);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void autodestruir()
    {
        this.gameObject.SetActive(false);
    }
}
