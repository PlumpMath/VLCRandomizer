using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VLCRandomizer
{
    class VLCRandomizer
    {
        public string vlcDirectory;
        public List<ContentRoot> contents;
        public List<ContentRoot> selectedContents
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
                foreach(ContentRoot c in this.selectedContents)
                    output.AddRange(c.createContentsList());

                return output;
            }
            set;
        }
        public string playlistName;

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
            VLCRandomizer rando = new VLCRandomizer();
            Random.
        }
        
    }
    class ContentRoot
    {
        public string path;
        public string rootName;
        public bool isSelected;
        public List<Content> contents;
        public List<Content> createContentsList()
        {
            

            return contents.Where( s => (s as Content).isSelected == true).ToList() ;
        }
    }
    class Content : ContentRoot
    {
        public List<string> episodes;

    };
    
    
}
