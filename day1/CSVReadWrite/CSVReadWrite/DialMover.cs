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
            for (int i = 0; i < amount; i++)
            {
                DialPos--;
                if (DialPos == 0)
                {
                    HitZeroCount++;
                }
                if (DialPos < 0)
                {
                    DialPos = 99;
                }
            }
        }
        else
        {
            for (int i = 0; i < amount; i++)
            {
                DialPos++;
                if (DialPos == 100)
                {
                    DialPos = 0;
                    HitZeroCount++;
                }
            }
        }
    }
}
