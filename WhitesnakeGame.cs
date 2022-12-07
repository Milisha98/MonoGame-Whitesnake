using Whitesnake.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Plane = Whitesnake.GameObjects.Plane;
using Whitesnake.Demo;
using Whitesnake.Core;

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
        private GameState _gameState = new GameState();
        private CameraPoint _cameraPoint;
        private Plane _plane;
        private ViewPort _viewport;
        private SmokeEmitter _smokeEmitter;
        private ScoreBoard _scoreBoard;
        private BLExEmitter _bLExEmitter;

        // Demo
        private DemoController _demo = new DemoController();

        // Debugging
        private SpriteFont _spriteFont;
        private string _debugText = "";


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

            _gameState.IsDemoMode = true;
            _gameState.DemoStep = _demo.GetNextStep(0);

            _cameraPoint = new CameraPoint(_gameState);
            _plane = new Plane(_gameState, _cameraPoint);
            _smokeEmitter = new SmokeEmitter(_gameState, _cameraPoint);
            _scoreBoard = new ScoreBoard(_gameState);
            _bLExEmitter = new BLExEmitter(_gameState, _cameraPoint);

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
            _smokeEmitter.LoadContent(Content);
            _scoreBoard.LoadContent(Content);
            _bLExEmitter.LoadContent(Content);

            // Initialize
            _cameraPoint.MapPosition = Vector2.Zero;
            _viewport = new ViewPort(_cameraPoint);

            // Debug
            _spriteFont = Content.Load<SpriteFont>("Text");
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
            if (keyboardState.IsKeyDown(Keys.OemTilde)) _gameState.PauseGame = _gameState.PauseGame == false;
            if (_gameState.PauseGame) return;
            if (keyboardState.IsKeyDown(Keys.Space) && _gameState.IsDemoMode)
            {
                _gameState.IsDemoMode = false;
                _smokeEmitter.EmitSmoke = true;
                _cameraPoint.ResetVelocity();
            }



            // Game Objects
            UpdateCamera();
            UpdateDemo();
            UpdatePlane();
            UpdateSmoke();
            UpdateScoreBoard();
            UpdateBlexEmitter();


            base.Update(gameTime);
        }

        private void UpdateCamera()
        {
            _cameraPoint.Update(_updateGameTime);
            _viewport.OnUpdateCamera();
        }

        private void UpdatePlane() =>
            _plane.Update(_updateGameTime);


        private void UpdateSmoke()
        {
            _smokeEmitter.MapPosition = _plane.SmokePosition;
            _smokeEmitter.Update(_updateGameTime);
        }

        private void UpdateScoreBoard()
        {
            _scoreBoard.Update(_updateGameTime);
            _gameState.Score = _scoreBoard.Score;
        }

        private void UpdateBlexEmitter() =>
            _bLExEmitter.Update(_updateGameTime);

        private void UpdateDemo()
        {
            if (_gameState.IsDemoMode == false) return;
            if (_gameState.PauseGame) return;

            var duration = _gameState.DemoStep.Duration;
            var elapsed = _gameState.DemoStep.Elapsed;

            elapsed += (int)_updateGameTime.ElapsedGameTime.TotalMilliseconds;
            _gameState.DemoStep.Elapsed = elapsed;
            _smokeEmitter.SmokeDuration = _gameState.DemoStep.MaxDecay; ;
            _smokeEmitter.EmitSmoke = _gameState.DemoStep.SmokeOn;

            if (elapsed >= duration)
            {
                _gameState.DemoStep = _demo.GetNextStep(_gameState.DemoStep.Sequence);
                if (_gameState.DemoStep == null) _gameState.IsDemoMode = false;
            }         
        }


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
            DrawCamera();
            DrawSmoke();
            DrawBlex();
            DrawPlane();
            DrawScoreBoard();

            DrawDebug();
            
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
        private void DrawSmoke() => _smokeEmitter.Draw(_spriteBatch, _drawGameTime, _viewport.Bounds);
        private void DrawScoreBoard() => _scoreBoard.Draw(_spriteBatch, _drawGameTime, _viewport.Bounds);
        private void DrawBlex() => _bLExEmitter.Draw(_spriteBatch, _drawGameTime, _viewport.Bounds);

        private void DrawDebug()
        {
            if (_gameState.ShowDebugging == false) return;
            DrawDebugText();
        }
        private void DrawDebugText() => _spriteBatch.DrawString(_spriteFont, _debugText, new Vector2(0, 0), Color.White);

        #endregion
    } 
}