using UnityEngine;

public class Cell
{
    class Cell_Script: MonoBehaviour
    {
        Game_Grid parent;
        int x;
        int y;

        public void Initialize_Cell_Info(Game_Grid parent, int x, int y)
        {
            this.parent = parent;
            this.x      = x;
            this.y      = y;
        }

        void Start()
        {

        }

        void OnMouseOver()
        {
            parent.Mouse_Over_Cell(x, y);
        }

        void OnMouseExit()
        {
            parent.Mouse_Exit_Cell(x, y);
        } 
    }

    GameObject          primitive;
    public bool         BFS_Selected;
    public bool         BFS_Processed;
    private Cell_Script script;
    SpriteRenderer      renderer;
    Color               original_color;
    public bool         Obstacle;

    public Cell(Game_Grid parent, GameObject prefab, int x, int y)
    {
        primitive = Object.Instantiate(prefab, new Vector2(x, y), Quaternion.identity);
        primitive.transform.parent = parent.transform;
        primitive.AddComponent<Cell_Script>();
        script = primitive.GetComponent<Cell_Script>();
        script.Initialize_Cell_Info(parent, x, y);

        BFS_Selected = false;
        BFS_Processed = false;
        Obstacle = false;
        if((x == 2 && y == 3) || (x == 2 && y == 4) || (x == 1 && y == 3) || (x == 2 && y == 2)) Obstacle = true;
        renderer = primitive.GetComponent<SpriteRenderer>();
        original_color = Obstacle? Color.grey : Color.white;
        renderer.material.color = original_color;
    }

    public void Reachable_Color()
    {
        renderer.material.color = Color.blue;
    }

    public void Original_Color()
    {
        renderer.material.color = original_color;
    }
}