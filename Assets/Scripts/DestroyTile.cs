using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class DestroyTile : MonoBehaviour {
    
    private Tilemap tilemap;

    // Use this for initialization
    void Start(){
        tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update(){

    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Player"){
            Vector3Int pPos = tilemap.WorldToCell(collision.rigidbody.position);
            Destroy(tilemap.GetTile(pPos));
        }
    }

}
