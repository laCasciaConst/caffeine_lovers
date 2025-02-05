using System.Numerics;
using Unity.Collections;
using UnityEngine;

// public = 해당 클래스 외부 사용 가능, private = 클래스 외부 접근 금지, 
// internal = 기본값 / 프로젝트에서 public처럼 사용, protected = 상속받은 자식만

namespace caffeine
{


    public class NewMonoBehaviourScript : MonoBehaviour
    {
        // world size 
        private Vector2Int worldSize = new Vector2Int(14, 10);

        // tile size (pixel);
        private Vector2Int tileSize = new Vector2Int(40, 20);

        // (0,0) 
        private Vector2Int origin = new Vector2Int(5, 1);

        private int[,] world;

        // Sprite (tileset)
        public Texture2D isoTileTexture;
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

            world = new int[worldSize.x, worldSize.y];
        }

        // Update is called once per frame
        private void Update()
        {
            UnityEngine.Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2Int cell = new Vector2Int(
                Mathf.FloorToInt(mousePos.x / tileSize.x),
                Mathf.FloorToInt(mousePos.y / tileSize.y)
            );

            if (Input.GetMouseButtonDown(0))
            {
                if (IsInsideWorld(cell.x, cell.y))
                {
                    world[cell.x, cell.y] = (world[cell.x, cell.y] + 1) % 6; //6 type on repeat
                }
            }

            DrawWorld();
        }

        private void DrawWorld()
        {
            for (int y = 0; y < worldSize.y; y++)
            {
                for (int x = 0; x < worldSize.x; x++)
                {
                    UnityEngine.Vector2 worldPos = ToScreen(x, y);
                    DrawTile(world[x, y], worldPos);
                }
            }
        }

        // javascript와 다르게 함수 호출 순서는 상관없음 
        private void DrawTile(int tileType, UnityEngine.Vector2 pos)
        {
            if (tileType == 0) return; //empty tile

            GameObject tile = new GameObject($"Tile_{pos.x}_{pos.y}");
            tile.transform.position = pos;

            SpriteRenderer sr = tile.AddComponent<SpriteRenderer>();
            sr.sprite = Sprite.Create(isoTileTexture,
            new Rect(tileType * tileSize.x, 0, tileSize.x, tileSize.y),
            new UnityEngine.Vector2(0.5f, 0.5f)
            );
        }

        private UnityEngine.Vector2 ToScreen(int x, int y)
        {
            return new UnityEngine.Vector2(
                (origin.x * tileSize.x) + (x - y) * (tileSize.x / 2),
                (origin.y * tileSize.y) + (x + y) * (tileSize.y / 2)
            );
        }

        private bool IsInsideWorld(int x, int y)
        {
            return x >= 0 && x < worldSize.x && y >= 0 && y < worldSize.y;
        }
    }
}