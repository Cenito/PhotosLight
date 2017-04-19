using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotosLight.Interfaces
{
    public interface IDialogService
    {
        Task ShowDialogAsync(string message);
    }
}
