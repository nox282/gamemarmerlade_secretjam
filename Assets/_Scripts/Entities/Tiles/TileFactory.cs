using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory : MonoBehaviour {
    public static TileFactory instance = null;

    public List<GameObject> TilePrefabs;
    public List<GameObject> ListOfTiles;

    public int NumberOfTile;
    public float MaxDistance;


    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        FillUpTiles();
    }
	
	void Update () {
        if(ListOfTiles.Count <= NumberOfTile)
            FillUpTiles();
        if(Vector3.Distance(Camera.main.transform.position, ListOfTiles[0].transform.position) > MaxDistance) {
            GameObject tile = ListOfTiles[0];
            ListOfTiles.RemoveAt(0);
            Destroy(tile);
        }
    }

    private void FillUpTiles() {
        while(ListOfTiles.Count <= NumberOfTile) {
            GameObject tile = PickTile();
            SnapTileToList(tile.GetComponent<Tile>());
        }
    }

    private GameObject PickTile() {
        return Instantiate(TilePrefabs[Random.Range(0, TilePrefabs.Count)],
                           transform.position,
                           Quaternion.identity);
    }

    private void SnapTileToList(Tile t) {
        Vector3 tPosition = new Vector3(0.0f, 0.0f, 0.0f);

        if (ListOfTiles.Count > 0) {
            Joint lastTileJoint = ListOfTiles[ListOfTiles.Count - 1].GetComponent<Joint>();
            tPosition = lastTileJoint.End.transform.position - t.GetComponent<Joint>().Start.transform.localPosition;
        }

        t.transform.position = tPosition;
        ListOfTiles.Add(t.gameObject);
    }
}
