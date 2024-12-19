using System.Collections.Generic;
using UnityEngine;

public class ActiveEnergi : MonoBehaviour
{
    [SerializeField] List<PickEnergi> energiList = new List<PickEnergi>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach(PickEnergi energi in energiList)
            {
                energi.EnableGameObject();
            }
        }
    }
}
