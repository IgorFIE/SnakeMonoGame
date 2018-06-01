using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SnakeTest.core;
using System;

namespace SnakeTest {
    public class GameLogic : Game {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Texture2D defaultTexture;
        private Random random;

        private Position food;
        private int newFoodX;
        private int newFoodY;

        private GameBoard gameBoard;
        private Snake snake;

        public GameLogic() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            setGameScreenSize();
            initGameVariables();
            base.Initialize();
        }

        private void initGameVariables() {
            random = new Random();
            gameBoard = new GameBoard(GameProperties.GAME_SCREEN / GameProperties.GAME_OBJS_SIZE);
            snake = new Snake(gameBoard.board[gameBoard.board.GetLength(0) / 2, gameBoard.board.GetLength(0) / 2]);
        }

        private void setGameScreenSize() {
            graphics.PreferredBackBufferWidth = GameProperties.GAME_SCREEN;
            graphics.PreferredBackBufferHeight = GameProperties.GAME_SCREEN;
            graphics.ApplyChanges();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            createDefaultTexture();
        }

        private void createDefaultTexture() {
            defaultTexture = new Texture2D(GraphicsDevice, 1, 1);
            defaultTexture.SetData(new[] { Color.White });
        }

        protected override void Update(GameTime gameTime) {
            handleEscapeExitGame();
            handleSnakeBehavious();
            handleFoodBehavious();
            base.Update(gameTime);
        }

        private void handleFoodBehavious() {
            checkCollisionWithFood();
            createFood();
        }

        private void handleSnakeBehavious() {
            snake.retrieveKeyPressed();
            snake.moveSnake(gameBoard.board);
        }

        private void handleEscapeExitGame() {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || snake.snakeHasCollision()) {
                Exit();
            }
        }

        private void createFood() {
            Console.WriteLine(snake.snakeBody.Count + " || " + (gameBoard.board.GetLength(0) * gameBoard.board.GetLength(1)));
            if (food == null && snake.snakeBody.Count < (gameBoard.board.GetLength(0) * gameBoard.board.GetLength(1))) {
                while (food == null) {
                    if (!checkIfNewFoodIntersectsWithSnakeBody()) {
                        food = gameBoard.board[newFoodX, newFoodY];
                    }
                }
            }
        }

        private bool checkIfNewFoodIntersectsWithSnakeBody() {
            newFoodY = random.Next(0, gameBoard.board.GetLength(0));
            newFoodX = random.Next(0, gameBoard.board.GetLength(0));
            foreach (Position pos in snake.snakeBody) {
                if (pos.x == newFoodX && pos.y == newFoodY) {
                    return true;
                }
            }
            return false;
        }

        private void checkCollisionWithFood() {
            if (food != null) {
                foreach (Position pos in snake.snakeBody) {
                    if (food.positionRectangle.Intersects(pos.positionRectangle)) {
                        snake.incrementSnakeLenght();
                        food = null;
                    }
                }
            }
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            drawBoard();
            drawFood();
            drawSnake();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void drawFood() {
            if (food != null) {
                spriteBatch.Draw(defaultTexture, food.positionRectangle, Color.Yellow);
            }
        }

        private void drawBoard() {
            foreach (Position pos in gameBoard.board) {
                spriteBatch.Draw(defaultTexture, pos.positionRectangle, Color.Black);
            }
        }

        private void drawSnake() {
            foreach (Position pos in snake.snakeBody) {
                spriteBatch.Draw(defaultTexture, pos.positionRectangle, Color.White);
            }
        }
    }
}
