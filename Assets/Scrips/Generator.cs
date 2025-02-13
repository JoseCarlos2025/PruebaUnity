using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject piece; // Prefab de la pieza
    public int width; // Ancho del mapa
    public int height; // Alto del mapa
    public int bombsNumber; // Número de bombas

    private GameObject[,] grid; // Matriz de datos para almacenar las piezas

    // Singleton para el acceso desde otras clases
    public static Generator instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        grid = new GameObject[width, height];

        // Generación del mapa
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject obj = Instantiate(piece, new Vector3(i, j, 0), Quaternion.identity);
                obj.GetComponent<Piece>().x = i;
                obj.GetComponent<Piece>().y = j;
                grid[i, j] = obj;
            }
        }

        // Centrar la cámara
        Camera.main.transform.position = new Vector3(width / 2f - 0.5f, height / 2f - 0.5f, -10);

        // Colocación aleatoria de las bombas
        for (int i = 0; i < bombsNumber; i++)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            if (!grid[x, y].GetComponent<Piece>().hasBomb)
            {
                grid[x, y].GetComponent<Piece>().hasBomb = true;
            }
            else
            {
                i--; // Si ya tiene una bomba, repetir la iteración
            }
        }
    }

    // Función para contar las bombas alrededor de una posición
    public int CheckBombsAround(int x, int y)
    {
        int count = 0;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue; // Saltar la casilla actual

                int newX = x + i;
                int newY = y + j;

                if (newX >= 0 && newX < width && newY >= 0 && newY < height)
                {
                    if (grid[newX, newY].GetComponent<Piece>().hasBomb)
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }
}


