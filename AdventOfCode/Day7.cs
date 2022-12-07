using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day7
    {
        public static void Solution()
        {
            string[] data = InData.ReadTxtFile(7);
            Part1(data);
            Part2(data);
        }

        private static void Part1(string[] data)
        {
            FileSystem fileSystem = CreateFileSystem(data);
            Console.WriteLine("Part1: " + fileSystem.AllDirsOfSizeLessThan(100000)
                                                    .Select(it => fileSystem.Size(it))
                                                    .Sum());
        }

        private static void Part2(string[] data)
        {
            FileSystem fileSystem = CreateFileSystem(data);
            long totalSpaceUsed = fileSystem.Size(0);
            long freeSpace = 70000000 - totalSpaceUsed;
            long difWithNeededSpace = 30000000 - freeSpace;
            Console.WriteLine("Part2: " + fileSystem.Size(fileSystem.FindSmallestDirectoryBiggerThan(difWithNeededSpace)));
        }

        private static FileSystem CreateFileSystem(string[] data)
        {
            FileSystem fileSystem= new FileSystem();
            for (int i = 1; i < data.Length - 1; i++)
            {
                string[] dataSplit = data[i].Split(' ');
                
                if (data[i] == "$ cd /")
                    fileSystem = new FileSystem();
                else if (data[i] == "$ ls")
                {
                    i++;
                    while (data[i] != "" && data[i][0] != '$')
                    {
                        string[] newDataSplit = data[i].Split(' ');
                        if (long.TryParse(newDataSplit[0], out var x))
                        {
                            fileSystem.AddFile(x, newDataSplit[1]);
                        }
                        else
                        {
                            fileSystem.AddDir(newDataSplit[1]);
                        }

                        i++;
                    }
                    i--;
                }
                else if (dataSplit[1] == "cd" && dataSplit[2] == "..")
                {
                    fileSystem.GoToParent();
                }
                else
                {
                    fileSystem.GoToChild(dataSplit[2]);
                }
            }

            return fileSystem;
        }
    }

    public enum DirType{
        Dir,
        File
    }
    
    public class FileSystem
    {
        private static int _pointer;
        private readonly List<int> _parent;
        private readonly List<string> _name;
        private readonly List<List<int>> _children;
        private readonly List<DirType> _dirType;
        private readonly List<long> _size;
        
        public long Size(int i)
        {
            if (_size[i] != -1) return _size[i];
            long total = 0;
            for (int j = 0; j < _children[i].Count; j++)
            {
                total += Size(_children[i][j]);
            }
            _size[i] = total;
            return total;
        }

        public FileSystem()
        {
            _pointer = 0;
            _parent = new List<int> {-1};
            _name = new List<string> {"/"};
            _children = new List<List<int>> {new List<int>()};
            _dirType = new List<DirType> {DirType.Dir};
            _size = new List<long> {-1};
        }

        public void AddFile(long size, string name)
        {
            _children[_pointer].Add(_parent.Count);
            _parent.Add(_pointer);
            _name.Add(name);
            _children.Add(new List<int>());
            _dirType.Add(DirType.File);
            _size.Add(size);
        }

        public void AddDir(string name)
        {
            _children[_pointer].Add(_parent.Count);
            _parent.Add(_pointer);
            _name.Add(name);
            _children.Add(new List<int>());
            _dirType.Add(DirType.Dir);
            _size.Add(-1);
        }

        public void GoToParent()
        {
            if(_pointer != 0)
                _pointer = _parent[_pointer];
        }

        public void GoToChild(string name)
        {
            var listOfChildren = _children[_pointer];
            foreach (var child in listOfChildren)
            {
                if (this._name[child] == name && _dirType[child] == DirType.Dir)
                    _pointer = child;
            }
        }

        public List<int> AllDirsOfSizeLessThan(int n)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < _parent.Count; i++)
            {
                if (_dirType[i] == DirType.Dir && Size(i) <= n)
                    result.Add(i);
            }
            return result;
        }

        public int FindSmallestDirectoryBiggerThan(long n)
        {
            int result = -1;
            long minSize = -1;
            for (int i = 0; i < _parent.Count; i++)
            {
                if (_dirType[i] == DirType.Dir && Size(i) >= n)
                    if (result == -1 || minSize > Size(i))
                    {
                        result = i;
                        minSize = Size(i);
                    }
            }
            return result;
        }
    }
}