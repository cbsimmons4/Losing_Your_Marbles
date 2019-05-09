using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBoxController : MonoBehaviour
{
    private List<KeyValuePair<int, int>> used_spaces;
    MapGenerator mg;
    private List<KeyValuePair<int, int>> open_spaces;
    private GameObject player;
    public GameObject MysteryBox;
    private int cap;
    private int cur_spawned;

    private float spawnrate ;

    private float nextspawn;

    // Start is called before the first frame update
    void Start()
    {
        this.spawnrate = 45f;
        this.nextspawn = (int)(Time.time + spawnrate);
       mg = GameObject.Find("Map Generator").GetComponent<MapGenerator>();
       open_spaces = mg.Get_OS();
       player = GameObject.Find("Player");
       used_spaces = new List<KeyValuePair<int, int>>();
        cap = mg.GetWidth()/4;
        this.cur_spawned = 0;

        for ( int j = 0; j < 3; j++)
        {
            this.nextspawn = Time.time + this.spawnrate;
            int i = Random.Range(0, open_spaces.Count);
            KeyValuePair<int, int> cur = open_spaces[i];
            open_spaces.RemoveAt(i);
            used_spaces.Add(cur);
            Instantiate(MysteryBox, new Vector3((cur.Key - mg.GetCenterX()) * 2, 2, (cur.Value - mg.GetCenterZ()) * 2), MysteryBox.transform.rotation).transform.parent = gameObject.transform;
            this.cur_spawned++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (open_spaces.Count == 0)
        {
            open_spaces = new List<KeyValuePair<int, int>>(used_spaces);
            used_spaces.Clear();
        }

        if (nextspawn <= Time.time && this.cur_spawned <= cap)
        {
            this.nextspawn = Time.time + this.spawnrate;
            int i = Random.Range(0, open_spaces.Count);
            KeyValuePair<int, int> cur = open_spaces[i];
            open_spaces.RemoveAt(i);
            used_spaces.Add(cur);
            Instantiate(MysteryBox, new Vector3( (cur.Key-mg.GetCenterX() )* 2, 2, (cur.Value - mg.GetCenterZ()) * 2), MysteryBox.transform.rotation).transform.parent = gameObject.transform;
            this.cur_spawned++;
         }
    }
}
