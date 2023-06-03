using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingWall : MonoBehaviour
{
    public bool breakWall;
    [SerializeField] private GameObject visualGameObject;

    private void Awake()
    {
        GetComponent<Collider2D>();
        GetComponent<CircleCollider2D>();
    }
    void Update()
    {
        if (breakWall)
        {
            Hide();
            GetComponent<Collider2D>().enabled = false;
        }
        else if (!breakWall)
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
    public void BreakWall()
    {
        breakWall = !breakWall;
    }
}
