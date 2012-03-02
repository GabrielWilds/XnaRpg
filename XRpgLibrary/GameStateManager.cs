using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace XRpgLibrary
{
    /// <summary>
    /// This is a game component that implements IUpdateable. A game state is a screen of UI/appearance elements. These can stack at the same time
    /// So older screen state can be saved and returned to, for example when opening and closing a menu while playing. GameStateManager keeps them
    /// all organized and tidy, and controls what the current top screen is.
    /// </summary>
    public class GameStateManager : GameComponent
    {
        #region Event region
        public event EventHandler OnStateChange;
        #endregion


        #region Fields and Properties

        Stack<GameState> _gameStates = new Stack<GameState>();

        /// <summary>
        /// XNA's Draw Order starts at the 0 and works its way upwards as it is told to draw elements, rendering them from lowest up.
        /// </summary>
        const int _startDrawOrder = 5000;
        const int _drawOrderInc = 100;
        int _drawOrder;

        /// <summary>
        /// I can't quite recall what GameState is for. It's a low level esoteric class that other shit's based on, I think. :V
        /// </summary>
        public GameState CurrentState
        {
            get { return _gameStates.Peek(); }
        }

        #endregion


        #region Constructor

        public GameStateManager(Game game)
            : base(game)
        {
            _drawOrder = _startDrawOrder;
        }

        #endregion


        #region XNA Methods

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        #endregion


        #region Methods Region

        public void PopState()
        {
            if (_gameStates.Count > 0)
            {
                RemoveState();
                _drawOrder -= _drawOrderInc;

                if (OnStateChange != null)
                    OnStateChange(this, null);
            }
        }

        private void RemoveState()
        {
            GameState state = _gameStates.Peek();
            OnStateChange -= state.StateChange;
            Game.Components.Remove(state);
            _gameStates.Pop();
        }

        public void PushState(GameState newState)
        {
            _drawOrder += _drawOrderInc;
            newState.DrawOrder = _drawOrder;

            AddState(newState);

            if (OnStateChange != null)
                OnStateChange(this, null);
        }

        private void AddState(GameState newState)
        {
            _gameStates.Push(newState);
            Game.Components.Add(newState);
            OnStateChange += newState.StateChange;
        }

        public void ChangeState(GameState newState)
        {
            while (_gameStates.Count > 0)
                RemoveState();

            newState.DrawOrder = _startDrawOrder;
            _drawOrder = _startDrawOrder;

            AddState(newState);

            if (OnStateChange != null)
                OnStateChange(this, null);
        }

        #endregion

    }
}
