using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotosLight.Interfaces
{
    public interface IShareService
    {
        void ShareContent(string title, string description, object data);
    }
}
