using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool open;
    [SerializeField] private GameObject visualGameObject;
    [SerializeField]
    private void Awake()
    {
        GetComponent<Collider2D>();
        GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            Hide();
            GetComponent<Collider2D>().enabled = false;
        }
        else if(!open)
        {
            GetComponent<Collider2D>().enabled = true;
            Show();
        }
    }

    private void Show()
    {
        visualGameObject.SetActive(true);
    }
    private void Hide()
    {
        visualGameObject.SetActive(false);
    }
    public void ToggleDoor()
    {
        open = !open;
    }
}
