using System.IO;
using SCALE.Constants;
using Environment = System.Environment;

namespace SCALE.GameData;

public static class FileUtilities
{
    public static DirectoryInfo GetUserDataFolder()
    {
        var appName = AppConstants.AppName;
        var savePath = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            appName
        );

        var directoryInfo = Directory.CreateDirectory(savePath);
        return directoryInfo;
    }

    public static string GetSavePath(string fileName)
    {
        var userDataFolder = GetUserDataFolder();
        var path = Path.Combine(userDataFolder.FullName, fileName);
        return path;
    }
}