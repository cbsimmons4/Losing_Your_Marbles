using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    private List<KeyValuePair<int, int>> used_spaces;
    MapGenerator mg;
    private List<KeyValuePair<int, int>> open_spaces;
    private GameObject player;
    public GameObject MysteryBox;
    private int cap;
    public int cur_spawned;

    private float spawnrate;

    private float nextspawn;
    // Start is called before the first frame update
    void Start()
    {
        this.spawnrate = 3f;
        this.nextspawn = (int)(Time.time + spawnrate);
        mg = GameObject.Find("Map Generator").GetComponent<MapGenerator>();
        open_spaces = mg.Get_OS();
        player = GameObject.Find("Player");
        used_spaces = new List<KeyValuePair<int, int>>();
        cap = MapGenerator.enemy_cap;
        this.cur_spawned = 0;
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
            Vector3 curpos = new Vector3((cur.Key - mg.GetCenterX()) * 2, 1, (cur.Value - mg.GetCenterZ()) * 2);
            if (Vector3.Distance(player.transform.position,curpos) >= 20) {
                Instantiate(MysteryBox, curpos + Vector3.up, MysteryBox.transform.rotation).transform.parent = gameObject.transform;
                this.cur_spawned++;
            }
        }
    }
}
