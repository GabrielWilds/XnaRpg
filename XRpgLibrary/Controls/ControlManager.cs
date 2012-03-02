using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XRpgLibrary.Controls
{
    /// <summary>
    /// ControlManager inherits from List, which its main function- holding a bunch of UI controls. It also controls browsing through those
    /// controls in a menu, relying on those control's "TabStop" property. SpriteFont's a font var. It's here for font consistency. Controls
    /// inherit that font for consistency in their constructor but it can be overridden.
    /// </summary>
    public class ControlManager : List<Control>
    {
        #region Fields and Properties

        int _selectedControl = 0;

        static SpriteFont _spriteFont;

        public static SpriteFont SpriteFont
        {
            get { return _spriteFont; }
        }

        #endregion



        #region Constructors

        public ControlManager(SpriteFont spriteFont)
            : base()
        {
            _spriteFont = spriteFont;
        }

        public ControlManager(SpriteFont spriteFont, int capacity)
            : base(capacity)
        {
            _spriteFont = spriteFont;
        }

        public ControlManager(SpriteFont spriteFont, IEnumerable<Control> collection)
            : base(collection)
        {
            _spriteFont = spriteFont;
        }

        #endregion


        #region Methods

        public void Update(GameTime gameTime, PlayerIndex playerIndex)
        {
            if (Count == 0)
                return;

            foreach (Control c in this)
            {
                if (c.Enabled)
                    c.Update(gameTime);
                if (c.HasFocus)
                    c.HandleInput(playerIndex);
            }
            
            if (InputHandler.ButtonPressed(Buttons.LeftThumbstickUp, playerIndex) || InputHandler.ButtonPressed(Buttons.DPadUp, playerIndex) || InputHandler.KeyPressed(Keys.Up))
                PreviousControl();

            if (InputHandler.ButtonPressed(Buttons.LeftThumbstickDown, playerIndex) || InputHandler.ButtonPressed(Buttons.DPadDown, playerIndex) || InputHandler.KeyPressed(Keys.Down))
                NextControl();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Control c in this)
            {
                if (c.Visible)
                    c.Draw(spriteBatch);
            }
        }

        public void NextControl()
        {
            if (Count == 0)
                return;

            int currentControl = _selectedControl;

            this[_selectedControl].HasFocus = false;

            do
            {
                _selectedControl++;

                if (_selectedControl == Count)
                    _selectedControl = 0;

                if (this[_selectedControl].TabStop && this[_selectedControl].Enabled)
                    break;
            } while (currentControl != _selectedControl);

            this[_selectedControl].HasFocus = true;
        }

        public void PreviousControl()
        {
            if (Count == 0)
                return;

            int currentControl = _selectedControl;

            this[_selectedControl].HasFocus = false;

            do
            {
                _selectedControl--;

                if (_selectedControl < 0)
                    _selectedControl = Count - 1;

                if (this[_selectedControl].TabStop && this[_selectedControl].Enabled)
                    break;

            } while (currentControl != _selectedControl);

            this[_selectedControl].HasFocus = true;
        }

        #endregion

    }
}
