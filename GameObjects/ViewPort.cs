using Microsoft.Xna.Framework;

namespace Whitesnake.GameObjects
{

    internal class ViewPort
    {
        private readonly CameraPoint _cameraPoint;

        public ViewPort(CameraPoint cameraPoint)
        {
            _cameraPoint = cameraPoint;
        }


        public Rectangle Bounds { get; set; }
        public Vector2 MapPosition { get; set; }
        public Vector2 ScreenPosition { get; set; }


        public void OnUpdateCamera()
        {
            // Update the ViewPort (Camera)
            MapPosition = _cameraPoint.MapPosition; //- new Vector2(Global.CameraWidth / 2, Global.CameraHeight / 2);
            Bounds = new Rectangle(MapPosition.ToPoint(), new Point(Global.CameraWidth, Global.CameraHeight));
        }

    }
}