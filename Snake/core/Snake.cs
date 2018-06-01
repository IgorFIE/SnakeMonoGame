using System.Collections.Generic;

namespace Snake.core {
    class Snake {

        public List<Position> snakeBody { get; private set; }
        private SnakeMovement snakeMovement;

        private int snakeLength = 1;
        private int delay;
        private int counterDelay;

        public Snake(Position position) {
            initSnakeProperties(position);
        }

        private void initSnakeProperties(Position position) {
            snakeBody = new List<Position>();
            snakeBody.Add(position);
            snakeMovement = new SnakeMovement(position);
            delay = GameProperties.SNAKE_DELAY;
        }

        public void retrieveKeyPressed() {
            snakeMovement.retrieveKeyPressed();
        }

        public void moveSnake(Position[,] gameBoardPositions) {
            if (counterDelay >= delay) {
                counterDelay = 0;
                snakeMovement.handleMovement(gameBoardPositions,snakeBody);
                resizeSnake();
            } else {
                counterDelay++;
            }
        }

        private void resizeSnake() {
            if (snakeBody.Count > snakeLength) {
                snakeBody.RemoveAt(0);
            }
        }

        public void incrementSnakeLenght() {
            snakeLength++;
            if (snakeLength % 10 == 0) {
                if (delay > 2) {
                    delay -= 2;
                }
            }
        }

        public bool snakeHasCollision() {
            return snakeMovement.snakeCollision;
        }
    }
}
