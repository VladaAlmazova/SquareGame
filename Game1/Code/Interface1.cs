using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Game1.Code
{
    public interface IGameplayModel
    {
        event EventHandler<GameplayEventArgs> Updated;

        void Update();
        void MovePlayer(Direction dir);

        public enum Direction : byte
        {
            forward,
            backward,
            right,
            left
        }
    }

    public class GameplayEventArgs : EventArgs
    {
        public Vector2 PlayerPos { get; set; }
    }

    public interface IGameplayView
    {
        //Включается в конце каждого цикла, чтобы обновить модель
        event EventHandler CycleFinished;
        event EventHandler<ControlsEventArgs> PlayerMoved;

        void LoadGameCycleParameters(Vector2 pos);
    }

    public class ControlsEventArgs : EventArgs
    {
        public IGameplayModel.Direction Direction { get; set; }
    }
}
