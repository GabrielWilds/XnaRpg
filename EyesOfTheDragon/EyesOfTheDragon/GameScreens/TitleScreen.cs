using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using XRpgLibrary;
using XRpgLibrary.Controls;

namespace EyesOfTheDragon.GameScreens
{
    /// <summary>
    /// The game's title screen. Defines a background, and UI elements. This is loaded in Game1 and controlled by the StateManager. This makes it
    /// the default game state on launch. ControlManager takes its UI elements, and Draw shows its background, and then the UI over it, and
    /// the player can interact with it (by pressing enter). This will make the State Manager load StartMenuScreen.
    /// </summary>
    public class TitleScreen : BaseGameState
    {
        #region Fields

        Texture2D backgroundImage;
        LinkLabel startLabel;

        #endregion


        #region Constructor

        public TitleScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
        }

        #endregion


        #region XNA Methods

        protected override void LoadContent()
        {
            ContentManager Content = GameRef.Content;
            backgroundImage = Content.Load<Texture2D>(@"Backgrounds\titlescreen");

            base.LoadContent();

            startLabel = new LinkLabel();
            startLabel.Position = new Vector2(350, 600);
            startLabel.Text = "Press ENTER to begin";
            startLabel.Color = Color.White;
            startLabel.TabStop = true;
            startLabel.HasFocus = true;
            startLabel.Selected += new EventHandler(startLabel_Selected);
            ControlManager.Add(startLabel);
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, PlayerIndex.One);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();

            base.Draw(gameTime);
            GameRef.SpriteBatch.Draw(backgroundImage, GameRef.ScreenRectangle, Color.White);

            ControlManager.Draw(GameRef.SpriteBatch);

            GameRef.SpriteBatch.End();
        }

        #endregion


        #region Title Screen Methods

        private void startLabel_Selected(object sender, EventArgs e)
        {
            StateManager.PushState(GameRef.StartMenuScreen);
        }

        #endregion

    }
}
