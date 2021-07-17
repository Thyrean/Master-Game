using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCamera : MonoBehaviour
{
    public bool charIsClose = false;
    public GameObject ConsoleCam;
    public GameObject HoverUI;
    public InputGroup InputGroup;

    private void Start()
    {
        ConsoleCam.SetActive(false);
        HoverUI.SetActive(false);
        InputGroup.Focused = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        charIsClose = true;
    }

    private void OnTriggerExit(Collider other)
    {
        charIsClose = false;
    }

    private void Update()
    {
        if (charIsClose == true)
        {
            HoverUI.SetActive(true);
        }
        else HoverUI.SetActive(false);

        if(charIsClose == true && Input.GetKeyDown(KeyCode.E))
        {
            InputGroup.Focused = true;
            ConsoleCam.SetActive(true);
            HoverUI.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else if (Input.GetKeyDown(KeyCode.E) || charIsClose == false)
        {
            InputGroup.Focused = false;
            ConsoleCam.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

/*OnTriggerEnter(){
}
public class ColliderCount
{
    private List<Collider> _objectsInCollider = new List<Collider>();
    public int ObjectsInCollider => _objectsInCollider.Count;
    [SerializeField]
    private string[] _tags;

    private void OnTriggerEnter(Collider other)
    {
        if (_tags.Contains(other.tag))
            _objectsInCollider.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        var index = _objectsInCollider.IndexOf(other);
        if (index != -1)
            _objectsInCollider.RemoveAt(index);
    }
*/
