using Mapi.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Mapi;

public class WalkGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private RenderTarget2D _renderTarget;
    private float _scale = 1f;

    // Game Time
    private GameTime _updateGameTime;
    private GameTime _drawGameTime;

    // Game Objects    
    private CameraPoint _cameraPoint = new();
    private Mech _mech = new();
    private ViewPort _viewport;

    // Debugging    
    private bool _break = false;
    private SpriteFont _debugText;


    public WalkGame()
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
        _mech.LoadContent(Content);

        // Initialize
        _cameraPoint.MapPosition = _mech.MapSprite.Size / 2;
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


        base.Update(gameTime);
    }

    private void UpdateCamera()
    {
        _cameraPoint.Update(_updateGameTime);
        _viewport.OnUpdateCamera();
    }

    #endregion


    #region Draw

    //
    // Draw
    //
    protected override void Draw(GameTime gameTime)
    {
        _scale = 1f / (((float)Global.RenderHeight) / GraphicsDevice.Viewport.Height);
        GraphicsDevice.SetRenderTarget(_renderTarget);
        GraphicsDevice.Clear(Color.Black);
        _drawGameTime = gameTime;

        // Draw Game Objects to back-buffer
        _spriteBatch.Begin();
        DrawMap();
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
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_renderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
        _spriteBatch.End();
    }

    private void DrawMap() => _mech.Draw(_spriteBatch, _drawGameTime, _viewport.Bounds);
    private void DrawCamera() => _cameraPoint.Draw(_spriteBatch, _drawGameTime, _viewport.Bounds);

    private void DrawDebugText() => _spriteBatch.DrawString(_debugText, _cameraPoint.MapPosition.ToString(), new Vector2(0, 0), Color.White);

    #endregion
}