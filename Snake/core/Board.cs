
namespace Snake.core {
    class GameBoard {

        public Position[,] board { get; private set; }

        public GameBoard(int size) {
            board = createBoard(size);
        }

        private Position[,] createBoard(int size) {
            Position[,] board = new Position[size, size];
            for (int x = 0; x < size; x++) {
                for (int y = 0; y < size; y++) {
                    board[x, y] = new Position(x, y);
                }
            }
            return board;
        }
    }
}
