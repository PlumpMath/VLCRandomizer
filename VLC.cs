using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VLCRandomizer
{
    class VLCRandomizer
    {
        public VLCRandomizer(string vlcDirectory, string playlistName)
        {
            this.vlcDirectory = vlcDirectory;
            this.playlistName = playlistName;
        }
        public string vlcDirectory;
        public string playlistName;
        public int numItems;
        public List<ContentRoot> contents;
        public List<ContentRoot> selectedRoot
        {
            get
            {

                return contents.Where(s => (s as ContentRoot).isSelected == true).ToList();
            }
            set;
        }
        public List<Content> selectedContent
        {
            get
            {
                List<Content> output = new List<Content>();
                foreach(ContentRoot c in this.selectedRoot)
                    output.AddRange(c.selectedContentList());

                return output;
            }
            set;
        }
        public void AddRoot(string path)
        {
            ContentRoot newRoot = new ContentRoot(path);

        }
        

        public void generatePlaylist()
        {
            int counter = 0;
            
            using(StreamWriter sw = new StreamWriter(playlistName))
            {
                sw.WriteLine("[playlist]");
                sw.WriteLine("NumberOfEntries=" + selectedContent.Count);
                foreach(Content c in selectedContent)
                {
                    counter++;
                    sw.WriteLine("File" + counter + "=" + c.path);
                }

            }
        }
        public void Main()
        {
            VLCRandomizer rando = new VLCRandomizer(vlcDirectory: "C:\\Program Files (x86)\\VideoLAN\\VLC", playlistName: "test.pls");
            rando.AddRoot("Y:\\Television\\American Dad");
            
        }
        
    }
    class ContentRoot
    {
        public string path;
        public string rootName
        {
            get
            {
                string[] delimiters = { "\\" };
                string[] result = path.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                return result[result.Length - 1];
            }
            set;
        }
        public bool isSelected;
        public List<Content> contents;
        public List<Content> selectedContentList()
        {
            return contents.Where( s => (s as Content).isSelected == true).ToList() ;
        }
        public List<Content> generateContentList()
        {
            string[] fileEntries = Directory.GetFiles(this.path);
            foreach(string s in fileEntries)
            {

            }
        }
        public ContentRoot(string path)
        {
            this.path = path;
            isSelected = false;
        }
        public bool isBottomLevel()
        {
            return false;
        }
    }
    class ContentDivider : ContentRoot
    {

    }
    class Content : ContentRoot
    {
        public string name
        {
            get
            {
                string[] delimiters = { "\\" };
                string[] result = path.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                return result[result.Length - 1];
            }
            set;
        }
        public ContentRoot parent; 
        public Content(string path, ContentRoot parent) : base(path)
        {
            this.parent = parent;
        }


    }
    //struct AllDirectories
    //{
    //    public List<string> DirectoriesWithoutFiles { get; set; }
    //    public List<string> DirectoriesWithFiles { get; set; }
    //}

    //static class FileSystemScanner
    //{
    //    public AllDirectories DivideDirectories(string startingPath)
    //    {
    //        var startingDir = new DirectoryInfo(startingPath);

    //        // allContent IList<FileSystemInfo>
    //        var allContent = GetAllFileSystemObjects(startingDir);
    //        var allFiles = allContent.Where(f => !(f.Attributes & FileAttributes.Directory))
    //                                 .Cast<FileInfo>();
    //        var dirs = allContent.Where(f => (f.Attributes & FileAttributes.Directory))
    //                             .Cast<DirectoryInfo>();
    //        var allDirs = new SortedList<DirectoryInfo>(dirs, new FileSystemInfoComparer());

    //        var res = new AllDirectories
    //        {
    //            DirectoriesWithFiles = new List<string>()
    //        };
    //        foreach (var file in allFiles)
    //        {
    //            var dirName = Path.GetDirectoryName(file.Name);
    //            if (allDirs.Remove(dirName))
    //            {
    //                // Was removed, so first time this dir name seen.
    //                res.DirectoriesWithFiles.Add(dirName);
    //            }
    //        }
    //        // allDirs now just contains directories without files
    //        res.DirectoriesWithoutFiles = new List<String>(addDirs.Select(d => d.Name));
    //    }

    //    class FileSystemInfoComparer : IComparer<FileSystemInfo>
    //    {
    //        public int Compare(FileSystemInfo l, FileSystemInfo r)
    //        {
    //            return String.Compare(l.Name, r.Name, StringComparison.OrdinalIgnoreCase);
    //        }
    //    }
    //    IList<FileSystemInfo> GetAllFileSystemObjects(DirectoryInfo root)
    //    {
    //        return root.GetFileSystemInfos("*.*", SearchOptions.AllDirectories);
    //    }
    //}

    
    
}
