using QFramework;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PrjectSurvivor
{
    public partial class RepeatTileController : ViewController
    {
        private Tilemap mUp;
        private Tilemap mDown;
        private Tilemap mLeft;
        private Tilemap mRight;
        private Tilemap mUpLeft;
        private Tilemap mUpRight;
        private Tilemap mDownLeft;
        private Tilemap mDownRight;
        private Tilemap mCenter;
        private int AreaX = 0;
        private int AreaY = 0;
        void CreateTileMaps()
        {
            mUp = Tilemap.InstantiateWithParent(transform);
            mDown = Tilemap.InstantiateWithParent(transform);
            mLeft = Tilemap.InstantiateWithParent(transform);
            mRight = Tilemap.InstantiateWithParent(transform);
            mUpLeft = Tilemap.InstantiateWithParent(transform);
            mUpRight = Tilemap.InstantiateWithParent(transform);
            mDownLeft = Tilemap.InstantiateWithParent(transform);
            mDownRight = Tilemap.InstantiateWithParent(transform);
            mCenter = Tilemap;

        }
        void UpDatePositions()
        {
            mUp.Position(new Vector3(x: AreaX * Tilemap.size.x, y: (AreaY + 1) * Tilemap.size.y));
            mDown.Position(new Vector3(x: AreaX * Tilemap.size.x, y: (AreaY - 1) * Tilemap.size.y));
            mLeft.Position(new Vector3(x: (AreaX - 1) * Tilemap.size.x, y: (AreaY + 0) * Tilemap.size.y));
            mRight.Position(new Vector3(x: (AreaX + 1) * Tilemap.size.x, y: (AreaY + 0) * Tilemap.size.y));
            mUpLeft.Position(new Vector3(x: (AreaX - 1) * Tilemap.size.x, y: (AreaY + 1) * Tilemap.size.y));
            mUpRight.Position(new Vector3(x: (AreaX + 1) * Tilemap.size.x, y: (AreaY + 1) * Tilemap.size.y));
            mDownLeft.Position(new Vector3(x: (AreaX - 1) * Tilemap.size.x, y: (AreaY - 1) * Tilemap.size.y));
            mDownRight.Position(new Vector3(x: (AreaX + 1) * Tilemap.size.x, y: (AreaY - 1) * Tilemap.size.y));
            mCenter.Position(new Vector3(x: (AreaX + 0) * Tilemap.size.x, y: (AreaY + 0) * Tilemap.size.y));

        }
        void Start()
        {

            Tilemap.CompressBounds();
            CreateTileMaps();
            UpDatePositions();
        }
        private void Update()
        {
            if (Player.Default && Time.frameCount % 60 == 0)
            {
                var cellPos = Tilemap.layoutGrid.WorldToCell(Player.Default.transform.Position());
                AreaX = cellPos.x / Tilemap.size.x;
                AreaY = cellPos.y / Tilemap.size.y;
                UpDatePositions();

            }
        }
    }
}
