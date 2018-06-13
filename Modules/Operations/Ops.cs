using System;

namespace Operations
{
    public class Ops
    {
        public static int Pow(int a, int b)
        {
            int ret = 1;
            for(int i = 0; i < b; i++)
                ret = ret * a;
            return ret;
        }
        public static int Mod (int a,int b)
        {
            return a % b;
        }
    }
}
