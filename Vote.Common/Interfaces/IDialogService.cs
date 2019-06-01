using System;
using System.Collections.Generic;
using System.Text;

namespace Vote.Common.Interfaces
{
    public interface IDialogService
    {
        void Alert(string message, string title, string okbtnText);

        void Alert( string message, string title, string okbtnText, Action confirmed);


    }
}
