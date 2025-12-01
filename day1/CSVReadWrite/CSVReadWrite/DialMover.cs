using System;
using System.Collections.Generic;
using System.Text;

namespace CSVReadWrite;
public class DialMover
{
    public int DialPos { get; set; }
    public int HitZeroCount { get; set; }

    public DialMover()
    {
        DialPos = 50;
        HitZeroCount = 0;
    }

    public void MoveDial(char direction, int amount)
    {
        if (direction == 'L')
        {
            if (DialPos == 0)
            {
                if (amount >= 100)
                {
                    HitZeroCount++;
                    MoveDial(direction, amount - 100);
                    return;
                }
                else
                {
                    DialPos = 100 - amount;
                    return;
                }
            }

            if (DialPos - amount > 0)
                DialPos -= amount;
            else if (DialPos - amount == 0)
            {
                DialPos = 0;
                HitZeroCount++;
            }
            else
            {
                var moved = DialPos;
                DialPos = 0;
                HitZeroCount++;
                MoveDial(direction, amount - moved);
            }
        }
        else
        {
            if (DialPos + amount < 100)
            {
                DialPos += amount;
            }
            else
            {
                var moved = 100 - DialPos;
                DialPos = 0;
                HitZeroCount++;
                MoveDial(direction, amount - moved);
            }
        }
    }
}
