using System;
using System.IO;

namespace Poetry.UI.FileSupport
{
    public interface IFileProvider
    {
        Stream OpenFile(string path);
    }
}
