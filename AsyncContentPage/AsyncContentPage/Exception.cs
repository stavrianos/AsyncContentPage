using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncContentPage
{
    [Serializable]
    class NavigationPageIsNullException : Exception
    {
        public NavigationPageIsNullException()
        {

        }

        public NavigationPageIsNullException(string message)
            : base(message)
        {

        }
    }
}
