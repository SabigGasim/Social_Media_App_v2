using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeApp.Interfaces;
public interface IFileManager
{
    Task<Result<string>> ReadFile(string path);
    Task<Result> WriteToFile(string path, string text);
}
