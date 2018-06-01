using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Snake.core {
    class SnakeMovement {

        public bool snakeCollision { get; private set; }
        protected Position currentPosition;

        private Keys lastKeyMovement;
        private Keys nextKeyMovement;

        public SnakeMovement(Position position) {
            currentPosition = position;
            nextKeyMovement = Keys.Right;
        }

        public void retrieveKeyPressed() {
            handleKeyPress(Keys.Down, Keys.Up);
            handleKeyPress(Keys.Up, Keys.Down);
            handleKeyPress(Keys.Right, Keys.Left);
            handleKeyPress(Keys.Left, Keys.Right);
        }

        private void handleKeyPress(Keys keyToCheck, Keys opositeKey) {
            if (Keyboard.GetState().IsKeyDown(keyToCheck)) {
                if (lastKeyMovement != opositeKey) {
                    nextKeyMovement = keyToCheck;
                }
            }
        }

        public void handleMovement(Position[,] gameBoardPositions, List<Position> snakeBody) {
            handleMovement(gameBoardPositions, snakeBody, Keys.Up, 0, -1);
            handleMovement(gameBoardPositions, snakeBody, Keys.Down, 0, 1);
            handleMovement(gameBoardPositions, snakeBody, Keys.Left, -1, 0);
            handleMovement(gameBoardPositions, snakeBody, Keys.Right, 1, 0);
        }

        private void handleMovement(Position[,] gameBoardPositions, List<Position> snakeBody, Keys keyToMove, int x, int y) {
            if (nextKeyMovement == keyToMove) {
                if (canMoveToPosition(keyToMove, gameBoardPositions.GetLength(0), snakeBody)) {
                    performMovement(gameBoardPositions, snakeBody, x, y);
                } else {
                    snakeCollision = true;
                }
            }
        }

        private void performMovement(Position[,] gameBoardPositions, List<Position> snakeBody, int x, int y) {
            currentPosition = gameBoardPositions[currentPosition.x + (x), currentPosition.y + (y)];
            lastKeyMovement = nextKeyMovement;
            snakeBody.Add(currentPosition);
        }

        private bool canMoveToPosition(Keys key, int size, List<Position> snakeBody) {
            return canMoveUp(key, snakeBody) || canMoveDown(key, size, snakeBody) || canMoveLeft(key, snakeBody) || canMoveRight(key, size, snakeBody);
        }

        private bool canMoveUp(Keys key, List<Position> snakeBody) {
            if (key == Keys.Up) {
                return currentPosition.y > 0 && !checkSnakePositionsForNewPosition(currentPosition.x, currentPosition.y - 1, snakeBody);
            }
            return false;
        }

        private bool canMoveDown(Keys key, int size, List<Position> snakeBody) {
            if (key == Keys.Down) {
                return currentPosition.y < size - 1 && !checkSnakePositionsForNewPosition(currentPosition.x, currentPosition.y + 1, snakeBody);
            }
            return false;
        }

        private bool canMoveLeft(Keys key, List<Position> snakeBody) {
            if (key == Keys.Left) {
                return currentPosition.x > 0 && !checkSnakePositionsForNewPosition(currentPosition.x - 1, currentPosition.y, snakeBody);
            }
            return false;
        }

        private bool canMoveRight(Keys key, int size, List<Position> snakeBody) {
            if (key == Keys.Right) {
                return currentPosition.x < size - 1 && !checkSnakePositionsForNewPosition(currentPosition.x + 1, currentPosition.y, snakeBody);
            }
            return false;
        }

        private bool checkSnakePositionsForNewPosition(int x, int y, List<Position> snakeBody) {
            foreach (Position pos in snakeBody) {
                if (pos.x == x && pos.y == y) {
                    return true;
                }
            }
            return false;
        }
    }
}
