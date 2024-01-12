/*Class ship by Erika Vestin HT 2023*/
//Namespae
namespace BattleshipGame
{ // Class ships
    class Ship(int size)
    {
        // Properties & constructor
        private int Size { get; set; } = size; //Ships size
        private int Hits { get; set; } = 0; // hits on ship
        private bool[,] Segments { get; set; } = new bool[size, 1]; //Array for segment of the ship

        // Place the ship on game plan
        public bool PlaceShip(GamePlan gameplan, int[] start, bool horizontal)
        {
            int row = start[0];
            int column = start[1];
            //CHeck if ship fits in gameplan
            if (horizontal)
            {
                if (column + Size > GamePlan.Size)
                {
                    return false;
                } //Check clear path for ship
                for (int i = 0; i < Size; i++)
                {
                    if (gameplan.GetGamePlanValue(row, column + i) != ' ')
                    {
                        return false;
                    }
                } //Place ship
                for (int i = 0; i < Size; i++)
                {
                    gameplan.SetGamePlanValue(row, column + i, 'S');
                    Segments[i, 0] = true;
                }
            } // If not fit, return false
            else
            {
                if (row + Size > GamePlan.Size)
                {
                    return false;
                } //Check clear path for ship
                for (int i = 0; i < Size; i++)
                {
                    if (gameplan.GetGamePlanValue(row + i, column) != ' ')
                    {
                        return false;
                    }
                } //Place ship and if ok, return true
                for (int i = 0; i < Size; i++)
                {
                    gameplan.SetGamePlanValue(row + i, column, 'S');
                    Segments[i, 0] = true;
                }
            }

            return true;
        }

        // Handle hit on the ship
        public bool Hit(int[] target)
        {
            int row = target[0];
            int column = target[1];

            // Check if target is valid position
            if (row < 0 || row >= Size || column < 0 || column >= Size)
            {
                // Invalid target position
                return false;
            }
            // Check if target has already been hit
            if (Segments[row, column])
            {
                // Already hit
                return false;
            }

            // Mark as hit
            Segments[row, column] = true;
            Hits++;

            // Check if hole ship is hit
            if (Hits == Size)
            {
                // Hole ship hit
                return true;
            }

            // Not hit yet
            return false;
        }

        // Check if ship destroyed
        public bool IsHit()
        {
            foreach (bool segmentHit in Segments)
            {
                if (!segmentHit)
                {
                    return false; // If any segment is not hit, hole ship is not hit
                }
            }
            return true; // All segments hit, hole ship hit
        }
    }
}
