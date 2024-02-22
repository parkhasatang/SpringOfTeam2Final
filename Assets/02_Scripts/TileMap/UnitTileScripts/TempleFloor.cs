using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TempleFloor : MonoBehaviour
{
    [SerializeField] private Tilemap templePyramid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Player"))
            {
                templePyramid.color = new Color(templePyramid.color.r, templePyramid.color.g, templePyramid.color.b, 0f);
            }
        }       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            templePyramid.color = new Color(templePyramid.color.r, templePyramid.color.g, templePyramid.color.b, 255f);
        }
    }


}
