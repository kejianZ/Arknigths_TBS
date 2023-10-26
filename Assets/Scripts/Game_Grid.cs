using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Game_Grid : MonoBehaviour
{
    public GameObject cell_prefab;

    private int                 grid_size;
    private Cell[,]             cells;
    private int                 max_distance;
    private Queue<(int, int)>   reachable_selected_buffer;

    void Start()
    {
        grid_size = 6;
        max_distance = 3;
        reachable_selected_buffer = new();
        Initialize_Grid();
    }

    void Initialize_Grid()
    {
        cells = new Cell[grid_size, grid_size];

        for(int i = 0; i < grid_size; i++)
        {
            for(int j = 0; j < grid_size; j++)
            {
                cells[i, j] = new Cell(this, cell_prefab, i, j);
            }
        }
    }

    public void Mouse_Over_Cell(int x, int y)
    {
        Show_Reachable_Cell(x, y);
    }

    public void Mouse_Exit_Cell(int x, int y)
    {
        cells[x, y].BFS_Processed = false;
        foreach((int i, int j) in reachable_selected_buffer)
        {
            cells[i, j].Original_Color();
            cells[i, j].BFS_Selected = false;
        }
    }

    void Show_Reachable_Cell(int x, int y)
    {
        if(cells[x, y].Obstacle == false && cells[x, y].BFS_Processed == false)
        {
            BFS(x, y);
            cells[x, y].BFS_Processed = true;
        }
    }

    private void BFS(int x, int y)
    {
        Queue<(int, int, int)> bfs_queue = new();
        bfs_queue.Enqueue((x, y, max_distance));
        while(bfs_queue.Count != 0)
        {
            (int i, int j, int d) = bfs_queue.Dequeue();
            if(i < 0 || i == grid_size || j < 0 || j == grid_size || cells[i, j].BFS_Selected != false || cells[i, j].Obstacle != false)
            {
                continue;
            }
            else
            {
                cells[i,j].Reachable_Color();
                cells[i,j].BFS_Selected = true;
                reachable_selected_buffer.Enqueue((i, j));
                if(d != 0)
                {
                    bfs_queue.Enqueue((i - 1, j,     d - 1));
                    bfs_queue.Enqueue((i + 1, j    , d - 1));
                    bfs_queue.Enqueue((i    , j - 1, d - 1));
                    bfs_queue.Enqueue((i    , j + 1, d - 1));
                }
            }
        }
    }

}
