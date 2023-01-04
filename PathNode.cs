using System;
using System.IO;
using System.Collections.Generic;

namespace testCons
{
    public class PathNode
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsFile { get; set; }
        public PathNode[] Children { get; set; }

        /// <remarks>for JOSN serializer.</remarks>
        public PathNode() {}
        
        private PathNode(string fullpath)
        {
            if (File.Exists(fullpath))
                IsFile = true;
            else if (Directory.Exists(fullpath))
                IsFile = false;
            else
                throw new Exception("Path does not exist.");

            fullpath = fullpath.TrimEnd('\\');
            int idx = fullpath.LastIndexOf('\\');
            if (idx == -1)
            {
                Name = fullpath;
                Path = string.Empty;
            }
            else
            {
                Name = fullpath.Substring(idx + 1);
                Path = fullpath.Substring(0, idx);
            }
        }

        public static PathNode GetNodeInfo(string fullpath, bool dirOnly)
        {
            PathNode node = null;

            if (File.Exists(fullpath) || Directory.Exists(fullpath))
            {
                node = new PathNode(fullpath);
                if (!node.IsFile)
                {
                    List<PathNode> children = new List<PathNode>();
                    string[] dirs = Directory.GetDirectories(fullpath);
                    foreach (string dir in dirs)
                        children.Add(new PathNode(dir));
                    if (!dirOnly)
                    {
                        string[] files = Directory.GetFiles(fullpath);
                        foreach (string file in files)
                            children.Add(new PathNode(file));
                    }
                    node.Children = children.ToArray();
                }
            }

            return node;
        }

        public void GetChildrenInfo(bool dirOnly)
        {
            if (IsFile)
                return;
            
            foreach (PathNode node in Children)
            {
                if (!node.IsFile)
                {
                    List<PathNode> children = new List<PathNode>();
                    string[] dirs = Directory.GetDirectories($"{node.Path}\\{node.Name}");
                    foreach (string dir in dirs)
                        children.Add(new PathNode(dir));
                    if (!dirOnly)
                    {
                        string[] files = Directory.GetFiles($"{node.Path}\\{node.Name}");
                        foreach (string file in files)
                            children.Add(new PathNode(file));
                    }
                    node.Children = children.ToArray();
                }
            }
        }
    }
}