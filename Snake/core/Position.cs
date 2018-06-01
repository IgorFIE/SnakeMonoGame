using Microsoft.Xna.Framework;

namespace Snake.core {
    class Position {

        public int x { get; private set; }
        public int y { get; private set; }
        public Rectangle positionRectangle { get; private set; }

        public Position(int x, int y) {
            this.x = x;
            this.y = y;
            initPositionRectangle();
        }

        private void initPositionRectangle() {
            positionRectangle = new Rectangle(x * GameProperties.GAME_OBJS_SIZE, y * GameProperties.GAME_OBJS_SIZE,
                                GameProperties.GAME_OBJS_SIZE, GameProperties.GAME_OBJS_SIZE);
        }
    }
}
