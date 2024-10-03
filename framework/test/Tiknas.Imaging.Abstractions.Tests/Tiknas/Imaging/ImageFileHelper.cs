using System;
using System.IO;

namespace Tiknas.Imaging;

public static class ImageFileHelper
{
    public static Stream GetJpgTestFileStream()
    {
        return GetTestFileStream("tiknas.jpg");
    }
    
    public static Stream GetPngTestFileStream()
    {
        return GetTestFileStream("tiknas.png");
    }
    
    public static Stream GetWebpTestFileStream()
    {
        return GetTestFileStream("tiknas.webp");
    }
    
    private static Stream GetTestFileStream(string fileName)
    {
        var assembly = typeof(ImageFileHelper).Assembly;
        var resourceStream = assembly.GetManifestResourceStream("Tiknas.Imaging.Files." + fileName);
        if (resourceStream == null)
        {
            throw new Exception($"File {fileName} does not exists!");
        }
        
        return resourceStream;
    }
}