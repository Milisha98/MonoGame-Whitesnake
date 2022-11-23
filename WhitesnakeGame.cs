using Whitesnake.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Plane = Whitesnake.GameObjects.Plane;

namespace Whitesnake
{

    public class WhitesnakeGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private RenderTarget2D _renderTarget;
        private float _scale = 1f;

        // Game Time
        private GameTime _updateGameTime;
        private GameTime _drawGameTime;

        // Game Objects    
        private CameraPoint _cameraPoint = new CameraPoint();
        private Plane _plane;
        private ViewPort _viewport;
        private bool _demoMode = true;

        // Debugging    
        private bool _break = false;
        private SpriteFont _debugText;


        public WhitesnakeGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Global.ScreenWidth;
            _graphics.PreferredBackBufferHeight = Global.ScreenHeight;
            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // Ensure that the game speed is fixed at 60FPS
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / Global.FPS);
            IsFixedTimeStep = true;

            _plane = new Plane(_cameraPoint);

            base.Initialize();
        }

        //
        // Load Content
        //
        protected override void LoadContent()
        {
            // Game
            _renderTarget = new RenderTarget2D(GraphicsDevice, Global.ScreenWidth, Global.ScreenHeight);
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Game Objects
            _cameraPoint.LoadContent(Content);
            _plane.LoadContent(Content);

            // Initialize
            _cameraPoint.MapPosition = Vector2.Zero;
            _viewport = new ViewPort(_cameraPoint);

            // Debug
            _debugText = Content.Load<SpriteFont>("Text");
        }

        #region Update

        //
        // Update
        //
        protected override void Update(GameTime gameTime)
        {
            _updateGameTime = gameTime;

            var keyboardState = Keyboard.GetState();
            var controller1 = GamePad.GetState(PlayerIndex.One);

            if (controller1.Buttons.Back == ButtonState.Pressed ||
                keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            // Keyboard
            if (keyboardState.IsKeyDown(Keys.OemTilde)) _break = _break == false;
            if (_break) return;

            // Game Objects
            UpdateCamera();
            UpdatePlane();

            base.Update(gameTime);
        }

        private void UpdateCamera()
        {
            _cameraPoint.Update(_updateGameTime);
            _viewport.OnUpdateCamera();
        }

        private void UpdatePlane() =>
            _plane.Update(_updateGameTime);


        #endregion

        #region Draw

        //
        // Draw
        //
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.PowderBlue);
            _drawGameTime = gameTime;

            // Draw Game Objects to back-buffer
            _spriteBatch.Begin();
            DrawPlane();
            DrawCamera();
            DrawDebugText();
            _spriteBatch.End();

            // Draw from back-buffer
            DrawAndScaleFromBackBuffer();
            base.Draw(gameTime);
        }

        private void DrawAndScaleFromBackBuffer()
        {
            // Display the Back-Buffer to the screen with a scale
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.PowderBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_renderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
            _spriteBatch.End();
        }

        private void DrawPlane() => _plane.Draw(_spriteBatch, _drawGameTime, _viewport.Bounds);
        private void DrawCamera() => _cameraPoint.Draw(_spriteBatch, _drawGameTime, _viewport.Bounds);
        private void DrawDebugText() => _spriteBatch.DrawString(_debugText, _cameraPoint.MapPosition.ToString(), new Vector2(0, 0), Color.White);

        #endregion
    } 
}