using System;

namespace _7_Refactoring
{
    internal class Precondition
    {
        internal static void Requires(bool precondition)
        {
            if (!precondition)
                throw new ApplicationException();
        }
    }
}