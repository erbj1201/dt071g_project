namespace BattleshipGame
{ // Class ships
    class Ship
    {
        private int size;
        private int hits;
        private bool[,] segments;
        // Constructor to initialize a ship with a specific size
        public Ship(int size)
        {
            this.size = size;
            hits = 0;
            segments = new bool[size, 1];
        }
        // Method to place the ship on the board
        public bool PlaceShip(GamePlan gameplan, int[] start, bool horizontal)
        {
            int row = start[0];
            int column = start[1];
            if (horizontal)
            {
                if (column + size > GamePlan.Size)
                {
                    return false;
                }
                for (int i = 0; i < size; i++)
                {
                    if (gameplan.GetGamePlanValue(row, column + i) != ' ')
                    {
                        return false;
                    }
                }
                for (int i = 0; i < size; i++)
                {
                    gameplan.SetGamePlanValue(row, column + i, 'S');
                    segments[i, 0] = true;
                }
            }
            else
            {
                if (row + size > GamePlan.Size)
                {
                    return false;
                }
                for (int i = 0; i < size; i++)
                {
                    if (gameplan.GetGamePlanValue(row + i, column) != ' ')
                    {
                        return false;
                    }
                }
                for (int i = 0; i < size; i++)
                {
                    gameplan.SetGamePlanValue(row + i, column, 'S');
                    segments[i, 0] = true;
                }
            }

            return true;
        }

        // Method to handle a hit on the ship
public bool Hit(int[] target)
{
    int row = target[0];
    int column = target[1];

    // Check if the target is a valid position on the ship
    if (row < 0 || row >= size || column < 0 || column >= size)
    {
        // Invalid target position
        return false;
    }

    // Check if the segment at the target position has already been hit
    if (segments[row, column])
    {
        // Already hit this segment
        return false;
    }

    // Mark the segment as hit
    segments[row, column] = true;
    hits++;

    // Check if all segments have been hit
    if (hits == size)
    {
        // Ship is destroyed
        return true;
    }

    // Ship is not destroyed yet
    return false;
}

 // Method to check if the ship is destroyed
        public bool IsHit()
{
    foreach (bool segmentHit in segments)
    {
        if (!segmentHit)
        {
            return false; // If any segment is not hit, the ship is not fully hit
        }
    }
    return true; // All segments are hit, the ship is fully hit
}
    }
}